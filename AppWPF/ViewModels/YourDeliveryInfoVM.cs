using AppWPF.database;
using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.ViewModels
{
    public class YourDeliveryInfoVM : ViewModelBase
	{
        private YourDeliveryInfoModel _yourDeliveryModel;

		public string Search
		{
			get
			{
				return _yourDeliveryModel.Search;
			}
			set
			{
				_yourDeliveryModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		public List<Product> Products
		{
			get
			{
				return _yourDeliveryModel.Products;
			}
			set
			{
				_yourDeliveryModel.Products = value;
				OnPropertyChanged(nameof(Products));
			}
		}

		public List<Order> Orders
		{
			get
			{
				return _yourDeliveryModel.Orders;
			}
			set
			{
				_yourDeliveryModel.Orders = value;
				OnPropertyChanged(nameof(Orders));
			}
		}

		public ICommand SelectOrder { get; set; }
		public ICommand SelectProduct { get; set; }
		public ICommand SearchBtn { get; set; }

		public YourDeliveryInfoVM() 
        {
			_yourDeliveryModel = new YourDeliveryInfoModel();
		}
    }
}
