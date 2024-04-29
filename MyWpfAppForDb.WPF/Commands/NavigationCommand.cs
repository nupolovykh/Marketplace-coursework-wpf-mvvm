using Microsoft.EntityFrameworkCore.Metadata;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels;
using MyWpfAppForDb.WPF.ViewModels.Factories;
using System;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace MyWpfAppForDb.WPF.Commands
{
	public class UpdateCurrentVMCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private readonly INavigator _navigator;
		private readonly IAppViewModelFactory _appViewModelFactory;

		public UpdateCurrentVMCommand(INavigator navigator,
			IAppViewModelFactory appViewModelFactory)
		{
			_navigator = navigator;
			_appViewModelFactory = appViewModelFactory;
		}

		public bool CanExecute(object parametr)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter is ViewType)
			{
				ViewType viewType = (ViewType)parameter;
				_navigator.CurrentViewModel = _appViewModelFactory.CreateViewModel(viewType);
			}
		}
	}

}
