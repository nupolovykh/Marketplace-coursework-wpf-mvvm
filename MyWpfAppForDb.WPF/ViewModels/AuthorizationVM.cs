using System.Windows.Input;
using MyWpfAppForDb.WPF.Models;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using MyWpfAppForDb.WPF.Commands;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.ModelEntities;
using AutoMapper;
using System.Linq;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class AuthorizationVM : ViewModelBase
    {
        private AuthorizationModel _authorizationModel;
        private CategoryGto _categoryDto;

        public string LoginEmail
        {
            get => _categoryDto.Name;
            set
            {
                _categoryDto.Name = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(LoginEmail));
            }
        }

        public string Password
        {
            get => _categoryDto.Name;
            set
            {
                _categoryDto.Name = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(LoginEmail));
            }
        }

        public ICommand AuthorizationCommand { get; set; }

        public AuthorizationVM(IHost host)
        {
            _authorizationModel = new AuthorizationModel();

            var db = host.Services.GetRequiredService<MarketPlaceContext>();
            IMapper _mapper = host.Services.GetRequiredService<IMapper>();

            var category = db.Categories.FirstOrDefault();

            _categoryDto = _mapper.Map<CategoryGto>(category);

            LoginEmail = _categoryDto.Name;
            Password = _categoryDto.Name;

            AuthorizationCommand = new LoginCommand(db);
        }
    }
}
