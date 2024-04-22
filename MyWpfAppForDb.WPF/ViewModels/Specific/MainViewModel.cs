using System.Windows.Input;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels.Factories;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAppViewModelFactory _appViewModelFactory;
        private readonly INavigator _navigator;

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public ICommand UpdateCurrentVMCommand { get; }

        public MainViewModel(INavigator navigator, IAppViewModelFactory appViewModelFactory)
        {
            _navigator = navigator;
            _appViewModelFactory = appViewModelFactory;

            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentVMCommand = new UpdateCurrentVMCommand(navigator, _appViewModelFactory);
            UpdateCurrentVMCommand.Execute(ViewType.Authorization);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;
            
            base.Dispose();
        }
    }

}
