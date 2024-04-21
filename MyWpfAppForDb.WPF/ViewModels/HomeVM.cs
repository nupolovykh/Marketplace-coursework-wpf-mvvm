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
            get => _mainModel.Search;
            set
            {
                _mainModel.Search = value;
                OnPropertyChanged();
            }
        }

        public List<Product> Products
        {
            get => _mainModel.Products;
            set
            {
                _mainModel.Products = value;
                OnPropertyChanged();
            }
        }

        public List<Category> Categories
        {
            get => _mainModel.Categories;
            set
            {
                _mainModel.Categories = value;
                OnPropertyChanged();
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
