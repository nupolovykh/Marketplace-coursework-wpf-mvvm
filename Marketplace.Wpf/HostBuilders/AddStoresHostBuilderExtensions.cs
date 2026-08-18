using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Wpf.State.Accounts;
using Marketplace.Wpf.State.Authenticators;
using Marketplace.Wpf.State.Delivery;
using Marketplace.Wpf.State.Navigators;
using Marketplace.Wpf.State.Products;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddStoresHostBuilderExtensions
	{
		public static IHostBuilder AddStores(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<INavigator,Navigator>();
				services.AddSingleton<IAuthenticator, Authenticator>();
				services.AddSingleton<IAccountStore, AccountStore>();
				services.AddSingleton<IProductWorker, ProductWorker>();
				services.AddSingleton<IDeliveryWorker, DeliveryWorker>();
			});

			return host;
		}

	}
}
