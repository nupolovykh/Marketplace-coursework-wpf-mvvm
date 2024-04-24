using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public partial class Client : EntityInstance
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
