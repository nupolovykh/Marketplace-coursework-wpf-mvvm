using MyWpfAppForDb.WPF.Models.DataTransferObjects;

namespace MyWpfAppForDb.WPF.Models
{
	public class ProfileModel
	{
		public EmployeeDto? CurrentEmployee { get; set; }
		public string? Password1 { get; set; } = string.Empty;
		public string? Password2 { get; set; } = string.Empty;
	}
}
