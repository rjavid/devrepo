using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    class OrderMaster
    {
          public int Cust_Id { get; set; }
          public int Order_Id { get; set; }
          public string Mobile_No { get; set; }
          public DateTime Purchase_Date { get; set; }
          public int Total_Purchase { get; set; }
          public int Total_Save { get; set; }
          public string Order_Status { get; set; }
          
    }
}
