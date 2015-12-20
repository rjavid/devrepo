using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public  class Home
    {
        DBHelper helper;
        public Home()
        {
            helper = new DBHelper();
        }

        public bool getVal()
        { 
        
           SqlParameter[] param=new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.CAT_ID,"ZZZ");
          bool ds = helper.InsertQuery(PROCEDURES.CAT_INSERT, param);
          return ds;
        }
    }
}
