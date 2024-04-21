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
            if (_viewModelStore!.CurrentViewModel is null) _viewModelStore!.CurrentViewModel = host.Services.GetRequiredService<AuthorizationVM>();

            _viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            PathToProfile = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<ProfileVM>());

            PathToAuthorization = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<AuthorizationVM>());

            PathToRegistration = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<RegistrationVM>());

            PathToYourDeliveryInfo = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<YourDeliveryInfoVM>());

            PathToProducts = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<ProductsVM>());

            PathToStatistics = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<StatisticsVM>());

            Return = new NavigateCommand(_viewModelStore, host.Services.GetRequiredService<HomeVM>());
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

}
