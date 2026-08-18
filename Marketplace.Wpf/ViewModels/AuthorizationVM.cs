using System.Windows.Input;
using Marketplace.Wpf.Models;
using Marketplace.Wpf.Commands;
using Marketplace.Wpf.State.Navigators;
using Marketplace.Wpf.State.Authenticators;

namespace Marketplace.Wpf.ViewModels
{
	public class AuthorizationVM : ViewModelBase
	{
		private AuthorizationModel _authorizationModel;

		public string LoginEmail
		{
			get => _authorizationModel.LoginEmail!;
			set
			{
				_authorizationModel.LoginEmail = value;
				OnPropertyChanged(nameof(LoginEmail));
				OnPropertyChanged(nameof(CanLogin));
			}
		}
		public string Password
		{
			get => _authorizationModel.Password!;
			set
			{
				_authorizationModel.Password = value;
				OnPropertyChanged(nameof(Password));
				OnPropertyChanged(nameof(CanLogin));
			}
		}

		public bool CanLogin => !string.IsNullOrEmpty(LoginEmail) && !string.IsNullOrEmpty(Password);

		public MessageViewModel ErrorMessageViewModel { get;  }
		public string ErrorMessage 
		{   
			set => ErrorMessageViewModel.Message = value;
		}

		public ICommand LoginCommand { get; set; }

		public AuthorizationVM(IAuthenticator authenticator, IRenavigator renavigator)
		{
			_authorizationModel = new AuthorizationModel();
			ErrorMessageViewModel = new MessageViewModel();

			LoginCommand = new LoginCommand(this, authenticator, renavigator);
		}

		public override void Dispose()
		{
			ErrorMessageViewModel.Dispose();
			base.Dispose();
		}

	}
}
