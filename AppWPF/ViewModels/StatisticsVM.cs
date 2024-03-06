using AppWPF.database;
using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.ViewModels
{
	public class StatisticsVM : ViewModelBase
	{
		private StatisticsModel _statisticsModel;

		public string Search
		{
			get
			{
				return _statisticsModel.Search;
			}
			set
			{
				_statisticsModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		public List<DeliveryPoint> DeliveryPoints
		{
			get
			{
				return _statisticsModel.DeliveryPoints;
			}
			set
			{
				_statisticsModel.DeliveryPoints = value;
				OnPropertyChanged(nameof(DeliveryPoints));
			}
		}

		public ICommand SearchBtn { get; set; }
		public ICommand SelectDeliveryPoint { get; set; }
		//public ICommand DrawTheGraph { get; set; }

		public StatisticsVM() 
		{
			_statisticsModel = new StatisticsModel();
		}
	}
}
