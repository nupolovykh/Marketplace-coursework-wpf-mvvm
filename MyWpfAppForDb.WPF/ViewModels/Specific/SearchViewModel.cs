using System.Windows.Input;

namespace MyWpfAppForDb.WPF.ViewModels.Specific
{
	public class SearchViewModel : ViewModelBase
	{
		private string? _search;

		public string Search
		{
			get => _search!;
			set
			{
				_search = value;
				OnPropertyChanged(nameof(Search));
				OnPropertyChanged(nameof(HasSearchString));
			}
		}

		public bool HasSearchString => !string.IsNullOrEmpty(Search);

		public ICommand? SearchCommand { get; set; }

	}
}
