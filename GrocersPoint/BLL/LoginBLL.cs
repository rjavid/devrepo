using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.SqlClient;
using System.Data;
namespace BLL
{
  public  class LoginBLL
    {

      public string LoginCheck(User_Details _user)
      {

          DBHelper helper = new DBHelper();
          SqlParameter []param=new SqlParameter[2];
          param[0]=new SqlParameter(CONSTANT.UID,_user.User_ID);

          string flag = string.Empty;
          param[1]=new SqlParameter(CONSTANT.PASSWORD,_user.User_Password);
           DataTable dt= helper.executeSelectQuery(PROCEDURES.LOGIN_CHECK, param).Tables[0];
           if (dt != null && dt.Rows.Count > 0)
           {

               foreach (DataRow item in dt.Rows)
               {
                   flag = item["Role_Name"].ToString();      
               }

           }
           return flag;
      
      }

    }
}
