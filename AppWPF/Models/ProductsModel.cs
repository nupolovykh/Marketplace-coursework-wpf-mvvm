using AppWPF.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models
{
	public class ProductsModel
	{
		public string Search {  get; set; }
		public List<ProductInstance> ProductInstances { get; set; }
		public List<Product> Products { get; set; }
	}
}
