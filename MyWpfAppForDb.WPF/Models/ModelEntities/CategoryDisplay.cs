using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class CategoryDisplay : ModelEntityBase
    {
        public CategoryDisplay()
        {
            ProductsInstances = new HashSet<ProductsInstanceDisplay>();
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

        public virtual ICollection<ProductsInstanceDisplay> ProductsInstances { get; set; }
    }
}
