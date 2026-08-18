using Marketplace.Wpf.Models.DataTransferObjects;
using System;

namespace Marketplace.Wpf.State.Accounts
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
