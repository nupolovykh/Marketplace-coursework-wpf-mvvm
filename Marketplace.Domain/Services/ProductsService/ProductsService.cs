using Marketplace.EntityFramework.Entities;
using Marketplace.EntityFramework.Services;
using Marketplace.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Marketplace.Domain.Exceptions;

namespace Marketplace.Domain.Services.ProductsService
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
				DateTime recentThreshold = DateTime.UtcNow.AddMonths(-2);

				// "Is this product part of a recent order?" used to be answered by
				// pulling every product and every recent order into memory and
				// building the cross product of the two. One EXISTS query answers it
				// - and answers it about *this* product, which the old code did not:
				// it threw whenever any product at all had a recent order.
				bool isInRecentOrder = await context.OrdersItems
					.AnyAsync(oi => oi.ProductId == id && oi.Order.OrderDate >= recentThreshold);

				if (isInRecentOrder) throw new IsNotAvailableException(id);
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
				return await WithDetails(context).ToListAsync();
			}
		}

		// Every catalogue query needs the same three navigations loaded and the same
		// search predicate; they were spelled out at each call site.
		private static IQueryable<Product> WithDetails(AppDbContext context)
			=> context.Products
				.Include(p => p.Market)
				.Include(p => p.ProductInstance)
					.ThenInclude(pi => pi.Category);

		private static IQueryable<Product> MatchingSearch(IQueryable<Product> products, string search)
			=> products.Where(p => p.Market.Name.Contains(search)
				|| p.ProductInstance.Name.Contains(search)
				|| p.ProductInstance.Category.Name.Contains(search));

		public async Task<IEnumerable<Product>> GetPage(int offset)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return await WithDetails(context)
					.Skip(offset * fetch)
					.Take(fetch)
					.ToListAsync();
			}
		}

		public async Task<IEnumerable<Product>> GetPageWithSearch(int offset, string search)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return await MatchingSearch(WithDetails(context), search)
					.Skip(offset * fetch)
					.Take(fetch)
					.ToListAsync();
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
				// Counting the matches used to mean materialising all of them, with
				// their joined market/category rows, and throwing the rows away.
				int matches = await MatchingSearch(context.Products, search).CountAsync();

				return (matches - 1) / fetch;
			}
		}

		public async Task<int> GetNewId()
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				// Product ids are ValueGeneratedNever, so the next id has to come from
				// the largest existing one. Counting rows instead collides with an
				// existing id as soon as anything has been deleted.
				return (await context.Products.MaxAsync(p => (int?)p.Id) ?? 0) + 1;
			}
		}
	}
}
