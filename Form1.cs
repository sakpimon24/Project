using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using iTextFont = iTextSharp.text.Font;

namespace Project
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=mydatabase.db";

        // ===== Downloads Path (Windows Known Folder) =====
        private static readonly Guid DownloadsFolderGuid =
            new Guid("374DE290-123F-4565-9164-39C4925E467B");

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken,
            out IntPtr ppszPath
        );

        private static string GetDownloadsPath()
        {
            IntPtr ppszPath;
            int hr = SHGetKnownFolderPath(DownloadsFolderGuid, 0, IntPtr.Zero, out ppszPath);

            if (hr == 0 && ppszPath != IntPtr.Zero)
            {
                try
                {
                    string path = Marshal.PtrToStringUni(ppszPath);
                    if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                        return path;
                }
                finally
                {
                    Marshal.FreeCoTaskMem(ppszPath);
                }
            }

            // fallback: Desktop (เผื่อเครื่องแปลกมาก ๆ)
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public Form1()
        {
            InitializeComponent();
            CreateDatabase();
        }

        // ===== DB Setup =====
        private void CreateDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    var createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS ProductData (
                            ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
                            ProductName TEXT,
                            Price INTEGER,
                            Total INTEGER,
                            Type TEXT
                        );";


                    using (var command = new SqliteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    if (!HasSampleData(connection))
                        InsertSampleData(connection);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
        }

        private bool HasSampleData(SqliteConnection connection)
        {
            var checkDataQuery = "SELECT COUNT(*) FROM ProductData";
            using (var command = new SqliteCommand(checkDataQuery, connection))
            {
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void InsertSampleData(SqliteConnection connection)
        {
            var insertQuery = "INSERT INTO ProductData (ProductName, Price, Total, Type) VALUES (@ProductName, @Price, @Total, @Type)";

            using (var command = new SqliteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductName", "Product A");
                command.Parameters.AddWithValue("@Price", 100);
                command.Parameters.AddWithValue("@Total", 50);
                command.Parameters.AddWithValue("@Type", "Electronics");
                command.ExecuteNonQuery();

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@ProductName", "Product B");
                command.Parameters.AddWithValue("@Price", 200);
                command.Parameters.AddWithValue("@Total", 30);
                command.Parameters.AddWithValue("@Type", "Clothing");
                command.ExecuteNonQuery();

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@ProductName", "Product C");
                command.Parameters.AddWithValue("@Price", 150);
                command.Parameters.AddWithValue("@Total", 20);
                command.Parameters.AddWithValue("@Type", "Furniture");
                command.ExecuteNonQuery();
            }
        }

        // ===== UI Load =====
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProductIds();
            LoadAllData();
        }

        private void LoadProductIds()
        {
            ProductID.Items.Clear();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var selectQuery = "SELECT ProductId, ProductName FROM ProductData";

                using (var command = new SqliteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductID.Items.Add(new KeyValuePair<int, string>(
                            reader.GetInt32(0),
                            reader.GetString(1)
                        ));
                    }
                }
            }

            ProductID.DisplayMember = "Key";
            ProductID.ValueMember = "Key";
        }

        private void LoadAllData()
        {
            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    var selectQuery = "SELECT * FROM ProductData";
                    using (var command = new SqliteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        AllData.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void ProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProductID.SelectedItem == null) return;

            var selectedItem = (KeyValuePair<int, string>)ProductID.SelectedItem;
            LoadProductDetails(selectedItem.Key);
        }

        private void LoadProductDetails(int productId)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var selectQuery = "SELECT ProductName, Price, Total, Type FROM ProductData WHERE ProductId = @ProductId";

                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ProductName.Text = reader.GetString(0);
                            Price.Text = reader.GetInt32(1).ToString();
                            TotalProduct.Text = reader.GetInt32(2).ToString();
                            ProductType.Text = reader.GetString(3);
                        }
                    }
                }
            }
        }
        private bool ProductNameExists(string name, int? excludeProductId = null)
        {
            string normalized = (name ?? "").Trim();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string sql =
                    excludeProductId == null
                    ? "SELECT 1 FROM ProductData WHERE lower(trim(ProductName)) = lower(trim(@Name)) LIMIT 1;"
                    : "SELECT 1 FROM ProductData WHERE lower(trim(ProductName)) = lower(trim(@Name)) AND ProductId <> @ExcludeId LIMIT 1;";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", normalized);

                    if (excludeProductId != null)
                        command.Parameters.AddWithValue("@ExcludeId", excludeProductId.Value);

                    var result = command.ExecuteScalar();
                    return result != null;
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ProductID.Text = "";
            ProductName.Text = "";
            Price.Text = "";
            TotalProduct.Text = "";
            ProductType.Text = "";
        }

        // ===== CRUD =====
        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            string name = ProductName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("กรุณาใส่ ProductName");
                return;
            }

            if (ProductNameExists(name))
            {
                MessageBox.Show("มี ProductName นี้อยู่แล้ว (ชื่อซ้ำ) ยกเลิกการเพิ่ม");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    var insertQuery = @"
                INSERT INTO ProductData (ProductName, Price, Total, Type)
                VALUES (@ProductName, @Price, @Total, @Type);";

                    using (var command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", ProductName.Text.Trim());
                        command.Parameters.AddWithValue("@Price", Convert.ToInt32(Price.Text));
                        command.Parameters.AddWithValue("@Total", Convert.ToInt32(TotalProduct.Text));
                        command.Parameters.AddWithValue("@Type", ProductType.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product added successfully.");

                LoadProductIds();  // ✅ สำคัญ: รีโหลด combobox
                LoadAllData();     // ✅ รีโหลดตาราง
                ClearBtn_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (ProductID.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            var selected = (KeyValuePair<int, string>)ProductID.SelectedItem;
            int id = selected.Key;

            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    var deleteQuery = "DELETE FROM ProductData WHERE ProductId = @ProductId;";
                    using (var command = new SqliteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", id);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product deleted successfully.");
                LoadProductIds();
                LoadAllData();
                ClearBtn_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }


        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (ProductID.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to edit.");
                return;
            }

            var selected = (KeyValuePair<int, string>)ProductID.SelectedItem;
            int id = selected.Key;
            string name = ProductName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("กรุณาใส่ ProductName");
                return;
            }

            if (ProductNameExists(name, excludeProductId: id))
            {
                MessageBox.Show("มี ProductName นี้อยู่แล้วในสินค้าอื่น (ชื่อซ้ำ) ยกเลิกการแก้ไข");
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    var updateQuery = @"
                UPDATE ProductData
                SET ProductName = @ProductName,
                    Price = @Price,
                    Total = @Total,
                    Type = @Type
                WHERE ProductId = @ProductId;";

                    using (var command = new SqliteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", id);
                        command.Parameters.AddWithValue("@ProductName", ProductName.Text.Trim());
                        command.Parameters.AddWithValue("@Price", Convert.ToInt32(Price.Text));
                        command.Parameters.AddWithValue("@Total", Convert.ToInt32(TotalProduct.Text));
                        command.Parameters.AddWithValue("@Type", ProductType.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product updated successfully.");
                LoadProductIds();
                LoadAllData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }


        // ===== PDF =====
        // ✅ ไม่ใช้ customerName ตามที่คุณขอ
        // ใช้แค่ productName, price, quantity และบังคับเซฟลง Downloads
        private string CreateReceipt(string productName, decimal price, int quantity)
        {
            try
            {
                string downloadsPath = GetDownloadsPath();

                string fileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(downloadsPath, fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (Document document = new Document(PageSize.A4))
                using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
                {
                    document.Open();

                    iTextFont titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22);
                    iTextFont headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                    iTextFont normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 16);

                    document.Add(new Paragraph("RECEIPT", titleFont) { Alignment = Element.ALIGN_CENTER });
                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Son pi mak Shop", headerFont));
                    document.Add(new Paragraph("666 Atiwan Dum Street, BangDik 676767", normalFont));
                    document.Add(new Paragraph("Tel: 02-123-4567", normalFont));
                    document.Add(new Paragraph("________________________________________________________"));

                    document.Add(new Paragraph($"Customer Name: {CustomerName.Text}", normalFont));
                    document.Add(new Paragraph($"Date: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont));
                    document.Add(new Paragraph($"Receipt No: RC-{DateTime.Now:yyyyMMddHHmmss}", normalFont));
                    document.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 2f, 4f, 2f, 2f });

                    table.AddCell(new PdfPCell(new Phrase("No.", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Item", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Qty", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Price", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    table.AddCell(new PdfPCell(new Phrase("1", normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(productName, normalFont)));
                    table.AddCell(new PdfPCell(new Phrase(quantity.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(price.ToString("N2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });

                    document.Add(table);
                    document.Add(new Paragraph("\n"));

                    decimal total = price * quantity;
                    decimal vat = total * 0.07m;
                    decimal grandTotal = total + vat;

                    document.Add(new Paragraph($"Subtotal: {total:N2} Baht", normalFont) { Alignment = Element.ALIGN_RIGHT });
                    document.Add(new Paragraph($"VAT 7%: {vat:N2} Baht", normalFont) { Alignment = Element.ALIGN_RIGHT });
                    document.Add(new Paragraph($"Total: {grandTotal:N2} Baht", headerFont) { Alignment = Element.ALIGN_RIGHT });

                    document.Add(new Paragraph("\n\n"));
                    document.Add(new Paragraph("Thank you for your business", normalFont) { Alignment = Element.ALIGN_CENTER });

                    document.Close();
                }

                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF Error:\n" + ex.ToString());
                return "";
            }
        }

        private void SlipBtn_Click(object sender, EventArgs e)
        {
            if (CustomerName.Text == "" || CustomerName.Text == null)
            {
                MessageBox.Show("Please Enter Customer Name");
                return;
            }
            if (ProductID.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to generate receipt.");
                return;
            }

            var selectedItem = (KeyValuePair<int, string>)ProductID.SelectedItem;
            int selectedProductId = selectedItem.Key;

            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();

                    var selectQuery = "SELECT ProductName, Price, Total FROM ProductData WHERE ProductId = @ProductId";
                    using (var command = new SqliteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", selectedProductId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("ไม่พบข้อมูลสินค้าตาม ProductId นี้");
                                return;
                            }

                            string productName = reader.GetString(0);

                            // ✅ Price เป็น INTEGER ใน DB
                            decimal price = Convert.ToDecimal(reader.GetInt32(1));

                            int quantity = Convert.ToInt32(TotalProduct.Text);

                            // ✅ สร้างใบเสร็จลง Downloads
                            string savedPath = CreateReceipt(productName, price, quantity);

                            if (!string.IsNullOrWhiteSpace(savedPath))
                                MessageBox.Show("Receipt generated at:\n" + savedPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.ToString());
            }
        }

        
    }
}
