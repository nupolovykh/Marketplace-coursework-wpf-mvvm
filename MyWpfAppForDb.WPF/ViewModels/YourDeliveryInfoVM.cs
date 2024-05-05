using System.Collections.ObjectModel;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.ViewModels.Specific;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class YourDeliveryInfoVM : ViewModelBase
	{
		private YourDeliveryInfoModel _yourDeliveryModel;

		public ObservableCollection<ProductDto> Products
		{
			get => _yourDeliveryModel.Products!;
			set
			{
				_yourDeliveryModel.Products = value;
				OnPropertyChanged(nameof(Products));
			}
		}

		public ObservableCollection<OrderDto> Orders
		{
			get => _yourDeliveryModel.Orders!;
			set
			{
				_yourDeliveryModel.Orders = value;
				OnPropertyChanged(nameof(Orders));
			}
		}

		public SearchViewModel SearchViewModel { get; }

		public YourDeliveryInfoVM()
		{
			_yourDeliveryModel = new YourDeliveryInfoModel();
			SearchViewModel = new SearchViewModel();
		}
	}
}
