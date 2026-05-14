using ShopContent_Shashin.Classes;
using ShopContent_Shashin.Modell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ShopContent_Shashin.Context
{
    public class ItemsContext : Items
    {
        public ItemsContext(bool save = false)
        {
            if(save)
                Save(true);
            Category = new Categorys();
        }

        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<CategorysContext> allCategorys = CategorysContext.AllCategorys();
            SqlConnection conn;
            SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Items]", out conn);

            while(reader.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDouble(2),
                    Description = reader.GetString(3),
                    Category = reader.IsDBNull(4) ?
                        null :
                        allCategorys.Where(x => x.Id == reader.GetInt32(4)).First()
                });
            }
            Connection.CloseConnection(conn);
            return allItems;

        }
        public void Save(bool New = false)
        {
            SqlConnection conn;
            if(New)
            {
                SqlDataReader reader = Connection.Query("INSERT INTO " +
                    "[dbo].[Items](" +
                    "Name, " +
                    "Price, " +
                    "Description) " +
                    "OUTPUT Inserted.Id " +
                    "VALUES (" +
                    $"N'{this.Name}', " +
                    $"{this.Price}, " +
                    $"N'{this.Description}')", out conn);

                reader.Read();
                this.Id = reader.GetInt32(0);
            } else
            {
                Connection.Query("UPDATE [dbo].[Items] " +
                    "SET " +
                    $"Name = N'{this.Name}', " +
                    $"Price = {this.Price}, " +
                    $"Description = N'{this.Description}', " +
                    $"IdCategory = {this.Category.Id} " +
                    "WHERE " +
                    $"Id = {this.Id}", out conn);
            }
            Connection.CloseConnection(conn);
            MainWindow.init.frame.Navigate(new View.Main());
        }

        public void Delete()
        {
            SqlConnection conn;
            Connection.Query("DELETE FROM [dbo].[Items] " +
                "WHERE " +
                $"Id = {this.Id}", out conn);
            Connection.CloseConnection(conn);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Category = CategorysContext.AllCategorys().Where(x => x.Id == this.Category.Id).First();
                    Save();
                });
            }
        }

        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
