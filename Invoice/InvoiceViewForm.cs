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
    public partial class InvoiceViewForm : Form
    {
        private int _invoiceId;
        public InvoiceViewForm(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            LoadView();

        }
        List<Panel> panelList = new List<Panel>();
        private void LoadView()
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
                            labelCustomer.Text = "اسم العميل: " + reader["CustomerName"].ToString();
                            labelDate.Text = "التاريخ: " + Convert.ToDateTime(reader["InvoiceDate"]).ToString("dd/MM/yyyy");
                            labelToA.Text = reader["TotalAmount"].ToString();
                            labelTot.Text = reader["Total"].ToString();
                            labelDiscount.Text = "%"+reader["Discount"].ToString();
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmdDetails = new SqlCommand("select * from InvoiceDetails where InvoiceID = @id", conn))
                    {
                        cmdDetails.Parameters.AddWithValue("@id", _invoiceId);
                        SqlDataReader detailReader = cmdDetails.ExecuteReader();
                        while (detailReader.Read())
                        {
                            int num = panelList.Count + 1;
                            int marginPanel = 35;
                            Panel row = new Panel();
                            row.Width = 770;
                            row.Height = marginPanel;
                            row.BackColor = Color.White;
                            row.Font = new Font("Arial", 12F, FontStyle.Bold);
                            row.ForeColor = Color.Black;
                            row.BorderStyle = BorderStyle.FixedSingle;
                            row.Location = new Point(-3, 1);

                            panel4.Height = marginPanel + 1 + (panelList.Count * (marginPanel - 1));

                            row.Top = 1 + (panelList.Count * (marginPanel - 1));
                            panel5.Top = 300 + (panelList.Count * (marginPanel - 1));
                            panel1.Height = 450 + (panelList.Count * (marginPanel - 1));



                            Label labelId = new Label();
                            labelId.Text = num.ToString();
                            labelId.Location = new Point(730, 1);
                            labelId.AutoSize = false;
                            labelId.Width = 35;
                            labelId.Height = marginPanel - 5;
                            labelId.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelProduct = new Label();
                            labelProduct.Text = detailReader["ProductName"].ToString();
                            labelProduct.Location = new Point(329, 1);
                            labelProduct.AutoSize = false;
                            labelProduct.Width = 400;
                            labelProduct.Height = marginPanel - 5;
                            labelProduct.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelQuantity = new Label();
                            labelQuantity.Text = detailReader["Quantity"].ToString();
                            labelQuantity.Location = new Point(248, 1);
                            labelQuantity.AutoSize = false;
                            labelQuantity.Width = 80;
                            labelQuantity.Height = marginPanel - 5;
                            labelQuantity.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelUintPrice = new Label();
                            labelUintPrice.Text = detailReader["UnitPrice"].ToString();
                            labelUintPrice.Location = new Point(147, 1);
                            labelUintPrice.AutoSize = false;
                            labelUintPrice.Width = 100;
                            labelUintPrice.Height = marginPanel - 5;
                            labelUintPrice.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelTotalPrice = new Label();
                            labelTotalPrice.Text = detailReader["TotalPrice"].ToString();
                            labelTotalPrice.Location = new Point(14, 1);
                            labelTotalPrice.AutoSize = false;
                            labelTotalPrice.Width = 132;
                            labelTotalPrice.Height = marginPanel - 5;
                            labelTotalPrice.TextAlign = ContentAlignment.MiddleCenter;

                            row.Controls.Add(labelTotalPrice);
                            row.Controls.Add(labelUintPrice);
                            row.Controls.Add(labelQuantity);
                            row.Controls.Add(labelProduct);
                            row.Controls.Add(labelId);

                            panel4.Controls.Add(row);
                            panelList.Add(row);
                        };
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
