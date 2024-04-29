using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
	public partial class Order : EntityInstance
	{
		public Order()
		{
			OrdersItems = new HashSet<OrdersItem>();
		}

		public int? DeliveryPointId { get; set; }
		public int? ClientId { get; set; }
		public DateTime? OrderDate { get; set; }
		public string Status { get; set; }
		public decimal? TotalAmount { get; set; }

		public virtual Client Client { get; set; }
		public virtual DeliveryPoint DeliveryPoint { get; set; }
		public virtual ICollection<OrdersItem> OrdersItems { get; set; }
	}
}
