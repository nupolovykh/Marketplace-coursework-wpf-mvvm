using AppWPF.Database;
using System.Collections.Generic;
using System.Windows.Input;
using AppWPF.ViewModels.Additional;
using AppWPF.ViewModels.Models;

namespace AppWPF.ViewModels.Pages
{
    public class ProductsVM : ViewModelBase
	{
		private ProductsModel _productsModel;
		private ViewModelStore _viewModelStore;

		public string Search {
			get
			{
				return _productsModel.Search;
			}
			set 
			{
				_productsModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		public List<Product> Products
		{
			get
			{
				return _productsModel.Products;
			}
			set
			{
				_productsModel.Products = value;
				OnPropertyChanged(nameof(Products));
			}
		}

		public List<ProductsInstance> ProductInstances
		{
			get
			{
				return _productsModel.ProductInstances;
			}
			set
			{
				_productsModel.ProductInstances = value;
				OnPropertyChanged(nameof(ProductInstances));
			}
		}

		public ICommand SearchBtn { get; set; }
		public ICommand SelectProduct { get; set; }
		public ICommand SelectProductInstance { get; set; }

		public ProductsVM(ViewModelStore viewModelStore)
		{
			_viewModelStore = viewModelStore;
			_productsModel = new ProductsModel();
		}
	}
}
