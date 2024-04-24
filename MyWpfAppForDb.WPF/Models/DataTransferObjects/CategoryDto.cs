using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWpfAppForDb.WPF.Models.DataTransferObjects
{
    public partial class CategoryDto : ModelGtoBase
    {
        public CategoryDto()
        {
            ProductsInstances = new HashSet<ProductsInstanceDto>();
        }

        private int _categoryId;

        public int CategoryId
        {
            get => _categoryId;
            set 
            {
                _categoryId = value;
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

        public virtual ICollection<ProductsInstanceDto> ProductsInstances { get; set; }
    }
}
