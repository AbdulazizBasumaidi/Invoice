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
    public partial class InvoiceListForm : Form
    {
        public InvoiceListForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InvoiceListForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        List<Panel> panelList = new List<Panel>();
        private void LoadData()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "select * from Invoices";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int _id = Convert.ToInt32(reader["InvoiceID"]);
                            int marginPanel = 35;
                            Panel row = new Panel();
                            row.Dock = DockStyle.Top;
                            row.Height = marginPanel;
                            row.BackColor = Color.White;
                            row.ForeColor = Color.Black;

                            Panel row2 = new Panel();
                            row2.Dock = DockStyle.Top;
                            row2.Height = marginPanel - 2;
                            row2.BackColor = Color.White;
                            row2.Font = new Font("Arial", 12F, FontStyle.Bold);
                            row2.ForeColor = Color.Black;
                            row2.BorderStyle = BorderStyle.FixedSingle;


                            panel3.Height = marginPanel + ((panelList.Count * marginPanel));

                            Label labelId = new Label();
                            labelId.Text = reader["InvoiceID"].ToString();
                            labelId.Dock = DockStyle.Right;
                            labelId.AutoSize = false;
                            labelId.Width = 83;
                            labelId.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelCustomer = new Label();
                            labelCustomer.Text = reader["CustomerName"].ToString();
                            labelCustomer.Dock = DockStyle.Right;
                            labelCustomer.AutoSize = false;
                            labelCustomer.Width = 400;
                            labelCustomer.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelDate = new Label();
                            labelDate.Text = Convert.ToDateTime(reader["InvoiceDate"]).ToString("dd/MM/yyyy");
                            labelDate.Dock = DockStyle.Right;
                            labelDate.AutoSize = false;
                            labelDate.Width = 100;
                            labelDate.TextAlign = ContentAlignment.MiddleCenter;

                            Label labelTotal = new Label();
                            labelTotal.Text = reader["TotalAmount"].ToString();
                            labelTotal.Dock = DockStyle.Right;
                            labelTotal.AutoSize = false;
                            labelTotal.Width = 150;
                            labelTotal.TextAlign = ContentAlignment.MiddleCenter;


                            Button buttonDelete = new Button();
                            buttonDelete.Width = 80;
                            buttonDelete.Height = 29;
                            buttonDelete.Location = new Point(1, 1);
                            buttonDelete.Margin = new Padding(0);
                            buttonDelete.Text = "حذف";
                            buttonDelete.FlatStyle = FlatStyle.Flat;
                            buttonDelete.FlatAppearance.BorderSize = 0;
                            buttonDelete.BackColor = Color.LightCoral;
                            buttonDelete.Click += (s, args) =>
                            {
                                var confirm = MessageBox.Show("هل تريد حذف الفاتورة؟", "تأكيد", MessageBoxButtons.YesNo);
                                if (confirm == DialogResult.Yes)
                                {
                                    try
                                    {
                                        Detete(_id);
                                        panel3.Controls.Remove(row);
                                        panelList.Remove(row);
                                        panel3.Height = ((panelList.Count * marginPanel));
                                        row.Focus();
                                        MessageBox.Show("تم حذف الفاتورة بنجاح");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                    
                                }

                                
                            };
                                


                            Button buttonEdit = new Button();
                            buttonEdit.Width = 80;
                            buttonEdit.Height = 29;
                            buttonEdit.Location = new Point(84, 1);
                            buttonEdit.Margin = new Padding(0);
                            buttonEdit.Text = "تعديل";
                            buttonEdit.FlatStyle = FlatStyle.Flat;
                            buttonEdit.FlatAppearance.BorderSize = 0;
                            buttonEdit.BackColor = Color.LightBlue;
                            EditInvoiceForm Eide = new EditInvoiceForm(_id);
                            buttonEdit.Click += (s, args) => Eide.ShowDialog();
                            Eide.FormClosed += (s, args) =>
                            {
                                
                                panel3.Controls.Clear();
                                panelList.Clear();
                                LoadData();
                            };


                            Button buttonView = new Button();
                            buttonView.Width = 80;
                            buttonView.Height = 29;
                            buttonView.Location = new Point(167, 1);
                            buttonView.Margin = new Padding(0);
                            buttonView.Text = "عرض";
                            buttonView.FlatStyle = FlatStyle.Flat;
                            buttonView.FlatAppearance.BorderSize = 0;
                            buttonView.BackColor = Color.LightGreen;
                            InvoiceViewForm view = new InvoiceViewForm(_id);
                            buttonView.Click += (s, args) => view.ShowDialog();

                            row.Controls.Add(row2);
                            row2.Controls.Add(buttonDelete);
                            row2.Controls.Add(buttonEdit);
                            row2.Controls.Add(buttonView);
                            row2.Controls.Add(labelTotal);
                            row2.Controls.Add(labelDate);
                            row2.Controls.Add(labelCustomer);
                            row2.Controls.Add(labelId);

                            panel3.Controls.Add(row);
                            panelList.Add(row);
                            row.Focus();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void Detete(int _id)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Delete from InvoiceDetails where InvoiceID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmdd = new SqlCommand("Delete from Invoices where InvoiceID = @id", conn))
                {
                    cmdd.Parameters.AddWithValue("@id", _id);
                    cmdd.ExecuteNonQuery();
                }
                
            }            
        }
    }
}
