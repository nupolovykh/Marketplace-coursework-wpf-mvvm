using MyWpfAppForDb.EntityFramework.Entities;
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

        Task Register(string email, string username, string password, string confirmPassword);

        Task Login(string username, string password);

        void Logout();
    }
}
