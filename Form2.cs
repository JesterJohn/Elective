using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace Elective
{
    public partial class Form2 : Form
    {
        DatabaseHelper db = new DatabaseHelper();
        string selectedImagePath = "";

        // ── Fixed barcode save folder ─────────────────────────────────────────────
        private readonly string barcodeFolderPath = @"C:\Users\Jester\source\repos\Elective\Barcode";

        public Form2()
        {
            InitializeComponent();
            RefreshGrid();
            picpath2.Hide();
        }

        private void RefreshGrid() => inveentorydata.DataSource = db.GetInventory();

        private void Form2_Load(object sender, EventArgs e) { }

        private void picpath2_TextChanged(object sender, EventArgs e) { }

        // ─────────────────────────────────────────────────────────────────────────
        // GENERATE BARCODE — generates, displays, AND saves to the fixed path
        // ─────────────────────────────────────────────────────────────────────────
        private void generatebarcodebtn_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Generate a unique random barcode number
                Random rnd = new Random();
                string barcodeData = rnd.Next(10000000, 99999999).ToString();
                Barcode_textbox.Text = barcodeData;

                // 2. Create the barcode image using ZXing
                var writer = new ZXing.Windows.Compatibility.BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.CODE_128,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = 100,
                        Width = 300,
                        Margin = 10
                    }
                };

                Bitmap barcodeBitmap = writer.Write(barcodeData);

                // 3. Display in the PictureBox
                barcode_picbox.Image = barcodeBitmap;
                barcode_picbox.SizeMode = PictureBoxSizeMode.Zoom;

                // 4. Ensure the save folder exists
                if (!Directory.Exists(barcodeFolderPath))
                    Directory.CreateDirectory(barcodeFolderPath);

                // 5. Save the barcode image as PNG
                //    File name = the barcode number, e.g. "12345678.png"
                string filePath = Path.Combine(barcodeFolderPath, barcodeData + ".png");
                barcodeBitmap.Save(filePath, ImageFormat.Png);

                // 6. Confirm to the user
                MessageBox.Show(
                    $"Barcode generated and saved!\n\nBarcode: {barcodeData}\nSaved to: {filePath}",
                    "Barcode Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error generating/saving barcode:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // ADD
        // ─────────────────────────────────────────────────────────────────────────
        private void addbtn_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(ProductID_Textbox.Text) ||
                string.IsNullOrWhiteSpace(ProductName_textbox.Text) ||
                string.IsNullOrWhiteSpace(Price_textbox.Text) ||
                string.IsNullOrWhiteSpace(Quantity_textbox.Text))
            {
                MessageBox.Show("Please fill in all required fields (ID, Name, Price, Quantity).",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(ProductID_Textbox.Text, out int pid))
            {
                MessageBox.Show("Product ID must be a number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(Price_textbox.Text, out decimal price))
            {
                MessageBox.Show("Price must be a valid number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(Quantity_textbox.Text, out int qty))
            {
                MessageBox.Show("Quantity must be a whole number.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO InventoryTbl VALUES (@id, @name, @price, @qty, @barcode, @img)";
            SqlParameter[] ps = {
                new SqlParameter("@id",      pid),
                new SqlParameter("@name",    ProductName_textbox.Text),
                new SqlParameter("@price",   price),
                new SqlParameter("@qty",     qty),
                new SqlParameter("@barcode", Barcode_textbox.Text),
                new SqlParameter("@img",     selectedImagePath)
            };

            if (db.ExecuteNonQuery(query, ps))
            {
                MessageBox.Show("Product added successfully!", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                clearbtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Failed to add product. Please check if the Product ID already exists.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // SEARCH
        // ─────────────────────────────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Search_textbox.Text))
            {
                MessageBox.Show("Please enter a Product ID to search.", "Search",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    string query = "SELECT * FROM InventoryTbl WHERE ProductID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", Search_textbox.Text);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ProductID_Textbox.Text = reader["ProductID"].ToString();
                        ProductName_textbox.Text = reader["ProductName"].ToString();
                        Price_textbox.Text = reader["Price"].ToString();
                        Quantity_textbox.Text = reader["Quantity"].ToString();

                        string barcodeText = reader["Barcode"].ToString();
                        Barcode_textbox.Text = barcodeText;

                        // Regenerate barcode image from the stored barcode value
                        if (!string.IsNullOrEmpty(barcodeText))
                        {
                            // First try to load the saved PNG from the Barcode folder
                            string savedBarcodePath = Path.Combine(barcodeFolderPath, barcodeText + ".png");
                            if (File.Exists(savedBarcodePath))
                            {
                                barcode_picbox.Image = Image.FromFile(savedBarcodePath);
                                barcode_picbox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                            else
                            {
                                // Fallback: regenerate from ZXing
                                var writer = new ZXing.Windows.Compatibility.BarcodeWriter
                                {
                                    Format = ZXing.BarcodeFormat.CODE_128,
                                    Options = new ZXing.Common.EncodingOptions { Height = 80, Width = 250, Margin = 10 }
                                };
                                barcode_picbox.Image = writer.Write(barcodeText);
                                barcode_picbox.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }

                        // Load product image
                        string imgPath = reader["ImagePath"]?.ToString();
                        if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                        {
                            product_picbox.Image = Image.FromFile(imgPath);
                            product_picbox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            product_picbox.Image = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product not found.", "Not Found",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearbtn_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during search: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // UPDATE
        // ─────────────────────────────────────────────────────────────────────────
        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductID_Textbox.Text))
            {
                MessageBox.Show("Please search for a product to update.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE InventoryTbl 
                             SET ProductName=@name, Price=@price, Quantity=@qty,
                                 Barcode=@barcode, ImagePath=@img
                             WHERE ProductID=@id";
            SqlParameter[] ps = {
                new SqlParameter("@id",      ProductID_Textbox.Text),
                new SqlParameter("@name",    ProductName_textbox.Text),
                new SqlParameter("@price",   Price_textbox.Text),
                new SqlParameter("@qty",     Quantity_textbox.Text),
                new SqlParameter("@barcode", Barcode_textbox.Text),
                new SqlParameter("@img",     selectedImagePath)
            };

            if (db.ExecuteNonQuery(query, ps))
            {
                MessageBox.Show("Product updated successfully!", "Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Update failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // DELETE
        // ─────────────────────────────────────────────────────────────────────────
        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductID_Textbox.Text))
            {
                MessageBox.Show("Please search for a product to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete Product ID: {ProductID_Textbox.Text}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM InventoryTbl WHERE ProductID=@id";
                SqlParameter[] ps = { new SqlParameter("@id", ProductID_Textbox.Text) };

                if (db.ExecuteNonQuery(query, ps))
                {
                    MessageBox.Show("Product deleted.", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                    clearbtn_Click(sender, e);
                }
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // CHOOSE FILE (product image)
        // ─────────────────────────────────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select Product Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = ofd.FileName;
                product_picbox.Image = Image.FromFile(selectedImagePath);
                product_picbox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // CLEAR
        // ─────────────────────────────────────────────────────────────────────────
        private void clearbtn_Click(object sender, EventArgs e)
        {
            ProductID_Textbox.Clear();
            ProductName_textbox.Clear();
            Price_textbox.Clear();
            Quantity_textbox.Clear();
            Barcode_textbox.Clear();
            Search_textbox.Clear();
            selectedImagePath = "";

            barcode_picbox.Image = null;
            product_picbox.Image = null;

            ProductID_Textbox.Focus();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // EXIT
        // ─────────────────────────────────────────────────────────────────────────
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ─────────────────────────────────────────────────────────────────────────
        // STUBS
        // ─────────────────────────────────────────────────────────────────────────
        private void inveentorydata_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}