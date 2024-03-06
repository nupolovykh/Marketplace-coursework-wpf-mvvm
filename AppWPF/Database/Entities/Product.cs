using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.database
{
	public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; } 
        public decimal Price { get; set; }
        public int ProductInstanceId { get; set; }
        public ProductInstance ProductInstance { get; set; }
        public int MarketId { get; set; }
        public Market Market { get; set; }
        public IEnumerable<ProductAndOrder> ProductAndOrders { get; set; }
    }
}
