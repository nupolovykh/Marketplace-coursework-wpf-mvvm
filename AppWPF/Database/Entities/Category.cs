using System;
using System.Collections.Generic;

namespace AppWPF.Database
{
    public partial class Category
    {
        public Category()
        {
            ProductsInstances = new HashSet<ProductsInstance>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ProductsInstance> ProductsInstances { get; set; }
    }
}
