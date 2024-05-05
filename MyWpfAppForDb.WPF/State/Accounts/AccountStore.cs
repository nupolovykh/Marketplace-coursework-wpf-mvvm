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
				RoleChanged();
			}
		}

		public event Action? StateChanged;

		private void RoleChanged()
		{
			IsAdminProp = _currentEmployee?.Role!.Name is "Admin";
			IsOperatorProp = _currentEmployee?.Role!.Name is "Operator";
			IsLoaderProp = _currentEmployee?.Role!.Name is "Loader";
		}

		private bool IsAdminProp { get; set; }

		private bool IsOperatorProp { get; set; }

		private bool IsLoaderProp { get; set; }

		public bool IsAdmin() => IsAdminProp;

		public bool IsOperator() => IsOperatorProp;

		public bool IsLoader() => IsLoaderProp;

	}
}
