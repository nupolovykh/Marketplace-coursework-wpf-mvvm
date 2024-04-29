using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
	public partial class Product : EntityInstance
	{
		public Product()
		{
			OrdersItems = new HashSet<OrdersItem>();
		}

		public int? ProductInstanceId { get; set; }
		public int? MarketId { get; set; }
		public decimal? Price { get; set; }
		public decimal? Rating { get; set; }

		public virtual Market Market { get; set; }
		public virtual ProductsInstance ProductInstance { get; set; }
		public virtual ICollection<OrdersItem> OrdersItems { get; set; }
	}
}
