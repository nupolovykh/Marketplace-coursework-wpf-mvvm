using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppWPF.Commands
{
    //public class LoginCommand : CommandBase
    //{
    //	private ViewModelStore _viewModelStore;
    //	private readonly EmployeeStore _employeeStore;
    //	private LoginViewModel _loginViewModel;

    //	public LoginCommand(ViewModelStore viewModelStore, EmployeeStore employeeStore, LoginViewModel loginViewModel)
    //	{
    //		_viewModelStore = viewModelStore;
    //		_employeeStore = employeeStore;
    //		_loginViewModel = loginViewModel;
    //	}

    //	public override void Execute(object parameter)
    //	{
    //		try
    //		{
    //			_employeeStore.CurrentEmployee = DataWork.GetEmployee(_loginViewModel.Login, _loginViewModel.Password);
    //			if (_employeeStore.CurrentEmployee.Position.Id == 3)
    //				_viewModelStore.CurrentViewModel = new AdminProductViewModel(_viewModelStore, _employeeStore);
    //			else
    //				_viewModelStore.CurrentViewModel = new EmployeeProductViewModel(_viewModelStore, _employeeStore);
    //		}
    //		catch (Exception ex)
    //		{
    //			MessageBox.Show(ex.Message);
    //		}
    //	}
    //}
}
