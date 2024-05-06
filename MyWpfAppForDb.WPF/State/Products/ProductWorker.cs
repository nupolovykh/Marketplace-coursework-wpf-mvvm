using AutoMapper;
using Microsoft.VisualBasic;
using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace MyWpfAppForDb.WPF.State.Products
{
	internal class ProductWorker : IProductWorker
	{
		private readonly IProductsService _productsService;
		private readonly IMapper _mapper;

		public ProductWorker(IProductsService productsService, IMapper mapper)
		{
			_productsService = productsService;
			_mapper = mapper;
		}

		public async Task<ProductQuaryResult> AddProduct()
		{
			try
			{
				int MarketId = int.Parse(MarketIdQuestion());
				int ProductInstanceId = int.Parse(ProductInstanceIdQuestion());

				ProductDto productDto = new ProductDto()
				{
					Id = await _productsService.GetNewId(),
					MarketId = MarketId,
					ProductInstanceId = ProductInstanceId
				};

				Product product = _mapper.Map<Product>(productDto);
				await _productsService.Create(product);

				return ProductQuaryResult.Success;
			}
			catch
			{
				return ProductQuaryResult.Error;
			}
		}

		private string MarketIdQuestion() => Interaction.InputBox("Enter the marketId:", "Prescribe the info", "write here");
		private string ProductInstanceIdQuestion() => Interaction.InputBox("Enter the productInstanceId:", "Prescribe the info", "write here");

		public async Task<ProductQuaryResult> UpdateProduct(ProductDto dto)
		{
			try
			{
				Product product = _mapper.Map<Product>(dto);
				await _productsService.Update(product.Id, product);
				return ProductQuaryResult.Success;
			}
			catch
			{
				return ProductQuaryResult.Error;
			}
		}

		public async Task<ProductQuaryResult> DeleteProduct(int id)
		{
			try
			{
				await _productsService.Delete(id);
				return ProductQuaryResult.Success;
			}
			catch (Exception ex)
			{
				ThrowError(ex);
				return ProductQuaryResult.Error;
			}
		}

		private void ThrowError(Exception exception) => MessageBox.Show(exception.Message).HasFlag(MessageBoxResult.No);

		public async Task<ObservableCollection<ProductDto>> GetPageWithSearch(int page, string search)
		{
			var products = (string.IsNullOrEmpty(search)) ?
				await _productsService.GetPage(page) : await _productsService.GetPageWithSearch(page, search);

			var productsDto = _mapper.Map<ObservableCollection<ProductDto>>(products);
			return productsDto;
		}

		public async Task<int> GetLastIdPageWithSearch(string search)
			=> (string.IsNullOrEmpty(search)) ? await _productsService.GetLastPageNumber() :
					await _productsService.GetLastPageNumberWithSearch(search);

	}
}
