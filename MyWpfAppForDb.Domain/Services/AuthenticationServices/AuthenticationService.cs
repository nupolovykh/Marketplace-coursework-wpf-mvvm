using Microsoft.AspNet.Identity;
using MyWpfAppForDb.Domain.Exceptions;
using MyWpfAppForDb.Domain.Services.AccountService;
using MyWpfAppForDb.EntityFramework.Entities;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services.AuthenticationServices
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IAccountService _accountService;
		private readonly IPasswordHasher _passwordHasher;

		public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher) 
		{ 
			_accountService = accountService;
			_passwordHasher = passwordHasher;
		}

		public async Task<Employee> Login(string username, string password)
		{
			Employee employee = await _accountService.GetByUsername(username);

			if (employee is null) throw new UserNotFoundException(username);

			PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(employee.Password, password);

			if (passwordResult != PasswordVerificationResult.Success) throw new InvalidPasswordException(username, password);

			return employee;
		}

		public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
		{
			RegistrationResult result = RegistrationResult.Success;

			if (password != confirmPassword) result = RegistrationResult.PasswordsDoNotMatch;

			Employee employee = await _accountService.GetByUsername(username);
			
			if(employee != null)
			{
				result = RegistrationResult.UsernameAlreadyExists;
			}

			if(result == RegistrationResult.Success)
			{
				string hashedPassword = _passwordHasher.HashPassword(password);

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
	}
}
