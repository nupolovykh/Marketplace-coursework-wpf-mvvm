using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using MyWpfAppForDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MyWpfAppForDb.Domain.Services.DeliveryService
{

	public class DeliveryServiceEmployee : IDeliveryServiceEmployee
	{
		private readonly AppDbContextFactory _dbContextFactory;
		private readonly NonQueryDataService<Employee> _nonQueryDataService;

		public DeliveryServiceEmployee(AppDbContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
			_nonQueryDataService = new NonQueryDataService<Employee>(_dbContextFactory);
		}

		public async Task<Employee> Create(Employee entity)
		{
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<Employee> Update(int id, Employee entity)
		{
			return await _nonQueryDataService.Update(id, entity);
		}

		public async Task<bool> Delete(int id)
		{
			return await _nonQueryDataService.Delete(id);
		}

		public async Task<Employee> Get(int id)
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				Employee entity = await context.Employees
					.Include(e => e.DeliveryPoint)
					.Include(e => e.Role)
					.FirstOrDefaultAsync((e) => e.Id == id);
				return entity;
			}
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<Employee> entities = await context.Employees
					.Include(e => e.DeliveryPoint)
					.Include(e => e.Role)
					.ToListAsync();
				return entities;
			}
		}

		public async Task<IEnumerable<Employee>> GetWithSearch(string search = "")
		{
			using (AppDbContext context = _dbContextFactory.CreateDbContext())
			{
				IEnumerable<Employee> entities = await context.Employees
					.Include(e => e.DeliveryPoint).Where(d => d.DeliveryPoint.City.Contains(search) || d.DeliveryPoint.Address.Contains(search))
					.Include(e => e.Role).Where(r => r.Role.Name.Contains(search))
						.ToListAsync();

				return entities;
			}
		}

	}
}
