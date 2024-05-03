using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;

namespace MyWpfAppForDb.WPF.Models
{
	public class HomeModel
	{
		public string? Search { get; set; }
		public ObservableCollection<ProductDto>? Products { get; set; }
		public ProductDto? ChoosenProduct { get; set; }
		public int MaxPage { get; set; }
		public int CurrentPage { get; set; }
	}
}
