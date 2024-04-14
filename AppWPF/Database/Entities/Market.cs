using System;
using System.Collections.Generic;

namespace AppWPF.Database
{
    public partial class Market
    {
        public Market()
        {
            Products = new HashSet<Product>();
        }

        public int MarketId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
