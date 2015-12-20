using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class SuperAdminBLL
    {
        public int createNewUser(User_Details _user, string type)
        {
            DBHelper helper = new DBHelper();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter(CONSTANT.UID, _user.User_ID);
            param[1] = new SqlParameter(CONSTANT.USER_NAME, _user.User_Name);
            param[2] = new SqlParameter(CONSTANT.PASSWORD, _user.User_Password);
            param[3] = new SqlParameter(CONSTANT.DESIGNATION, _user.User_Designation);
            param[4] = new SqlParameter(CONSTANT.USER_EMAIL, _user.User_Email);
            param[5] = new SqlParameter(CONSTANT.TYPE, type);


            int res = helper.InsertQuery(PROCEDURES.ADD_NEW_USER, param);

            return res;



        }

        public string getUserDetais()
        {
            DBHelper helper = new DBHelper();
            DataTable dt = helper.executeSelectQuery(PROCEDURES.GET_USER_DETAIS).Tables[0];

            if (dt != null)
            {
                return (JSONConvertor.getJSONData(dt));
            }
            else
            {
                return null;
            }

        }

        public List<string> GetUserID(string mode)
        {
            List<string> lst = new List<string>();

            DBHelper helper = new DBHelper();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter(CONSTANT.MODE, mode);
            DataSet ds = helper.executeSelectQuery(PROCEDURES.DELETE_USER_OPERTAION, param);
            if (ds != null)
            {

                foreach (DataRow item in ds.Tables[0].Rows)
                {


                    lst.Add(item[0].ToString());


                }
            }

            return lst;

        }


        public List<string> GetUserID(string mode, string user_id)
        {
            List<string> lst = new List<string>();

            DBHelper helper = new DBHelper();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter(CONSTANT.MODE, mode);
            param[1] = new SqlParameter(CONSTANT.USER_ID, user_id);
            DataSet ds = helper.executeSelectQuery(PROCEDURES.DELETE_USER_OPERTAION, param);
            if (ds != null)
            {

                foreach (DataRow item in ds.Tables[0].Rows)
                {


                    lst.Add(item[0].ToString());


                }
            }

            return lst;

        }


        public int ChangePassword(string user_id, string Password)
        {
            DBHelper helper = new DBHelper();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter(CONSTANT.USER_ID, user_id);
            param[1] = new SqlParameter(CONSTANT.PASSWORD, Password);
            int res = helper.executeUpdateQuery(PROCEDURES.CHANGE_PASSWORD, param);
            return res;

        }

        public string PasswordExist(string user_id,string password)
        {
            DBHelper helper = new DBHelper();
            string str = string.Empty;
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter(CONSTANT.USER_ID, user_id);
            param[1] = new SqlParameter(CONSTANT.PASSWORD, password);
            DataSet  ds = helper.executeSelectQuery(PROCEDURES.PASSWORD_EXISTS, param);
            if (ds != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    str = item[0].ToString();
                }
            }

            return str;
        }

    }
}
