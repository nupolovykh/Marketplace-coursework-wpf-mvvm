using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
    public partial class DeliveryPointDto : ModelGtoBase
    {
        public DeliveryPointDto()
        {
            Employees = new HashSet<EmployeeDto>();
            Orders = new HashSet<OrderDto>();
        }

        private int _deliveryPointId;

        public int DeliveryPointId
        {
            get => _deliveryPointId;
            set
            {
                _deliveryPointId = value;
                OnPropertyChanged();
            }
        }

        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _city;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private decimal _rating;

        public decimal Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        private string _zipcode;

        public string Zipcode
        {
            get => _zipcode;
            set
            {
                _zipcode = value;
                OnPropertyChanged();
            }
        }

        public virtual ICollection<EmployeeDto> Employees { get; set; }
        public virtual ICollection<OrderDto> Orders { get; set; }
    }
}
