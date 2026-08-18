using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Wpf.ViewModels;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddViewsHostBuilderExtensions
	{
		public static IHostBuilder AddViews(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
			});

			return host;
		}
	}
}
