using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddConfigurationHostBuilderExtensions
	{
		public static IHostBuilder AddConfiguration(this IHostBuilder host)
		{
			host.ConfigureAppConfiguration(c =>
			{
				c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
				c.AddEnvironmentVariables();
			});

			return host;
		}
	}
}
