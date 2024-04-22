using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWpfAppForDb.WPF.Models.ModelEntities
{
    public partial class CategoryGto : INotifyPropertyChanged
    {
        public CategoryGto()
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


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
