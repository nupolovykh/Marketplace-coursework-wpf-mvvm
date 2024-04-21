using System.Windows.Input;
using MyWpfAppForDb.WPF.Models;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MyWpfAppForDb.WPF.Commands;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class AuthorizationVM : ViewModelBase
    {
        private AuthorizationModel _authorizationModel;

        public string LoginEmail
        {
            get => _authorizationModel.LoginEmail;
            set
            {
                _authorizationModel.LoginEmail = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _authorizationModel.Password;
            set
            {
                _authorizationModel.Password = value;
                OnPropertyChanged();
            }
        }

        public ICommand AuthorizationCommand { get; set; }

        public AuthorizationVM(IHost host)
        {
            _authorizationModel = new AuthorizationModel();

            var db = host.Services.GetRequiredService<MarketPlaceContext>();

            var user = db.Categories.FirstOrDefault()!;

            LoginEmail = user.Name;
            Password = user.Name;

            AuthorizationCommand = new LoginCommand(db);
        }
    }
}
