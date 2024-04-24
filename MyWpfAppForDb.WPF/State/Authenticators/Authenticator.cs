using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using System;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Authenticators
{
    internal class Authenticator : IAuthenticator
    {
        private readonly IAccountStore _accountStore;

        public Authenticator(IAccountStore accountStore)
        {
            _accountStore = accountStore;
        }

        public EmployeeDto CurrentAccount
        {
            get => _accountStore.CurrentEmployee;
            private set
            {
                _accountStore.CurrentEmployee = value;
                StateChanged.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action StateChanged;

        public async Task Login(string username, string password)
        {
            
        }

        public async Task Register(string email, string username, string password, string confirmPassword)
        {
            
        }

        public void Logout()
        {
            CurrentAccount = null!;
        }
    }
}
