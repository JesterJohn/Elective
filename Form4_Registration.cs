using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Elective
{
    public partial class Form4_Registration : Form
    {
        // Update this connection string with your SQL Server details
        string connectionString = @"Data Source=LAPTOP-9QMQALA3\SQLEXPRESS;Initial Catalog= EmployeeDB;Integrated Security=True";

        public Form4_Registration()
        {
            InitializeComponent();
        }

        private void Form4_Registration_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // --- DATABASE OPERATIONS ---

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt; // Assuming your DataGridView is named dataGridView1
            }
        }



        private void ClearFields()
        {
            emp_id_textbox.Clear();
            fname_textbox.Clear();
            mname_textbox.Clear();
            surname_textbox.Clear();
            address_textbox.Clear();
            age_textbox.Clear();
            gender_textbox.Clear();
            status_textbox.Clear();
            email_textbox.Clear();
            department_textbox.Clear();
            picbox.Image = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)

        {



        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg;*.png)|*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picbox.ImageLocation = ofd.FileName;
                picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employees WHERE EmployeeID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fname_textbox.Text = reader["FirstName"].ToString();
                    mname_textbox.Text = reader["MiddleName"].ToString();
                    surname_textbox.Text = reader["Surname"].ToString();
                    address_textbox.Text = reader["Address"].ToString();
                    age_textbox.Text = reader["Age"].ToString();
                    gender_textbox.Text = reader["Gender"].ToString();
                    status_textbox.Text = reader["Status"].ToString();
                    email_textbox.Text = reader["Email"].ToString();
                    department_textbox.Text = reader["Department"].ToString();

                    if (File.Exists(reader["ImagePath"].ToString()))
                        picbox.ImageLocation = reader["ImagePath"].ToString();
                }
                else
                {
                    MessageBox.Show("Employee not found.");
                }
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employees VALUES (@ID, @FN, @MN, @SN, @Addr, @Age, @Gen, @Stat, @Email, @Dept, @Img)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);
                    cmd.Parameters.AddWithValue("@FN", fname_textbox.Text);
                    cmd.Parameters.AddWithValue("@MN", mname_textbox.Text);
                    cmd.Parameters.AddWithValue("@SN", surname_textbox.Text);
                    cmd.Parameters.AddWithValue("@Addr", address_textbox.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(age_textbox.Text));
                    cmd.Parameters.AddWithValue("@Gen", gender_textbox.Text);
                    cmd.Parameters.AddWithValue("@Stat", status_textbox.Text);
                    cmd.Parameters.AddWithValue("@Email", email_textbox.Text);
                    cmd.Parameters.AddWithValue("@Dept", department_textbox.Text);
                    cmd.Parameters.AddWithValue("@Img", picbox.ImageLocation ?? "");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Registered Successfully!");
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(emp_id_textbox.Text))
                {
                    MessageBox.Show("Please enter an Employee ID to update.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Employees SET 
                            FirstName = @FN, 
                            MiddleName = @MN, 
                            Surname = @SN, 
                            Address = @Addr, 
                            Age = @Age, 
                            Gender = @Gen, 
                            Status = @Stat, 
                            Email = @Email, 
                            Department = @Dept, 
                            ImagePath = @Img 
                            WHERE EmployeeID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);
                    cmd.Parameters.AddWithValue("@FN", fname_textbox.Text);
                    cmd.Parameters.AddWithValue("@MN", mname_textbox.Text);
                    cmd.Parameters.AddWithValue("@SN", surname_textbox.Text);
                    cmd.Parameters.AddWithValue("@Addr", address_textbox.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(age_textbox.Text));
                    cmd.Parameters.AddWithValue("@Gen", gender_textbox.Text);
                    cmd.Parameters.AddWithValue("@Stat", status_textbox.Text);
                    cmd.Parameters.AddWithValue("@Email", email_textbox.Text);
                    cmd.Parameters.AddWithValue("@Dept", department_textbox.Text);
                    cmd.Parameters.AddWithValue("@Img", picbox.ImageLocation ?? "");

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee updated successfully!");
                        LoadData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Employee ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if record exists
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);
                    int exists = (int)checkCmd.ExecuteScalar();

                    string finalQuery;
                    if (exists > 0)
                    {
                        // Logic for Update
                        finalQuery = @"UPDATE Employees SET FirstName=@FN, MiddleName=@MN, Surname=@SN, Address=@Addr, 
                               Age=@Age, Gender=@Gen, Status=@Stat, Email=@Email, Department=@Dept, ImagePath=@Img 
                               WHERE EmployeeID=@ID";
                    }
                    else
                    {
                        // Logic for Insert
                        finalQuery = @"INSERT INTO Employees (EmployeeID, FirstName, MiddleName, Surname, Address, Age, Gender, Status, Email, Department, ImagePath) 
                               VALUES (@ID, @FN, @MN, @SN, @Addr, @Age, @Gen, @Stat, @Email, @Dept, @Img)";
                    }

                    SqlCommand cmd = new SqlCommand(finalQuery, conn);
                    cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text);
                    cmd.Parameters.AddWithValue("@FN", fname_textbox.Text);
                    cmd.Parameters.AddWithValue("@MN", mname_textbox.Text);
                    cmd.Parameters.AddWithValue("@SN", surname_textbox.Text);
                    cmd.Parameters.AddWithValue("@Addr", address_textbox.Text);
                    cmd.Parameters.AddWithValue("@Age", int.TryParse(age_textbox.Text, out int age) ? age : 0);
                    cmd.Parameters.AddWithValue("@Gen", gender_textbox.Text);
                    cmd.Parameters.AddWithValue("@Stat", status_textbox.Text);
                    cmd.Parameters.AddWithValue("@Email", email_textbox.Text);
                    cmd.Parameters.AddWithValue("@Dept", department_textbox.Text);
                    cmd.Parameters.AddWithValue("@Img", picbox.ImageLocation ?? "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record saved successfully!");
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void attendance_btn_Click(object sender, EventArgs e)
        {
            Form4 attendance = new Form4();
            attendance.ShowDialog();
        }

        private void attendancereport_btn_Click(object sender, EventArgs e)
        {
            Form4_Reports reports = new Form4_Reports();
            reports.ShowDialog();
        }
    }
}