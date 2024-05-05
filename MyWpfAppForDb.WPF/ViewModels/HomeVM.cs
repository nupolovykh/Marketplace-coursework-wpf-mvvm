using System.Windows.Input;
using MyWpfAppForDb.WPF.Models;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.Commands;
using System.Collections.ObjectModel;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.ViewModels.Specific;
using MyWpfAppForDb.WPF.State.Products;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class HomeVM : ViewModelBase
	{
		private HomeModel _homeModel;

		private readonly IProductWorker _productsWorker;
		private readonly IAccountStore _store;

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

		public SearchViewModel SearchViewModel { get; }
		private string SearchString => SearchViewModel.Search;
		private bool HasSearchString => SearchViewModel.HasSearchString;

		private bool NotFirstPage => CurrentPage is not 0;

		public bool RequiredRole => _store.IsOperator() || _store.IsAdmin();

		public ICommand Next { get; set; }
		public ICommand Prev { get; set; }

		public ICommand AddCommand { get; set; }
		public ICommand SaveCommand { get; set; }
		public ICommand DeleteCommand { get; set; }

		public HomeVM(IProductWorker productsWorker, IAccountStore store)
		{
			_homeModel = new HomeModel();
			_productsWorker = productsWorker;
			_store = store;

			SearchViewModel = new SearchViewModel();
			CurrentPage = 0;
			GetPage(CurrentPage, SearchString);

			Next = new DelegateCommand(
				action: (_) => GetPage(++CurrentPage, SearchString),
				condition: (_) => CurrentPage != MaxPage,
				vmb: this);

			Prev = new DelegateCommand(
				action: (_) => GetPage(--CurrentPage, SearchString),
				condition: (_) => NotFirstPage,
				vmb: this);

			AddCommand = new DelegateCommand(
				action: (_) => AddProduct(CurrentPage, SearchString),
				condition: (_) => true,
				vmb: this);

			SaveCommand = new DelegateCommand(
				action: (_) => UpdateProduct(CurrentPage, SearchString, ChoosenProduct),
				condition: (_) => ChoosenProduct is not null,
				vmb: this);

			DeleteCommand = new RelayGenericCommand<int>(
				action: (id) => DeleteProduct(id, CurrentPage, SearchString), //lal
				condition: (id) => true);

			SearchViewModel.SearchCommand = new RelayGenericCommand<string>(
				action: (search) => GetPage(CurrentPage, search), //lal
				condition: (search) => true);
		}

		private async void UpdateProduct(int page, string search, ProductDto product)
		{
			await _productsWorker.UpdateProduct(product);
			GetPage(page, search);
		}

		private async void AddProduct(int page, string search)
		{
			await _productsWorker.AddProduct();
			GetPage(page, search);
		}

		private async void DeleteProduct(int id, int page, string search)
		{
			await _productsWorker.DeleteProduct(id);
			GetPage(page, search);
		}

		private async void GetPage(int page, string search)
		{
			Products = await _productsWorker.GetPageWithSearch(page, search);
			MaxPage = await _productsWorker.GetLastIdPageWithSearch(search);
			if (Products.Count is 0 && NotFirstPage) Prev.Execute(null);
		}

		public override void Dispose()
		{
			SearchViewModel.Dispose();
			base.Dispose();
		}
	}
}
