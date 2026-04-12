using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    internal class DatabaseHelper
    {
        // Use the connection string from your email
        private readonly string connectionString = @"Data Source=LAPTOP-9QMQALA3\SQLEXPRESS;Initial Catalog=InventoryDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection() => new SqlConnection(connectionString);

        // Method to refresh the DataGridView
        public DataTable GetInventory()
        {
            using (SqlConnection conn = GetConnection())
            {
                string query = "SELECT * FROM InventoryTbl ORDER BY ProductID DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Generic method for Add, Update, Delete
        public bool ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

