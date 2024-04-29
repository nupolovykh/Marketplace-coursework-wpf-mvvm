using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.ViewModels
{
	public class MessageViewModel : ViewModelBase
	{
		public string _message;

		public string Message
		{
			get => _message;
			set
			{
				_message = value;
				OnPropertyChanged(nameof(Message));
				OnPropertyChanged(nameof(HasMessage));
			}
		}

		public bool HasMessage => !string.IsNullOrEmpty(Message);

	}
}
