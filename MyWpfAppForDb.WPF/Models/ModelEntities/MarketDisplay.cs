using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class MarketDisplay : ModelEntityBase
    {
        public MarketDisplay()
        {
            Products = new HashSet<ProductDisplay>();
        }

        public int MarketId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ProductDisplay> Products { get; set; }
    }
}
