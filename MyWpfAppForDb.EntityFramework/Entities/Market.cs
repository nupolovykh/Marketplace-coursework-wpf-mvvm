using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
	public partial class Market : EntityInstance
	{
		public Market()
		{
			Products = new HashSet<Product>();
		}

		public string Name { get; set; }
		public string City { get; set; }
		public string Address { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
