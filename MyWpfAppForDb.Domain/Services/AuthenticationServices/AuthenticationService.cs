using Microsoft.AspNet.Identity;
using MyWpfAppForDb.EntityFramework.Entities;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher) 
        { 
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Employee> Login(string username, string password)
        {
            Employee employee = await _accountService.GetByUsername(username);

            if (employee == null) return null;

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(employee.Password, password);

            if (passwordResult != PasswordVerificationResult.Success) return null;

            return employee;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword) result = RegistrationResult.PasswordsDoNotMatch;

            Employee employee = await _accountService.GetByUsername(username);
            
            if(username != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if(result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                Employee newEmployee = new Employee()
                {
                    Email = email,
                    Name = username,
                    Password = hashedPassword,
                    Salary = 1000
                };

                await _accountService.Create(newEmployee);
            }
            return result;
        }
    }
}
