using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MyWpfAppForDb.Domain.Services.DeliveryService
{
	public class DeliveryServiceOrder : IDeliveryServiceOrder
	{
		private readonly AppDbContextFactory _dbContextFactory;
		private readonly NonQueryDataService<Order> _nonQueryDataService;

		public DeliveryServiceOrder(AppDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
			_nonQueryDataService = new NonQueryDataService<Order>(_dbContextFactory);
		}

		public async Task<Order> Create(Order entity)
		{
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<Order> Update(int id, Order entity)
		{
			return await _nonQueryDataService.Update(id, entity);
		}

		public async Task<bool> Delete(int id)
		{
			return await _nonQueryDataService.Delete(id);
		}

		public async Task<Order> Get(int id)
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				Order entity = await context.Orders
					.Include(o => o.OrdersItems)
					.Include(o => o.Client)
						.FirstOrDefaultAsync((d) => d.Id == id);
				return entity;
			}
		}

		public async Task<IEnumerable<Order>> GetAll()
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<Order> entities = await context.Orders.ToListAsync();
				return entities;
			}
		}

		public async Task<IEnumerable<Order>> GetWithIncludes()
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<Order> entities = await context.Orders
					.Include(o => o.OrdersItems)
					.Include(o => o.Client)
						.ToListAsync();

				return entities;
			}
		}

		//impossible quaries for normal data including => ohhhhh yeah

		public async Task<IEnumerable<Order>> HardGetWithSearch(int id, WhereCondition condition = WhereCondition.None, string search = "")
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				var entities = await context.Orders.Where(o => o.DeliveryPointId == id)
					.Include(o => o.DeliveryPoint)
						.ThenInclude(d => d.Employees)
					.Include(o => o.OrdersItems)
						.ThenInclude(oi => oi.Product)
							.ThenInclude(p => p.ProductInstance)
								.ThenInclude(p => p.Category)
					.Include(o => o.OrdersItems)
						.ThenInclude(oi => oi.Product)
							.ThenInclude(p => p.Market)
					.Include(o => o.Client)
						.ToListAsync();

				if(condition == WhereCondition.None && search == string.Empty) return entities;

				switch (condition)
				{
					case WhereCondition.ByDeliveryPoint:
						return entities.Where(e => e.DeliveryPoint.Address.Contains(search) || e.DeliveryPoint.City.Contains(search));

					case WhereCondition.ByProduct:

						return entities.Where(e => e.OrdersItems
							.Any(oi => oi.Product != null && (oi.Product.ProductInstance.Name.Contains(search) || oi.Product.Market.Name.Contains(search))))
							.Select(e => new Order
							{
								Id = e.Id,
								DeliveryPoint = e.DeliveryPoint,
								OrderDate = e.OrderDate,
								Status = e.Status,
								TotalAmount = e.TotalAmount,
								Client = e.Client,
								ClientId = e.ClientId,
								DeliveryPointId = e.DeliveryPointId,
								OrdersItems = e.OrdersItems
									.Where(oi => oi.Product != null && (oi.Product.ProductInstance.Name.Contains(search) || oi.Product.Market.Name.Contains(search)))
									.ToList()
							});

					case WhereCondition.ByEmployee:

						return entities.Where(e => e.DeliveryPoint.Employees
							 .Any(emp => emp.Name.Contains(search) || emp.Email.Contains(search)))
							 .Select(e => new
							 {
								 Order = e,
								 FilteredEmployees = e.DeliveryPoint.Employees.Where(emp => emp.Name.Contains(search) || emp.Email.Contains(search))
							 }).AsEnumerable()
								.Select(x =>
								{
									var order = x.Order;
									order.DeliveryPoint.Employees = x.FilteredEmployees.ToList();
									return order;
								});

					default:
						return entities;

				}
			}
		}
	}
}
