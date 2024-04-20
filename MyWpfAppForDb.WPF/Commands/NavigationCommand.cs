using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly ViewModelStore _viewModelStore;
        private readonly ViewModelBase _newViewModel;

        public NavigateCommand(ViewModelStore viewModelStore, ViewModelBase newViewModel)
        {
            _viewModelStore = viewModelStore;
            _newViewModel = newViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModelStore.CurrentViewModel = _newViewModel;
        }
    }

}
