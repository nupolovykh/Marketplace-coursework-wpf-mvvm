using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class OrdersItemDisplay : ModelEntityBase
    {
        public int OrdersItemId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }

        public virtual OrderDisplay Order { get; set; }
        public virtual ProductDisplay Product { get; set; }
    }
}
