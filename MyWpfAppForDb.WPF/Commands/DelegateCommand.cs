using MyWpfAppForDb.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWpfAppForDb.WPF.Commands
{
	internal class DelegateCommand : AsyncCommandBase
	{
		private readonly Action<object> _action;
		private readonly Predicate<object> _canExecuteFunc;
		private readonly ViewModelBase _viewModel;

		public DelegateCommand(Action<object> action, Predicate<object> condition, ViewModelBase vmb)
		{
			_action = action;
			_canExecuteFunc = condition;
			_viewModel = vmb;

			_viewModel.PropertyChanged += Reload_PropertyChanged!;
		}

		public override bool CanExecute(object? parameter)
		{
			bool v = (_canExecuteFunc == null || _canExecuteFunc(parameter!)) && base.CanExecute(parameter);
			return v;
		}

		public override async Task ExecuteAsync(object? parameter)
		{
			_action(parameter!);
		}

		public void Reload_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnCanExecuteChanged();
		}
	}
}
