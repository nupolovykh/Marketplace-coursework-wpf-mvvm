using System.Net.Http.Headers;
using System.Windows.Input;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.State.Authenticators;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class ProfileVM : ViewModelBase
	{
		private readonly IAccountStore _store;
		private readonly IAuthenticator _authenticator;

		private ProfileModel _profileModel;

		public EmployeeDto CurrentEmployee
		{
			get => _profileModel.CurrentEmployee!;
			set
			{
				_profileModel.CurrentEmployee = value;
				OnPropertyChanged(nameof(CurrentEmployee));
				OnPropertyChanged(nameof(CanAdjust));
			}
		}
		public string Password1
		{
			get => _profileModel.Password1!;
			set
			{
				_profileModel.Password1 = value;
				OnPropertyChanged(nameof(Password1));
				OnPropertyChanged(nameof(CanAdjust));
			}
		}
		public string Password2
		{
			get => _profileModel.Password2!;
			set
			{
				_profileModel.Password2 = value;
				OnPropertyChanged(nameof(Password2));
				OnPropertyChanged(nameof(CanAdjust));
			}
		}

		public MessageViewModel ErrorMessageViewModel { get; }
		public string ErrorMessage
		{
			set => ErrorMessageViewModel.Message = value;
		}

		public bool CanAdjust => !string.IsNullOrEmpty(Password1) && !string.IsNullOrEmpty(Password2)
			&& Password1 == Password2 && CurrentEmployee is not null;

		public ICommand ApplyChanges { get; set; }
		public ICommand Logout { get; set; }

		public ProfileVM(IAuthenticator authenticator, IAccountStore store)
		{
			_profileModel = new ProfileModel();
			ErrorMessageViewModel = new MessageViewModel();
			_store = store;
			_authenticator = authenticator;

			if (_authenticator.IsLoggedIn)
			{
				CurrentEmployee = _store.CurrentEmployee;
				_store.StateChanged += () => OnPropertyChanged(nameof(CurrentEmployee));
			}

			ApplyChanges = new ProfileCommand(this, authenticator);

			Logout = new DelegateCommand(
				action: (_) =>
				{
					_authenticator.Logout();
					CurrentEmployee = null!;
				},
				condition: (_) => _authenticator.IsLoggedIn,
				vmb: this);
		}

		public override void Dispose()
		{
			ErrorMessageViewModel.Dispose();
			base.Dispose();
		}
	}
}
