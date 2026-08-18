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

		public async Task<Employee> Login(string username, string password)
		{
			Employee employee = await _accountService.GetByUsername(username);

			if (employee is null) throw new UserNotFoundException(username);

			if (!password.VerifyHash(employee.Password)) throw new InvalidPasswordException(username);

			return employee;
		}

		public async Task<AccountResult> Register(string email, string username, string password, string confirmPassword)
		{
			AccountResult result = AccountResult.Success;

			if (password != confirmPassword) result = AccountResult.PasswordsDoNotMatch;

			Employee employee = await _accountService.GetByUsername(username);
			
			if(employee != null)
			{
				result = AccountResult.UsernameAlreadyExists;
			}

			if(result == AccountResult.Success)
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

		public async Task<AccountResult> Adjust(Employee employee)
		{
			AccountResult result = AccountResult.Success;

			Employee check = await _accountService.GetByUsername(employee.Name);

			if (check is not null && check.Id != employee.Id) result = AccountResult.UsernameAlreadyExists;

			check = await _accountService.GetByEmail(employee.Email);

			if (check is not null && check.Id != employee.Id) result = AccountResult.EmailAlreadyExists;

			if (result == AccountResult.Success)
			{
				string hashedPassword = employee.Password.ToHash();

				employee.Password = hashedPassword;

				await _accountService.Update(employee.Id, employee);
			}

			return result;
		}
	}
}
