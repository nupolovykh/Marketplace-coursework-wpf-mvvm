using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels.Factories;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IAppViewModelFactory _appViewModelFactory;
		private readonly INavigator _navigator;
		private readonly IAccountStore _store;

		public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
		public EmployeeDto CurrentEmployee => _store.CurrentEmployee;
		public ICommand UpdateCurrentVMCommand { get; }

		public MainViewModel(INavigator navigator, IAppViewModelFactory appViewModelFactory, IAccountStore store)
		{
			_navigator = navigator;
			_appViewModelFactory = appViewModelFactory;
			_store = store;

			_store.StateChanged += () => OnPropertyChanged(nameof(CurrentEmployee));
			_navigator.StateChanged += () => OnPropertyChanged(nameof(CurrentViewModel));

			UpdateCurrentVMCommand = new UpdateCurrentVMCommand(navigator, _appViewModelFactory);
			UpdateCurrentVMCommand.Execute(ViewType.Authorization);
		}

		public override void Dispose()
		{
			_navigator.StateChanged -= () => OnPropertyChanged(nameof(CurrentViewModel));
			_store.StateChanged -= () => OnPropertyChanged(nameof(CurrentEmployee));

			base.Dispose();
		}
	}

}
