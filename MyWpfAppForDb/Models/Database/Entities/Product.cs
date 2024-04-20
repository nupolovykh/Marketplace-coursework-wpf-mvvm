using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.Models.Database.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrdersItems = new HashSet<OrdersItem>();
        }

        public int ProductId { get; set; }
        public int? ProductInstanceId { get; set; }
        public int? MarketId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }

        public virtual Market? Market { get; set; }
        public virtual ProductsInstance? ProductInstance { get; set; }
        public virtual ICollection<OrdersItem> OrdersItems { get; set; }
    }
}
