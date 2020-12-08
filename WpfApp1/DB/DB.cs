using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DbConnect
{
    class DB
    {


        SqlConnection connection = new SqlConnection(
    new SqlConnectionStringBuilder()
    {
        DataSource = "192.168.221.12",
        InitialCatalog = "prk",
        UserID = "user03",
        Password = "03"
    }.ConnectionString
);

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}