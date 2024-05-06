using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.State.Delivery;
using MyWpfAppForDb.WPF.ViewModels.Specific;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class YourDeliveryInfoVM : ViewModelBase
	{
		private YourDeliveryInfoModel _yourDeliveryModel;

		private readonly IDeliveryWorker _deliveryWorker;
		private readonly IAccountStore _accountStore;

		public ObservableCollection<OrderDto> Orders
		{
			get => _yourDeliveryModel.Orders!;
			set
			{
				_yourDeliveryModel.Orders = value;
				OnPropertyChanged(nameof(Orders));
			}
		}

		private OrderDto _selectedOrder;
		public OrderDto SelectedOrder
		{
			get => _selectedOrder!;
			set
			{
				_selectedOrder = value;
				OnPropertyChanged(nameof(SelectedOrder));
			}
		}

		private OrdersItemDto _selectedOrderItem;
		public OrdersItemDto SelectedOrderItem
		{
			get => _selectedOrderItem!;
			set
			{
				_selectedOrderItem = value;
				OnPropertyChanged(nameof(SelectedOrderItem));
			}
		}

		private EmployeeDto _selectedEmployee;
		public EmployeeDto SelectedEmployee
		{
			get => _selectedEmployee!;
			set
			{
				_selectedEmployee = value;
				OnPropertyChanged(nameof(SelectedEmployee));
			}
		}

		private TabItem _selectedTab;
		public TabItem SelectedTab
		{
			get => _selectedTab;
			set
			{
				_selectedTab = value;
				OnPropertyChanged(nameof(SelectedTab));
			}
		}

		private EmployeeDto CurrentEmployee => _accountStore.CurrentEmployee;

		public SearchViewModel SearchViewModel { get; }
		public string SearchString => SearchViewModel.Search;
		private bool HasSearchString => SearchViewModel.HasSearchString;

		public YourDeliveryInfoVM(IDeliveryWorker deliveryWorker, IAccountStore accountStore)
		{
			_yourDeliveryModel = new YourDeliveryInfoModel();
			SearchViewModel = new SearchViewModel();

			_deliveryWorker = deliveryWorker;
			_accountStore = accountStore;

			if (CurrentEmployee is not null)
			{
				InitContent();

				SearchViewModel.SearchCommand = new DelegateCommand(
					action: (_) => Search(SelectedTab, SearchString),
					condition: (_) => true,
					vmb: this);
			}
		}

		private async void InitContent()
		{
			Orders = await _deliveryWorker.GetOrders(CurrentEmployee);
		}

		private int PreviousId { get; set; }

		private async void Search(TabItem selectedTab, string search)
		{
			PreviousId = SelectedOrder?.Id ?? 0;
			if (selectedTab.Header is "Order - DeliveryPoint")
			{
				Orders = await _deliveryWorker.GetOrdersByDelivery(CurrentEmployee, search);
			}
			else if (selectedTab.Header is "Items - Product")
			{
				Orders = await _deliveryWorker.GetOrdersByProduct(CurrentEmployee, search);
				SelectedOrder = Orders.FirstOrDefault(o => o.Id == PreviousId)!;
			}
			else
			{
				Orders = await _deliveryWorker.GetOrdersByEmployee(CurrentEmployee, search);
				SelectedOrder = Orders.FirstOrDefault(o => o.Id == PreviousId)!;
			}
		}

		public override void Dispose()
		{
			SearchViewModel.Dispose();
			base.Dispose();
		}
	}
}
