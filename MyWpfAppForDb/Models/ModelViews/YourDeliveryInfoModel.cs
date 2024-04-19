using AppWPF.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models.PlainModels
{
    public class YourDeliveryInfoModel
    {
        public string? Search { get; set; }
        public List<Product>? Products { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
