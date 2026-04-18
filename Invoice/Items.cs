using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invoice
{
    public class Items
    {
        public TextBox txtProductName { get; set; }
        public TextBox txtPrice { get; set; }
        public TextBox txtTotalPrice { get; set; }
        public TextBox txtQuantity { get; set; }
        public Button btnDelete { get; set; }
    }
}
