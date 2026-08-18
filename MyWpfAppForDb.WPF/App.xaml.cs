using System.Windows;
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

			// Rebuilding from seed data on every launch used to be unconditional, so
			// nothing a user registered or edited survived a restart. It is a
			// development switch now, off by default.
			bool recreateDatabase = _host.Services.GetRequiredService<IConfiguration>()
				.GetValue<bool>("Database:RecreateOnStartup");

			RecreatorDatabase.RecreateDatabase(_host, recreateDatabase).Wait();
			if (ConnectionChecker.DatabaseValidation(_host, out var result) is not null) MessageBox.Show(result.Message);

			Window window = _host.Services.GetRequiredService<MainWindow>();
			window.Show();

			base.OnStartup(e);
		}
	}
}
