using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
	internal class ProfileCommand : AsyncCommandBase
	{
		private readonly ProfileVM _viewModel;
		private readonly IAuthenticator _authenticator;

		public ProfileCommand(ProfileVM viewModel, IAuthenticator authenticator)
		{
			_viewModel = viewModel;
			_authenticator = authenticator;

			_viewModel.PropertyChanged += ProfileVM_PropertyChanged!;
		}

		public override bool CanExecute(object? parameter) => _viewModel.CanAdjust && base.CanExecute(parameter);

		public override async Task ExecuteAsync(object? parameter)
		{
			_viewModel.ErrorMessage = string.Empty;
			try
			{
				AccountResult adjustingResult = await _authenticator.Adjust(_viewModel.CurrentEmployee, _viewModel.Password2);
				switch (adjustingResult)
				{
					case AccountResult.Success:
						_viewModel.ErrorMessage = "Adjusting passed.";
						break;
					case AccountResult.PasswordsDoNotMatch:
						_viewModel.ErrorMessage = "Password does not match confirm password.";
						break;
					case AccountResult.EmailAlreadyExists:
						_viewModel.ErrorMessage = "This email already exist.";
						break;
					case AccountResult.UsernameAlreadyExists:
						_viewModel.ErrorMessage = "This username already exist.";
						break;
					default:
						_viewModel.ErrorMessage = "Adjusting failed.";
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				_viewModel.ErrorMessage = "Adjusting failed.";
			}
		}

		private void ProfileVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(ProfileVM.CanAdjust))
			{
				OnCanExecuteChanged();
			}
		}
	}
}
