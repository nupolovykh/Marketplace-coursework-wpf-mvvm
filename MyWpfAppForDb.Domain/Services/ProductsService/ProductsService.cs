using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using MyWpfAppForDb.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using MyWpfAppForDb.Domain.Exceptions;

namespace MyWpfAppForDb.Domain.Services.ProductsService
{
	public class ProductsService : IProductsService
	{
		private static int fetch = 10;

		private readonly AppDbContextFactory _contextFactory;
		private readonly NonQueryDataService<Product> _nonQueryDataService;

		public ProductsService(AppDbContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
			_nonQueryDataService = new NonQueryDataService<Product>(contextFactory);
		}

		public async Task<Product> Create(Product entity)
		{
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<Product> Update(int id, Product entity)
		{
			return await _nonQueryDataService.Update(id, entity);
		}

		public async Task<bool> Delete(int id)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				List<Product> products = await context.Products
					.Include(p => p.OrdersItems)
						.ThenInclude(pi => pi.Order)
							.ToListAsync();

				List<Order> recentOrders = await context.Orders
					.Include(o => o.OrdersItems)
						.ThenInclude(oi => oi.Product)
					.Where(o => o.OrderDate >= DateTime.UtcNow.AddMonths(-2))
						.ToListAsync();

				var encounters = products.SelectMany(product =>
					recentOrders.Where(o => o.OrdersItems.Any(oi => oi.Product.Id == product.Id))
						.Select(order => new { Product = product, Order = order })).ToList();

				if (encounters.Any()) throw new IsNotAvailableExeprion(id);
			}

			return await _nonQueryDataService.Delete(id);
		}

		public async Task<Product> Get(int id)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				Product entity = await context.Products
					.Include(p => p.Market)
					.Include(p => p.ProductInstance)
						.ThenInclude(pi => pi.Category)
					.FirstOrDefaultAsync((p) => p.Id == id);
				return entity;
			}
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<Product> entities = await context.Products
					.Include(p => p.Market)
					.Include(p => p.ProductInstance)
						.ThenInclude(pi => pi.Category).ToListAsync();
				return entities;
			}
		}

		public async Task<IEnumerable<Product>> GetPage(int offset)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<Product> entities = await context.Products
					.Include(p => p.Market)
					.Include(p => p.ProductInstance)
						.ThenInclude(pi => pi.Category)
					.Skip(offset * fetch)
					.Take(fetch)
				.ToListAsync();
					
				return entities;
			}
		}

		public async Task<IEnumerable<Product>> GetPageWithSearch(int offset, string search)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<Product> entities = await context.Products
					.Include(p => p.Market)
					.Include(p => p.ProductInstance)
						.ThenInclude(pi => pi.Category)
							.Where(p => p.Market.Name.Contains(search) ||
									p.ProductInstance.Name.Contains(search) ||
										p.ProductInstance.Category.Name.Contains(search))
							.Skip(offset * fetch)
							.Take(fetch)
								.ToListAsync();

				return entities;
			}
		}

		public async Task<int> GetLastPageNumber()
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return (await context.Products.CountAsync() - 1) / fetch;
			}
		}

		public async Task<int> GetLastPageNumberWithSearch(string search)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<Product> entities = await context.Products
					.Include(p => p.Market)
					.Include(p => p.ProductInstance)
						.ThenInclude(pi => pi.Category)
							.Where(p => p.Market.Name.Contains(search) ||
									p.ProductInstance.Name.Contains(search) ||
										p.ProductInstance.Category.Name.Contains(search))
								.ToListAsync();

				return (entities.Count() - 1) / fetch;
			}
		}

		public async Task<int> GetNewId()
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Products.CountAsync();
			}
		}
	}
}
