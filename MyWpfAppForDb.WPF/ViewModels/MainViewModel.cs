using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.Commands;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
	{
		private readonly ViewModelStore _viewModelStore;

		public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;

		public ICommand PathToProfile { get; set; }

		public ICommand PathToAuthorization { get; set; }

		public ICommand PathToRegistration { get; set; }

		public ICommand PathToYourDeliveryInfo { get; set; }

		public ICommand PathToProducts { get; set; }

		public ICommand PathToStatistics { get; set; }

		public ICommand Return { get; set; }

        public MainViewModel(IHost host)
		{
			_viewModelStore = host.Services.GetRequiredService<ViewModelStore>();

			_viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

			PathToProfile = new NavigateCommand(_viewModelStore, new ProfileVM(_viewModelStore));

			PathToAuthorization = new NavigateCommand(_viewModelStore, new AuthorizationVM(_viewModelStore));

			PathToRegistration = new NavigateCommand(_viewModelStore, new RegistrationVM(_viewModelStore));

			PathToYourDeliveryInfo = new NavigateCommand(_viewModelStore, new YourDeliveryInfoVM(_viewModelStore));

			PathToProducts = new NavigateCommand(_viewModelStore, new ProductsVM(_viewModelStore));

			PathToStatistics = new NavigateCommand(_viewModelStore, new StatisticsVM(_viewModelStore));

			Return = new NavigateCommand(_viewModelStore, new HomeVM(_viewModelStore));
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}

}
