using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Services.DeliveryService
{
	public enum WhereCondition
	{
		None,
		ByDeliveryPoint,
		ByProduct,
		ByEmployee
	}

	public interface IDeliveryService : IDataService<Order>
	{
		Task<IEnumerable<Order>> HardGetWithSearch(int id, WhereCondition condition, string search = "");
	}
}
