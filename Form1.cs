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
    public partial class Form1 : Form
    {
        private string inventoryConn = @"Server=LAPTOP-9QMQALA3\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;";
        private string transactionConn = @"Server=LAPTOP-9QMQALA3\SQLEXPRESS;Database=TransactionDB;Trusted_Connection=True;";

        private DataTable orderTable = new DataTable();
        private TextBox _numpadTarget = null;
        private string selectedPaymentMode = "CASH";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupOrderTable();
            SetupDataGridView();
            _numpadTarget = Qty_Textbox;
            HighlightPaymentButton(cashbtn);

            Timer clock = new Timer();
            clock.Interval = 1000;
            clock.Tick += (s, ev) =>
                DateTime_Label.Text = DateTime.Now.ToString("MM/dd/yyyy   hh:mm:ss tt");
            clock.Start();
        }

        // ── ORDER TABLE & GRID ───────────────────────────────────────────────────
        private void SetupOrderTable()
        {
            orderTable.Columns.Add("Date", typeof(string));
            orderTable.Columns.Add("Barcode", typeof(string));
            orderTable.Columns.Add("ProductID", typeof(string));
            orderTable.Columns.Add("ProductName", typeof(string));
            orderTable.Columns.Add("Price", typeof(double));
            orderTable.Columns.Add("Quantity", typeof(int));
            orderTable.Columns.Add("AmountToPay", typeof(double));
        }

        private void SetupDataGridView()
        {
            PaymentSum_datagrid.DataSource = orderTable;
            PaymentSum_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PaymentSum_datagrid.ReadOnly = true;
            PaymentSum_datagrid.AllowUserToAddRows = false;
            PaymentSum_datagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PaymentSum_datagrid.RowHeadersVisible = false;
            PaymentSum_datagrid.MultiSelect = false;
            PaymentSum_datagrid.BackgroundColor = Color.White;
            PaymentSum_datagrid.GridColor = Color.FromArgb(220, 220, 220);
            PaymentSum_datagrid.BorderStyle = BorderStyle.FixedSingle;
            PaymentSum_datagrid.Font = new Font("Segoe UI", 9.5f);

            PaymentSum_datagrid.DefaultCellStyle.BackColor = Color.White;
            PaymentSum_datagrid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            PaymentSum_datagrid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            PaymentSum_datagrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            PaymentSum_datagrid.DefaultCellStyle.SelectionForeColor = Color.White;
            PaymentSum_datagrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            PaymentSum_datagrid.RowTemplate.Height = 28;

            PaymentSum_datagrid.EnableHeadersVisualStyles = false;
            PaymentSum_datagrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            PaymentSum_datagrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            PaymentSum_datagrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            PaymentSum_datagrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            PaymentSum_datagrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 45, 48);
            PaymentSum_datagrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            PaymentSum_datagrid.ColumnHeadersHeight = 34;
            PaymentSum_datagrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            PaymentSum_datagrid.Columns["Date"].HeaderText = "Date & Time";
            PaymentSum_datagrid.Columns["Barcode"].HeaderText = "Barcode";
            PaymentSum_datagrid.Columns["ProductID"].HeaderText = "Product ID";
            PaymentSum_datagrid.Columns["ProductName"].HeaderText = "Product Name";
            PaymentSum_datagrid.Columns["Price"].HeaderText = "Price (₱)";
            PaymentSum_datagrid.Columns["Quantity"].HeaderText = "Qty";
            PaymentSum_datagrid.Columns["AmountToPay"].HeaderText = "Amount to Pay (₱)";

            PaymentSum_datagrid.Columns["Price"].DefaultCellStyle.Format = "N2";
            PaymentSum_datagrid.Columns["AmountToPay"].DefaultCellStyle.Format = "N2";
            PaymentSum_datagrid.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PaymentSum_datagrid.Columns["AmountToPay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PaymentSum_datagrid.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PaymentSum_datagrid.Columns["ProductID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PaymentSum_datagrid.Columns["Date"].FillWeight = 18;
            PaymentSum_datagrid.Columns["Barcode"].FillWeight = 12;
            PaymentSum_datagrid.Columns["ProductID"].FillWeight = 10;
            PaymentSum_datagrid.Columns["ProductName"].FillWeight = 22;
            PaymentSum_datagrid.Columns["Price"].FillWeight = 12;
            PaymentSum_datagrid.Columns["Quantity"].FillWeight = 8;
            PaymentSum_datagrid.Columns["AmountToPay"].FillWeight = 18;
        }

        // ── BARCODE LOOKUP ───────────────────────────────────────────────────────
        private void Barcode_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(Barcode_textbox.Text))
            {
                LoadProductByBarcode(Barcode_textbox.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ProductID_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(ProductID_Textbox.Text))
            {
                LoadProductByBarcode(ProductID_Textbox.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void LoadProductByBarcode(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(inventoryConn))
            {
                string query = @"SELECT ProductID, ProductName, Price, Barcode, Quantity, ImagePath
                                 FROM InventoryTbl WHERE Barcode = @barcode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@barcode", barcode);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader["Quantity"]) <= 0)
                        {
                            MessageBox.Show("This product is out of stock.", "Out of Stock",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ClearCurrentProduct();
                            return;
                        }
                        Barcode_textbox.Text = reader["Barcode"].ToString();
                        ProductID_Textbox.Text = reader["ProductID"].ToString();
                        ProductName_Textbox.Text = reader["ProductName"].ToString();
                        Price_Textbox.Text = Convert.ToDouble(reader["Price"]).ToString("F2");
                        Qty_Textbox.Text = "1";

                        string imgPath = reader["ImagePath"].ToString();
                        pictureBox1.Image = (!string.IsNullOrEmpty(imgPath) && System.IO.File.Exists(imgPath))
                            ? Image.FromFile(imgPath) : null;

                        Qty_Textbox.Focus();
                        _numpadTarget = Qty_Textbox;
                    }
                    else
                    {
                        MessageBox.Show("Product not found.", "Not Found",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCurrentProduct();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DB Error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProductID_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductID_Textbox.Text)) return;
            using (SqlConnection conn = new SqlConnection(inventoryConn))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT ProductName, Price, Barcode, ImagePath FROM InventoryTbl WHERE ProductID = @id", conn);
                cmd.Parameters.AddWithValue("@id", ProductID_Textbox.Text.Trim());
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ProductName_Textbox.Text = reader["ProductName"].ToString();
                        Price_Textbox.Text = Convert.ToDouble(reader["Price"]).ToString("F2");
                        Barcode_textbox.Text = reader["Barcode"].ToString();
                        string imgPath = reader["ImagePath"].ToString();
                        pictureBox1.Image = (!string.IsNullOrEmpty(imgPath) && System.IO.File.Exists(imgPath))
                            ? Image.FromFile(imgPath) : null;
                    }
                }
                catch { }
            }
        }

        // ── CALCULATE ────────────────────────────────────────────────────────────
        private void calculatebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductID_Textbox.Text) || string.IsNullOrWhiteSpace(ProductName_Textbox.Text))
            {
                MessageBox.Show("Please load a product first.", "No Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(Price_Textbox.Text, out double price) || price <= 0)
            {
                MessageBox.Show("Invalid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(Qty_Textbox.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Enter a valid quantity (1 or more).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pid = ProductID_Textbox.Text.Trim();
            string pname = ProductName_Textbox.Text.Trim();
            string barcode = Barcode_textbox.Text.Trim();

            int availableStock = GetCurrentStock(pid);
            int alreadyInCart = 0;
            foreach (DataRow r in orderTable.Rows)
                if (r["ProductID"].ToString() == pid)
                    alreadyInCart = Convert.ToInt32(r["Quantity"]);

            if (alreadyInCart + qty > availableStock)
            {
                MessageBox.Show(
                    $"Not enough stock!\n\nAvailable: {availableStock}\nAlready in cart: {alreadyInCart}\nRequested: {qty}",
                    "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double amount = price * qty;
            string dateStamp = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            bool found = false;
            foreach (DataRow row in orderTable.Rows)
            {
                if (row["ProductID"].ToString() == pid)
                {
                    int newQty = Convert.ToInt32(row["Quantity"]) + qty;
                    row["Quantity"] = newQty;
                    row["AmountToPay"] = Convert.ToDouble(row["Price"]) * newQty;
                    row["Date"] = dateStamp;
                    found = true;
                    break;
                }
            }
            if (!found)
                orderTable.Rows.Add(dateStamp, barcode, pid, pname, price, qty, amount);

            RecalculateTotals();
            ClearCurrentProduct();
            ProductID_Textbox.Focus();
        }

        private int GetCurrentStock(string productID)
        {
            using (SqlConnection conn = new SqlConnection(inventoryConn))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Quantity FROM InventoryTbl WHERE ProductID = @id", conn);
                cmd.Parameters.AddWithValue("@id", productID);
                try { conn.Open(); object r = cmd.ExecuteScalar(); return r != null ? Convert.ToInt32(r) : 0; }
                catch { return 0; }
            }
        }

        // ── TOTALS ───────────────────────────────────────────────────────────────
        private void RecalculateTotals()
        {
            double total = 0;
            foreach (DataRow row in orderTable.Rows)
                total += Convert.ToDouble(row["AmountToPay"]);
            AmounttoPay_Textbox.Text = total.ToString("N2");
            ComputeChange();
        }

        private void ComputeChange()
        {
            bool hasTotal = double.TryParse(AmounttoPay_Textbox.Text.Replace(",", ""), out double total);
            bool hasCash = double.TryParse(CaashRendered_Textbox.Text, out double cash);
            if (hasTotal && hasCash)
            {
                double change = cash - total;
                change_txtbox.Text = change >= 0 ? change.ToString("N2") : "Insufficient";
                change_txtbox.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
                change_txtbox.Text = "";
        }

        private void CaashRendered_Textbox_TextChanged(object sender, EventArgs e) => ComputeChange();

        // ── SAVE ─────────────────────────────────────────────────────────────────
        // Saves to TransactionDB then auto-opens/refreshes Form3 to display the data
        // ─────────────────────────────────────────────────────────────────────────
        private void _Click(object sender, EventArgs e)
        {

        }

        // ── CLEAR ────────────────────────────────────────────────────────────────
        private void clearbtn_Click(object sender, EventArgs e)
        {
            orderTable.Rows.Clear();
            ClearCurrentProduct();
            AmounttoPay_Textbox.Text = "";
            CaashRendered_Textbox.Text = "";
            change_txtbox.Text = "";
            selectedPaymentMode = "CASH";
            HighlightPaymentButton(cashbtn);
            ProductID_Textbox.Focus();
        }

        private void ClearCurrentProduct()
        {
            Barcode_textbox.Clear();
            ProductID_Textbox.Clear();
            ProductName_Textbox.Clear();
            Price_Textbox.Clear();
            Qty_Textbox.Clear();
            pictureBox1.Image = null;
        }

        // ── NUMPAD ───────────────────────────────────────────────────────────────
        private void NumpadBtn_Click(object sender, EventArgs e)
        {
            if (_numpadTarget == null) return;
            string value = ((Button)sender).Text;
            if (value == "+/-")
            {
                if (double.TryParse(_numpadTarget.Text, out double num))
                    _numpadTarget.Text = (-num).ToString("F2");
                return;
            }
            if (value == "." && _numpadTarget.Text.Contains(".")) return;
            if (_numpadTarget.Text == "0" && value != ".")
                _numpadTarget.Text = "";
            _numpadTarget.Text += value;
        }

        private void NumericTextbox_Enter(object sender, EventArgs e) =>
            _numpadTarget = sender as TextBox;

        // ── PAYMENT MODE ─────────────────────────────────────────────────────────
        private void cashbtn_Click(object sender, EventArgs e) { selectedPaymentMode = "CASH"; HighlightPaymentButton(cashbtn); }
        private void checkbtn_Click(object sender, EventArgs e) { selectedPaymentMode = "CHECK"; HighlightPaymentButton(checkbtn); }
        private void smartpaybtn_Click(object sender, EventArgs e) { selectedPaymentMode = "SMART PAY"; HighlightPaymentButton(smartpaybtn); }
        private void cardbtn_Click(object sender, EventArgs e) { selectedPaymentMode = "CARD"; HighlightPaymentButton(cardbtn); }

        private void HighlightPaymentButton(Button selected)
        {
            foreach (Button btn in new[] { cashbtn, checkbtn, smartpaybtn, cardbtn })
            {
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = Color.Black;
            }
            selected.BackColor = Color.FromArgb(45, 45, 48);
            selected.ForeColor = Color.White;
        }

        // ── NAVIGATION ───────────────────────────────────────────────────────────
        private void inventorybtn_Click(object sender, EventArgs e)
        {
            Form2 inventory = new Form2();
            inventory.Show();
        }

        private void transactionbtn_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is Form3 existing)
                {
                    existing.RefreshData();
                    existing.BringToFront();
                    existing.Focus();
                    return;
                }
            }
            Form3 transaction = new Form3();
            transaction.Show();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        // ── STUBS ────────────────────────────────────────────────────────────────
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged_1(object sender, EventArgs e) { }
        private void change_txtbox_TextChanged(object sender, EventArgs e) { }
        private void savebtn_Click_1(object sender, EventArgs e)
        {
            // 1. Validation Checks
            if (orderTable.Rows.Count == 0)
            {
                MessageBox.Show("No items in order.", "Empty Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(CaashRendered_Textbox.Text, out double cashRendered))
            {
                MessageBox.Show("Please enter the Cash Rendered amount.", "Missing Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double amountToPay = double.Parse(AmounttoPay_Textbox.Text.Replace(",", ""));
            double change = cashRendered - amountToPay;

            if (change < 0)
            {
                MessageBox.Show("Cash rendered is insufficient.", "Payment Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Setup Transaction Data
            DateTime saleDateTime = DateTime.Now;
            int totalItems = orderTable.Rows.Count;
            int totalQuantity = 0;
            foreach (DataRow r in orderTable.Rows)
                totalQuantity += Convert.ToInt32(r["Quantity"]);

            // 3. GENERATE AUTO-INCREMENT TRANSACTION ID
            string transactionID = "1001"; // Default starting ID
            using (SqlConnection conn = new SqlConnection(transactionConn))
            {
                try
                {
                    conn.Open();
                    // We get the max ID and cast to BIGINT to ensure numerical sorting
                    string idQuery = "SELECT MAX(CAST(TransactionID AS BIGINT)) FROM TransactionSummaryTbl";
                    SqlCommand idCmd = new SqlCommand(idQuery, conn);
                    object result = idCmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        transactionID = (Convert.ToInt64(result) + 1).ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Fallback to timestamp if the table check fails (safety net)
                    transactionID = saleDateTime.ToString("yyyyMMddHHmmss");
                }
            }

            // 4. Write to TransactionDB 
            using (SqlConnection tConn = new SqlConnection(transactionConn))
            {
                tConn.Open();
                SqlTransaction sqlTrans = tConn.BeginTransaction();
                try
                {
                    // Insert into SalesTbl (Individual Items)
                    string itemSql = @"
                INSERT INTO SalesTbl
                    (TransactionID, Barcode, ProductID, ProductName, Price,
                     Quantity, AmountToPay, TotalAmountToPay,
                     CashRendered, Change, PaymentMode, SaleDate)
                VALUES
                    (@transID,@barcode,@pid,@pname,@price,
                     @qty,@itemAmount,@totalAmount,
                     @cash,@change,@paymode,@date)";

                    foreach (DataRow row in orderTable.Rows)
                    {
                        SqlCommand cmd = new SqlCommand(itemSql, tConn, sqlTrans);
                        cmd.Parameters.AddWithValue("@transID", transactionID);
                        cmd.Parameters.AddWithValue("@barcode", row["Barcode"].ToString());
                        cmd.Parameters.AddWithValue("@pid", row["ProductID"].ToString());
                        cmd.Parameters.AddWithValue("@pname", row["ProductName"].ToString());
                        cmd.Parameters.AddWithValue("@price", Convert.ToDouble(row["Price"]));
                        cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(row["Quantity"]));
                        cmd.Parameters.AddWithValue("@itemAmount", Convert.ToDouble(row["AmountToPay"]));
                        cmd.Parameters.AddWithValue("@totalAmount", amountToPay);
                        cmd.Parameters.AddWithValue("@cash", cashRendered);
                        cmd.Parameters.AddWithValue("@change", change);
                        cmd.Parameters.AddWithValue("@paymode", selectedPaymentMode);
                        cmd.Parameters.AddWithValue("@date", saleDateTime);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert into TransactionSummaryTbl (The Header)
                    string summarySql = @"
                INSERT INTO TransactionSummaryTbl
                    (TransactionID, TotalItems, TotalQuantity,
                     TotalAmountToPay, CashRendered, Change, PaymentMode, SaleDate)
                VALUES
                    (@transID,@totalItems,@totalQty,
                     @totalAmount,@cash,@change,@paymode,@date)";

                    SqlCommand sumCmd = new SqlCommand(summarySql, tConn, sqlTrans);
                    sumCmd.Parameters.AddWithValue("@transID", transactionID);
                    sumCmd.Parameters.AddWithValue("@totalItems", totalItems);
                    sumCmd.Parameters.AddWithValue("@totalQty", totalQuantity);
                    sumCmd.Parameters.AddWithValue("@totalAmount", amountToPay);
                    sumCmd.Parameters.AddWithValue("@cash", cashRendered);
                    sumCmd.Parameters.AddWithValue("@change", change);
                    sumCmd.Parameters.AddWithValue("@paymode", selectedPaymentMode);
                    sumCmd.Parameters.AddWithValue("@date", saleDateTime);
                    sumCmd.ExecuteNonQuery();

                    sqlTrans.Commit();
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    MessageBox.Show("Save to TransactionDB failed:\n" + ex.Message, "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 5. Deduct stock from InventoryDB 
            using (SqlConnection iConn = new SqlConnection(inventoryConn))
            {
                iConn.Open();
                SqlTransaction invTrans = iConn.BeginTransaction();
                try
                {
                    string deductSql = @"UPDATE InventoryTbl
                                 SET Quantity = Quantity - @qty
                                 WHERE ProductID = @pid AND Quantity >= @qty";
                    foreach (DataRow row in orderTable.Rows)
                    {
                        SqlCommand deductCmd = new SqlCommand(deductSql, iConn, invTrans);
                        deductCmd.Parameters.AddWithValue("@pid", row["ProductID"].ToString());
                        deductCmd.Parameters.AddWithValue("@qty", Convert.ToInt32(row["Quantity"]));
                        if (deductCmd.ExecuteNonQuery() == 0)
                        {
                            invTrans.Rollback();
                            MessageBox.Show($"Stock insufficient for: {row["ProductName"]}\nInventory not updated.",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    invTrans.Commit();
                }
                catch (Exception ex)
                {
                    invTrans.Rollback();
                    MessageBox.Show("Inventory deduction failed:\n" + ex.Message, "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 6. Success Message and UI Refresh
            MessageBox.Show(
                 $"Transaction Saved!\n\n" +
                 $"Transaction ID : {transactionID}\n" +
                 $"Amount to Pay  : ₱{amountToPay:N2}",
                 "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh or Show Form3
            Form3 txnForm = null;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is Form3 existing)
                {
                    txnForm = existing;
                    break;
                }
            }

            if (txnForm != null)
            {
                txnForm.RefreshData();
                txnForm.BringToFront();
            }
            else
            {
                txnForm = new Form3();
                txnForm.Show();
            }

            // Optional: Auto-clear fields after save
            clearbtn_Click(null, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }

}










































