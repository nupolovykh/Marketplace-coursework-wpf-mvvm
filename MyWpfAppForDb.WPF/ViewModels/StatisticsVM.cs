using System.Collections.ObjectModel;
using System.Windows.Controls;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.State.Delivery;
using MyWpfAppForDb.WPF.ViewModels.Specific;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class StatisticsVM : ViewModelBase
	{
		private StatisticsModel _statisticsModel;

		private readonly IDeliveryWorker _deliveryWorker;
		private readonly IAccountStore _accountStore;

		public ObservableCollection<DeliveryPointDto> DeliveryPoints
		{
			get => _statisticsModel.DeliveryPoints!;
			set
			{
				_statisticsModel.DeliveryPoints = value;
				OnPropertyChanged(nameof(DeliveryPoints));
			}
		}

		private DeliveryPointDto _selectedDeliveryPoint;
		public DeliveryPointDto SelectedDeliveryPoint
		{
			get => _selectedDeliveryPoint!;
			set
			{
				_selectedDeliveryPoint = value;
				OnPropertyChanged(nameof(SelectedDeliveryPoint));
			}
		}

		public ObservableCollection<EmployeeDto> Employees
		{
			get => _statisticsModel.Employees!;
			set
			{
				_statisticsModel.Employees = value;
				OnPropertyChanged(nameof(Employees));
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

		private EmployeeDto CurrentEmployee => _accountStore.CurrentEmployee;

		public SearchViewModel SearchViewModel { get; }
		public string SearchString => SearchViewModel.Search;
		private bool HasSearchString => SearchViewModel.HasSearchString;

		public StatisticsVM(IDeliveryWorker deliveryWorker, IAccountStore accountStore)
		{
			_statisticsModel = new StatisticsModel();
			SearchViewModel = new SearchViewModel();

			_deliveryWorker = deliveryWorker;
			_accountStore = accountStore;

			if (CurrentEmployee is not null)
			{
				Search(string.Empty);

				SearchViewModel.SearchCommand = new DelegateCommand(
					action: (_) => Search(SearchString),
					condition: (_) => true,
					vmb: this);
			}
		}

		private async void Search(string? search = "")
		{
			DeliveryPoints = await _deliveryWorker.GetDeliveryPoints(search);
			Employees = await _deliveryWorker.GetEmployees(search);
		}

		public override void Dispose()
		{
			SearchViewModel.Dispose();
			base.Dispose();
		}
	}
}
