using System;
using System.Windows;
using MyWpfAppForDb.EntityFramework;
using MyWpfAppForDb.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.WPF.HostBuilders;

namespace MyWpfAppForDb.WPF
{
    public partial class App : Application
    {
        private readonly ViewModelStore _viewModelStore;

        private readonly IServiceProvider? _serviceProvider;
        private readonly IHost _host;

        public App()
        {
            _viewModelStore = new ViewModelStore();
            _viewModelStore.CurrentViewModel = new AuthorizationVM(_viewModelStore);

            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null!)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            if (DatabaseInspector.DatabaseValidation(_host, out var result) is not null)
            {
                MessageBox.Show(result.Message);
            }

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }
    }

    public static class DatabaseInspector
    {
        public static Exception DatabaseValidation(IHost host, out Exception exeption)
        {
            try
            {
                var db = host.Services.GetService<MarketPlaceContext>()!;
                if (!db.Database.CanConnect())
                {
                    throw new Exception("Have no connection");
                }
            }

            catch (Exception ex)
            {
                exeption = ex;
                return ex;
            }

            exeption = null!;
            return null!;
        }
    }
}
