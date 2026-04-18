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
    public partial class CreateInvoiceForm : Form
    {
        List<Items> itemList = new List<Items>();
        private Items rowItem = new Items();
        private decimal total;
        private decimal discount;
        private int quantity;
        public decimal price;


        Invoice invoice = new Invoice();
        public CreateInvoiceForm()
        {
            InitializeComponent();
            AddInvoice(rowItem);
        }
        private void CreateInvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
       

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ChckItems() == 1)
            {
                AddInvoice(rowItem);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveInvoice();  
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DiscountChanged();

        }

        private void AddInvoice(Items rowItem)
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
            rowItem.txtQuantity.TextChanged += (s, e) => Valid();

            rowItem.txtPrice = new TextBox();
            rowItem.txtPrice.Width = 100;
            rowItem.txtPrice.Location = new Point(123, 0);
            rowItem.txtPrice.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtPrice.RightToLeft = RightToLeft.Yes;
            rowItem.txtPrice.TextAlign = HorizontalAlignment.Center;
            rowItem.txtPrice.TextChanged += (s, e) => Valid();

            rowItem.txtTotalPrice = new TextBox();
            rowItem.txtTotalPrice.Width = 120;
            rowItem.txtTotalPrice.Location = new Point(0, 0);
            rowItem.txtTotalPrice.BorderStyle = BorderStyle.FixedSingle;
            rowItem.txtTotalPrice.RightToLeft = RightToLeft.Yes;
            rowItem.txtTotalPrice.Enabled = false;
            rowItem.txtTotalPrice.TextAlign = HorizontalAlignment.Center;
            rowItem.txtTotalPrice.BackColor = Color.White;

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
            return;
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

        private void Save()
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

        private void SaveInvoice()
        {

            if (ChckResult() == 0 || ChckItems() == 0)
            {
                Valid();
                return;
            }
            Save();

            try
            {
                InvoiceDAL del = new InvoiceDAL();
                int id = del.AddInvoice(invoice);
                MessageBox.Show("تم حفظ الفاتورة بنجاح برقم " + id);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الحفظ" + ex.Message);
            }
        }
    }
}
