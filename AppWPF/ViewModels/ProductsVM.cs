using AppWPF.Database;
using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.ViewModels
{
	public class ProductsVM : ViewModelBase
	{
		private ProductsModel _productsModel;

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

		public ProductsVM()
		{
			_productsModel = new ProductsModel();
		}
	}
}
