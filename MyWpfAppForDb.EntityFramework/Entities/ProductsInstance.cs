using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public partial class ProductsInstance : EntityInstance
    {
        public ProductsInstance()
        {
            Products = new HashSet<Product>();
        }

        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Availability { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
