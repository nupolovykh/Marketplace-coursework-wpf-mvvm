using System.Collections.Generic;
using System.Windows.Input;
using AppWPF.Models.Database.Entities;
using AppWPF.Models.PlainModels;
using MyWpfAppForDb.ViewModels;

namespace AppWPF.ViewModels
{
    public class CartVM : ViewModelBase
    {
        private CartModel _cartModel;
        private ViewModelStore _viewModelStore;

        public string PersonalData
        {
            get
            {
                return _cartModel.PersonalData;
            }
            set
            {
                _cartModel.PersonalData = value;
                OnPropertyChanged(nameof(PersonalData));
            }
        }

        public string CardNumber
        {
            get
            {
                return _cartModel.CardNumber;
            }
            set
            {
                _cartModel.CardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        public string Date
        {
            get
            {
                return _cartModel.Date!;
            }
            set
            {
                _cartModel.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string CodeCVC
        {
            get
            {
                return _cartModel.CodeCVC;
            }
            set
            {
                _cartModel.CodeCVC = value;
                OnPropertyChanged(nameof(CodeCVC));
            }
        }

        public List<Product> Products
        {
            get
            {
                return _cartModel.Products;
            }
            set
            {
                _cartModel.Products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ICommand Pay { get; set; }

        public CartVM(ViewModelStore viewModelStore)
        {
            _viewModelStore = viewModelStore;
            _cartModel = new CartModel();
        }
    }
}
