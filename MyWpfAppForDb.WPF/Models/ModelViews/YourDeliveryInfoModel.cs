using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System.Collections.ObjectModel;

namespace MyWpfAppForDb.WPF.Models
{
	public class YourDeliveryInfoModel
	{
		public ObservableCollection<OrderDto>? Orders { get; set; }
	}
}
