using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
	public partial class CategoryDto : ModelGtoBase
	{
		public CategoryDto()
		{
			ProductsInstances = new HashSet<ProductsInstanceDto>();
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

		public virtual ICollection<ProductsInstanceDto> ProductsInstances { get; set; }
	}
}
