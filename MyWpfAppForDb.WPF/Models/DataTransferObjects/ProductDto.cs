using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
	public partial class ProductDto : ModelGtoBase
	{
		public ProductDto()
		{
			OrdersItems = new HashSet<OrdersItemDto>();
		}

		private int _id;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged();
			}
		}

		private int? _productInstanceId;

		public int? ProductInstanceId
		{
			get => _productInstanceId;
			set
			{
				_productInstanceId = value;
				OnPropertyChanged();
			}
		}

		private int? _marketId;

		public int? MarketId
		{
			get => _marketId;
			set
			{
				_marketId = value;
				OnPropertyChanged();
			}
		}

		private string? _name;

		public string? Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		private decimal? _price;

		public decimal? Price
		{
			get => _price;
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}

		private decimal? _rating;

		public decimal? Rating
		{
			get => _rating;
			set
			{
				_rating = value;
				OnPropertyChanged();
			}
		}

		public virtual MarketDto Market { get; set; }
		public virtual ProductsInstanceDto ProductInstance { get; set; }
		public virtual ICollection<OrdersItemDto> OrdersItems { get; set; }
	}
}
