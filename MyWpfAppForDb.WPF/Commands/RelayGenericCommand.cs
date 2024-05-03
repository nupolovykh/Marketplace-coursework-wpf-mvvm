using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWpfAppForDb.WPF.Commands
{
	internal class RelayGenericCommand<T> : AsyncCommandBase
	{
		private readonly Action<T> _action;
		private readonly Func<T, bool> _canExecuteFunc;

		public RelayGenericCommand(Action<T> action, Func<T, bool> condition)
		{
			_action = action;
			_canExecuteFunc = condition;
		}

		public override bool CanExecute(object? parameter)
		{
			if (parameter is T typedParameter)
			{
				return (_canExecuteFunc == null || _canExecuteFunc(typedParameter)) && base.CanExecute(parameter);
			}
			return base.CanExecute(parameter);
		}

		public override async Task ExecuteAsync(object? parameter)
		{
			if (parameter is T typedParameter)
			{
				_action(typedParameter);
			}
		}
	}
}
