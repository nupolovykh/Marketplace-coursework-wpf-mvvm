using System.Collections.Generic;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.WPF.Models
{
    public class HomeModel
    {
        public string? Search { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
    }
}
