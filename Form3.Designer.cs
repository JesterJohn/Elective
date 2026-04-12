namespace Elective
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Groupbox1 = new System.Windows.Forms.GroupBox();
            this.Sales_Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemsSold_Label = new System.Windows.Forms.Label();
            this.Transaction_datagrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.deletetransbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Search_textbox = new System.Windows.Forms.TextBox();
            this.exitbtn = new System.Windows.Forms.Button();
            this.Groupbox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Transaction_datagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Groupbox1
            // 
            this.Groupbox1.Controls.Add(this.exitbtn);
            this.Groupbox1.Controls.Add(this.Search_textbox);
            this.Groupbox1.Controls.Add(this.button1);
            this.Groupbox1.Controls.Add(this.deletetransbtn);
            this.Groupbox1.Controls.Add(this.Sales_Label);
            this.Groupbox1.Controls.Add(this.label3);
            this.Groupbox1.Controls.Add(this.ItemsSold_Label);
            this.Groupbox1.Controls.Add(this.Transaction_datagrid);
            this.Groupbox1.Controls.Add(this.label1);
            this.Groupbox1.Location = new System.Drawing.Point(12, 12);
            this.Groupbox1.Name = "Groupbox1";
            this.Groupbox1.Size = new System.Drawing.Size(2540, 1526);
            this.Groupbox1.TabIndex = 0;
            this.Groupbox1.TabStop = false;
            this.Groupbox1.Text = "TRANSACTION";
            // 
            // Sales_Label
            // 
            this.Sales_Label.AutoSize = true;
            this.Sales_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sales_Label.Location = new System.Drawing.Point(398, 158);
            this.Sales_Label.Name = "Sales_Label";
            this.Sales_Label.Size = new System.Drawing.Size(32, 46);
            this.Sales_Label.TabIndex = 4;
            this.Sales_Label.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(505, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 46);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sales:";
            // 
            // ItemsSold_Label
            // 
            this.ItemsSold_Label.AutoSize = true;
            this.ItemsSold_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsSold_Label.Location = new System.Drawing.Point(1521, 158);
            this.ItemsSold_Label.Name = "ItemsSold_Label";
            this.ItemsSold_Label.Size = new System.Drawing.Size(230, 46);
            this.ItemsSold_Label.TabIndex = 2;
            this.ItemsSold_Label.Text = "Items Sold:";
            // 
            // Transaction_datagrid
            // 
            this.Transaction_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Transaction_datagrid.Location = new System.Drawing.Point(6, 242);
            this.Transaction_datagrid.Name = "Transaction_datagrid";
            this.Transaction_datagrid.RowHeadersWidth = 62;
            this.Transaction_datagrid.RowTemplate.Height = 28;
            this.Transaction_datagrid.Size = new System.Drawing.Size(2528, 1204);
            this.Transaction_datagrid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(839, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(707, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "TRANSACTION REPORT ";
            // 
            // deletetransbtn
            // 
            this.deletetransbtn.Location = new System.Drawing.Point(1446, 1452);
            this.deletetransbtn.Name = "deletetransbtn";
            this.deletetransbtn.Size = new System.Drawing.Size(549, 56);
            this.deletetransbtn.TabIndex = 62;
            this.deletetransbtn.Text = "DELETE TRANSACTION";
            this.deletetransbtn.UseVisualStyleBackColor = true;
            this.deletetransbtn.Click += new System.EventHandler(this.transactionbtn_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 1452);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(501, 56);
            this.button1.TabIndex = 63;
            this.button1.Text = "SEARCH TRANSACTION ID";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Search_textbox
            // 
            this.Search_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search_textbox.Location = new System.Drawing.Point(513, 1460);
            this.Search_textbox.Name = "Search_textbox";
            this.Search_textbox.Size = new System.Drawing.Size(548, 48);
            this.Search_textbox.TabIndex = 64;
            // 
            // exitbtn
            // 
            this.exitbtn.Location = new System.Drawing.Point(2001, 1452);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(533, 56);
            this.exitbtn.TabIndex = 65;
            this.exitbtn.Text = "EXIT";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1558);
            this.Controls.Add(this.Groupbox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Groupbox1.ResumeLayout(false);
            this.Groupbox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Transaction_datagrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Groupbox1;
        private System.Windows.Forms.DataGridView Transaction_datagrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Sales_Label;
        private System.Windows.Forms.Label ItemsSold_Label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button deletetransbtn;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.TextBox Search_textbox;
    }
}