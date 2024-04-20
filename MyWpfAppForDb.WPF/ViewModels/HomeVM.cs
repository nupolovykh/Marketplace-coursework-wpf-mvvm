using System.Collections.Generic;
using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private MainModel _mainModel;
        private ViewModelStore _viewModelStore;

        public string Search
        {
            get
            {
                return _mainModel.Search;
            }
            set
            {
                _mainModel.Search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public List<Product> Products
        {
            get
            {
                return _mainModel.Products;
            }
            set
            {
                _mainModel.Products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public List<Category> Categories
        {
            get
            {
                return _mainModel.Categories;
            }
            set
            {
                _mainModel.Categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ICommand Next { get; set; }
        public ICommand Prev { get; set; }
        public ICommand SelectCategory { get; set; }
        public ICommand SelectProduct { get; set; }
        public ICommand SearchBtn { get; set; }

        public HomeVM(ViewModelStore viewModelStore)
        {
            _viewModelStore = viewModelStore;
            _mainModel = new MainModel();
        }
    }
}
