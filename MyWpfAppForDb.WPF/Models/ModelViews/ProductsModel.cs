using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models
{
    public class ProductsModel
    {
        public string? Search { get; set; }
        public List<ProductsInstance>? ProductInstances { get; set; }
        public List<Product>? Products { get; set; }
    }
}
