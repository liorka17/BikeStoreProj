namespace BikeStoreProj
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;


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
            BikeTypeBox = new ComboBox();
            BikeSizeBox = new ComboBox();
            BikeColorBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            FirstName = new TextBox();
            LastName = new TextBox();
            PhoneNumber = new TextBox();
            Adress = new TextBox();
            EmailBox = new TextBox();
            label5 = new Label();
            AddOrder = new Button();
            ShowCusOrderHistoryBtn = new Button();
            ShowYearlySalesBtn = new Button();
            SubmitOrderDetailsBtn = new Button();
            SalesPerDayBtn = new Button();
            dataGridViewMain = new DataGridView();
            BikeStoreLabel = new Label();
            QuantitySelector = new NumericUpDown();
            QuantityLabel = new Label();
            label6 = new Label();
            YearComboBox = new ComboBox();
            SalesDatePicker = new DateTimePicker();
            OrdersHistoPanel = new Panel();
            BikeDetailsPanel = new Panel();
            CustomersInfoPanel = new Panel();
            SubmitsPanel = new Panel();
            inventoryBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuantitySelector).BeginInit();
            OrdersHistoPanel.SuspendLayout();
            BikeDetailsPanel.SuspendLayout();
            CustomersInfoPanel.SuspendLayout();
            SubmitsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // BikeTypeBox
            // 
            BikeTypeBox.BackColor = SystemColors.Control;
            BikeTypeBox.FormattingEnabled = true;
            BikeTypeBox.Location = new Point(0, 56);
            BikeTypeBox.Margin = new Padding(3, 4, 3, 4);
            BikeTypeBox.Name = "BikeTypeBox";
            BikeTypeBox.Size = new Size(149, 33);
            BikeTypeBox.TabIndex = 0;
            BikeTypeBox.SelectedIndexChanged += BikeTypeBox_SelectedIndexChanged;
            // 
            // BikeSizeBox
            // 
            BikeSizeBox.BackColor = SystemColors.Control;
            BikeSizeBox.FormattingEnabled = true;
            BikeSizeBox.Location = new Point(155, 57);
            BikeSizeBox.Margin = new Padding(3, 4, 3, 4);
            BikeSizeBox.Name = "BikeSizeBox";
            BikeSizeBox.Size = new Size(160, 33);
            BikeSizeBox.TabIndex = 1;
            BikeSizeBox.SelectedIndexChanged += BikeSizeBox_SelectedIndexChanged;
            // 
            // BikeColorBox
            // 
            BikeColorBox.BackColor = SystemColors.Control;
            BikeColorBox.FormattingEnabled = true;
            BikeColorBox.Location = new Point(320, 56);
            BikeColorBox.Margin = new Padding(3, 4, 3, 4);
            BikeColorBox.Name = "BikeColorBox";
            BikeColorBox.Size = new Size(160, 33);
            BikeColorBox.TabIndex = 2;
            BikeColorBox.SelectedIndexChanged += BikeColorBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(111, 29);
            label1.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Salmon;
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Arial Black", 14F, FontStyle.Bold);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(165, 10);
            label2.Name = "label2";
            label2.Size = new Size(160, 42);
            label2.TabIndex = 4;
            label2.Text = "Bike Size";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Salmon;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Arial Black", 14F, FontStyle.Bold);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(331, 10);
            label3.Name = "label3";
            label3.Size = new Size(177, 42);
            label3.TabIndex = 5;
            label3.Text = "Bike Color";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Top;
            label4.Font = new Font("Arial", 16F, FontStyle.Bold);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(258, 37);
            label4.TabIndex = 6;
            label4.Text = "Customers Info'";
            label4.Click += label4_Click;
            // 
            // FirstName
            // 
            FirstName.BackColor = Color.Salmon;
            FirstName.Font = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);
            FirstName.ForeColor = SystemColors.Window;
            FirstName.Location = new Point(0, 65);
            FirstName.Margin = new Padding(3, 4, 3, 4);
            FirstName.Name = "FirstName";
            FirstName.Size = new Size(659, 40);
            FirstName.TabIndex = 7;
            FirstName.Text = "First Name";
            FirstName.TextChanged += FirstName_TextChanged;
            // 
            // LastName
            // 
            LastName.BackColor = Color.Salmon;
            LastName.Font = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);
            LastName.ForeColor = SystemColors.Window;
            LastName.Location = new Point(0, 104);
            LastName.Margin = new Padding(3, 4, 3, 4);
            LastName.Name = "LastName";
            LastName.Size = new Size(659, 40);
            LastName.TabIndex = 8;
            LastName.Text = "Last Name";
            LastName.TextChanged += LastName_TextChanged;
            // 
            // PhoneNumber
            // 
            PhoneNumber.BackColor = Color.Salmon;
            PhoneNumber.Font = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);
            PhoneNumber.ForeColor = SystemColors.Window;
            PhoneNumber.Location = new Point(0, 145);
            PhoneNumber.Margin = new Padding(3, 4, 3, 4);
            PhoneNumber.Name = "PhoneNumber";
            PhoneNumber.Size = new Size(654, 40);
            PhoneNumber.TabIndex = 9;
            PhoneNumber.Text = "Phone Number";
            PhoneNumber.TextChanged += PhoneNumber_TextChanged;
            // 
            // Adress
            // 
            Adress.BackColor = Color.Salmon;
            Adress.Font = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);
            Adress.ForeColor = SystemColors.Window;
            Adress.Location = new Point(0, 224);
            Adress.Margin = new Padding(3, 4, 3, 4);
            Adress.Name = "Adress";
            Adress.Size = new Size(657, 40);
            Adress.TabIndex = 10;
            Adress.Text = "Adress";
            Adress.TextChanged += Adress_TextChanged;
            // 
            // EmailBox
            // 
            EmailBox.BackColor = Color.Salmon;
            EmailBox.Font = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);
            EmailBox.ForeColor = SystemColors.Window;
            EmailBox.Location = new Point(0, 184);
            EmailBox.Margin = new Padding(3, 4, 3, 4);
            EmailBox.Name = "EmailBox";
            EmailBox.Size = new Size(657, 40);
            EmailBox.TabIndex = 11;
            EmailBox.Text = "Email";
            EmailBox.TextChanged += EmailBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Salmon;
            label5.BorderStyle = BorderStyle.Fixed3D;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Arial Black", 14F, FontStyle.Bold);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(3, 10);
            label5.Name = "label5";
            label5.Size = new Size(171, 42);
            label5.TabIndex = 14;
            label5.Text = "Bike Type";
            label5.Click += label5_Click_1;
            // 
            // AddOrder
            // 
            AddOrder.BackColor = Color.Salmon;
            AddOrder.Dock = DockStyle.Top;
            AddOrder.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            AddOrder.ForeColor = SystemColors.Control;
            AddOrder.Location = new Point(0, 0);
            AddOrder.Name = "AddOrder";
            AddOrder.Size = new Size(446, 119);
            AddOrder.TabIndex = 15;
            AddOrder.Text = "Preview order";
            AddOrder.UseVisualStyleBackColor = false;
            AddOrder.Click += PreviewOrderBeforeSubmitingBtn_Click;
            // 
            // ShowCusOrderHistoryBtn
            // 
            ShowCusOrderHistoryBtn.BackColor = Color.Salmon;
            ShowCusOrderHistoryBtn.Dock = DockStyle.Bottom;
            ShowCusOrderHistoryBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowCusOrderHistoryBtn.ForeColor = SystemColors.Control;
            ShowCusOrderHistoryBtn.Location = new Point(0, 213);
            ShowCusOrderHistoryBtn.Name = "ShowCusOrderHistoryBtn";
            ShowCusOrderHistoryBtn.Size = new Size(446, 113);
            ShowCusOrderHistoryBtn.TabIndex = 16;
            ShowCusOrderHistoryBtn.Text = "Show Customer Orders History";
            ShowCusOrderHistoryBtn.UseVisualStyleBackColor = false;
            ShowCusOrderHistoryBtn.Click += ShowCusOrderHistoryBtn_Click;
            // 
            // ShowYearlySalesBtn
            // 
            ShowYearlySalesBtn.BackColor = Color.Salmon;
            ShowYearlySalesBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ShowYearlySalesBtn.ForeColor = SystemColors.Control;
            ShowYearlySalesBtn.Location = new Point(0, 168);
            ShowYearlySalesBtn.Name = "ShowYearlySalesBtn";
            ShowYearlySalesBtn.Size = new Size(413, 101);
            ShowYearlySalesBtn.TabIndex = 17;
            ShowYearlySalesBtn.Text = "Show Yearly Sales";
            ShowYearlySalesBtn.UseVisualStyleBackColor = false;
            ShowYearlySalesBtn.Click += ShowYearlySalesBtn_Click;
            // 
            // SubmitOrderDetailsBtn
            // 
            SubmitOrderDetailsBtn.BackColor = Color.Salmon;
            SubmitOrderDetailsBtn.Dock = DockStyle.Fill;
            SubmitOrderDetailsBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            SubmitOrderDetailsBtn.ForeColor = SystemColors.Control;
            SubmitOrderDetailsBtn.Location = new Point(0, 119);
            SubmitOrderDetailsBtn.Name = "SubmitOrderDetailsBtn";
            SubmitOrderDetailsBtn.Size = new Size(446, 94);
            SubmitOrderDetailsBtn.TabIndex = 18;
            SubmitOrderDetailsBtn.Text = "Submit Order Details";
            SubmitOrderDetailsBtn.UseVisualStyleBackColor = false;
            SubmitOrderDetailsBtn.Click += SubmitOrderDetailsBtn_Click;
            // 
            // SalesPerDayBtn
            // 
            SalesPerDayBtn.BackColor = Color.Salmon;
            SalesPerDayBtn.Dock = DockStyle.Top;
            SalesPerDayBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            SalesPerDayBtn.ForeColor = Color.Transparent;
            SalesPerDayBtn.Location = new Point(0, 0);
            SalesPerDayBtn.Name = "SalesPerDayBtn";
            SalesPerDayBtn.Size = new Size(416, 108);
            SalesPerDayBtn.TabIndex = 20;
            SalesPerDayBtn.Text = "Sales Per Day";
            SalesPerDayBtn.UseVisualStyleBackColor = false;
            SalesPerDayBtn.Click += SalesPerDayBtn_Click;
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.BackgroundColor = SystemColors.Info;
            dataGridViewMain.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.GridColor = Color.Salmon;
            dataGridViewMain.Location = new Point(12, 457);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.ReadOnly = true;
            dataGridViewMain.RowHeadersWidth = 62;
            dataGridViewMain.RowTemplate.ReadOnly = true;
            dataGridViewMain.Size = new Size(1712, 406);
            dataGridViewMain.TabIndex = 21;
            dataGridViewMain.CellContentClick += dataGridViewMain_CellContentClick;
            // 
            // BikeStoreLabel
            // 
            BikeStoreLabel.AutoSize = true;
            BikeStoreLabel.BorderStyle = BorderStyle.FixedSingle;
            BikeStoreLabel.Font = new Font("Arial Black", 16F, FontStyle.Bold | FontStyle.Underline);
            BikeStoreLabel.Location = new Point(0, 0);
            BikeStoreLabel.Name = "BikeStoreLabel";
            BikeStoreLabel.Size = new Size(404, 47);
            BikeStoreLabel.TabIndex = 22;
            BikeStoreLabel.Text = "Bike store orders App";
            // 
            // QuantitySelector
            // 
            QuantitySelector.BackColor = SystemColors.Control;
            QuantitySelector.Location = new Point(486, 58);
            QuantitySelector.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuantitySelector.Name = "QuantitySelector";
            QuantitySelector.Size = new Size(171, 31);
            QuantitySelector.TabIndex = 3;
            QuantitySelector.TextAlign = HorizontalAlignment.Center;
            QuantitySelector.Value = new decimal(new int[] { 1, 0, 0, 0 });
            QuantitySelector.ValueChanged += QuantitySelector_ValueChanged;
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.BackColor = Color.Salmon;
            QuantityLabel.BorderStyle = BorderStyle.Fixed3D;
            QuantityLabel.Font = new Font("Arial Black", 14F, FontStyle.Bold);
            QuantityLabel.ForeColor = SystemColors.Control;
            QuantityLabel.Location = new Point(510, 10);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new Size(149, 42);
            QuantityLabel.TabIndex = 24;
            QuantityLabel.Text = "Quantity";
            QuantityLabel.Click += QuantityLabel_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1556, 892);
            label6.Name = "label6";
            label6.Size = new Size(157, 25);
            label6.TabIndex = 25;
            label6.Text = "כל הזכויות שמורות";
            label6.Click += label6_Click;
            // 
            // YearComboBox
            // 
            YearComboBox.BackColor = Color.Salmon;
            YearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            YearComboBox.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            YearComboBox.Location = new Point(3, 296);
            YearComboBox.Name = "YearComboBox";
            YearComboBox.Size = new Size(405, 33);
            YearComboBox.TabIndex = 0;
            YearComboBox.SelectedIndexChanged += YearComboBox_SelectedIndexChanged_1;
            // 
            // SalesDatePicker
            // 
            SalesDatePicker.CalendarFont = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            SalesDatePicker.CalendarForeColor = Color.Salmon;
            SalesDatePicker.CalendarMonthBackground = Color.Salmon;
            SalesDatePicker.CalendarTitleBackColor = Color.Salmon;
            SalesDatePicker.CalendarTitleForeColor = Color.Salmon;
            SalesDatePicker.CalendarTrailingForeColor = Color.Salmon;
            SalesDatePicker.Location = new Point(0, 113);
            SalesDatePicker.Name = "SalesDatePicker";
            SalesDatePicker.Size = new Size(408, 31);
            SalesDatePicker.TabIndex = 26;
            SalesDatePicker.ValueChanged += SalesDatePicker_ValueChanged;
            // 
            // OrdersHistoPanel
            // 
            OrdersHistoPanel.BackColor = Color.Salmon;
            OrdersHistoPanel.Controls.Add(SalesPerDayBtn);
            OrdersHistoPanel.Controls.Add(YearComboBox);
            OrdersHistoPanel.Controls.Add(SalesDatePicker);
            OrdersHistoPanel.Controls.Add(ShowYearlySalesBtn);
            OrdersHistoPanel.Location = new Point(1182, 61);
            OrdersHistoPanel.Name = "OrdersHistoPanel";
            OrdersHistoPanel.Size = new Size(416, 349);
            OrdersHistoPanel.TabIndex = 27;
            // 
            // BikeDetailsPanel
            // 
            BikeDetailsPanel.BackColor = Color.Salmon;
            BikeDetailsPanel.Controls.Add(label3);
            BikeDetailsPanel.Controls.Add(label2);
            BikeDetailsPanel.Controls.Add(QuantitySelector);
            BikeDetailsPanel.Controls.Add(BikeColorBox);
            BikeDetailsPanel.Controls.Add(label5);
            BikeDetailsPanel.Controls.Add(BikeSizeBox);
            BikeDetailsPanel.Controls.Add(BikeTypeBox);
            BikeDetailsPanel.Controls.Add(QuantityLabel);
            BikeDetailsPanel.Location = new Point(12, 61);
            BikeDetailsPanel.Name = "BikeDetailsPanel";
            BikeDetailsPanel.Size = new Size(659, 119);
            BikeDetailsPanel.TabIndex = 28;
            BikeDetailsPanel.Paint += BikeDetailsPanel_Paint;
            // 
            // CustomersInfoPanel
            // 
            CustomersInfoPanel.BackColor = Color.Salmon;
            CustomersInfoPanel.BackgroundImageLayout = ImageLayout.None;
            CustomersInfoPanel.Controls.Add(EmailBox);
            CustomersInfoPanel.Controls.Add(Adress);
            CustomersInfoPanel.Controls.Add(PhoneNumber);
            CustomersInfoPanel.Controls.Add(LastName);
            CustomersInfoPanel.Controls.Add(FirstName);
            CustomersInfoPanel.Controls.Add(label4);
            CustomersInfoPanel.Location = new Point(12, 186);
            CustomersInfoPanel.Name = "CustomersInfoPanel";
            CustomersInfoPanel.Size = new Size(657, 264);
            CustomersInfoPanel.TabIndex = 29;
            CustomersInfoPanel.Paint += CustomersInfoPanel_Paint;
            // 
            // SubmitsPanel
            // 
            SubmitsPanel.BackColor = Color.Salmon;
            SubmitsPanel.Controls.Add(SubmitOrderDetailsBtn);
            SubmitsPanel.Controls.Add(ShowCusOrderHistoryBtn);
            SubmitsPanel.Controls.Add(AddOrder);
            SubmitsPanel.Location = new Point(707, 61);
            SubmitsPanel.Name = "SubmitsPanel";
            SubmitsPanel.Size = new Size(446, 326);
            SubmitsPanel.TabIndex = 30;
            SubmitsPanel.Paint += SubmitsPanel_Paint;
            // 
            // inventoryBtn
            // 
            inventoryBtn.BackColor = Color.Salmon;
            inventoryBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            inventoryBtn.ForeColor = SystemColors.Control;
            inventoryBtn.Location = new Point(707, 387);
            inventoryBtn.Name = "inventoryBtn";
            inventoryBtn.Size = new Size(143, 57);
            inventoryBtn.TabIndex = 31;
            inventoryBtn.Text = "inventory";
            inventoryBtn.UseVisualStyleBackColor = false;
            inventoryBtn.Click += inventoryBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1712, 1040);
            Controls.Add(inventoryBtn);
            Controls.Add(SubmitsPanel);
            Controls.Add(CustomersInfoPanel);
            Controls.Add(BikeDetailsPanel);
            Controls.Add(OrdersHistoPanel);
            Controls.Add(label6);
            Controls.Add(BikeStoreLabel);
            Controls.Add(dataGridViewMain);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Bike store app";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuantitySelector).EndInit();
            OrdersHistoPanel.ResumeLayout(false);
            BikeDetailsPanel.ResumeLayout(false);
            BikeDetailsPanel.PerformLayout();
            CustomersInfoPanel.ResumeLayout(false);
            CustomersInfoPanel.PerformLayout();
            SubmitsPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox BikeTypeBox;
        private System.Windows.Forms.ComboBox BikeSizeBox;
        private System.Windows.Forms.ComboBox BikeColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.TextBox PhoneNumber;
        private System.Windows.Forms.TextBox Adress;
        private System.Windows.Forms.TextBox EmailBox;
        private Label label5;
        private Button AddOrder;
        private Button ShowCusOrderHistoryBtn;
        private Button ShowYearlySalesBtn;
        private Button SubmitOrderDetailsBtn;
        private Button SalesPerDayBtn;
        private DataGridView dataGridViewMain;
        private Label BikeStoreLabel;
        private NumericUpDown QuantitySelector;
        private Label QuantityLabel;
        private Label label6;
        private ComboBox YearComboBox;
        private DateTimePicker SalesDatePicker;
        private Panel OrdersHistoPanel;
        private Panel BikeDetailsPanel;
        private Panel CustomersInfoPanel;
        private Panel SubmitsPanel;
        private Button inventoryBtn;
    }
}
