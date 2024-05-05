using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;

namespace MyWpfAppForDb.WPF.State.Accounts
{
	public interface IAccountStore
	{
		EmployeeDto CurrentEmployee { get; set; }

		event Action StateChanged;

		bool IsAdmin();

		bool IsOperator();

		bool IsLoader();
	}
}
