using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Marketplace.Wpf.Models.DataTransferObjects
{
	public class ModelDtoBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
