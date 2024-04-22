using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.ViewModels;
using MyWpfAppForDb.WPF.ViewModels.Factories;

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

                services.AddSingleton<CreateViewModel<HomeVM>>(services => () => services.GetRequiredService<HomeVM>());
                services.AddSingleton<CreateViewModel<AuthorizationVM>>(services => () => services.GetRequiredService<AuthorizationVM>());
                services.AddSingleton<CreateViewModel<ProductsVM>>(services => () => services.GetRequiredService<ProductsVM>());
                services.AddSingleton<CreateViewModel<ProfileVM>>(services => () => services.GetRequiredService<ProfileVM>());
                services.AddSingleton<CreateViewModel<RegistrationVM>>(services => () => services.GetRequiredService<RegistrationVM>());
                services.AddSingleton<CreateViewModel<StatisticsVM>>(services => () => services.GetRequiredService<StatisticsVM>());
                services.AddSingleton<CreateViewModel<YourDeliveryInfoVM>>(services => () => services.GetRequiredService<YourDeliveryInfoVM>());

                services.AddSingleton<IAppViewModelFactory, AppViewModelFactory>();
            });

            return host;
        }
    }
}
