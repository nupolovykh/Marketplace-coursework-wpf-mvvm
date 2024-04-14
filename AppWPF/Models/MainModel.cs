using AppWPF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models
{
	public class MainModel
	{
		public string? Search { get; set; }
		public List<Category>? Categories { get; set; }
		public List<Product>? Products { get; set; }
	}
}
