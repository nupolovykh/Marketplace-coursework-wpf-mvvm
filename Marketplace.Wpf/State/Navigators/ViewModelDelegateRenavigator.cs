using Marketplace.Wpf.ViewModels;

namespace Marketplace.Wpf.State.Navigators
{
	internal class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
	{
		private readonly INavigator _navigator;
		private readonly CreateViewModel<TViewModel> _createViewModel;

		public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
		{
			_navigator = navigator;
			_createViewModel = createViewModel;
		}

		public void Renavigate()
		{
			_navigator.CurrentViewModel = _createViewModel();
		}
	}
}
