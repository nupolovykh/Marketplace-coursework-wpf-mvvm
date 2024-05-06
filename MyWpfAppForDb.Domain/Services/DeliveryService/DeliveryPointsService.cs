using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using MyWpfAppForDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyWpfAppForDb.Domain.Services.DeliveryService
{
	public class DeliveryPointsService : IDeliveryPointService
	{
		private readonly AppDbContextFactory _dbContextFactory;
		private readonly NonQueryDataService<DeliveryPoint> _nonQueryDataService;

		public DeliveryPointsService(AppDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
			_nonQueryDataService = new NonQueryDataService<DeliveryPoint>(_dbContextFactory);
		}

		public async Task<DeliveryPoint> Create(DeliveryPoint entity)
		{
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<DeliveryPoint> Update(int id, DeliveryPoint entity)
		{
			return await _nonQueryDataService.Update(id, entity);
		}

		public async Task<bool> Delete(int id)
		{
			return await _nonQueryDataService.Delete(id);
		}

		public async Task<DeliveryPoint> Get(int id)
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				DeliveryPoint entity = await context.DeliveryPoints
					.Include(d => d.Orders)
					.Include(d => d.Employees)
						.FirstOrDefaultAsync((e) => e.Id == id);
				return entity;
			}
		}

		public async Task<IEnumerable<DeliveryPoint>> GetAll()
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<DeliveryPoint> entities = await context.DeliveryPoints
					.Include(d => d.Orders)
					.Include(d => d.Employees)
						.ToListAsync();
				return entities;
			}
		}

		public async Task<IEnumerable<DeliveryPoint>> GetWithSearch(string search = "")
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<DeliveryPoint> entities = await context.DeliveryPoints
					.Include(d => d.Employees).Where(d => d.Employees.Any(e => e.Name.Contains(search) || e.Email.Contains(search)))
					.Include(d => d.Orders)
						.ToListAsync();

				return entities;
			}
		}
	}


}
