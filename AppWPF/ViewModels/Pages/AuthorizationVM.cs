using System.Windows.Input;
using AppWPF.ViewModels.Additional;
using AppWPF.ViewModels.Commands;
using AppWPF.ViewModels.Models;

namespace AppWPF.ViewModels.Pages
{
    public class AuthorizationVM : ViewModelBase
	{
		private AuthorizationModel _authorizationModel;

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

		public AuthorizationVM() 
		{
			_authorizationModel = new AuthorizationModel();
		}
	}
}
