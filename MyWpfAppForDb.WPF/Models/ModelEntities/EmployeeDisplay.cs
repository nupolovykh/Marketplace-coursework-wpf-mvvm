using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class EmployeeDisplay : ModelEntityBase
    {
        public int EmployeeId { get; set; }
        public int? DeliveryPointId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal? Salary { get; set; }

        public virtual DeliveryPointDisplay DeliveryPoint { get; set; }
    }
}
