using System.Collections.ObjectModel;
using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.ViewModels.Specific;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class StatisticsVM : ViewModelBase
	{
		private StatisticsModel _statisticsModel;

		public ObservableCollection<DeliveryPointDto> DeliveryPoints
		{
			get => _statisticsModel.DeliveryPoints!;
			set
			{
				_statisticsModel.DeliveryPoints = value;
				OnPropertyChanged(nameof(SelectDeliveryPoint));
			}
		}

		public SearchViewModel SearchViewModel { get; }

		public ICommand SelectDeliveryPoint { get; set; }
		//public ICommand DrawTheGraph { get; set; }

		public StatisticsVM()
		{
			_statisticsModel = new StatisticsModel();
			SearchViewModel = new SearchViewModel();
		}
	}
}
