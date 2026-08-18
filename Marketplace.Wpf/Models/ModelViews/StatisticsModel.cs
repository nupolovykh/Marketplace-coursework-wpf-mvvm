using Marketplace.Wpf.Models.DataTransferObjects;
using System.Collections.ObjectModel;

namespace Marketplace.Wpf.Models
{
	public class StatisticsModel
	{
		public ObservableCollection<DeliveryPointDto>? DeliveryPoints { get; set; }

		public ObservableCollection<EmployeeDto>? Employees { get; set; }
	}
}
