using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.State.Authenticators;
using MyWpfAppForDb.WPF.State.Navigators;
using MyWpfAppForDb.WPF.ViewModels;
using MyWpfAppForDb.WPF.ViewModels.Factories;
using System;

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
                services.AddSingleton<CreateViewModel<AuthorizationVM>>(services => () => CreateAuthorizationVM(services));
                services.AddSingleton<CreateViewModel<ProductsVM>>(services => () => services.GetRequiredService<ProductsVM>());
                services.AddSingleton<CreateViewModel<ProfileVM>>(services => () => services.GetRequiredService<ProfileVM>());
                services.AddSingleton<CreateViewModel<RegistrationVM>>(services => () => services.GetRequiredService<RegistrationVM>());
                services.AddSingleton<CreateViewModel<StatisticsVM>>(services => () => services.GetRequiredService<StatisticsVM>());
                services.AddSingleton<CreateViewModel<YourDeliveryInfoVM>>(services => () => services.GetRequiredService<YourDeliveryInfoVM>());

                services.AddSingleton<IAppViewModelFactory, AppViewModelFactory>();

                services.AddSingleton<ViewModelDelegateRenavigator<HomeVM>>();
                services.AddSingleton<ViewModelDelegateRenavigator<AuthorizationVM>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegistrationVM>>();
            });

            return host;
        }


        private static AuthorizationVM CreateAuthorizationVM(IServiceProvider services)
        {
            return new AuthorizationVM(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<RegistrationVM>>());
        }

        //private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        //{
        //    return new RegisterViewModel(
        //        services.GetRequiredService<IAuthenticator>(),
        //        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
        //        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
        //}
    }
}
