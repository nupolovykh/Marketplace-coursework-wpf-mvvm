using MyWpfAppForDb.EntityFramework.Entities;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models
{
	public class StatisticsModel
	{
		public string? Search { get; set; }
		public List<DeliveryPoint>? DeliveryPoints { get; set; }
	}
}
