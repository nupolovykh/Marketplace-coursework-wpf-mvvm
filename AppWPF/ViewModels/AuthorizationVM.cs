using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using AppWPF.Models;

namespace AppWPF.ViewModels
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
