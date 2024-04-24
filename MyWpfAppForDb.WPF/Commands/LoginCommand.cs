using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly AuthorizationVM _authorizationVM;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(AuthorizationVM viewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authorizationVM = viewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _authorizationVM.PropertyChanged += AuthorizationVM_PropertyChanged;
        }

        public override bool CanExecute(object parameter) => _authorizationVM.CanLogin && base.CanExecute(parameter);

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_authorizationVM.LoginEmail, _authorizationVM.Password);
                _renavigator.Renavigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
