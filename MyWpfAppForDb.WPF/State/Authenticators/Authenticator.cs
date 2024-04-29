using AutoMapper;
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

		public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
			=> await _authenticationService.Register(email, username, password, confirmPassword);

		public void Logout()
		{
			CurrentAccount = null!;
		}
	}
}
