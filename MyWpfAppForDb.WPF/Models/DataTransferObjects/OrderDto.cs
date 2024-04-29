using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
	public partial class OrderDto : ModelGtoBase
	{
		public OrderDto()
		{
			OrdersItems = new HashSet<OrdersItemDto>();
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

		private int? _deliveryPointId;

		public int? DeliveryPointId
		{
			get => _deliveryPointId;
			set
			{
				_deliveryPointId = value;
				OnPropertyChanged();
			}
		}

		private int? _clientId;

		public int? ClientId
		{
			get => _clientId;
			set
			{
				_clientId = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _orderDate;

		public DateTime? OrderDate
		{
			get => _orderDate;
			set
			{
				_orderDate = value;
				OnPropertyChanged();
			}
		}

		private string _status;

		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				OnPropertyChanged();
			}
		}

		private decimal? _totalAmount;

		public decimal? TotalAmount
		{
			get => _totalAmount;
			set
			{
				_totalAmount = value;
				OnPropertyChanged();
			}
		}

		public virtual ClientDto Client { get; set; }
		public virtual DeliveryPointDto DeliveryPoint { get; set; }
		public virtual ICollection<OrdersItemDto> OrdersItems { get; set; }
	}
}
