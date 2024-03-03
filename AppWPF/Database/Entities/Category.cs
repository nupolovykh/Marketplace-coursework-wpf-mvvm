using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.database
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Products> Products { get; set;}
    }
}
