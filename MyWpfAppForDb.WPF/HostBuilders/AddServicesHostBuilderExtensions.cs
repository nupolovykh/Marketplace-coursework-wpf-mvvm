using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.Domain.Services.AccountService;
using MyWpfAppForDb.Domain.Services.ProductsService;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using System.Security.Principal;
namespace MyWpfAppForDb.WPF.HostBuilders
{
	internal static class AddServicesHostBuilderExtensions
	{
		public static IHostBuilder AddServices(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddSingleton<IPasswordHasher, PasswordHasher>();
				services.AddSingleton<IAuthenticationService, AuthenticationService>();
				services.AddSingleton<IDataService<Employee>, AccountDataService>();
				services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IProductsService, ProductsService>();
            });

			return host;
		}
	}
}
