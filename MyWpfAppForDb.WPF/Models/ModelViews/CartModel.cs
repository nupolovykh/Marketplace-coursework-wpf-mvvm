using System.Collections.Generic;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.WPF.Models
{
    public class CartModel
    {
        public string? PersonalData { get; set; }
        public string? CardNumber { get; set; }
        public string? Date { get; set; }
        public string? CodeCVC { get; set; }
        public List<Product>? Products { get; set; }
    }
}
