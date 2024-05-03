using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services
{
	public static class RecreatorDatabase
	{
		public static async Task RecreateDatabase(IHost host, bool condition)
		{
			if (!condition) return;

			try
			{
				var factory = host.Services.GetRequiredService<AppDbContextFactory>();

				using (var db = factory.CreateDbContext())
				{
					await db.Database.EnsureDeletedAsync();
				}
			}

			catch (Exception)
			{
				throw;
			}
		}
	}
}
