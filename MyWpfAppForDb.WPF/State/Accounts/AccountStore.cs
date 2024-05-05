using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using System;

namespace MyWpfAppForDb.WPF.State.Accounts
{
	internal class AccountStore : IAccountStore
	{
		private EmployeeDto? _currentEmployee;

		public EmployeeDto CurrentEmployee
		{
			get => _currentEmployee!;
			set 
			{
				_currentEmployee = value;
				StateChanged?.Invoke();
			}
		}
		public event Action? StateChanged;
	}
}
