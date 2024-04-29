using MyWpfAppForDb.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.ViewModels.Factories
{
	public interface IAppViewModelFactory
	{
		ViewModelBase CreateViewModel(ViewType viewType);
	}
}
