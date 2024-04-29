using System.Collections.Generic;
using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class StatisticsVM : ViewModelBase
	{
		private StatisticsModel _statisticsModel;
		private ViewModelStore _viewModelStore;

		public string Search
		{
			get => _statisticsModel.Search;
			set
			{
				_statisticsModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}
		//
		public List<DeliveryPoint> DeliveryPoints
		{
			get => _statisticsModel.DeliveryPoints;
			set
			{
				_statisticsModel.DeliveryPoints = value;
				OnPropertyChanged(nameof(SelectDeliveryPoint));
			}
		}

		public ICommand SearchBtn { get; set; }
		public ICommand SelectDeliveryPoint { get; set; }
		//public ICommand DrawTheGraph { get; set; }

		public StatisticsVM(ViewModelStore viewModelStore)
		{
			_viewModelStore = viewModelStore;
			_statisticsModel = new StatisticsModel();
		}
	}
}
