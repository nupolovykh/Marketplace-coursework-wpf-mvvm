using MyWpfAppForDb.Domain.Services.DeliveryService;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Delivery
{
	public interface IDeliveryWorker
	{
		Task<ObservableCollection<OrderDto>> GetOrders(EmployeeDto current);

		Task<ObservableCollection<OrderDto>> GetOrdersByDelivery(EmployeeDto current, string search);

		Task<ObservableCollection<OrderDto>> GetOrdersByProduct(EmployeeDto current, string search);

		Task<ObservableCollection<OrderDto>> GetOrdersByEmployee(EmployeeDto current, string search);
	}
}
