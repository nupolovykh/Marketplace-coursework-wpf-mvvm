using Marketplace.Wpf.Models.DataTransferObjects;

namespace Marketplace.Wpf.Models
{
	public class ProfileModel
	{
		public EmployeeDto? CurrentEmployee { get; set; }
		public string? Password1 { get; set; } = string.Empty;
		public string? Password2 { get; set; } = string.Empty;
	}
}
