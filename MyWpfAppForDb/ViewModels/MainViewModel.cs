using System.Windows.Input;
using AppWPF.Commands;
using MyWpfAppForDb.ViewModels;

namespace AppWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
	{
		private readonly ViewModelStore _viewModelStore;

		public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;

		public ICommand PathToProfile { get; set; }

		public ICommand PathToAuthorization { get; set; }

		public ICommand PathToRegistration { get; set; }

		public ICommand PathToCart { get; set; }

		public ICommand PathToYourDeliveryInfo { get; set; }

		public ICommand PathToProducts { get; set; }

		public ICommand PathToStatistics { get; set; }

		public ICommand Return { get; set; }


		public MainViewModel(ViewModelStore viewModelStore)
		{
			_viewModelStore = viewModelStore;
			_viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

			PathToProfile = new NavigateCommand(viewModelStore, new ProfileVM(viewModelStore));

			PathToAuthorization = new NavigateCommand(viewModelStore, new AuthorizationVM(viewModelStore));

			PathToRegistration = new NavigateCommand(viewModelStore, new RegistrationVM(viewModelStore));

			PathToCart = new NavigateCommand(viewModelStore, new CartVM(viewModelStore));

			PathToYourDeliveryInfo = new NavigateCommand(viewModelStore, new YourDeliveryInfoVM(viewModelStore));

			PathToProducts = new NavigateCommand(viewModelStore, new ProductsVM(viewModelStore));

			PathToStatistics = new NavigateCommand(viewModelStore, new StatisticsVM(viewModelStore));

			Return = new NavigateCommand(viewModelStore, new MainVM(viewModelStore));
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}

}
