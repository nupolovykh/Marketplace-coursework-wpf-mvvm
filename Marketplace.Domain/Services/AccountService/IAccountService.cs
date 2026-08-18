using Marketplace.EntityFramework.Entities;
using Marketplace.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Services.AccountService
{
	public interface IAccountService : IDataService<Employee>
	{
		Task<Employee> GetByUsername(string username);

		Task<Employee> GetByEmail(string username);
	}
}
