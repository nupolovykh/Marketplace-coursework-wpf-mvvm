using System;
using System.Windows.Input;

namespace AppWPF.ViewModels.BaseClasses
{
	public abstract class CommandBase : ICommand
	{
		public event EventHandler CanExecuteChanged;
		public virtual bool CanExecute(object parameter) => true;
		public abstract void Execute(object parameter);

		protected void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}
	}

	internal class DefaultCommand : CommandBase
	{
		public DefaultCommand() { }
		public override void Execute(object parameter) { }
	}

}
