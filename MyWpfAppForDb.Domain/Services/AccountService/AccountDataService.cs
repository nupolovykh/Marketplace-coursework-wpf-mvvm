using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Services.AccountService
{
	public class AccountDataService : IAccountService
	{
		private readonly AppDbContextFactory _contextFactory;
		private readonly NonQueryDataService<Employee> _nonQueryDataService;

		public AccountDataService(AppDbContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
			_nonQueryDataService = new NonQueryDataService<Employee>(contextFactory);
		}

		public async Task<Employee> Create(Employee entity)
		{
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<bool> Delete(int id)
		{
			return await _nonQueryDataService.Delete(id);
		}

		public async Task<Employee> Get(int id)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				Employee entity = await context.Employees
					.Include(e => e.DeliveryPoint)
					.FirstOrDefaultAsync((e) => e.Id == id);
				return entity;
			}
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<Employee> entities = await context.Employees
					.Include(e => e.DeliveryPoint).ToListAsync();
				return entities;
			}
		}

		public async Task<Employee> GetByEmail(string email)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Employees
					.Include(e => e.DeliveryPoint)
					.FirstOrDefaultAsync(e => e.Email == email);
			}
		}

		public async Task<Employee> GetByUsername(string username)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Employees
					.Include(e => e.DeliveryPoint)
					.FirstOrDefaultAsync(a => a.Name == username);
			}
		}

		public async Task<Employee> Update(int id, Employee entity)
		{
			return await _nonQueryDataService.Update(id, entity);
		}
	}
}
