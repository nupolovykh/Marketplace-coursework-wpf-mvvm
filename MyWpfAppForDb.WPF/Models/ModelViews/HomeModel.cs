using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;

namespace MyWpfAppForDb.WPF.Models
{
	public class HomeModel
	{
		public ObservableCollection<ProductDto>? Products { get; set; }
		public ProductDto? ChoosenProduct { get; set; }
		public int MaxPage { get; set; } = 0;
		public int CurrentPage { get; set; } = 0;
	}
}
