using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using System;

namespace MyWpfAppForDb.EntityFramework.Services
{
	public static class ConnectionChecker
	{
		public static Exception DatabaseValidation(IHost host, out Exception exeption)
		{
			try
			{
				var factory = host.Services.GetRequiredService<AppDbContextFactory>();

				using (var db = factory.CreateDbContext())
				{
					db.Database.EnsureCreated();
					if (!db.Database.CanConnect()) throw new Exception("Never gonna give you up");
				}
			}

			catch (Exception ex)
			{
				exeption = ex;
				return ex;
			}

			exeption = null!;
			return null!;
		}
	}
}
