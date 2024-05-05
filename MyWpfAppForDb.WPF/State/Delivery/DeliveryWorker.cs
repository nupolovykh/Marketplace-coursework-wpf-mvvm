using AutoMapper;
using MyWpfAppForDb.Domain.Services.DeliveryService;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Delivery
{
	internal class DeliveryWorker : IDeliveryWorker
	{
		private readonly IDeliveryService _deliveryService;
		private readonly IMapper _mapper;

		public DeliveryWorker(IDeliveryService deliveryService, IMapper mapper) 
		{
			_deliveryService = deliveryService;
			_mapper = mapper;
		}

		public async Task<ObservableCollection<OrderDto>> GetOrders(EmployeeDto current)
		{
			var orders = await _deliveryService.HardGetWithSearch(current.DeliveryPointId, WhereCondition.None, "");
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByDelivery(EmployeeDto current, string search)
		{
			var orders = await _deliveryService.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByDeliveryPoint, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByProduct(EmployeeDto current, string search)
		{
			var orders = await _deliveryService.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByProduct, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByEmployee(EmployeeDto current, string search)
		{
			var orders = await _deliveryService.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByEmployee, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}
	}
}
