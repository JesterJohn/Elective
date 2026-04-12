using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elective
{
    public partial class Form3 : Form
    {
        private string transactionConn = @"Server=LAPTOP-9QMQALA3\SQLEXPRESS;Database=TransactionDB;Trusted_Connection=True;";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Clear designer text so labels don't double-prefix
            Sales_Label.Text = "Sales: ₱0.00";
            ItemsSold_Label.Text = "Items Sold: 0";

            SetupDataGridView();
            LoadTransactions();
        }

        // Called by Form1 after every successful Save
        public void RefreshData()
        {
            LoadTransactions();
        }

        private void SetupDataGridView()
        {
            Transaction_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Transaction_datagrid.ReadOnly = true;
            Transaction_datagrid.AllowUserToAddRows = false;
            Transaction_datagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Transaction_datagrid.RowHeadersVisible = false;
            Transaction_datagrid.MultiSelect = false;
            Transaction_datagrid.BackgroundColor = Color.White;
            Transaction_datagrid.GridColor = Color.FromArgb(220, 220, 220);
            Transaction_datagrid.BorderStyle = BorderStyle.FixedSingle;
            Transaction_datagrid.Font = new Font("Segoe UI", 9.5f);

            Transaction_datagrid.DefaultCellStyle.BackColor = Color.White;
            Transaction_datagrid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            Transaction_datagrid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            Transaction_datagrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            Transaction_datagrid.DefaultCellStyle.SelectionForeColor = Color.White;
            Transaction_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            Transaction_datagrid.RowTemplate.Height = 28;

            Transaction_datagrid.EnableHeadersVisualStyles = false;
            Transaction_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            Transaction_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Transaction_datagrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            Transaction_datagrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            Transaction_datagrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 45, 48);
            Transaction_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            Transaction_datagrid.ColumnHeadersHeight = 34;
            Transaction_datagrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadTransactions()
        {
            using (SqlConnection conn = new SqlConnection(transactionConn))
            {
                string query = @"
                    SELECT 
                        s.SaleDate AS [Date & Time], 
                        s.TransactionID AS [Transaction ID], 
                        s.Barcode, s.ProductID, s.ProductName, 
                        s.Price AS [Price (₱)], 
                        s.Quantity AS [Qty], 
                        s.AmountToPay AS [Item Total (₱)], 
                        s.TotalAmountToPay AS [Grand Total (₱)], 
                        s.CashRendered AS [Cash Rendered (₱)], 
                        s.Change AS [Change (₱)], 
                        s.PaymentMode AS [Payment Mode]
                    FROM SalesTbl s
                    ORDER BY s.SaleDate DESC";

                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Transaction_datagrid.DataSource = dt;

                    // Update the Summary Labels at the top of Form 3
                    double totalSales = 0;
                    int totalQty = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        // We sum 'Item Total' for total sales
                        totalSales += Convert.ToDouble(row["Item Total (₱)"]);
                        totalQty += Convert.ToInt32(row["Qty"]);
                    }

                    Sales_Label.Text = $"Sales: ₱{totalSales:N2}";
                    ItemsSold_Label.Text = $"Items Sold: {totalQty}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report: " + ex.Message);
                }
            }
        }

        private void UpdateSummaryLabels(SqlConnection conn)
        {
            string sql = @"
                SELECT
                    ISNULL(SUM(TotalAmountToPay), 0) AS TotalSales,
                    ISNULL(SUM(TotalQuantity),    0) AS TotalItemsSold
                FROM TransactionSummaryTbl";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    // Set text directly — do NOT prefix with "Sales:" in the designer label
                    Sales_Label.Text = $"Sales: ₱{Convert.ToDouble(rdr["TotalSales"]):N2}";
                    ItemsSold_Label.Text = $"Items Sold: {Convert.ToInt32(rdr["TotalItemsSold"])}";
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Sales_Label.Text = "Sales: ₱0.00";
                ItemsSold_Label.Text = "Items Sold: 0";
                MessageBox.Show("Failed to load summary:\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void transactionbtn_Click_1(object sender, EventArgs e)
        {
            if (Transaction_datagrid.SelectedRows.Count > 0)
            {
                string transID = Transaction_datagrid.SelectedRows[0].Cells["Transaction ID"].Value.ToString();

                DialogResult confirm = MessageBox.Show($"Are you sure you want to delete Transaction #{transID}?",
                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(transactionConn))
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            // Delete from both tables
                            string delSales = "DELETE FROM SalesTbl WHERE TransactionID = @id";
                            string delSum = "DELETE FROM TransactionSummaryTbl WHERE TransactionID = @id";

                            using (SqlCommand cmd1 = new SqlCommand(delSales, conn, trans))
                            {
                                cmd1.Parameters.AddWithValue("@id", transID);
                                cmd1.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd2 = new SqlCommand(delSum, conn, trans))
                            {
                                cmd2.Parameters.AddWithValue("@id", transID);
                                cmd2.ExecuteNonQuery();
                            }

                            trans.Commit();
                            MessageBox.Show("Transaction deleted successfully.");
                            LoadTransactions(); // Refresh the list
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            MessageBox.Show("Delete failed: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchID = Search_textbox.Text.Trim(); // Replace txtSearchID with your textbox name
            if (string.IsNullOrEmpty(searchID))
            {
                LoadTransactions(); // Reload all if empty
                return;
            }

            using (SqlConnection conn = new SqlConnection(transactionConn))
            {
                string query = @"SELECT s.SaleDate AS [Date & Time], s.TransactionID AS [Transaction ID], 
                         s.Barcode, s.ProductID, s.ProductName, s.Price AS [Price (₱)], 
                         s.Quantity AS [Qty], s.AmountToPay AS [Item Total (₱)], 
                         s.TotalAmountToPay AS [Grand Total (₱)], s.CashRendered AS [Cash Rendered (₱)], 
                         s.Change AS [Change (₱)], s.PaymentMode AS [Payment Mode]
                         FROM SalesTbl s WHERE s.TransactionID LIKE @id ORDER BY s.SaleDate DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@id", "%" + searchID + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Transaction_datagrid.DataSource = dt;
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

























