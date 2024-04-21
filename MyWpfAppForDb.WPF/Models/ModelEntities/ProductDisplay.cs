using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class ProductDisplay : ModelEntityBase
    {
        public ProductDisplay()
        {
            OrdersItems = new HashSet<OrdersItemDisplay>();
        }

        public int ProductId { get; set; }
        public int? ProductInstanceId { get; set; }
        public int? MarketId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }

        public virtual MarketDisplay Market { get; set; }
        public virtual ProductsInstanceDisplay ProductInstance { get; set; }
        public virtual ICollection<OrdersItemDisplay> OrdersItems { get; set; }
    }
}
