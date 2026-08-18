using Marketplace.Domain.Services.ProductsService;
using Marketplace.Wpf.Models.DataTransferObjects;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Marketplace.Wpf.State.Products
{
	public interface IProductWorker
	{
		Task<ProductQuaryResult> AddProduct();

		Task<ProductQuaryResult> UpdateProduct(ProductDto product);

		Task<ProductQuaryResult> DeleteProduct(int id);

		Task<ObservableCollection<ProductDto>> GetPageWithSearch(int page, string search);

		Task<int> GetLastIdPageWithSearch(string search);
	}
}
