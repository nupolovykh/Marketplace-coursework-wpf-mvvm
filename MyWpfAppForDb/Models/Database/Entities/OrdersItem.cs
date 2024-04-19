using System;
using System.Collections.Generic;

namespace AppWPF.Models.Database.Entities
{
    public partial class OrdersItem
    {
        public int OrdersItemId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
