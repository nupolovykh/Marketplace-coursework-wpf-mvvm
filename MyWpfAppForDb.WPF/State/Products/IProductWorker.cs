using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Products
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
