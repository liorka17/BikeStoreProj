using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;


namespace BikeStoreProj
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        private string connStr = "server=MININT-OIFTK0O\\SQLEXPRESS;database=BikeStore;trusted_connection=true;trustservercertificate=true";

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connStr);
            conn.Open();
            LoadBikeData();
            LoadYearsToComboBox(); // ����� ������ ����� ����� �����

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            base.OnFormClosing(e);
        }

        private void LoadBikeData()
        {
            PopulateComboBox("SELECT DISTINCT Type FROM BikeTypes", BikeTypeBox, "Type");
            PopulateComboBox("SELECT DISTINCT BikeSize FROM BikeTypes", BikeSizeBox, "BikeSize");
            PopulateComboBox("SELECT DISTINCT Color FROM BikeTypes", BikeColorBox, "Color");
        }

        private void PopulateComboBox(string query, ComboBox comboBox, string fieldName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader[fieldName].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        //�������� ���� ������
        // ������� ������ ���� ��� ��� ����� ����� ����
        private int AddNewCustomer(string firstName, string lastName, string phoneNumber, string address, string email)
        {
            string normalizedPhoneNumber = NormalizePhoneNumber(phoneNumber);

            string insertQuery = @"
        INSERT INTO Customers (FirstName, LastName, PhoneNumber, Address, Email) 
        OUTPUT INSERTED.CustomerID 
        VALUES (@FirstName, @LastName, @PhoneNumber, @Address, @Email)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", normalizedPhoneNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);

                return (int)cmd.ExecuteScalar(); // ����� ���� ���� ���
            }
        }

        private int? EnsureCustomerExists(string firstName, string lastName, string phoneNumber)
        {
            string normalizedPhoneNumber = NormalizePhoneNumber(phoneNumber);

            // ����� ���� ����
            string checkQuery = @"
        SELECT CustomerID 
        FROM Customers 
        WHERE REPLACE(REPLACE(REPLACE(PhoneNumber, '-', ''), ' ', ''), '(', '') = @PhoneNumber
          AND FirstName = @FirstName
          AND LastName = @LastName";

            using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", normalizedPhoneNumber);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result); // ����� ���� ���� ����
                }
            }

            // �� ����� �� ����, ������� null
            return null;
        }

        // ������� ������ ���� ���� ����
        private void UpdateCustomerDetails(int customerId, string firstName, string lastName, string phoneNumber, string address, string email)
        {
            string query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, " +
                           "PhoneNumber = @PhoneNumber, Address = @Address, Email = @Email " +
                           "WHERE CustomerID = @CustomerID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
            }
        }
        //

        // ������� ������ �� ���� ����� ��� ���� ���� �������
        private int? GetCustomerIdByPhone(string phoneNumber)
        {
            string normalizedPhoneNumber = NormalizePhoneNumber(phoneNumber);
            string query = "SELECT CustomerID FROM Customers WHERE REPLACE(REPLACE(REPLACE(PhoneNumber, '-', ''), ' ', ''), '(', '') = @PhoneNumber";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PhoneNumber", normalizedPhoneNumber);
                object result = cmd.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }
        //
        //�������� ���� ����� ���� ����� �����
        private void PreviewOrderBeforeSubmitingBtn_Click(object sender, EventArgs e)
        {
            // ����� ��� ����� �����
            if (string.IsNullOrWhiteSpace(FirstName.Text) ||
                string.IsNullOrWhiteSpace(LastName.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(Adress.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                BikeTypeBox.SelectedItem == null ||
                BikeSizeBox.SelectedItem == null ||
                BikeColorBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all the required fields before proceeding.");
                return;
            }

            // ����� ����� �����
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string phoneNumber = PhoneNumber.Text;
            string address = Adress.Text;
            string email = EmailBox.Text;

            // ����� ���� ����� ���� �������
            int? customerId = GetCustomerIdByPhone(phoneNumber);

            if (customerId == null)
            {
                // ����� ���� ��� ���� �������
                AddNewCustomer(firstName, lastName, phoneNumber, address, email);
                customerId = GetCustomerIdByPhone(phoneNumber); // ���� ����� ����
            }
            else
            {
                // �� ����� ��� ����, ���� ����� ������ ����� ����� ����� �����
                DialogResult result = MessageBox.Show(
                    "Customer already exists. Would you like to update the customer details?",
                    "Customer Exists",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateCustomerDetails((int)customerId, firstName, lastName, phoneNumber, address, email);
                }
            }

            // ����� ���� ������
            string bikeType = BikeTypeBox.SelectedItem.ToString();
            string bikeSize = BikeSizeBox.SelectedItem.ToString();
            string bikeColor = BikeColorBox.SelectedItem.ToString();
            int quantity = (int)QuantitySelector.Value; // ��� ����� ������
            decimal unitPrice = GetBikePrice(bikeType, bikeSize, bikeColor); // ���� ������
            decimal totalPrice = unitPrice * quantity;

            // ���� ���� ������
            string orderDetails = $"Customer Name: {firstName} {lastName}\n" +
                                  $"Phone Number: {phoneNumber}\n" +
                                  $"Address: {address}\n" +
                                  $"Email: {email}\n\n" +
                                  $"Bike Type: {bikeType}\n" +
                                  $"Bike Size: {bikeSize}\n" +
                                  $"Bike Color: {bikeColor}\n" +
                                  $"Quantity: {quantity}\n" +
                                  $"Unit Price: {unitPrice:C}\n" +
                                  $"Total Price: {totalPrice:C}";

            MessageBox.Show(orderDetails, "Order Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SubmitOrderDetailsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ����� ������ ������
                if (string.IsNullOrWhiteSpace(FirstName.Text) ||
                    string.IsNullOrWhiteSpace(LastName.Text) ||
                    string.IsNullOrWhiteSpace(PhoneNumber.Text) ||
                    string.IsNullOrWhiteSpace(Adress.Text) ||
                    string.IsNullOrWhiteSpace(EmailBox.Text) ||
                    BikeTypeBox.SelectedItem == null ||
                    BikeSizeBox.SelectedItem == null ||
                    BikeColorBox.SelectedItem == null)
                {
                    MessageBox.Show("Please fill all the required fields before proceeding.");
                    return;
                }

                // ���� ������
                string firstName = FirstName.Text.Trim();
                string lastName = LastName.Text.Trim();
                string phoneNumber = PhoneNumber.Text.Trim();
                string address = Adress.Text.Trim();
                string email = EmailBox.Text.Trim();
                string bikeType = BikeTypeBox.SelectedItem.ToString();
                string bikeSize = BikeSizeBox.SelectedItem.ToString();
                string bikeColor = BikeColorBox.SelectedItem.ToString();
                int quantity = (int)QuantitySelector.Value;
                decimal unitPrice = GetBikePrice(bikeType, bikeSize, bikeColor);
                decimal totalPrice = unitPrice * quantity;

                // ����� ���� ����
                int? customerId = EnsureCustomerExists(firstName, lastName, phoneNumber);

                // �� ����� �� ����, ������� ����
                if (customerId == null)
                {
                    customerId = AddNewCustomer(firstName, lastName, phoneNumber, address, email);
                }

                // ����� ����� ���� ���� �������
                int orderId = AddNewOrder(customerId.Value, totalPrice);

                // ����� ���� ������ ���� �������
                AddOrderDetails(orderId, bikeType, bikeSize, bikeColor, quantity, unitPrice);

                // ����� �����
                UpdateBikeStock(bikeType, bikeSize, bikeColor, quantity);

                // ���� ���� ������ �����
                LoadOrderDetailsToGrid(orderId);

                MessageBox.Show("Order submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //
        //�������� ����� ������ ������ �� ���� ����� �����
        private void ShowCusOrderHistoryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ���� ��� �������
                string input = PromptForInput("Enter Customer ID or Phone Number:", "Customer Search");
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Customer ID or Phone Number is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ����� ���� ������ �� ����� ����� �����
                string normalizedPhoneNumber = NormalizePhoneNumber(input.Trim());
                bool isPhoneNumber = normalizedPhoneNumber.Length >= 9 && normalizedPhoneNumber.Length <= 10 && long.TryParse(normalizedPhoneNumber, out _);

                // ����� ��� ����� �-ID (���� ����) �� �� ����� �����
                bool isNumeric = int.TryParse(input, out int customerId) && !isPhoneNumber;

                // ����� ������ ������ ������
                //MessageBox.Show($"Input: {input}\nNormalized Phone Number: {normalizedPhoneNumber}\nIsPhoneNumber: {isPhoneNumber}\nIsNumeric: {isNumeric}");

                // ������ ����� ��������� �������
                string query = @"
        SELECT 
            o.OrderID AS 'Order ID',
            o.OrderDate AS 'Order Date',
            o.TotalAmount AS 'Total Amount',
            c.FirstName AS 'First Name',
            c.LastName AS 'Last Name',
            c.Email AS 'Email',
            REPLACE(REPLACE(REPLACE(c.PhoneNumber, '-', ''), ' ', ''), '(', '') AS 'Phone Number',
            c.Address AS 'Address',
            bt.Type AS 'Bike Type',
            bt.BikeSize AS 'Bike Size',
            bt.Color AS 'Bike Color',
            od.Quantity AS 'Quantity',
            od.UnitPrice AS 'Unit Price',
            od.TotalPrice AS 'Total Price'
        FROM 
            Orders o
        JOIN 
            Customers c ON o.CustomerID = c.CustomerID
        JOIN 
            OrderDetails od ON o.OrderID = od.OrderID
        JOIN 
            BikeTypes bt ON od.BikeID = bt.BikeID
        WHERE 
            (@CustomerID > 0 AND c.CustomerID = @CustomerID) OR
            (@PhoneNumber IS NOT NULL AND REPLACE(REPLACE(REPLACE(c.PhoneNumber, '-', ''), ' ', ''), '(', '') = @PhoneNumber)
        ORDER BY 
            o.OrderDate DESC";

                // ����� �������
                Dictionary<string, object> parameters = new();
                if (isNumeric)
                {
                    parameters["@CustomerID"] = customerId;
                    parameters["@PhoneNumber"] = DBNull.Value;
                }
                else if (isPhoneNumber)
                {
                    parameters["@CustomerID"] = DBNull.Value;
                    parameters["@PhoneNumber"] = normalizedPhoneNumber;
                }
                else
                {
                    //essageBox.Show("Invalid input. Please enter a valid Customer ID or Phone Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ����� ����� �������� ������ ������
                //MessageBox.Show($"Parameters:\nCustomerID: {(isNumeric ? customerId.ToString() : "NULL")}\nPhoneNumber: {normalizedPhoneNumber}");

                // ���� �� ������� �����
                LoadDataToGrid(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inventoryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ������ ����� ����� ����� ���� ����� BikeTypes
                string query = @"
        SELECT 
            BikeID AS 'Bike ID', 
            Type AS 'Bike Type', 
            BikeSize AS 'Bike Size', 
            Color AS 'Bike Color', 
            StockQuantity AS 'Stock Quantity', 
            SalePrice AS 'Sale Price'
        FROM BikeTypes
        ORDER BY BikeID ASC";

                // ����� �������� ����� �� ������� �����
                LoadDataToGrid(query);

                // �� ��� ������, ���� ����� ������
                if (dataGridViewMain.Rows.Count == 0)
                {
                    MessageBox.Show("No inventory data available.", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // ����� ������� ����� �����
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesPerDayBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ���� ����� ���� ��-DateTimePicker
                DateTime selectedDate = SalesDatePicker.Value.Date;

                // ����� �� ������ ������ ���� �����
                string countQuery = @"
            SELECT COUNT(*)
            FROM Orders
            WHERE CAST(OrderDate AS DATE) = @SelectedDate";

                using (SqlCommand cmd = new SqlCommand(countQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

                    // ����� ������� ����� ���� �������
                    int orderCount = (int)cmd.ExecuteScalar();

                    if (orderCount == 0)
                    {
                        // �� ��� ������ ���� �����, ���� ����� ������
                        MessageBox.Show($"No sales found for {selectedDate.ToShortDateString()}.", "No Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // ����� ���������
                    }
                }

                // ����� ������� ������ ������
                string query = @"
        SELECT 
            o.OrderID AS 'Order ID',
            c.FirstName AS 'Customer First Name',
            c.LastName AS 'Customer Last Name',
            o.OrderDate AS 'Order Date',
            od.Quantity AS 'Quantity',
            bt.Type AS 'Bike Type',
            bt.BikeSize AS 'Bike Size',
            bt.Color AS 'Bike Color',
            od.UnitPrice AS 'Unit Price',
            od.TotalPrice AS 'Total Price'
        FROM 
            Orders o
        JOIN 
            Customers c ON o.CustomerID = c.CustomerID
        JOIN 
            OrderDetails od ON o.OrderID = od.OrderID
        JOIN 
            BikeTypes bt ON od.BikeID = bt.BikeID
        WHERE 
            CAST(o.OrderDate AS DATE) = @SelectedDate
        ORDER BY 
            o.OrderDate DESC";

                // ���� ������� ������ ������
                Dictionary<string, object> parameters = new()
        {
            { "@SelectedDate", selectedDate }
        };

                // ����� ������� �����
                LoadDataToGrid(query, parameters);

                // ����� �� �������
                decimal totalSales = 0;
                foreach (DataGridViewRow row in dataGridViewMain.Rows)
                {
                    if (row.Cells["Total Price"].Value != null)
                    {
                        totalSales += Convert.ToDecimal(row.Cells["Total Price"].Value);
                    }
                }

                // ���� �� �������
                MessageBox.Show($"Total sales for {selectedDate.ToShortDateString()}: {totalSales:C}", "Total Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowYearlySalesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ����� �� ����� ��� �-ComboBox
                if (YearComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ���� ���� ������
                string selectedYear = YearComboBox.SelectedItem.ToString();

                // ������ SQL ����� ����� ���� ������ ����
                string query = @"
            SELECT 
                o.OrderID AS 'Order ID',
                o.OrderDate AS 'Order Date',
                o.TotalAmount AS 'Total Amount',
                c.FirstName AS 'Customer First Name',
                c.LastName AS 'Customer Last Name'
            FROM 
                Orders o
            JOIN 
                Customers c ON o.CustomerID = c.CustomerID
            WHERE 
                YEAR(o.OrderDate) = @SelectedYear
            ORDER BY 
                o.OrderDate";

                // ������� ���� �������
                Dictionary<string, object> parameters = new()
        {
            { "@SelectedYear", selectedYear }
        };

                // ���� �� ������� �����
                LoadDataToGrid(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ������� ����� ����� ��� ������
        private string PromptForInput(string message, string title)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(message, title, "");
        }

        // ������� ������ ���� �����
        private string NormalizePhoneNumber(string phoneNumber)
        {
            return phoneNumber.Replace("-", "")
                              .Replace(" ", "")
                              .Replace("(", "")
                              .Replace(")", "")
                              .Trim();
        }

        private decimal GetBikePrice(string bikeType, string bikeSize, string bikeColor)
        {
            try
            {
                string query = "SELECT SalePrice FROM BikeTypes WHERE Type = @Type AND BikeSize = @BikeSize AND Color = @Color";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Type", bikeType);
                    cmd.Parameters.AddWithValue("@BikeSize", bikeSize);
                    cmd.Parameters.AddWithValue("@Color", bikeColor);

                    object result = cmd.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal price))
                    {
                        return price;
                    }
                    else
                    {
                        MessageBox.Show("Price not found for the selected bike configuration.");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving bike price: {ex.Message}");
                return 0;
            }
        }
        //
        //�������� ������ ������ ���� �� ����� ������ �� ����� ���� �������
        private int AddNewOrder(int customerId, decimal totalAmount)
        {
            string query = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) OUTPUT INSERTED.OrderID VALUES (@CustomerID, GETDATE(), @TotalAmount)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                return (int)cmd.ExecuteScalar(); // Return newly inserted OrderID
            }
        }

        private void AddOrderDetails(int orderId, string bikeType, string bikeSize, string bikeColor, int quantity, decimal unitPrice)
        {
            string query = "INSERT INTO OrderDetails (OrderID, BikeID, Quantity, UnitPrice) " +
                           "VALUES (@OrderID, (SELECT BikeID FROM BikeTypes WHERE Type = @Type AND BikeSize = @BikeSize AND Color = @Color), @Quantity, @UnitPrice)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@Type", bikeType);
                cmd.Parameters.AddWithValue("@BikeSize", bikeSize);
                cmd.Parameters.AddWithValue("@Color", bikeColor);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);

                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateBikeStock(string bikeType, string bikeSize, string bikeColor, int quantity)
        {
            string query = "UPDATE BikeTypes SET StockQuantity = StockQuantity - @Quantity " +
                           "WHERE Type = @Type AND BikeSize = @BikeSize AND Color = @Color";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Type", bikeType);
                cmd.Parameters.AddWithValue("@BikeSize", bikeSize);
                cmd.Parameters.AddWithValue("@Color", bikeColor);

                cmd.ExecuteNonQuery();
            }
        }

        private void LoadOrderDetailsToGrid(int orderId)
        {
            try
            {
                string query = @"
        SELECT 
            od.OrderDetailID AS 'Order Detail ID',
            o.OrderID AS 'Order ID',
            c.FirstName AS 'First Name',
            c.LastName AS 'Last Name',
            REPLACE(REPLACE(REPLACE(c.PhoneNumber, '-', ''), ' ', ''), '(', '') AS 'Phone Number',
            c.Email AS 'Email',
            c.Address AS 'Address',
            bt.Type AS 'Bike Type',
            bt.BikeSize AS 'Bike Size',
            bt.Color AS 'Bike Color',
            od.Quantity AS 'Quantity',
            od.UnitPrice AS 'Unit Price',
            od.TotalPrice AS 'Total Price'
        FROM 
            OrderDetails od
        JOIN 
            Orders o ON od.OrderID = o.OrderID
        JOIN 
            Customers c ON o.CustomerID = c.CustomerID
        JOIN 
            BikeTypes bt ON od.BikeID = bt.BikeID
        WHERE 
            o.OrderID = @OrderID";

                Dictionary<string, object> parameters = new()
        {
            { "@OrderID", orderId }
        };

                // ���� �� ������� �����
                LoadDataToGrid(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTotalRowToGrid()
        {
            try
            {
                if (dataGridViewMain.DataSource is DataTable dataTable)
                {
                    // ����� �� ����� ����� ������� "Total Price"
                    decimal totalSum = dataTable.AsEnumerable()
                                                 .Where(row => row["Total Price"] != DBNull.Value)
                                                 .Sum(row => row.Field<decimal>("Total Price"));

                    // ����� ���� ���� ����� �������
                    DataRow totalRow = dataTable.NewRow();
                    totalRow["Bike Type"] = "Total"; // ����� �� ������ �����
                    totalRow["Total Price"] = totalSum; // ����� �� ����� �����

                    // ����� ����� �����
                    dataTable.Rows.Add(totalRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        //�������� ������ ������
        private void LoadDataToGrid(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViewMain.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadYearsToComboBox()
        {
            try
            {
                string query = "SELECT DISTINCT YEAR(OrderDate) AS Year FROM Orders ORDER BY Year";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        YearComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            string year = reader["Year"].ToString();
                            YearComboBox.Items.Add(year);
                            //MessageBox.Show($"Year Loaded: {year}"); // ����� �����
                        }
                    }
                }
                if (YearComboBox.Items.Count > 0)
                {
                    YearComboBox.SelectedIndex = 0; // ��� ��� ������ ������ ����
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}");
            }
        }

        private void BikeTypeBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void BikeSizeBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void BikeColorBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void FirstName_TextChanged(object sender, EventArgs e) { }
        private void LastName_TextChanged(object sender, EventArgs e) { }
        private void Adress_TextChanged(object sender, EventArgs e) { }
        private void EmailBox_TextChanged(object sender, EventArgs e) { }
        private void PhoneNumber_TextChanged(object sender, EventArgs e) { }
        private void label5_Click_1(object sender, EventArgs e) { }
        private void dataGridViewMain_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void QuantityLabel_Click(object sender, EventArgs e) { }
        private void QuantitySelector_ValueChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void YearComboBox_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void SalesDatePicker_ValueChanged(object sender, EventArgs e) { }
        private void BikeDetailsPanel_Paint(object sender, PaintEventArgs e) { }
        private void CustomersInfoPanel_Paint(object sender, PaintEventArgs e) { }
        private void SubmitsPanel_Paint(object sender, PaintEventArgs e) { }
             
    }
}
