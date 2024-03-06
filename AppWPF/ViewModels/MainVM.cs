using AppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.ViewModels
{
    public class MainVM : ViewModelBase
    {
		private MainModel _mainModel;

		public ICommand Next { get; set; }
		public ICommand Prev { get; set; }

		public MainVM()
        {
            _mainModel = new MainModel();
        }
    }
}
