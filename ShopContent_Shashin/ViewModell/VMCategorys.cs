using ShopContent_Shashin.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopContent_Shashin.ViewModell
{
    public class VMCategorys : INotifyPropertyChanged
    {
        public ObservableCollection<CategorysContext> Categorys { get; set; }
        public VMCategorys() =>
            Categorys = CategorysContext.AllCategorys();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
