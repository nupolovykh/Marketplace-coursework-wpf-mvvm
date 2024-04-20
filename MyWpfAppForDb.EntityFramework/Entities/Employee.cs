using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public int? DeliveryPointId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal? Salary { get; set; }

        public virtual DeliveryPoint DeliveryPoint { get; set; }
    }
}
