using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppWPF.Models;

namespace AppWPF.ViewModels
{
	public class AuthorizationVM : ViewModelBase
	{
		private AuthorizationModel _authorizationModel;

		public string Login
		{
			get 
			{ 
				return _authorizationModel.Login; 
			}
			set
			{ 
				_authorizationModel.Login = value;
				OnPropertyChanged(nameof(Login));
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


        public AuthorizationVM() {
			_authorizationModel = new AuthorizationModel();
		}
	}
}
