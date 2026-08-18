using Marketplace.Wpf.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Wpf.ViewModels.Factories
{
	public interface IAppViewModelFactory
	{
		ViewModelBase CreateViewModel(ViewType viewType);
	}
}
