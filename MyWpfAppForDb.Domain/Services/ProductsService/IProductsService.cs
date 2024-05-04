using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Services.ProductsService
{
	public interface IProductsService : IDataService<Product>
	{
		Task<IEnumerable<Product>> GetPage(int offset);

		Task<int> GetLastPageNumber();

		Task<int> GetNewId();
	}
}
