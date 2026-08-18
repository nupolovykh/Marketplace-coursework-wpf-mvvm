using Marketplace.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Marketplace.Wpf.Models.DataTransferObjects
{
	public partial class ClientDto : ModelDtoBase
	{
		public ClientDto()
		{
			Orders = new HashSet<OrderDto>();
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

		private string _email;

		public string Email
		{
			get => _email;
			set
			{
				_email = value;
				OnPropertyChanged();
			}
		}

		private string _phone;

		public string Phone
		{
			get => _phone;
			set
			{
				_phone = value;
				OnPropertyChanged();
			}
		}

		public virtual ICollection<OrderDto> Orders { get; set; }
	}
}
