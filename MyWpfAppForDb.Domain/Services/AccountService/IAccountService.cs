using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Services.AccountService
{
	public interface IAccountService : IDataService<Employee>
	{
		Task<Employee> GetByUsername(string username);

		Task<Employee> GetByEmail(string username);
	}
}
