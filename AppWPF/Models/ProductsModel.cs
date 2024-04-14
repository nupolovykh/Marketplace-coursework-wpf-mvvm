using AppWPF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models
{
	public class ProductsModel
	{
		public string? Search { get; set; }
		public List<ProductsInstance>? ProductInstances { get; set; }
		public List<Product>? Products { get; set; }
	}
}
