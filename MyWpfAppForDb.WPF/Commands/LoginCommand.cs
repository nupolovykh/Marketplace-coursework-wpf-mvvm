using MyWpfAppForDb.Domain.Exceptions;
using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly AuthorizationVM _viewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(AuthorizationVM viewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _viewModel = viewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _viewModel.PropertyChanged += AuthorizationVM_PropertyChanged!;
        }

        public override bool CanExecute(object parameter) => _viewModel.CanLogin && base.CanExecute(parameter);

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticator.Login(_viewModel.LoginEmail, _viewModel.Password);
                _renavigator.Renavigate();
            }

            catch (UserNotFoundException)
            {
                _viewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _viewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Login failed.";
            }
        }

        private void AuthorizationVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AuthorizationVM.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
