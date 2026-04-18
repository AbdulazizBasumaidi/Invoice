using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Invoice
{
    public class InvoiceDAL
    {
        public int AddInvoice(Invoice invoice)
        {
            int invoiceId = 0;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                try
                {
                    string sqlInvoice = "Insert into Invoices (CustomerName, InvoiceDate, Total, Discount, TotalAmount) output inserted.InvoiceID values (@CustomerName, @InvoiceDate, @Total, @Discount, @TotalAmount)";
                    using (SqlCommand cmdInvoice = new SqlCommand(sqlInvoice, conn))
                    {
                        cmdInvoice.Parameters.AddWithValue("@CustomerName", invoice.CustomerName);
                        cmdInvoice.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                        cmdInvoice.Parameters.AddWithValue("@Total", invoice.Total);
                        cmdInvoice.Parameters.AddWithValue("@Discount", invoice.Discount);
                        cmdInvoice.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                        invoiceId = (int)cmdInvoice.ExecuteScalar();

                        foreach (var detail in invoice.Details)
                        {
                            string sqlDetail = "Insert into InvoiceDetails (InvoiceID, ProductName, Quantity, UnitPrice, TotalPrice) values (@InvoiceID, @ProductName, @Quantity, @UnitPrice, @TotalPrice)";
                            using (SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn))
                            {
                                cmdDetail.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                cmdDetail.Parameters.AddWithValue("@ProductName", detail.ProductName);
                                cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                                cmdDetail.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                                cmdDetail.Parameters.AddWithValue("@TotalPrice", detail.TotalPrice);
                                cmdDetail.ExecuteNonQuery();
                            }

                        }
                    }

                }
                catch
                {
                    throw;
                }
                conn.Close();
                return invoiceId;
            }
        }

    }
}
