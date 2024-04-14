using AppWPF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models
{
    public class StatisticsModel
    {
        public string? Search { get; set; }
		public List<DeliveryPoint>? DeliveryPoints { get; set; }
    }
}
