using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Printing;
using System.Text;

namespace ShopContent_Shashin.Classes
{
    public class Connection
    {
        private static readonly string config = "server=localhost\\SQLEXPRESS;" +
            "Trusted_Connection=No;" +
            "DataBase=ShopContent;" +
            "User=test_user;" +
            "PWD=1111";

        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(config);
            conn.Open();
            return conn;
        }

        public static SqlDataReader Query(string SQL, out SqlConnection conn)
        {
            conn = OpenConnection();
            return new SqlCommand(SQL, conn).ExecuteReader();
        }

        public static void CloseConnection(SqlConnection conn)
        {
            conn.Close();
            SqlConnection.ClearPool(conn);
        }
    }
}
