using System.Windows.Input;
using AppWPF.ViewModels.Additional;
using AppWPF.ViewModels.Models;

namespace AppWPF.ViewModels.Pages
{
    public class ProfileVM : ViewModelBase
	{
		private ProfileModel _profileModel;

		public string Login
		{
			get
			{
				return _profileModel.Login;
			}
			set
			{
				_profileModel.Login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		public string Email
		{
			get
			{
				return _profileModel.Email;
			}
			set
			{
				_profileModel.Email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public string Phone
		{
			get
			{
				return _profileModel.Phone;
			}
			set
			{
				_profileModel.Phone = value;
				OnPropertyChanged(nameof(Phone));
			}
		}

		public string Password1
		{
			get
			{
				return _profileModel.Password1;
			}
			set
			{
				_profileModel.Password1 = value;
				OnPropertyChanged(nameof(Password1));
			}
		}

		public string Password2
		{
			get
			{
				return _profileModel.Password2;
			}
			set
			{
				_profileModel.Password2 = value;
				OnPropertyChanged(nameof(Password2));
			}
		}

		public ICommand ApplyChanges { get; set; }


		public ProfileVM()
		{
			_profileModel = new ProfileModel();
		}
	}
}
