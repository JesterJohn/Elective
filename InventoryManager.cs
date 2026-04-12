using System;
using System.Data;
using System.Data.SqlClient;

public class InventoryManager
{
    // Assuming your 'db' class is accessible or defined here
    private DatabaseConnection db = new DatabaseConnection();

    public bool AddProduct(int id, string name, decimal price, int qty, string barcode, string imagePath)
    {
        using (SqlConnection con = db.GetConnection())
        {
            // Query matching your table schema
            string query = "INSERT INTO InventoryTbl (ProductID, ProductName, Price, Quantity, Barcode, ImagePath) " +
                           "VALUES (@id, @name, @price, @qty, @barcode, @path)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@barcode", barcode);
            cmd.Parameters.AddWithValue("@path", imagePath ?? (object)DBNull.Value);

            try
            {
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }
    }

    public DataTable GetAllProducts()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = db.GetConnection())
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM InventoryTbl", con);
            da.Fill(dt);
        }
        return dt;
    }
}