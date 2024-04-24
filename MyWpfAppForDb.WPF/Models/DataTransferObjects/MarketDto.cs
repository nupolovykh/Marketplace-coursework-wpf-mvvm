using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
    public partial class MarketDto : ModelGtoBase
    {
        public MarketDto()
        {
            Products = new HashSet<ProductDto>();
        }

        private int _marketId;

        public int MarketId
        {
            get => _marketId;
            set
            {
                _marketId = value;
                OnPropertyChanged();
            }
        }

        private string _marketName;

        public string Name
        {
            get => _marketName;
            set
            {
                _marketName = value;
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

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
