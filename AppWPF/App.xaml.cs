using AppWPF.CustomControls;
using AppWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var window = new WinARP();
            window.Grid.Children.Clear();
			window.Grid.Children.Add(new Cart());
			window.Grid.Children.Add(new Header());

			this.MainWindow = window;
            window.Show();
        }
    }
}
