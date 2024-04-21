using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.HostBuilders
{
    internal static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
                {
                    services.AddTransient<MainViewModel>();
                    services.AddSingleton<ViewModelStore>();

                    services.AddTransient<AuthorizationVM>();
                    services.AddTransient<HomeVM>();
                    services.AddTransient<ProductsVM>();
                    services.AddTransient<ProfileVM>();
                    services.AddTransient<RegistrationVM>();
                    services.AddTransient<StatisticsVM>();
                    services.AddTransient<YourDeliveryInfoVM>();
                }
            );
            return host;
        }
    }
}
