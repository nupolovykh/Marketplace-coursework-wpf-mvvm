using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using System;

namespace MyWpfAppForDb.WPF.HostBuilders
{
	internal static class AddDbContextHostBuilderExtensions
	{
		public static IHostBuilder AddDbContext(this IHostBuilder host)
		{
			host.ConfigureServices((context, services) =>
			{
				string connectionString = context.Configuration.GetConnectionString("sqlite");
				Action<DbContextOptionsBuilder> configureDbContext = (o) => o.UseSqlite(connectionString);

				services.AddDbContext<AppDbContext>(configureDbContext);
				services.AddSingleton(new AppDbContextFactory(configureDbContext));
			});

			return host;
		}
	}
}
