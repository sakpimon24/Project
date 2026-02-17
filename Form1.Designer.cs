namespace Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AllData = new DataGridView();
            Logoname = new Label();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            ProductName = new TextBox();
            ProductType = new TextBox();
            ClearBtn = new Button();
            SlipBtn = new Button();
            Price = new TextBox();
            TotalProduct = new TextBox();
            ProductID = new ComboBox();
            EditBtn = new Button();
            DelBtn = new Button();
            AddBtn = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            CustomerName = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)AllData).BeginInit();
            SuspendLayout();
            // 
            // AllData
            // 
            AllData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AllData.Location = new Point(12, 66);
            AllData.Name = "AllData";
            AllData.Size = new Size(330, 419);
            AllData.TabIndex = 0;
            // 
            // Logoname
            // 
            Logoname.AutoSize = true;
            Logoname.BackColor = SystemColors.GradientActiveCaption;
            Logoname.BorderStyle = BorderStyle.FixedSingle;
            Logoname.Font = new Font("Impact", 30F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Logoname.ForeColor = SystemColors.MenuHighlight;
            Logoname.Location = new Point(72, 9);
            Logoname.Name = "Logoname";
            Logoname.Size = new Size(203, 50);
            Logoname.TabIndex = 1;
            Logoname.Text = "SON PI MAK";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(364, 137);
            label2.Name = "label2";
            label2.Size = new Size(63, 21);
            label2.TabIndex = 2;
            label2.Text = "ชื่อสินค้า";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(364, 274);
            label4.Name = "label4";
            label4.Size = new Size(87, 21);
            label4.TabIndex = 3;
            label4.Text = "จำนวนสินค้า";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(364, 208);
            label3.Name = "label3";
            label3.Size = new Size(40, 21);
            label3.TabIndex = 4;
            label3.Text = "ราคา";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(364, 66);
            label5.Name = "label5";
            label5.Size = new Size(70, 21);
            label5.TabIndex = 5;
            label5.Text = "รหัสสินค้า";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(364, 340);
            label6.Name = "label6";
            label6.Size = new Size(62, 21);
            label6.TabIndex = 6;
            label6.Text = "หมวดหมู่";
            // 
            // ProductName
            // 
            ProductName.Font = new Font("Segoe UI", 12F);
            ProductName.Location = new Point(465, 134);
            ProductName.Name = "ProductName";
            ProductName.Size = new Size(411, 29);
            ProductName.TabIndex = 8;
            // 
            // ProductType
            // 
            ProductType.Font = new Font("Segoe UI", 12F);
            ProductType.Location = new Point(465, 338);
            ProductType.Name = "ProductType";
            ProductType.Size = new Size(411, 29);
            ProductType.TabIndex = 11;
            // 
            // ClearBtn
            // 
            ClearBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ClearBtn.Location = new Point(506, 432);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(128, 44);
            ClearBtn.TabIndex = 18;
            ClearBtn.Text = "ล้างทั้งหมด";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // SlipBtn
            // 
            SlipBtn.BackColor = Color.Blue;
            SlipBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            SlipBtn.ForeColor = Color.White;
            SlipBtn.Location = new Point(668, 432);
            SlipBtn.Name = "SlipBtn";
            SlipBtn.Size = new Size(190, 44);
            SlipBtn.TabIndex = 21;
            SlipBtn.Text = "ออกใบเสร็จ";
            SlipBtn.UseVisualStyleBackColor = false;
            SlipBtn.Click += SlipBtn_Click;
            // 
            // Price
            // 
            Price.Font = new Font("Segoe UI", 12F);
            Price.Location = new Point(465, 205);
            Price.Name = "Price";
            Price.Size = new Size(411, 29);
            Price.TabIndex = 23;
            // 
            // TotalProduct
            // 
            TotalProduct.Font = new Font("Segoe UI", 12F);
            TotalProduct.Location = new Point(465, 271);
            TotalProduct.Name = "TotalProduct";
            TotalProduct.Size = new Size(411, 29);
            TotalProduct.TabIndex = 24;
            // 
            // ProductID
            // 
            ProductID.Font = new Font("Segoe UI", 12F);
            ProductID.FormattingEnabled = true;
            ProductID.Location = new Point(465, 63);
            ProductID.Name = "ProductID";
            ProductID.Size = new Size(412, 29);
            ProductID.TabIndex = 25;
            ProductID.SelectedIndexChanged += ProductID_SelectedIndexChanged;
            // 
            // EditBtn
            // 
            EditBtn.BackColor = Color.Yellow;
            EditBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            EditBtn.ForeColor = Color.Black;
            EditBtn.Location = new Point(372, 432);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(125, 44);
            EditBtn.TabIndex = 26;
            EditBtn.Text = "แก้ไข";
            EditBtn.UseVisualStyleBackColor = false;
            EditBtn.Click += EditBtn_Click;
            // 
            // DelBtn
            // 
            DelBtn.BackColor = Color.Red;
            DelBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            DelBtn.ForeColor = Color.White;
            DelBtn.Location = new Point(506, 382);
            DelBtn.Name = "DelBtn";
            DelBtn.Size = new Size(128, 44);
            DelBtn.TabIndex = 27;
            DelBtn.Text = "ลบ";
            DelBtn.UseVisualStyleBackColor = false;
            DelBtn.Click += DelBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.BackColor = Color.Lime;
            AddBtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            AddBtn.ForeColor = Color.Black;
            AddBtn.Location = new Point(372, 382);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(128, 44);
            AddBtn.TabIndex = 28;
            AddBtn.Text = "เพิ่ม";
            AddBtn.UseVisualStyleBackColor = false;
            AddBtn.Click += AddBtn_Click_1;
            // 
            // CustomerName
            // 
            CustomerName.Font = new Font("Segoe UI", 12F);
            CustomerName.Location = new Point(708, 390);
            CustomerName.Name = "CustomerName";
            CustomerName.Size = new Size(169, 29);
            CustomerName.TabIndex = 30;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(639, 393);
            label1.Name = "label1";
            label1.Size = new Size(62, 21);
            label1.TabIndex = 29;
            label1.Text = "ชื่อลูกค้า";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 497);
            Controls.Add(CustomerName);
            Controls.Add(label1);
            Controls.Add(AddBtn);
            Controls.Add(DelBtn);
            Controls.Add(EditBtn);
            Controls.Add(ProductID);
            Controls.Add(TotalProduct);
            Controls.Add(Price);
            Controls.Add(SlipBtn);
            Controls.Add(ClearBtn);
            Controls.Add(ProductType);
            Controls.Add(ProductName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(Logoname);
            Controls.Add(AllData);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)AllData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView AllData;
        private Label Logoname;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label5;
        private Label label6;
        private TextBox CustomerName;
        private TextBox ProductName;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox ProductType;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button ConfirmButton;
        private Button ClearBtn;
        private Button button7;
        private Button button8;
        private Button SlipBtn;
        private TextBox Price;
        private TextBox TotalProduct;
        private ComboBox ProductID;
        private Button EditBtn;
        private Button DelBtn;
        private Button AddBtn;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
    }
}
