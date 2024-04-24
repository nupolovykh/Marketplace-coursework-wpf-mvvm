using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
    public partial class EmployeeDto : ModelGtoBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private int? _deliveryPointId;

        public int? DeliveryPointId
        {
            get => _deliveryPointId;
            set
            {
                _deliveryPointId = value;
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

        private decimal? _salary;

        public decimal? Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged();
            }
        }

        public virtual DeliveryPointDto DeliveryPoint { get; set; }
    }
}
