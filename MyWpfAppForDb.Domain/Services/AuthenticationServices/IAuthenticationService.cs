using MyWpfAppForDb.EntityFramework.Entities;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services.AuthenticationServices
{
	public enum RegistrationResult
	{
		Success,
		PasswordsDoNotMatch,
		EmailAlreadyExists,
		UsernameAlreadyExists
	}

	public interface IAuthenticationService
	{
		Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

		Task<Employee> Login(string username, string password);
	}
}
