using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public partial class OrdersItem : EntityInstance
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
