using System.Windows.Input;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class RegistrationVM : ViewModelBase
	{
		private RegistrationModel _registrationVM;

		public string Login
		{
			get => _registrationVM.Login!;
			set
			{
				_registrationVM.Login = value;
				OnPropertyChanged(nameof(Login));
				OnPropertyChanged(nameof(CanRegist));
			}
		}

		public string Email
		{
			get => _registrationVM.Email!;
			set
			{
				_registrationVM.Email = value;
				OnPropertyChanged(nameof(Email));
				OnPropertyChanged(nameof(CanRegist));
			}
		}

		public string Password1
		{
			get => _registrationVM.Password1!;
			set
			{
				_registrationVM.Password1 = value;
				OnPropertyChanged(nameof(Password1));
				OnPropertyChanged(nameof(CanRegist));
			}
		}

		public string Password2
		{
			get => _registrationVM.Password2!;
			set
			{
				_registrationVM.Password2 = value;
				OnPropertyChanged(nameof(Password2));
				OnPropertyChanged(nameof(CanRegist));
			}
		}

		public bool CanRegist => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password1) && !string.IsNullOrEmpty(Password2);

		public MessageViewModel ErrorMessageViewModel { get; }

		public string ErrorMessage
		{
			set => ErrorMessageViewModel.Message = value;
		}

		public ICommand RegistrationCommand { get; set; }


		public RegistrationVM(IAuthenticator authenticator, IRenavigator renavigator)
		{
			_registrationVM = new RegistrationModel();
			ErrorMessageViewModel = new MessageViewModel();

			RegistrationCommand = new RegisterCommand(this, authenticator, renavigator);
		}

		public override void Dispose()
		{
			ErrorMessageViewModel.Dispose();
			base.Dispose();
		}

	}
}
