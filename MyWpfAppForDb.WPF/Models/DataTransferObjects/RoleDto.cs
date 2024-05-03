using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
	public partial class RoleDto : ModelGtoBase
	{
		public RoleDto()
		{
			Employees = new HashSet<EmployeeDto>();
		}

		private int _id;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged();
			}
		}

		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public virtual ICollection<EmployeeDto> Employees { get; set; }
	}
}
