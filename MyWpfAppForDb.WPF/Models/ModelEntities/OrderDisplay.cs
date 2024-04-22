using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class OrderDisplay : ModelEntityBase
    {
        public OrderDisplay()
        {
            OrdersItems = new HashSet<OrdersItemDisplay>();
        }

        public int OrderId { get; set; }
        public int? DeliveryPointId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual ClientGto Client { get; set; }
        public virtual DeliveryPointDisplay DeliveryPoint { get; set; }
        public virtual ICollection<OrdersItemDisplay> OrdersItems { get; set; }
    }
}
