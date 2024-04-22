using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyWpfAppForDb.WPF.HostBuilders
{
    internal static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(c =>
            {
                //dbservices
            });

            return host;
        }
    }
}
