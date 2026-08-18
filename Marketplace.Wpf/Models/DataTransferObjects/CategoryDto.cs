using Marketplace.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Marketplace.Wpf.Models.DataTransferObjects
{
	public partial class CategoryDto : ModelDtoBase
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
