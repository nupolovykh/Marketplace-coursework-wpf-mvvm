using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.ViewModels;

namespace MyWpfAppForDb.WPF.HostBuilders
{
    internal static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
                {
                    services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                }
            );
            return host;
        }
    }
}
