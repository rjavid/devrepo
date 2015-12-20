using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerUI.Models;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace CustomerUI.BLL
{
    public class CategoryBLL
    {
        public List<ProductCategory> getCategory(string CategoryName)
        {

            List<ProductCategory> lst = new List<ProductCategory>();

            DBHelper helper = new DBHelper();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter(CONSTANT.CATEGORY_NAME, CategoryName);

            DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_CATEGORY_WISE_PRODUCT,param);
            
            if (ds != null)
            {

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ProductCategory sub = new ProductCategory();
                    sub.ProductName = row["Prod_Name"].ToString();
                    sub.ProductImage = row["Prod_Img"].ToString();
                    sub.ProductMRP = row["MRP"].ToString();
                    sub.ProductPrice = row["Selling Price"].ToString();
                    sub.ProductQuanity = row["packed_quantity"].ToString();
                    sub.ProductId = row["Prod_Id"].ToString();
                    sub.ProductBrand = row["Brand_Name"].ToString();
                    lst.Add(sub);
                }
            }


            return lst;
            
        }
    }
}