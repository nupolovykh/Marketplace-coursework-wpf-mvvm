using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Wpf.State.Authenticators;
using Marketplace.Wpf.State.Navigators;
using Marketplace.Wpf.ViewModels;
using Marketplace.Wpf.ViewModels.Factories;
using System;

namespace Marketplace.Wpf.HostBuilders
{
	internal static class AddViewModelsHostBuilderExtensions
	{
		public static IHostBuilder AddViewModels(this IHostBuilder host)
		{
			host.ConfigureServices(services =>
			{
				services.AddTransient<MainViewModel>();

				services.AddTransient<AuthorizationVM>();
				services.AddTransient<HomeVM>();
				services.AddTransient<ProfileVM>();
				services.AddTransient<RegistrationVM>();
				services.AddTransient<StatisticsVM>();
				services.AddTransient<YourDeliveryInfoVM>();

				services.AddSingleton<CreateViewModel<HomeVM>>(services => () => services.GetRequiredService<HomeVM>());
				services.AddSingleton<CreateViewModel<AuthorizationVM>>(services => () => CreateAuthorizationVM(services));
				services.AddSingleton<CreateViewModel<ProfileVM>>(services => () => services.GetRequiredService<ProfileVM>());
				services.AddSingleton<CreateViewModel<RegistrationVM>>(services => () => CreateRegistrationVM(services));
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
				services.GetRequiredService<ViewModelDelegateRenavigator<HomeVM>>());
		}

		private static RegistrationVM CreateRegistrationVM(IServiceProvider services)
		{
			return new RegistrationVM(
				services.GetRequiredService<IAuthenticator>(),
				services.GetRequiredService<ViewModelDelegateRenavigator<HomeVM>>());
		}
	}
}
