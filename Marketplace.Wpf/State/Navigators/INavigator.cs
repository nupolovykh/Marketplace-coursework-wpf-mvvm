using Marketplace.Wpf.ViewModels;
using System;

namespace Marketplace.Wpf.State.Navigators
{
	public enum ViewType
	{
		Authorization,
		Home,
		Registration,
		Profile,
		Statistics,
		YourDeliveryInfo
	}

	public interface INavigator
	{
		ViewModelBase CurrentViewModel { get; set; }
		event Action StateChanged;
	}
}
