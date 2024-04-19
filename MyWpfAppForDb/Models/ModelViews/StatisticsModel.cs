using AppWPF.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models.PlainModels
{
    public class StatisticsModel
    {
        public string? Search { get; set; }
        public List<DeliveryPoint>? DeliveryPoints { get; set; }
    }
}
