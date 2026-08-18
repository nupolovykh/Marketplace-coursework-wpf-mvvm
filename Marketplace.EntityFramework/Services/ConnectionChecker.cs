using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.EntityFramework;
using System;

namespace Marketplace.EntityFramework.Services
{
	public static class ConnectionChecker
	{
		public static Exception DatabaseValidation(IHost host, out Exception exception)
		{
			try
			{
				var factory = host.Services.GetRequiredService<AppDbContextFactory>();

				using (var db = factory.CreateDbContext())
				{
					db.Database.EnsureCreated();
					if (!db.Database.CanConnect()) throw new Exception("The database was created but cannot be connected to.");
				}
			}

			catch (Exception ex)
			{
				exception = ex;
				return ex;
			}

			exception = null!;
			return null!;
		}
	}
}
