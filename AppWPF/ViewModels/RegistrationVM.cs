using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.ViewModels
{
	public class RegistrationVM : ViewModelBase
	{
		private RegistrationModel _registrationVM;

		public string Login
		{
			get
			{
				return _registrationVM.Login;
			}
			set
			{
				_registrationVM.Login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		public string Email
		{
			get
			{
				return _registrationVM.Email;
			}
			set
			{
				_registrationVM.Email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public string Password1
		{
			get
			{
				return _registrationVM.Password1;
			}
			set
			{
				_registrationVM.Password1 = value;
				OnPropertyChanged(nameof(Password1));
			}
		}

		public string Password2
		{
			get
			{
				return _registrationVM.Password2;
			}
			set
			{
				_registrationVM.Password2 = value;
				OnPropertyChanged(nameof(Password2));
			}
		}

		public ICommand RegistrationCommand { get; set; }


		public RegistrationVM()
		{
			_registrationVM = new RegistrationModel();
		}

	}
}
