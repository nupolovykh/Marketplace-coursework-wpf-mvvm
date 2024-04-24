using System;
using System.Windows;
using MyWpfAppForDb.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.HostBuilders;
using MyWpfAppForDb.EntityFramework.Services;

namespace MyWpfAppForDb.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null!)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddStores()
                .AddServices()
                .AddViewModels()
                .AddMapping()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            if (ConnectionChecker.DatabaseValidation(_host, out var result) is not null)
            {
                MessageBox.Show(result.Message);
            }

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }
    }
}
