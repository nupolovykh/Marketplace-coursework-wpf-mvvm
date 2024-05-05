using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System.Collections.ObjectModel;

namespace MyWpfAppForDb.WPF.Models
{
	public class StatisticsModel
	{
		public ObservableCollection<DeliveryPointDto>? DeliveryPoints { get; set; }
	}
}
