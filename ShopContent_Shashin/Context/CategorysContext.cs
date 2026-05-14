using ShopContent_Shashin.Classes;
using ShopContent_Shashin.Modell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace ShopContent_Shashin.Context
{
    public class CategorysContext : Categorys
    {
        public static ObservableCollection<CategorysContext> AllCategorys()
        {
            ObservableCollection<CategorysContext> allCategorys = new ObservableCollection<CategorysContext>();
            SqlConnection conn;
            SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Categorys]", out conn);

            while(reader.Read())
            {
                allCategorys.Add(new CategorysContext()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            Connection.CloseConnection(conn);
            return allCategorys;
        }
    }
}
