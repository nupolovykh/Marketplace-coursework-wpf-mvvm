using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Entities
{
    public class Role : EntityInstance
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
