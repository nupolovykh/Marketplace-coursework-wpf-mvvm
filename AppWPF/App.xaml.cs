using AppWPF.ViewModels;
using AppWPF.ViewModels.BaseClasses;
using AppWPF.ViewModels.Pages;
using System.Windows;

namespace AppWPF
{
    public partial class App : Application
    {
		private readonly ViewModelStore _viewModelStore;

		public App()
		{
			_viewModelStore = new ViewModelStore();
			_viewModelStore.CurrentViewModel = new MainVM();
		}

		protected override void OnStartup(StartupEventArgs e)
        {
			MainWindow = new WinMain()
			{
				DataContext = new GenericViewModel(_viewModelStore)
			};
			MainWindow.Show();

			base.OnStartup(e);
		}
    }
}
