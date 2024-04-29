using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Authenticators
{
	public interface IAuthenticator
	{
		EmployeeDto CurrentAccount { get; }

		bool IsLoggedIn { get; }

		event Action StateChanged;

		Task Login(string username, string password);

		Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

		void Logout();
	}
}
