using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Marketplace.EntityFramework.Services
{
	/// <summary>
	/// Drops the database so the next EnsureCreated call rebuilds it from the seed
	/// data. Development convenience only - it destroys everything the user has
	/// entered, so it is off unless Database:RecreateOnStartup says otherwise.
	/// </summary>
	public static class RecreatorDatabase
	{
		public static async Task RecreateDatabase(IHost host, bool condition)
		{
			if (!condition) return;

			var factory = host.Services.GetRequiredService<AppDbContextFactory>();

			using (var db = factory.CreateDbContext())
			{
				await db.Database.EnsureDeletedAsync();
			}
		}
	}
}
