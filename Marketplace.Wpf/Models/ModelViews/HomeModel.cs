using System.Collections.Generic;
using System.Collections.ObjectModel;
using Marketplace.EntityFramework.Entities;
using Marketplace.Wpf.Models.DataTransferObjects;

namespace Marketplace.Wpf.Models
{
	public class HomeModel
	{
		public ObservableCollection<ProductDto>? Products { get; set; }
		public ProductDto? ChoosenProduct { get; set; }
		public int MaxPage { get; set; } = 0;
		public int CurrentPage { get; set; } = 0;
	}
}
