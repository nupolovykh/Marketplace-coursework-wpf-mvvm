using AppWPF.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Models
{
	public class CartModel
	{
		public string PersonalData { get; set; }
		public string CardNumber { get; set; }
		public string Date { get; set; }
		public string CodeCVC { get; set; }
		public List<Product> Products { get; set; }
	}
}
