using AppWPF.ViewModels.Additional;
using System.Windows.Input;
using AppWPF.ViewModels.Commands;
using AppWPF.ViewModels.Pages;

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

			PathToProfile = new NavigateCommand(viewModelStore, new ProfileVM());

			PathToAuthorization = new NavigateCommand(viewModelStore, new AuthorizationVM());

			PathToRegistration = new NavigateCommand(viewModelStore, new RegistrationVM());

			PathToCart = new NavigateCommand(viewModelStore, new CartVM());

			PathToYourDeliveryInfo = new NavigateCommand(viewModelStore, new YourDeliveryInfoVM());

			PathToProducts = new NavigateCommand(viewModelStore, new ProductsVM());

			PathToStatistics = new NavigateCommand(viewModelStore, new StatisticsVM());

			Return = new NavigateCommand(viewModelStore, new MainVM());
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}

}
