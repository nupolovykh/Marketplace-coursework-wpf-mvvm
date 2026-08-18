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

		public async Task Login(string loginOrEmail, string password)
		{
			var employee = await _authenticationService.Login(loginOrEmail, password);
			if (employee is null) return;

			var dto = _mapper.Map<EmployeeDto>(employee);
			CurrentAccount = dto;
		}

		public async Task<AccountResult> Register(string email, string username, string password, string confirmPassword)
		{
			var result = await _authenticationService.Register(email, username, password, confirmPassword);

			// Logging in unconditionally meant a rejected registration (name taken,
			// passwords not matching) immediately threw UserNotFoundException or
			// InvalidPasswordException instead of showing why it was rejected.
			if (result == AccountResult.Success) await Login(username, password);

			return result;
		}

		public async Task<AccountResult> Adjust(EmployeeDto dto, string newPassword)
		{
			Employee employee = _mapper.Map<Employee>(dto);

			var result = await _authenticationService.Adjust(employee, newPassword);

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
