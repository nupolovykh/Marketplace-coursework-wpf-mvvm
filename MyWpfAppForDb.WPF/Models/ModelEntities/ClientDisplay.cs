using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class ClientDisplay : ModelEntityBase
    {
        public ClientDisplay()
        {
            Orders = new HashSet<OrderDisplay>();
        }

        private int _clientId;

        public int ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set 
            { 
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _phone;

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public virtual ICollection<OrderDisplay> Orders { get; set; }
    }
}
