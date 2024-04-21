using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class DeliveryPointDisplay : ModelEntityBase
    {
        public DeliveryPointDisplay()
        {
            Employees = new HashSet<EmployeeDisplay>();
            Orders = new HashSet<OrderDisplay>();
        }

        public int DeliveryPointId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal? Rating { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<EmployeeDisplay> Employees { get; set; }
        public virtual ICollection<OrderDisplay> Orders { get; set; }
    }
}
