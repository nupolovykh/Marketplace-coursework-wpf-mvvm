using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Domain.Services.AccountService;
using Marketplace.Domain.Services.DeliveryService;
using Marketplace.Domain.Services.ProductsService;
using Marketplace.EntityFramework.Entities;
using Marketplace.EntityFramework.Services;
using Marketplace.EntityFramework.Services.AuthenticationServices;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddServicesHostBuilderExtensions
	{
		public static IHostBuilder AddServices(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<IAuthenticationService, AuthenticationService>();
				services.AddSingleton<IDataService<Employee>, AccountDataService>();
				services.AddSingleton<IAccountService, AccountDataService>();
				services.AddSingleton<IProductsService, ProductsService>();
				services.AddSingleton<IDeliveryServiceOrder, DeliveryServiceOrder>();
				services.AddSingleton<IDeliveryPointService, DeliveryPointsService>();
				services.AddSingleton<IDeliveryServiceEmployee, DeliveryServiceEmployee>();
			});

			return host;
		}
	}
}
