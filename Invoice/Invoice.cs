using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerName { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
    }

        
}
