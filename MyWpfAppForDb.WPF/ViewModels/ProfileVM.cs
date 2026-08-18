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

		// Leaving both password boxes empty saves name/email/phone and keeps the
		// current password; filling them in changes it. Previously nothing could be
		// saved at all without retyping a password twice.
		public bool CanAdjust => CurrentEmployee is not null && Password1 == Password2;

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
