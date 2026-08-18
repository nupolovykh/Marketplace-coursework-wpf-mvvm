using Marketplace.Wpf.Models.DataTransferObjects;
using System.Collections.ObjectModel;

namespace Marketplace.Wpf.Models
{
	public class YourDeliveryInfoModel
	{
		public ObservableCollection<OrderDto>? Orders { get; set; }
	}
}
