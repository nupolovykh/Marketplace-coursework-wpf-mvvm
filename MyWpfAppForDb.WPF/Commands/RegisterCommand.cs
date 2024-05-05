using MyWpfAppForDb.Domain.Exceptions;
using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
	public class RegisterCommand : AsyncCommandBase
	{
		private readonly RegistrationVM _viewModel;
		private readonly IAuthenticator _authenticator;
		private readonly IRenavigator _renavigator;

		public RegisterCommand(RegistrationVM viewModel, IAuthenticator authenticator, IRenavigator renavigator)
		{
			_viewModel = viewModel;
			_authenticator = authenticator;
			_renavigator = renavigator;

			_viewModel.PropertyChanged += RegistrationVM_PropertyChanged!;
		}

		public override bool CanExecute(object? parameter) => _viewModel.CanRegist && base.CanExecute(parameter);

		public override async Task ExecuteAsync(object? parameter)
		{
			_viewModel.ErrorMessage = string.Empty;
			try
			{
				AccountResult registrationResult = 
					await _authenticator.Register(_viewModel.Email, _viewModel.Login, _viewModel.Password1, _viewModel.Password2);

				switch (registrationResult)
				{
					case AccountResult.Success:
						_renavigator.Renavigate();
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
						_viewModel.ErrorMessage = "Registration failed.";
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				_viewModel.ErrorMessage = "Registration failed.";
			}
		}

		private void RegistrationVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(RegistrationVM.CanRegist))
			{
				OnCanExecuteChanged();
			}
		}
	}
}
