using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    class OrderSummary
    {
        public int Detail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Prod_Id { get; set; }
        public int Quantity { get; set; }
        public int Total_Amount { get; set; }
        public DateTime Purchase_Date { get; set; }
    }
}
