using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddConfigurationHostBuilderExtensions
	{
		public static IHostBuilder AddConfiguration(this IHostBuilder host)
		{
			host.ConfigureAppConfiguration(c =>
			{
				// Without an explicit base path the file is looked for in the current
				// working directory, so "dotnet run --project Marketplace.Wpf" from the
				// repository root failed to start. appsettings.json is copied next to
				// the assembly, which is where it is read from.
				c.SetBasePath(AppContext.BaseDirectory);
				c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
				c.AddEnvironmentVariables();
			});

			return host;
		}
	}
}
