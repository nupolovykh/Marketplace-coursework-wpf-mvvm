using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
    public partial class ProductsInstanceDto : ModelGtoBase
    {
        public ProductsInstanceDto()
        {
            Products = new HashSet<ProductDto>();
        }

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

        private int? _categoryId;

        public int? CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        private string _productName;

        public string Name
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private bool? _availability;

        public bool? Availability
        {
            get => _availability;
            set
            {
                _availability = value;
                OnPropertyChanged();
            }
        }

        public virtual CategoryDto Category { get; set; }
        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
