using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyWpfAppForDb.EntityFramework.Entities;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services
{
	public class NonQueryDataService<T> where T : EntityInstance
	{
		private readonly AppDbContextFactory _contextFactory;

		public NonQueryDataService(AppDbContextFactory contextfactory)
		{
			_contextFactory = contextfactory;
		}

		public async Task<T> Create(T entity)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				// Ids are ValueGeneratedNever across the model, so they are assigned
				// here - from the largest existing one, not from the row count, which
				// reuses an id as soon as a row in the middle has been deleted.
				entity.Id = (await context.Set<T>().MaxAsync(e => (int?)e.Id) ?? 0) + 1;

				EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
				await context.SaveChangesAsync();

				return createdResult.Entity;
			}
		}

		public async Task<T> Update(int id, T entity)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				entity.Id = id;

				context.Set<T>().Update(entity);
				await context.SaveChangesAsync();

				return entity;
			}
		}

		public async Task<bool> Delete(int id)
		{
			using (AppDbContext context = _contextFactory.CreateDbContext())
			{
				T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
				context.Set<T>().Remove(entity);
				await context.SaveChangesAsync();

				return true;
			}
		}
	}
}
