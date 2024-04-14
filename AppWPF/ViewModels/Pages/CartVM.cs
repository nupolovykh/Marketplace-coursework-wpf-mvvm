using AppWPF.Database;
using System.Collections.Generic;
using System.Windows.Input;
using AppWPF.ViewModels.Additional;
using AppWPF.ViewModels.Models;

namespace AppWPF.ViewModels.Pages
{
    public class CartVM : ViewModelBase
	{
		private CartModel _cartModel;

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

		public CartVM()
		{
			_cartModel = new CartModel();
		}
	}
}
