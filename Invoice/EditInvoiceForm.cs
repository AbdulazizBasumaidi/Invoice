using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Invoice
{
    public partial class EditInvoiceForm : Form
    {
        
        List<Items> itemList = new List<Items>();
        private Items rowItem = new Items();
        private decimal total;
        private decimal discount;
        private int quantity;
        public decimal price;

        Invoice invoice = new Invoice();
        private int _invoiceId;
        public EditInvoiceForm(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            LoadInvoice(rowItem);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
       

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ChckItems() == 1)
            {
                AddInvoice(rowItem,  false,  null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditInvoice();
        }

        private void EditInvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DiscountChanged();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void AddInvoice(Items rowItem, bool Load, SqlDataReader readerd)       
        {

            int marginPanel = 30;

            rowItem = new Items();

            Panel productPanel = new Panel();
            productPanel.Height = marginPanel;
            productPanel.BackColor = Color.White;
            productPanel.Dock = DockStyle.Bottom;
            productPanel.Font = new Font("Arial", 12F, FontStyle.Bold);
            productPanel.ForeColor = Color.Black;

            rowItem.btnDelete = new Button();
            rowItem.btnDelete.BackColor = Color.Red;
            rowItem.btnDelete.BackgroundImage = Properties.Resources.Delete;
            rowItem.btnDelete.BackgroundImageLayout = ImageLayout.Center;
            rowItem.btnDelete.FlatAppearance.BorderSize = 0;
            rowItem.btnDelete.FlatStyle = FlatStyle.Flat;
            rowItem.btnDelete.ForeColor = Color.Black;
            rowItem.btnDelete.Location = new Point(762, 0);
            rowItem.btnDelete.Margin = new Padding(0);
            rowItem.btnDelete.Size = new Size(22, 26);

            rowItem.txtProductName = new TextBox();
            rowItem.txtProductName.Width = 430;
            rowItem.txtProductName.Location = new Point(329, 0);
            rowItem.txtProductName.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtProductName.RightToLeft = RightToLeft.Yes;

            rowItem.txtQuantity = new TextBox();
            rowItem.txtQuantity.Width = 100;
            rowItem.txtQuantity.Location = new Point(226, 0);
            rowItem.txtQuantity.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtQuantity.RightToLeft = RightToLeft.Yes;
            rowItem.txtQuantity.TextAlign = HorizontalAlignment.Center;
            

            rowItem.txtPrice = new TextBox();
            rowItem.txtPrice.Width = 100;
            rowItem.txtPrice.Location = new Point(123, 0);
            rowItem.txtPrice.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtPrice.RightToLeft = RightToLeft.Yes;
            rowItem.txtPrice.TextAlign = HorizontalAlignment.Center;
            

            rowItem.txtTotalPrice = new TextBox();
            rowItem.txtTotalPrice.Width = 120;
            rowItem.txtTotalPrice.Location = new Point(0, 0);
            rowItem.txtTotalPrice.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtTotalPrice.RightToLeft = RightToLeft.Yes;
            rowItem.txtTotalPrice.Enabled = false;
            rowItem.txtTotalPrice.TextAlign = HorizontalAlignment.Center;
            rowItem.txtTotalPrice.BackColor = Color.White;

            if(Load)
            {
                rowItem.txtProductName.Text = readerd["ProductName"].ToString();
                rowItem.txtQuantity.Text = readerd["Quantity"].ToString();
                rowItem.txtPrice.Text = readerd["UnitPrice"].ToString();
            }
            rowItem.txtQuantity.TextChanged += (s, e) => Valid();
            rowItem.txtPrice.TextChanged += (s, e) => Valid();

            productPanel.Controls.Add(rowItem.txtProductName);
            productPanel.Controls.Add(rowItem.txtQuantity);
            productPanel.Controls.Add(rowItem.txtPrice);
            productPanel.Controls.Add(rowItem.txtTotalPrice);
            productPanel.Controls.Add(rowItem.btnDelete);
            panelAdd.Controls.Add(productPanel);
            itemList.Add(rowItem);




            if (itemList.Count < 14)
            {
                flowLayoutPanelAdd.Height = ((itemList.Count * marginPanel));
                flowLayoutPanelAdd.AutoScroll = false;
                buttonAdd.Top = 150 + (itemList.Count * marginPanel);
            }
            else { flowLayoutPanelAdd.AutoScroll = true; }

            rowItem.btnDelete.Click += (s, e) =>
            {

                productPanel.Controls.Remove(rowItem.txtProductName);
                productPanel.Controls.Remove(rowItem.txtQuantity);
                productPanel.Controls.Remove(rowItem.txtPrice);
                productPanel.Controls.Remove(rowItem.txtTotalPrice);
                productPanel.Controls.Remove(rowItem.btnDelete);
                panelAdd.Controls.Remove(productPanel);
                itemList.Remove(rowItem);

                panelAdd.Height = (itemList.Count) * marginPanel;

                if (itemList.Count < 14)
                {
                    flowLayoutPanelAdd.Height = (itemList.Count) * marginPanel;
                    buttonAdd.Top = 150 + ((itemList.Count) * marginPanel);
                    flowLayoutPanelAdd.AutoScroll = false;
                }

                Valid();
                

            };
            panelAdd.Height = ((itemList.Count * marginPanel));

            rowItem.txtProductName.Focus();


        }
    
        private void LoadInvoice(Items rowItem)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * from Invoices where InvoiceID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _invoiceId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            labelId.Text = "رقم الفاتورة: " + reader["InvoiceID"].ToString();
                            textBoxCustomer.Text = reader["CustomerName"].ToString();
                            dateTimePickerDate.Value = Convert.ToDateTime(reader["InvoiceDate"]);
                            textBoxTotal.Text = reader["Total"].ToString();
                            textBoxDiscount.Text = reader["Discount"].ToString();
                            textBoxTotalAmount.Text = reader["TotalAmount"].ToString();
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmdd = new SqlCommand("Select * from InvoiceDetails where InvoiceID = @id", conn))
                    {
                        cmdd.Parameters.AddWithValue("@id", _invoiceId);
                        SqlDataReader readerd = cmdd.ExecuteReader();
                        while (readerd.Read())
                        {
                            AddInvoice(rowItem, true, readerd);                            
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int ChckItems()
        {
            if (itemList.Count > 0)
            {
                foreach (Items row in itemList)
                {
                    if (string.IsNullOrWhiteSpace(row.txtProductName.Text))
                    {
                        MessageBox.Show("الرجاء إدخال المنتج");
                        row.txtProductName.Focus();
                        row.txtProductName.SelectAll();
                        return 0;
                    }

                    
                    if (!int.TryParse(row.txtQuantity.Text, out quantity))
                    {
                        MessageBox.Show("الرجاء إدخال الكمية");
                        row.txtQuantity.Focus();
                        row.txtQuantity.SelectAll();
                        return 0;
                    }

                    if (!decimal.TryParse(row.txtPrice.Text, out price))
                    {
                        MessageBox.Show("الرجاء إدخال السعر");
                        row.txtPrice.Focus();
                        row.txtPrice.SelectAll();
                        return 0;
                    }
                    string productName = row.txtProductName.Text;
                }
                return 1;
            }
            else
            { return 1; }
        }

        private void Valid()
        {

            Invoice invoice = new Invoice();

            foreach (Items row in itemList)
            {

                if (!int.TryParse(row.txtQuantity.Text, out quantity))
                {
                    return;
                }


                if (!decimal.TryParse(row.txtPrice.Text, out price))
                {
                    return;
                }
                quantity = Convert.ToInt32(row.txtQuantity.Text);
                price = Convert.ToDecimal(row.txtPrice.Text);
                row.txtTotalPrice.Text = (quantity * price).ToString();
                invoice.Details.Add(new InvoiceDetail
                {
                    Quantity = quantity,
                    UnitPrice = price
                });
                invoice.Total += quantity * price;

            }
            textBoxTotal.Text = invoice.Total.ToString();


            if (!decimal.TryParse(textBoxDiscount.Text, out discount))
            {
                return;
            }

            if (textBoxDiscount.Text != "")
            {
                textBoxTotalAmount.Text = (invoice.Total - ((discount) / 100 * invoice.Total)).ToString();
            }
        }

        private void DiscountChanged()
        {


            if (!decimal.TryParse(textBoxDiscount.Text, out discount))
            {
                if (textBoxDiscount.Text != "")
                {
                    MessageBox.Show("يرجى التأكد من قيمة الخصم");
                    return;
                }

            }
            if (discount > 100)
            {
                MessageBox.Show("يرجى التأكد من قيمة الخصم");
                return;
            }
            if (!decimal.TryParse(textBoxTotal.Text, out total))
            {
                return;
            }
            textBoxTotalAmount.Text = (total - ((discount / 100) * total)).ToString("F2");
        }

        private int ChckResult()
        {
            if (string.IsNullOrWhiteSpace(textBoxCustomer.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم العميل");
                textBoxCustomer.Focus();
                textBoxCustomer.SelectAll();
                return 0;
            }

            if (!decimal.TryParse(textBoxDiscount.Text, out discount))
            {
                MessageBox.Show("يرجى التأكد من قيمة الخصم");
                textBoxDiscount.Focus();
                textBoxDiscount.SelectAll();
                return 0;
            }
            if (discount > 100)
            {

                MessageBox.Show("يرجى التأكد من قيمة الخصم");
                textBoxDiscount.Focus();
                textBoxDiscount.SelectAll();
                return 0;
            }
            return 1;
        }

        public void Save()
        {
            
            total = Convert.ToDecimal(textBoxTotal.Text);
            invoice.CustomerName = textBoxCustomer.Text.Trim();
            invoice.InvoiceDate = dateTimePickerDate.Value;
            invoice.Total = total;
            invoice.Discount = discount;
            invoice.TotalAmount = (total - ((discount / 100) * total));

            foreach (Items row in itemList)
            {
                string proN = row.txtProductName.Text;
                int quan = Convert.ToInt32(row.txtQuantity.Text);
                decimal uinP = Convert.ToDecimal(row.txtPrice.Text);
                invoice.Details.Add(new InvoiceDetail
                {
                    ProductName = proN,
                    Quantity = quan,
                    UnitPrice = uinP
                });

            }

            
        }

        private void EditInvoice()
        {

            if (ChckResult() == 0 || ChckItems() == 0)
            {
                Valid();
                return;
            }
            Save();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sqlUpdate = "Update Invoices set CustomerName=@CustomerName, InvoiceDate=@InvoiceDate, Total=@Total, Discount=@Discount, TotalAmount=@TotalAmount where InvoiceId=@id";
                    using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@CustomerName", invoice.CustomerName);
                        cmdUpdate.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                        cmdUpdate.Parameters.AddWithValue("@Total", invoice.Total);
                        cmdUpdate.Parameters.AddWithValue("@Discount", invoice.Discount);
                        cmdUpdate.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                        cmdUpdate.Parameters.AddWithValue("@id", _invoiceId);
                        cmdUpdate.ExecuteNonQuery();
                    }
                    string sqlDelete = "delete from InvoiceDetails where InvoiceId=@id";
                    using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@id", _invoiceId);
                        cmdDelete.ExecuteNonQuery();
                    }
                    foreach (var detail in invoice.Details)
                    {
                        string sqlDetail = "Insert into InvoiceDetails (InvoiceID, ProductName, Quantity, UnitPrice, TotalPrice) values (@InvoiceID, @ProductName, @Quantity, @UnitPrice, @TotalPrice)";
                        using (SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@InvoiceID", _invoiceId);
                            cmdDetail.Parameters.AddWithValue("@ProductName", detail.ProductName);
                            cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@TotalPrice", detail.TotalPrice);
                            cmdDetail.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("تم تعديل الفاتورة رقم " + _invoiceId + "بنجاح");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

}