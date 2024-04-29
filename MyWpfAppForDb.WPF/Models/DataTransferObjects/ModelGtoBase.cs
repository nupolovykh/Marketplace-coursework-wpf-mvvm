using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
	public class ModelGtoBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
