using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Extensions;
using Marketplace.Domain.Services.AccountService;
using Marketplace.Domain.Services.ProductsService;
using Marketplace.EntityFramework;
using Marketplace.EntityFramework.Entities;
using Marketplace.EntityFramework.Services.AuthenticationServices;

int failures = 0;

void Check(bool passed, string description)
{
	Console.WriteLine($"{(passed ? "  ok  " : " FAIL ")} {description}");
	if (!passed) failures++;
}

async Task<bool> Throws<TException>(Func<Task> action) where TException : Exception
{
	try
	{
		await action();
		return false;
	}
	catch (TException)
	{
		return true;
	}
	catch
	{
		return false;
	}
}

string databasePath = Path.Combine(Path.GetTempPath(), $"marketplace-smoke-{Guid.NewGuid():N}.db");
Console.WriteLine($"database: {databasePath}\n");

var factory = new AppDbContextFactory(options => options.UseSqlite($"Data Source={databasePath}"));

try
{
	// 1. Schema and seed data. EnsureCreated is what the app itself runs at
	//    startup, so this covers the whole model-building path including every
	//    HasData block.
	using (AppDbContext context = factory.CreateDbContext())
	{
		Check(await context.Database.EnsureCreatedAsync(), "database is created from the model");
		Check(await context.Employees.CountAsync() == 10, "10 employees are seeded");
		Check(await context.Products.CountAsync() == 20, "20 products are seeded");
		Check(await context.Roles.CountAsync() == 3, "3 roles are seeded");
	}

	Check(File.Exists(databasePath), "the SQLite file exists on disk");

	var accountService = new AccountDataService(factory);
	var authentication = new AuthenticationService(accountService);
	var products = new ProductsService(factory);

	// 2. The seeded password hashes have to verify against the PBKDF2 code that
	//    replaced Microsoft.AspNet.Identity's hasher.
	Employee seeded = await authentication.Login("John Doe", "123");
	Check(seeded is not null && seeded.Id == 1, "a seeded employee can log in by name");

	// 3. The login field accepts an email address, not only the name.
	Employee byEmail = await authentication.Login("john.doe@example.com", "123");
	Check(byEmail is not null && byEmail.Id == 1, "the same employee can log in by email");

	Check(await Throws<InvalidPasswordException>(() => authentication.Login("John Doe", "wrong")),
		"a wrong password is rejected");
	Check(await Throws<UserNotFoundException>(() => authentication.Login("Nobody At All", "123")),
		"an unknown login is rejected");

	// 4. Register -> login round trip: the password hashed on registration must
	//    verify on the way back in.
	string login = $"citest_{DateTime.UtcNow:yyyyMMddHHmmss}";
	string email = $"{login}@example.com";
	const string password = "Correct-Horse-Battery-Staple-1";

	Check(await authentication.Register(email, login, password, password) == AccountResult.Success,
		"a new employee can register");

	Employee registered = await authentication.Login(login, password);
	Check(registered is not null, "the newly registered employee can log in");
	Check(registered is not null && registered.Password != password,
		"the stored password is a hash, not the password itself");

	// 5. Rejected registrations must be rejected with a reason, and must not
	//    create a second row.
	Check(await authentication.Register("other@example.com", login, password, password) == AccountResult.UsernameAlreadyExists,
		"registering a taken name reports UsernameAlreadyExists");
	Check(await authentication.Register(email, $"{login}_other", password, password) == AccountResult.EmailAlreadyExists,
		"registering a taken email reports EmailAlreadyExists");
	Check(await authentication.Register("mismatch@example.com", $"{login}_x", password, "something-else") == AccountResult.PasswordsDoNotMatch,
		"mismatched passwords report PasswordsDoNotMatch");

	using (AppDbContext context = factory.CreateDbContext())
	{
		Check(await context.Employees.CountAsync() == 11, "rejected registrations created no rows");
	}

	// 6. Editing the profile with the password boxes left empty must keep the
	//    current password rather than hashing an empty string.
	registered.Email = $"changed_{email}";
	Check(await authentication.Adjust(registered, string.Empty) == AccountResult.Success,
		"the profile can be saved without touching the password");

	Employee afterAdjust = await authentication.Login(login, password);
	Check(afterAdjust is not null, "the old password still works after that save");
	Check(afterAdjust is not null && afterAdjust.Email == $"changed_{email}", "the email change was persisted");

	// 7. ...and supplying a password must actually change it.
	const string newPassword = "An-Entirely-Different-One-2";
	Check(await authentication.Adjust(afterAdjust, newPassword) == AccountResult.Success, "the password can be changed");
	Check(await Throws<InvalidPasswordException>(() => authentication.Login(login, password)),
		"the old password no longer works");
	Check((await authentication.Login(login, newPassword)) is not null, "the new password works");

	// 8. Catalogue paging and search, including the navigations the product card
	//    binds to.
	List<Product> firstPage = (await products.GetPage(0)).ToList();
	Check(firstPage.Count == 10, "the first catalogue page holds 10 products");
	Check(firstPage.All(p => p.Market is not null && p.ProductInstance?.Category is not null),
		"market, instance and category are loaded with each product");
	Check(await products.GetLastPageNumber() == 1, "20 seeded products make 2 pages");

	string term = firstPage[0].ProductInstance.Name;
	List<Product> found = (await products.GetPageWithSearch(0, term)).ToList();
	Check(found.Count > 0, $"searching for \"{term}\" returns results");

	using (AppDbContext context = factory.CreateDbContext())
	{
		int expected = await context.Products.CountAsync(p => p.ProductInstance.Name.Contains(term)
			|| p.Market.Name.Contains(term)
			|| p.ProductInstance.Category.Name.Contains(term));
		Check(await products.GetLastPageNumberWithSearch(term) == (expected - 1) / 10,
			"the searched page count matches the number of matching rows");
	}

	// 9. Ids are assigned by the application, so the next one has to clear every
	//    id already in the table.
	Check(await products.GetNewId() == 21, "the next product id follows the largest existing one");

	Check("123".VerifyHash(seeded.Password), "the seeded hash verifies");
	Check(!"1234".VerifyHash(seeded.Password), "a near-miss password does not verify");
}
finally
{
	if (File.Exists(databasePath)) File.Delete(databasePath);
}

Console.WriteLine();

if (failures > 0)
{
	Console.WriteLine($"{failures} check(s) failed.");
	return 1;
}

Console.WriteLine("Smoke test passed: seeding, hashing, login, registration, profile edits and catalogue queries all behaved as expected.");
return 0;
