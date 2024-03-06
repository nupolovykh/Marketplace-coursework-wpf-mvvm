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
    public class MainVM : ViewModelBase
    {
		private MainModel _mainModel;

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

		public MainVM()
        {
            _mainModel = new MainModel();
        }
    }
}
