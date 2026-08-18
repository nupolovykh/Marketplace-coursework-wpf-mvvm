using Microsoft.EntityFrameworkCore.Metadata;
using Marketplace.Wpf.State.Navigators;
using Marketplace.Wpf.ViewModels;
using Marketplace.Wpf.ViewModels.Factories;
using System;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace Marketplace.Wpf.Commands
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
