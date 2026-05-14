using ShopContent_Shashin.Classes;
using ShopContent_Shashin.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopContent_Shashin.ViewModell
{
    public class VMItems : INotifyPropertyChanged
    {
        public ObservableCollection<ItemsContext> Items {  get; set; }
        public RelayCommand NewItem
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ItemsContext newModell = new ItemsContext(true);
                    Items.Add(newModell);
                    MainWindow.init.frame.Navigate(new View.Add(newModell));
                });
            }
        }

        public VMItems() =>
            Items = ItemsContext.AllItems();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
