using System.Windows.Input;
using AppWPF.Models.PlainModels;
using AppWPF.Commands;
using MyWpfAppForDb.ViewModels;

namespace AppWPF.ViewModels
{
    public class AuthorizationVM : ViewModelBase
    {
        private AuthorizationModel _authorizationModel;
        private ViewModelStore _viewModelStore;

        public string LoginEmail
        {
            get
            {
                return _authorizationModel.LoginEmail;
            }
            set
            {
                _authorizationModel.LoginEmail = value;
                OnPropertyChanged(nameof(LoginEmail));
            }
        }

        public string Password
        {
            get
            {
                return _authorizationModel.Password;
            }
            set
            {
                _authorizationModel.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand AuthorizationCommand { get; set; }

        public AuthorizationVM(ViewModelStore viewModelStore)
        {
            _viewModelStore = viewModelStore;
            _authorizationModel = new AuthorizationModel();
            //AuthorizationCommand = new LoginCommand(viewModelStore, employeeStore, this);
        }
    }
}
