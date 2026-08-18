using Marketplace.EntityFramework.Entities;
using Marketplace.EntityFramework.Services.AuthenticationServices;
using Marketplace.Wpf.Models.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace Marketplace.Wpf.State.Authenticators
{
	public interface IAuthenticator
	{
		bool IsLoggedIn { get; }

		event Action StateChanged;

		Task Login(string loginOrEmail, string password);

		Task<AccountResult> Register(string email, string username, string password, string confirmPassword);

		Task<AccountResult> Adjust(EmployeeDto employee, string newPassword);

		void Logout();
	}
}
