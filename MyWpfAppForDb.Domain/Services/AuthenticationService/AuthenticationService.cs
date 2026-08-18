using MyWpfAppForDb.Domain.Extensions;
using MyWpfAppForDb.Domain.Exceptions;
using MyWpfAppForDb.Domain.Services.AccountService;
using MyWpfAppForDb.EntityFramework.Entities;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services.AuthenticationServices
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IAccountService _accountService;

		public AuthenticationService(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<Employee> Login(string loginOrEmail, string password)
		{
			// The login box is labelled "Login / Email", so accept either - it used
			// to match against the employee name only.
			Employee employee = await _accountService.GetByUsername(loginOrEmail)
				?? await _accountService.GetByEmail(loginOrEmail);

			if (employee is null) throw new UserNotFoundException(loginOrEmail);

			if (!password.VerifyHash(employee.Password)) throw new InvalidPasswordException(loginOrEmail);

			return employee;
		}

		public async Task<AccountResult> Register(string email, string username, string password, string confirmPassword)
		{
			if (password != confirmPassword) return AccountResult.PasswordsDoNotMatch;

			AccountResult result = AccountResult.Success;

			if (await _accountService.GetByUsername(username) is not null)
			{
				result = AccountResult.UsernameAlreadyExists;
			}
			else if (await _accountService.GetByEmail(email) is not null)
			{
				result = AccountResult.EmailAlreadyExists;
			}

			if (result == AccountResult.Success)
			{
				string hashedPassword = password.ToHash();

				Employee newEmployee = new Employee()
				{
					RoleId = 2,
					Email = email,
					Name = username,
					Password = hashedPassword
				};

				await _accountService.Create(newEmployee);
			}

			return result;
		}

		public async Task<AccountResult> Adjust(Employee employee, string newPassword)
		{
			AccountResult result = AccountResult.Success;

			Employee check = await _accountService.GetByUsername(employee.Name);

			if (check is not null && check.Id != employee.Id) result = AccountResult.UsernameAlreadyExists;

			check = await _accountService.GetByEmail(employee.Email);

			if (check is not null && check.Id != employee.Id) result = AccountResult.EmailAlreadyExists;

			if (result == AccountResult.Success)
			{
				// An empty password box means "leave my password alone" - the stored
				// hash is read back from the database rather than trusting whatever
				// the view model happened to be carrying.
				employee.Password = string.IsNullOrEmpty(newPassword)
					? (await _accountService.Get(employee.Id)).Password
					: newPassword.ToHash();

				await _accountService.Update(employee.Id, employee);
			}

			return result;
		}
	}
}
