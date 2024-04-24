using System.Windows.Input;
using MyWpfAppForDb.WPF.Models;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;
using AutoMapper;
using System.Linq;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class AuthorizationVM : ViewModelBase
    {
        private AuthorizationModel _authorizationModel;
        //private CategoryDto _categoryDto;

        public string LoginEmail
        {
            get => _authorizationModel.LoginEmail!;
            set
            {
                _authorizationModel.LoginEmail = value;
                OnPropertyChanged(nameof(LoginEmail));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string Password
        {
            get => _authorizationModel.Password!;
            set
            {
                _authorizationModel.Password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public bool CanLogin => !string.IsNullOrEmpty(LoginEmail) && !string.IsNullOrEmpty(Password);

        public ICommand LoginCommand { get; set; }

        public AuthorizationVM(IHost host)
        {
            _authorizationModel = new AuthorizationModel();

            //var db = host.Services.GetRequiredService<AppDbContext>();
            //IMapper _mapper = host.Services.GetRequiredService<IMapper>();

            //var category = db.Categories.FirstOrDefault();

            //_categoryDto = _mapper.Map<CategoryDto>(category);

            //LoginEmail = _categoryDto.Name;
            //Password = _categoryDto.Name;

            //LoginCommand = new LoginCommand(db);
            //LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
        }
    }
}
