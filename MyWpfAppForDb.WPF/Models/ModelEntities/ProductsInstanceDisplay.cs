using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class ProductsInstanceDisplay : ModelEntityBase
    {
        public ProductsInstanceDisplay()
        {
            Products = new HashSet<ProductDisplay>();
        }

        public int ProductInstanceId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Availability { get; set; }

        public virtual CategoryDisplay Category { get; set; }
        public virtual ICollection<ProductDisplay> Products { get; set; }
    }
}
