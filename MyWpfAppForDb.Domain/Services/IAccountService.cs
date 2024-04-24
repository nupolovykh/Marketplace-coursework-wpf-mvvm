using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services
{
    public interface IAccountService : IDataService<Employee>
    {
        Task<Employee> GetByUsername(string username);

        Task<Employee> GetByEmail(string username);
    }
}
