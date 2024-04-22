using System.Windows.Input;
using MyWpfAppForDb.WPF.Models;

namespace MyWpfAppForDb.WPF.ViewModels
{
    public class RegistrationVM : ViewModelBase
    {
        private RegistrationModel _registrationVM;
        private ViewModelStore _viewModelStore;

        public string Login
        {
            get => _registrationVM.Login;
            set
            {
                _registrationVM.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Email
        {
            get => _registrationVM.Email;
            set
            {
                _registrationVM.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password1
        {
            get => _registrationVM.Password1;
            set
            {
                _registrationVM.Password1 = value;
                OnPropertyChanged(nameof(Password1));
            }
        }

        public string Password2
        {
            get => _registrationVM.Password2;
            set
            {
                _registrationVM.Password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }

        public ICommand RegistrationCommand { get; set; }


        public RegistrationVM(ViewModelStore viewModelStore)
        {
            _viewModelStore = viewModelStore;
            _registrationVM = new RegistrationModel();
        }

    }
}
