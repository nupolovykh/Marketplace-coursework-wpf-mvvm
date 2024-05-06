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
		private readonly IDeliveryServiceOrder _deliveryServiceOrder;
		private readonly IDeliveryServiceEmployee _deliveryServiceEmployee;
		private readonly IDeliveryPointService _deliveryPointService;
		private readonly IMapper _mapper;

		public DeliveryWorker(IDeliveryServiceOrder deliveryServiceOrder, IDeliveryServiceEmployee deliveryServiceEmployee,
			IDeliveryPointService deliveryPointService, IMapper mapper) 
		{
			_deliveryServiceOrder = deliveryServiceOrder;
			_deliveryServiceEmployee = deliveryServiceEmployee;
			_deliveryPointService = deliveryPointService;
			_mapper = mapper;
		}

		public async Task<ObservableCollection<OrderDto>> GetOrders(EmployeeDto current)
		{
			var orders = await _deliveryServiceOrder.HardGetWithSearch(current.DeliveryPointId, WhereCondition.None, "");
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByDelivery(EmployeeDto current, string search)
		{
			var orders = await _deliveryServiceOrder.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByDeliveryPoint, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByProduct(EmployeeDto current, string search)
		{
			var orders = await _deliveryServiceOrder.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByProduct, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<OrderDto>> GetOrdersByEmployee(EmployeeDto current, string search)
		{
			var orders = await _deliveryServiceOrder.HardGetWithSearch(current.DeliveryPointId, WhereCondition.ByEmployee, search);
			return _mapper.Map<ObservableCollection<OrderDto>>(orders);
		}

		public async Task<ObservableCollection<DeliveryPointDto>> GetDeliveryPoints(string? search = "")
		{
			var orders = await _deliveryPointService.GetWithSearch(search);
			return _mapper.Map<ObservableCollection<DeliveryPointDto>>(orders);
		}

		public async Task<ObservableCollection<EmployeeDto>> GetEmployees(string? search = "")
		{
			var orders = await _deliveryServiceEmployee.GetWithSearch(search);
			return _mapper.Map<ObservableCollection<EmployeeDto>>(orders);
		}
	}
}
