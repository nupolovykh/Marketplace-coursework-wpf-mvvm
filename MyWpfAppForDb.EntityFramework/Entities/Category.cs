using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public partial class Category : EntityInstance
    {
        public Category()
        {
            ProductsInstances = new HashSet<ProductsInstance>();
        }

        public string Name { get; set; }

        public virtual ICollection<ProductsInstance> ProductsInstances { get; set; }
    }
}
