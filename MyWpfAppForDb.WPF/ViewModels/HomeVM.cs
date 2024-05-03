using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AutoMapper;
using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.Commands;
using System.Collections.ObjectModel;
using System;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class HomeVM : ViewModelBase
	{
		private HomeModel _homeModel;

		private IProductsService _productsService;
		private IMapper _mapper;

		public int CurrentPage
		{
			get => _homeModel.CurrentPage;
			set
			{
				_homeModel.CurrentPage = value;
				OnPropertyChanged(nameof(CurrentPage));
			}
		}

		public int MaxPage
		{
			get => _homeModel.MaxPage;
			set
			{
				_homeModel.MaxPage = value;
				OnPropertyChanged(nameof(MaxPage));
			}
		}

		public ObservableCollection<ProductDto> Products
		{
			get => _homeModel.Products!;
			set
			{
				_homeModel.Products = value;
				OnPropertyChanged(nameof(Products));
			}
		}

		public ProductDto ChoosenProduct
		{
			get => _homeModel.ChoosenProduct!;
			set
			{
				_homeModel.ChoosenProduct = value;
				OnPropertyChanged(nameof(ChoosenProduct));
			}
		}

		public string Search
		{
			get => _homeModel.Search!;
			set
			{
				_homeModel.Search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		public ICommand Next { get; set; }
		public ICommand Prev { get; set; }
		public ICommand SearchBtn { get; set; }

		public ICommand DeleteCommand { get; set; }

		public HomeVM(IProductsService productsService, IMapper mapper)
		{
			_homeModel = new HomeModel();
			_productsService = productsService;
			_mapper	= mapper;

			AsyncInitializing();

			Next = new DelegateCommand(
				action: (_) => ChangePage(++CurrentPage),
				condition: (_) => CurrentPage != MaxPage,
				vmb: this);

			Prev = new DelegateCommand(
				action: (_) => ChangePage(--CurrentPage),
				condition: (_) => CurrentPage != 0,
				vmb: this);

			DeleteCommand = new RelayGenericCommand<int>(
				action: (id) => DeleteProduct(id),
				condition: (id) => true);
		}

		private async void AsyncInitializing()
		{
			CurrentPage = 0;
			MaxPage = await _productsService.GetLastPageNumber();

			var products = await _productsService.GetPage(CurrentPage);
			Products = _mapper.Map<ObservableCollection<ProductDto>>(products);
		}

		private async void DeleteProduct(int id)
		{
			await _productsService.Delete(id);
			var products = await _productsService.GetPage(CurrentPage);
			Products = _mapper.Map<ObservableCollection<ProductDto>>(products);

			MaxPage = await _productsService.GetLastPageNumber();
			if (Products.Count is 0) Prev.Execute(null);
		}

		private async void ChangePage(int page)
		{
			var products = await _productsService.GetPage(page);
			Products = _mapper.Map<ObservableCollection<ProductDto>>(products);
		}
	}
}
