using MyWpfAppForDb.WPF.Models.DataTransferObjects;
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

		Task<ObservableCollection<DeliveryPointDto>> GetDeliveryPoints(string? search = "");

		Task<ObservableCollection<EmployeeDto>> GetEmployees(string? search = "");
	}
}
