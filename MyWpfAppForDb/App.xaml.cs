using AppWPF.Models.Database;
using AppWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MyWpfAppForDb.ViewModels;
using System;
using System.Windows;

namespace AppWPF
{
    public partial class App : Application
    {
        private readonly ViewModelStore _viewModelStore;
        private static IServiceProvider? _serviceProvider;

        public App()
        {
            _viewModelStore = new ViewModelStore();
            _viewModelStore.CurrentViewModel = new AuthorizationVM(_viewModelStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceCollection services = new ServiceCollection();
            services.ServicesInit();
            _serviceProvider = services.BuildServiceProvider();

            try {
                var db = _serviceProvider.GetService<MarketPlaceContext>()!;
                if (!db.Database.CanConnect()) throw new Exception("Have no connection");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MainWindow = new WinMain()
            {
                DataContext = new MainViewModel(_viewModelStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

    internal static class ServicesBuilder
    {
        public static void ServicesInit(this IServiceCollection services)
        {
            services.AddDbContext<MarketPlaceContext>();
        }
    }
}
