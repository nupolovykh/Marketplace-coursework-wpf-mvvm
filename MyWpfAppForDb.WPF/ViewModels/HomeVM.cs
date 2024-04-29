using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AutoMapper;
using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Controls;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class HomeVM : ViewModelBase
	{
		private HomeModel _homeModel;

		private IProductsService _productsService;
        private IMapper _mapper;

        public List<ProductDto> Products
		{
			get => _homeModel.Products;
			set
			{
				_homeModel.Products = value;
				OnPropertyChanged(nameof(Products));
			}
		}

        public ProductDto ChoosenProduct
		{
			get => _homeModel.ChoosenProduct;
			set
			{
				_homeModel.ChoosenProduct = value;
				OnPropertyChanged(nameof(ChoosenProduct));
			}
		}

		public string Search
		{
			get => _homeModel.Search;
			set
			{
				_homeModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		public ICommand Next { get; set; }
		public ICommand Prev { get; set; }
		public ICommand SearchBtn { get; set; }

		public HomeVM(IProductsService productsService, IMapper mapper)
		{
            _homeModel = new HomeModel();
			_productsService = productsService;
			_mapper	= mapper;

            FillingOfTheListBox();
		}

		private async void FillingOfTheListBox()
		{
            var product = await _productsService.GetAll();

            Products = _mapper.Map<List<ProductDto>>(product.ToList());


			//List<ProductCard> ProductCardss = new List<ProductCard>();
			//foreach (var p in Products)
			//{
			//	var NewCard = new ProductCard();
			//	NewCard.DataContext = p;
			//}

			//ProductCards = ProductCardss;
		}
    }
}
