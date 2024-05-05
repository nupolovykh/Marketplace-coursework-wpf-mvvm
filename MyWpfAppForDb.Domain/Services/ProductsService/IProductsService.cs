using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Services.ProductsService
{
	public enum ProductQuaryResult
	{
		Success,
		ThisProductCanNotBeDeleted,
		Error
	}

	public interface IProductsService : IDataService<Product>
	{
		Task<IEnumerable<Product>> GetPage(int offset);

		Task<IEnumerable<Product>> GetPageWithSearch(int offset, string search);

		Task<int> GetLastPageNumber();

		Task<int> GetLastPageNumberWithSearch(string search);

		Task<int> GetNewId();
	}
}
