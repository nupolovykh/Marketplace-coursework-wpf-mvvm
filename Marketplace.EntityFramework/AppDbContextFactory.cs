using Microsoft.EntityFrameworkCore;
using System;

namespace Marketplace.EntityFramework
{
	public class AppDbContextFactory
	{
		private readonly Action<DbContextOptionsBuilder> _config;

		public AppDbContextFactory(Action<DbContextOptionsBuilder> config) 
		{
			_config = config;
		}

		public AppDbContext CreateDbContext()
		{
			DbContextOptionsBuilder<AppDbContext> options = new();

			_config(options);

			return new AppDbContext(options.Options);
		}
	}
}
