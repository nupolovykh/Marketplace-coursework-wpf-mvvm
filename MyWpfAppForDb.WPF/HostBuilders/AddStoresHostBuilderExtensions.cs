using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.State.Navigators;

namespace MyWpfAppForDb.WPF.HostBuilders
{
    internal static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator,Navigator>();
            });

            return host;
        }

    }
}
