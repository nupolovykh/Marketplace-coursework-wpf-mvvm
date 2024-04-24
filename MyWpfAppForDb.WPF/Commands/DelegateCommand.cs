using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWpfAppForDb.WPF.Commands
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> ExecuteAction;
        private readonly Func<object, bool> CanExecuteFunc;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            ExecuteAction = executeAction;
            CanExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
