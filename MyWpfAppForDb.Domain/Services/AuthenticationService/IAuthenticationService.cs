using MyWpfAppForDb.EntityFramework.Entities;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services.AuthenticationServices
{
	public enum AccountResult
	{
		Success,
		PasswordsDoNotMatch,
		EmailAlreadyExists,
		UsernameAlreadyExists
	}

	public interface IAuthenticationService
	{
		Task<Employee> Login(string username, string password);

		Task<AccountResult> Register(string email, string username, string password, string confirmPassword);

		Task<AccountResult> Adjust(Employee employee);
	}
}
