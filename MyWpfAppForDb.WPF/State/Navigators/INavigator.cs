using MyWpfAppForDb.WPF.ViewModels;
using System;

namespace MyWpfAppForDb.WPF.State.Navigators
{
	public enum ViewType
	{
		Authorization,
		Home,
		Registration,
		Profile,
		Statistics,
		YourDeliveryInfo,
		Products
	}

	public interface INavigator
	{
		ViewModelBase CurrentViewModel { get; set; }
		event Action StateChanged;
	}
}
