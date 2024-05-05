using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Authenticators
{
	public interface IAuthenticator
	{
		bool IsLoggedIn { get; }

		event Action StateChanged;

		Task Login(string username, string password);

		Task<AccountResult> Register(string email, string username, string password, string confirmPassword);

		Task<AccountResult> Adjust(EmployeeDto employee, string password);

		void Logout();
	}
}
