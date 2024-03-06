using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.database
{
	public class Order
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int DeliveryPointId { get; set; }
        public DeliveryPoint DeliveryPoint { get; set; }
        public IEnumerable<ProductAndOrder> ProductAndOrders { get; set; }
    }
}
