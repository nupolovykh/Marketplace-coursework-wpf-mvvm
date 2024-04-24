using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly AuthorizationVM _authorizationVM;

        public LoginCommand(AuthorizationVM viewModel)
        {
            _authorizationVM = viewModel;
        }

        public override bool CanExecute(object parameter) => 
            _authorizationVM.CanLogin && base.CanExecute(parameter);

        public override async Task ExecuteAsync(object parameter)
        {
            
        }

    }
}
