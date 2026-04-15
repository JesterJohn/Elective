using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Elective
{
    public partial class Form4_Registration : Form
    {
        string connectionString = @"Data Source=LAPTOP-9QMQALA3\SQLEXPRESS;Initial Catalog=EmployeeDB;Integrated Security=True";

        public Form4_Registration()
        {
            InitializeComponent();
        }

        private void Form4_Registration_Load(object sender, EventArgs e)
        {
            genter_combobox.Items.AddRange(new string[] { "Male", "Female" });
            civilstat_combobox.Items.AddRange(new string[] { "Single", "Married", "Widowed", "Separated", "Annulled" });
            employmenttype_combobox.Items.AddRange(new string[] { "Regular", "Probationary", "Contractual", "Project-Based", "Seasonal", "Part-Time", "Agency-Hired", "OJT / Intern", "Consultant" });
            employeetype_combobox.Items.AddRange(new string[] { "Supervisor", "Manager", "Senior Manager", "Director", "Executive" });
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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
            genter_combobox.SelectedIndex = -1;
            civilstat_combobox.SelectedIndex = -1;
            email_textbox.Clear();
            department_textbox.Clear();
            dateTimePicker1.Value = DateTime.Today;
            contactno_textbox.Clear();
            emgname_textbox.Clear();
            emgcontactno_textbox.Clear();
            nationality_textbox.Clear();
            position_textbox.Clear();
            employmenttype_combobox.SelectedIndex = -1;
            employeetype_combobox.SelectedIndex = -1;
            dateTimePicker2.Value = DateTime.Today;
            sss_textbox.Clear();
            philhealth_textbox.Clear();
            pagibig_textbox.Clear();
            tin_textbox.Clear();
            picbox.Image = null;
        }

        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@FN", fname_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@MN", mname_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@SN", surname_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Addr", address_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Age", int.TryParse(age_textbox.Text, out int age) ? age : 0);
            cmd.Parameters.AddWithValue("@Gen", genter_combobox.SelectedItem?.ToString() ?? "");
            cmd.Parameters.AddWithValue("@Stat", civilstat_combobox.SelectedItem?.ToString() ?? "");
            cmd.Parameters.AddWithValue("@Email", email_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Dept", department_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@ContactNum", contactno_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@EmerName", emgname_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@EmerContact", emgcontactno_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Nationality", nationality_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Position", position_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@EmpType", employmenttype_combobox.SelectedItem?.ToString() ?? "");
            cmd.Parameters.AddWithValue("@EmpTypeClass", employeetype_combobox.SelectedItem?.ToString() ?? "");
            cmd.Parameters.AddWithValue("@DateHired", dateTimePicker2.Value.Date);
            cmd.Parameters.AddWithValue("@SSS", sss_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@PhilHealth", philhealth_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@PagIbig", pagibig_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@TIN", tin_textbox.Text.Trim());
            cmd.Parameters.AddWithValue("@Img", picbox.ImageLocation ?? "");
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emp_id_textbox.Text))
            {
                MessageBox.Show("Please enter an Employee ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Employees 
                        (EmployeeID, FirstName, MiddleName, Surname, Address, Age, Gender, CivilStatus,
                         Email, Department, DateOfBirth, ContactNumber, EmergencyName, EmergencyContact,
                         Nationality, Position, EmploymentType, EmployeeType, DateHired,
                         SSSNumber, PhilHealthNumber, PagIbigNumber, TINNumber, ImagePath)
                        VALUES 
                        (@ID, @FN, @MN, @SN, @Addr, @Age, @Gen, @Stat,
                         @Email, @Dept, @DOB, @ContactNum, @EmerName, @EmerContact,
                         @Nationality, @Position, @EmpType, @EmpTypeClass, @DateHired,
                         @SSS, @PhilHealth, @PagIbig, @TIN, @Img)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    AddParameters(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emp_id_textbox.Text))
            {
                MessageBox.Show("Please enter an Employee ID to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Employees SET
                        FirstName=@FN, MiddleName=@MN, Surname=@SN, Address=@Addr, Age=@Age,
                        Gender=@Gen, CivilStatus=@Stat, Email=@Email, Department=@Dept,
                        DateOfBirth=@DOB, ContactNumber=@ContactNum, EmergencyName=@EmerName,
                        EmergencyContact=@EmerContact, Nationality=@Nationality, Position=@Position,
                        EmploymentType=@EmpType, EmployeeType=@EmpTypeClass, DateHired=@DateHired,
                        SSSNumber=@SSS, PhilHealthNumber=@PhilHealth, PagIbigNumber=@PagIbig,
                        TINNumber=@TIN, ImagePath=@Img
                        WHERE EmployeeID=@ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    AddParameters(cmd);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Employee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emp_id_textbox.Text))
            {
                MessageBox.Show("Please enter an Employee ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text.Trim());
                    int exists = (int)checkCmd.ExecuteScalar();

                    string finalQuery;
                    if (exists > 0)
                    {
                        finalQuery = @"UPDATE Employees SET
                            FirstName=@FN, MiddleName=@MN, Surname=@SN, Address=@Addr, Age=@Age,
                            Gender=@Gen, CivilStatus=@Stat, Email=@Email, Department=@Dept,
                            DateOfBirth=@DOB, ContactNumber=@ContactNum, EmergencyName=@EmerName,
                            EmergencyContact=@EmerContact, Nationality=@Nationality, Position=@Position,
                            EmploymentType=@EmpType, EmployeeType=@EmpTypeClass, DateHired=@DateHired,
                            SSSNumber=@SSS, PhilHealthNumber=@PhilHealth, PagIbigNumber=@PagIbig,
                            TINNumber=@TIN, ImagePath=@Img
                            WHERE EmployeeID=@ID";
                    }
                    else
                    {
                        finalQuery = @"INSERT INTO Employees 
                            (EmployeeID, FirstName, MiddleName, Surname, Address, Age, Gender, CivilStatus,
                             Email, Department, DateOfBirth, ContactNumber, EmergencyName, EmergencyContact,
                             Nationality, Position, EmploymentType, EmployeeType, DateHired,
                             SSSNumber, PhilHealthNumber, PagIbigNumber, TINNumber, ImagePath)
                            VALUES 
                            (@ID, @FN, @MN, @SN, @Addr, @Age, @Gen, @Stat,
                             @Email, @Dept, @DOB, @ContactNum, @EmerName, @EmerContact,
                             @Nationality, @Position, @EmpType, @EmpTypeClass, @DateHired,
                             @SSS, @PhilHealth, @PagIbig, @TIN, @Img)";
                    }

                    SqlCommand cmd = new SqlCommand(finalQuery, conn);
                    AddParameters(cmd);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emp_id_textbox.Text))
            {
                MessageBox.Show("Please enter an Employee ID to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);

                // IMPORTANT: Trim input to avoid mismatch
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = emp_id_textbox.Text.Trim();

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            fname_textbox.Text = reader["FirstName"]?.ToString();
                            mname_textbox.Text = reader["MiddleName"]?.ToString();
                            surname_textbox.Text = reader["Surname"]?.ToString();
                            address_textbox.Text = reader["Address"]?.ToString();
                            age_textbox.Text = reader["Age"]?.ToString();

                            genter_combobox.SelectedItem = reader["Gender"]?.ToString();
                            civilstat_combobox.SelectedItem = reader["CivilStatus"]?.ToString();

                            email_textbox.Text = reader["Email"]?.ToString();
                            department_textbox.Text = reader["Department"]?.ToString();
                            contactno_textbox.Text = reader["ContactNumber"]?.ToString();
                            emgname_textbox.Text = reader["EmergencyName"]?.ToString();
                            emgcontactno_textbox.Text = reader["EmergencyContact"]?.ToString();
                            nationality_textbox.Text = reader["Nationality"]?.ToString();
                            position_textbox.Text = reader["Position"]?.ToString();

                            employmenttype_combobox.SelectedItem = reader["EmploymentType"]?.ToString();
                            employeetype_combobox.SelectedItem = reader["EmployeeType"]?.ToString();

                            sss_textbox.Text = reader["SSSNumber"]?.ToString();
                            philhealth_textbox.Text = reader["PhilHealthNumber"]?.ToString();
                            pagibig_textbox.Text = reader["PagIbigNumber"]?.ToString();
                            tin_textbox.Text = reader["TINNumber"]?.ToString();

                            // Dates (safe handling)
                            if (reader["DateOfBirth"] != DBNull.Value)
                                dateTimePicker1.Value = Convert.ToDateTime(reader["DateOfBirth"]);

                            if (reader["DateHired"] != DBNull.Value)
                                dateTimePicker2.Value = Convert.ToDateTime(reader["DateHired"]);

                            // Image handling
                            string imgPath = reader["ImagePath"]?.ToString();
                            if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                            {
                                picbox.ImageLocation = imgPath;
                                picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                            else
                            {
                                picbox.Image = null;
                            }

                            emp_id_textbox.ReadOnly = true;

                            MessageBox.Show("Record Found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Employee ID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ClearFields();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search Error: " + ex.Message);
                }
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emp_id_textbox.Text))
            {
                MessageBox.Show("Please enter an Employee ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employees WHERE EmployeeID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", emp_id_textbox.Text.Trim());
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Employee deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Employee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picbox.ImageLocation = ofd.FileName;
                picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void scan_btn_Click(object sender, EventArgs e)
        {
            // Reserved for future scanner integration
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

        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void generateid_btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // This query gets the maximum ID currently in the table
                    // We assume a format like "EMP-1001" or just numeric strings
                    string query = "SELECT TOP 1 EmployeeID FROM Employees ORDER BY EmployeeID DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string lastId = result.ToString();

                        // Logic to extract numbers from the string
                        string numericPart = "";
                        foreach (char c in lastId)
                        {
                            if (char.IsDigit(c)) numericPart += c;
                        }

                        if (int.TryParse(numericPart, out int lastNumber))
                        {
                            // Increment the number and format it (e.g., EMP-1001 -> EMP-1002)
                            int nextNumber = lastNumber + 1;

                            // This maintains a 4-digit padding (0001, 0002, etc.)
                            // Adjust "EMP-" to whatever prefix you prefer or remove it for plain numbers
                            emp_id_textbox.Text =  nextNumber.ToString("D4");
                        }
                    }
                    else
                    {
                        // If the table is empty, start with the first ID
                        emp_id_textbox.Text = "1001";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}