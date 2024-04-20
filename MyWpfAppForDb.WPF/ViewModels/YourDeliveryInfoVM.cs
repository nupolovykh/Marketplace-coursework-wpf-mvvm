using System.Collections.Generic;
using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class YourDeliveryInfoVM : ViewModelBase
    {
        private YourDeliveryInfoModel _yourDeliveryModel;
        private ViewModelStore _viewModelStore;

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

        public YourDeliveryInfoVM(ViewModelStore viewModelStore)
        {
            _viewModelStore = viewModelStore;
            _yourDeliveryModel = new YourDeliveryInfoModel();
        }
    }
}
