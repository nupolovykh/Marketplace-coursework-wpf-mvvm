using AutoMapper;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.EntityFramework.Services.AuthenticationServices;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using MyWpfAppForDb.WPF.State.Accounts;
using System;
using System.Threading.Tasks;

namespace MyWpfAppForDb.WPF.State.Authenticators
{
	internal class Authenticator : IAuthenticator
	{
		private readonly IAccountStore _accountStore;
		private readonly IAuthenticationService _authenticationService;
		private readonly IMapper _mapper;

		public Authenticator(IAccountStore accountStore, IAuthenticationService authenticationService, IMapper mapper)
		{
			_accountStore = accountStore;
			_authenticationService = authenticationService;
			_mapper = mapper;
		}

		public EmployeeDto CurrentAccount
		{
			get => _accountStore.CurrentEmployee;
			private set
			{
				_accountStore.CurrentEmployee = value;
				StateChanged?.Invoke();
			}
		}

		public bool IsLoggedIn => CurrentAccount != null;

		public event Action StateChanged;

		public async Task Login(string username, string password)
		{
			var employee = await _authenticationService.Login(username, password);
			if (employee is null) return;

			var dto = _mapper.Map<EmployeeDto>(employee);
			CurrentAccount = dto;
		}

		public async Task<AccountResult> Register(string email, string username, string password, string confirmPassword)
		{
			var result = await _authenticationService.Register(email, username, password, confirmPassword);

			await this.Login(username, password);

			return result;
		}

		public async Task<AccountResult> Adjust(EmployeeDto dto, string password)
		{
			Employee employee = _mapper.Map<Employee>(dto);
			employee.Password = password;

			var result = await _authenticationService.Adjust(employee);

			if(result == AccountResult.Success)
			{
				CurrentAccount = dto;
			}
			return result;
		}

		public void Logout()
		{
			CurrentAccount = null!;
		}
	}
}
