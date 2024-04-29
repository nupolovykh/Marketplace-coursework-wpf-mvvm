using MyWpfAppForDb.EntityFramework.Entities;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models
{
	public class YourDeliveryInfoModel
	{
		public string? Search { get; set; }
		public List<Product>? Products { get; set; }
		public List<Order>? Orders { get; set; }
	}
}
