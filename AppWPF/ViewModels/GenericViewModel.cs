using AppWPF.ViewModels.BaseClasses;

namespace AppWPF.ViewModels
{
	public class GenericViewModel : ViewModelBase
	{
		private readonly ViewModelStore _viewModelStore;

		public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;

		public GenericViewModel(ViewModelStore viewModelStore)
		{
			_viewModelStore = viewModelStore;
			_viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}

}
