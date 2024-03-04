using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.database
{
    class DeliveryPoint
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Rating { get; set; }
        public string ZipCode { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
