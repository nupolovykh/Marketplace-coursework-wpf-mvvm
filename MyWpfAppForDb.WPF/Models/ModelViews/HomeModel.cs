using System.Collections.Generic;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;

namespace MyWpfAppForDb.WPF.Models
{
	public class HomeModel
	{
		public string? Search { get; set; }
		public List<ProductDto>? Products { get; set; }
		public ProductDto? ChoosenProduct { get; set; }
	}
}
