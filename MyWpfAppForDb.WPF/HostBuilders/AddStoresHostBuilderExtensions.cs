using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.EntityFramework.Services;
using MyWpfAppForDb.WPF.State.Accounts;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.State.Products;

namespace MyWpfAppForDb.WPF.HostBuilders
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
			});

			return host;
		}

	}
}
