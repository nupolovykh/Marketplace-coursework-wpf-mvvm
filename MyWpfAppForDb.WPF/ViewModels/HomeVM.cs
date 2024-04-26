using System.Collections.Generic;
using System.Windows.Input;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private HomeModel _homeModel;

        public string Search
        {
            get => _homeModel.Search!;
            set
            {
                _homeModel.Search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public List<Product> Products
        {
            get => _homeModel.Products!;
            set
            {
                _homeModel.Products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public List<Category> Categories
        {
            get => _homeModel.Categories!;
            set
            {
                _homeModel.Categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ICommand Next { get; set; }
        public ICommand Prev { get; set; }
        public ICommand SelectCategory { get; set; }
        public ICommand SelectProduct { get; set; }
        public ICommand SearchBtn { get; set; }

        public HomeVM()
        {
            _homeModel = new HomeModel();
        }
    }
}
