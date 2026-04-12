namespace Elective
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clearbtn = new System.Windows.Forms.Button();
            this.generatebarcodebtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Search_textbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Barcode_textbox = new System.Windows.Forms.TextBox();
            this.Quantity_textbox = new System.Windows.Forms.TextBox();
            this.Price_textbox = new System.Windows.Forms.TextBox();
            this.ProductName_textbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ProductID_Textbox = new System.Windows.Forms.TextBox();
            this.exitbtn = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            this.updatebtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.picpath2 = new System.Windows.Forms.TextBox();
            this.product_picbox = new System.Windows.Forms.PictureBox();
            this.barcode_picbox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.inveentorydata = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.product_picbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcode_picbox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inveentorydata)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearbtn);
            this.groupBox1.Controls.Add(this.generatebarcodebtn);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Search_textbox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.Barcode_textbox);
            this.groupBox1.Controls.Add(this.Quantity_textbox);
            this.groupBox1.Controls.Add(this.Price_textbox);
            this.groupBox1.Controls.Add(this.ProductName_textbox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ProductID_Textbox);
            this.groupBox1.Controls.Add(this.exitbtn);
            this.groupBox1.Controls.Add(this.deletebtn);
            this.groupBox1.Controls.Add(this.updatebtn);
            this.groupBox1.Controls.Add(this.addbtn);
            this.groupBox1.Controls.Add(this.picpath2);
            this.groupBox1.Controls.Add(this.product_picbox);
            this.groupBox1.Controls.Add(this.barcode_picbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1402, 1534);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INVENTORY";
            // 
            // clearbtn
            // 
            this.clearbtn.Location = new System.Drawing.Point(36, 938);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(463, 81);
            this.clearbtn.TabIndex = 109;
            this.clearbtn.Text = "CLEAR";
            this.clearbtn.UseVisualStyleBackColor = true;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // generatebarcodebtn
            // 
            this.generatebarcodebtn.Location = new System.Drawing.Point(185, 762);
            this.generatebarcodebtn.Name = "generatebarcodebtn";
            this.generatebarcodebtn.Size = new System.Drawing.Size(314, 81);
            this.generatebarcodebtn.TabIndex = 108;
            this.generatebarcodebtn.Text = "GENERATE BARCODE";
            this.generatebarcodebtn.UseVisualStyleBackColor = true;
            this.generatebarcodebtn.Click += new System.EventHandler(this.generatebarcodebtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(834, 761);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(394, 82);
            this.button2.TabIndex = 106;
            this.button2.Text = "CHOOSE FILE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 1040);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 46);
            this.label2.TabIndex = 105;
            this.label2.Text = "Search ID:";
            // 
            // Search_textbox
            // 
            this.Search_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search_textbox.Location = new System.Drawing.Point(366, 1037);
            this.Search_textbox.Name = "Search_textbox";
            this.Search_textbox.Size = new System.Drawing.Size(739, 53);
            this.Search_textbox.TabIndex = 104;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1110, 1037);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 56);
            this.button1.TabIndex = 103;
            this.button1.Text = "SEARCH";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Barcode_textbox
            // 
            this.Barcode_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcode_textbox.Location = new System.Drawing.Point(366, 1434);
            this.Barcode_textbox.Name = "Barcode_textbox";
            this.Barcode_textbox.Size = new System.Drawing.Size(1011, 53);
            this.Barcode_textbox.TabIndex = 102;
            // 
            // Quantity_textbox
            // 
            this.Quantity_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity_textbox.Location = new System.Drawing.Point(366, 1354);
            this.Quantity_textbox.Name = "Quantity_textbox";
            this.Quantity_textbox.Size = new System.Drawing.Size(1011, 53);
            this.Quantity_textbox.TabIndex = 101;
            // 
            // Price_textbox
            // 
            this.Price_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Price_textbox.Location = new System.Drawing.Point(366, 1273);
            this.Price_textbox.Name = "Price_textbox";
            this.Price_textbox.Size = new System.Drawing.Size(1011, 53);
            this.Price_textbox.TabIndex = 100;
            // 
            // ProductName_textbox
            // 
            this.ProductName_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductName_textbox.Location = new System.Drawing.Point(366, 1193);
            this.ProductName_textbox.Name = "ProductName_textbox";
            this.ProductName_textbox.Size = new System.Drawing.Size(1011, 53);
            this.ProductName_textbox.TabIndex = 99;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(28, 1437);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 46);
            this.label8.TabIndex = 98;
            this.label8.Text = "Barcode:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 1357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 46);
            this.label4.TabIndex = 97;
            this.label4.Text = "Quantity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 1276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 46);
            this.label3.TabIndex = 96;
            this.label3.Text = "Price:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 1196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 46);
            this.label1.TabIndex = 95;
            this.label1.Text = "Product Name: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 1123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 46);
            this.label5.TabIndex = 94;
            this.label5.Text = "Product ID:";
            // 
            // ProductID_Textbox
            // 
            this.ProductID_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductID_Textbox.Location = new System.Drawing.Point(366, 1120);
            this.ProductID_Textbox.Name = "ProductID_Textbox";
            this.ProductID_Textbox.Size = new System.Drawing.Size(1011, 53);
            this.ProductID_Textbox.TabIndex = 93;
            // 
            // exitbtn
            // 
            this.exitbtn.Location = new System.Drawing.Point(910, 938);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(467, 82);
            this.exitbtn.TabIndex = 92;
            this.exitbtn.Text = "EXIT";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(505, 939);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(399, 81);
            this.deletebtn.TabIndex = 91;
            this.deletebtn.Text = "DELETE";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // updatebtn
            // 
            this.updatebtn.Location = new System.Drawing.Point(704, 851);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(673, 81);
            this.updatebtn.TabIndex = 90;
            this.updatebtn.Text = "UPDATE";
            this.updatebtn.UseVisualStyleBackColor = true;
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.Location = new System.Drawing.Point(36, 849);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(662, 81);
            this.addbtn.TabIndex = 88;
            this.addbtn.Text = "ADD";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // picpath2
            // 
            this.picpath2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picpath2.Location = new System.Drawing.Point(834, 694);
            this.picpath2.Name = "picpath2";
            this.picpath2.Size = new System.Drawing.Size(394, 35);
            this.picpath2.TabIndex = 86;
            this.picpath2.TextChanged += new System.EventHandler(this.picpath2_TextChanged);
            // 
            // product_picbox
            // 
            this.product_picbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.product_picbox.Location = new System.Drawing.Point(704, 137);
            this.product_picbox.Name = "product_picbox";
            this.product_picbox.Size = new System.Drawing.Size(673, 611);
            this.product_picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.product_picbox.TabIndex = 85;
            this.product_picbox.TabStop = false;
            // 
            // barcode_picbox
            // 
            this.barcode_picbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.barcode_picbox.Location = new System.Drawing.Point(36, 137);
            this.barcode_picbox.Name = "barcode_picbox";
            this.barcode_picbox.Size = new System.Drawing.Size(662, 611);
            this.barcode_picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.barcode_picbox.TabIndex = 60;
            this.barcode_picbox.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.inveentorydata);
            this.groupBox2.Location = new System.Drawing.Point(1420, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1132, 1534);
            this.groupBox2.TabIndex = 93;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "INVENTORY";
            // 
            // inveentorydata
            // 
            this.inveentorydata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inveentorydata.Location = new System.Drawing.Point(6, 25);
            this.inveentorydata.Name = "inveentorydata";
            this.inveentorydata.RowHeadersWidth = 62;
            this.inveentorydata.RowTemplate.Height = 28;
            this.inveentorydata.Size = new System.Drawing.Size(1120, 1475);
            this.inveentorydata.TabIndex = 0;
            this.inveentorydata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inveentorydata_CellContentClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1558);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.product_picbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcode_picbox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inveentorydata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox barcode_picbox;
        private System.Windows.Forms.TextBox picpath2;
        private System.Windows.Forms.PictureBox product_picbox;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.Button deletebtn;
        private System.Windows.Forms.Button updatebtn;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView inveentorydata;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Search_textbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Barcode_textbox;
        private System.Windows.Forms.TextBox Quantity_textbox;
        private System.Windows.Forms.TextBox Price_textbox;
        private System.Windows.Forms.TextBox ProductName_textbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ProductID_Textbox;
        private System.Windows.Forms.Button generatebarcodebtn;
        private System.Windows.Forms.Button clearbtn;
    }
}