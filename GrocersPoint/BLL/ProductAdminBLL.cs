using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
   public class ProductAdminBLL
    {


       public List<SubCategoryDetails> getAllSubCategory()
       {
           List<SubCategoryDetails> AllCategory = new List<SubCategoryDetails>();
         DBHelper helper = new DBHelper();
         DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_ALL_CATEGORY);
         if(ds!=null)
         {
             
            foreach (DataRow row in ds.Tables[0].Rows)
	            {
                    SubCategoryDetails sub = new SubCategoryDetails();
                    sub.Cat_Name=row["Cat_Name"].ToString();
                    sub.Sub_Cat_Id=Convert.ToInt32(row["Sub_Cat_Id"]);
                    sub.Sub_Cat_Name = row["Sub_Cat_Name"].ToString();
		            AllCategory.Add(sub);
	            }
         }

        

         return AllCategory;
       }

       public int deleteProduct(string product_id)
       {
           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.PRODUCT_ID, product_id);
           int res = helper.InsertQuery(PROCEDURES.DELETE_PRODUCT, param);
           return res;
       }


       public int deleteSubCategory(int id)
       {
           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.SUB_CATEGORY_ID, id);
           int res = helper.InsertQuery(PROCEDURES.DELETE_SUB_CATEGORY, param);

           return res;
        
       }

       public string EnterBrand(string Brand)
        {
            //List<Category> cat = new List<Category>();
            DBHelper helper = new DBHelper();
           
            SqlParameter[] param=new SqlParameter[1];
            param[0] = new SqlParameter(CONSTANT.BRAND_NAME, Brand);
            //Category catg = null;
            DataTable dt = helper.executeSelectQuery(PROCEDURES.INSERT_BRAND, param).Tables[0];
           if(dt!=null)
           {
             return  JSONConvertor.getJSONData(dt);
              
           }
           else
           {
               return string.Empty;
           }
           
        }

       public List<Brand> GetBrands()
       {


           List<Brand> brandlst = new List<Brand>();
           DBHelper helper = new DBHelper();
            
           DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_ALL_BRANDS);
           if (ds != null)
           {
               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   Brand brand= new Brand();
                   brand.Brand_Id  = Convert.ToInt32 (row["Brand_Id"]);
                   brand.Brand_Name = row["Brand_Name"].ToString();

                   brandlst.Add(brand);
               }
           }


           return brandlst;
       
       }

       public int InsertBrand(Brand brand)
       {

           DBHelper helper = new DBHelper();
           SqlParameter []parm=new SqlParameter[1];
           parm[0]=new SqlParameter(CONSTANT.BRAND_NAME,brand.Brand_Name);
           int res = helper.InsertQuery(PROCEDURES.INSERT_BRAND, parm);
           return res;

       }

       public int UpdateBrand(Brand brand)
       {
           DBHelper helper = new DBHelper();
           SqlParameter[] parm = new SqlParameter[2];
           parm[0] = new SqlParameter(CONSTANT.BRAND_ID, brand.Brand_Id);
           parm[1] = new SqlParameter(CONSTANT.BRAND_NAME, brand.Brand_Name);

           int res = helper.executeUpdateQuery(PROCEDURES.UPDATE_BRAND, parm);
          return res;
       
       }

       public int DeleteBrand(int Brand_Id)
       {

           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.BRAND_ID, Brand_Id);
           int res = helper.InsertQuery(PROCEDURES.DELETE_BRAND, param);
           return res;
       
       }

       public List<ProductDetails> Get_product_list()
       {

           List<ProductDetails> products = new List<ProductDetails>();
           DBHelper helper = new DBHelper();
           DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_PRODUCTS);
           if (ds != null)
           {

               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   ProductDetails product = new ProductDetails();
                   product.Prod_Id = row["Prod_Id"].ToString();
                   product.Prod_Name = row["Prod_Name"].ToString();
                   product.Prod_Img = row["Prod_Img"].ToString();
                   product.Brand_Name = row["Brand_Name"].ToString();
                   product.Sub_Cat_Name = row["Sub_Cat_Name"].ToString();
                   //product. = row["Prod_Img"].ToString();
                   product.Price = Convert.ToInt32(row["Price"]);
                   product.Offer_Price= Convert.ToInt32(row["offer_price"]);
                   product.Purchase_Price = Convert.ToInt32(row["Purchase_Price"]);
                   product.Prod_Quantity = row["packed_quantity"].ToString();
                   products.Add(product);
               }
           }
           return products;
 
       }


       public int EnterSubCat(int catID, string subCategoryName)
       {
           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[2];
           param[0] = new SqlParameter(CONSTANT.CAT_ID , catID );
           param[1] = new SqlParameter(CONSTANT.SUB_CAT_NAME, subCategoryName);
           int res = helper.InsertQuery(PROCEDURES.INSERT_SUB_CATEGORY, param);
           return res;


       }


       public int InsertCategory(string categoryName)
       {
            DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.CATEGORY_NAME, categoryName);

           int res = helper.InsertQuery(PROCEDURES.INSERT_CATEGORY, param);
           return res;
       }

       public int UpdateCategory(Category cat)
       {
           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[2];
           param[0] = new SqlParameter(CONSTANT.CAT_ID, cat.Cat_Id);

           param[1] = new SqlParameter(CONSTANT.CATEGORY_NAME, cat.Cat_Name);
           int res = helper.InsertQuery(PROCEDURES.UPDATE_CATEGORY, param);
           return res;
       
       }

       public int DeleteCategory(int cat_id)
       {
           DBHelper helper = new DBHelper();

           SqlParameter[] param = new SqlParameter[1];
           param[0] = new SqlParameter(CONSTANT.CAT_ID, cat_id);
           int res = helper.InsertQuery(PROCEDURES.DELETE_CATEGORY, param);

           return res;
       }


       public string GetBrand()
       {
           DBHelper helper = new DBHelper();
           DataTable dt = helper.executeSelectQuery(PROCEDURES.GET_BRAND).Tables[0];
           if (dt != null)
           {
               return JSONConvertor.getJSONData(dt);

           }
           else
           {
               return string.Empty;
           }

       }

       public int updateSubCategory(SubCategoryDetails sub)
       {

           DBHelper helper = new DBHelper();
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter(CONSTANT.SUB_CATEGORY_ID, sub.Sub_Cat_Id);
           param[1] = new SqlParameter(CONSTANT.SUB_CAT_NAME, sub.Sub_Cat_Name);
           param[2] = new SqlParameter(CONSTANT.CAT_ID, sub.Cat_Name);
           int res = helper.executeUpdateQuery(PROCEDURES.UPDATE_SUB_CATEGORY, param);
           return res;
       }


       public Dictionary<string, string> GetSubcategory()
       {
           DBHelper helper = new DBHelper();
           Dictionary<string, string> lst = new Dictionary<string, string>();
           DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_SUB_CATEGORY);
           if (ds != null)
           {
               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   lst.Add(row["Sub_Cat_Id"].ToString(), row["Sub_Cat_Name"].ToString());
               }

           }

           return lst;


       }

       public Dictionary<string,string> GetCategory()
       {
          
           DBHelper helper = new DBHelper();
           Dictionary<string, string> lst = new Dictionary<string,string>();
           DataSet ds = helper.executeSelectQuery(PROCEDURES.GET_CATEGORY);
           if (ds != null)
           {
               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   lst.Add(row["Cat_Id"].ToString(), row["Cat_Name"].ToString());
               }

           }

           return lst;


       }


       public string EnterProduct(string image, string product, string SubCategoryId, string BrandID)
       {
           DBHelper helper = new DBHelper();
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter(CONSTANT.CAT_ID , image );
           param[1] = new SqlParameter(CONSTANT.SUB_CAT_NAME, product);
           param[2] = new SqlParameter(CONSTANT.CAT_ID , SubCategoryId );
           param[3] = new SqlParameter(CONSTANT.SUB_CAT_NAME, BrandID);
           DataTable dt = helper.executeSelectQuery(PROCEDURES.INSERT_PRODUCT, param).Tables[0];
           if (dt != null)
           {
               return JSONConvertor.getJSONData(dt);

           }
           else
           {
               return string.Empty;
           }
            

       }

       public string getdata()
       {
           DBHelper helper = new DBHelper();
           DataTable dt = helper.executeSelectQuery(PROCEDURES.GET_CATEGORY).Tables[0];
           if (dt != null)
           {
               return JSONConvertor.getJSONData(dt);

           }
           else
           {
               return string.Empty;
           }

       }

       public int insertProduct(Product product)
       {
           DBHelper helper = new DBHelper();
           SqlParameter[] param = new SqlParameter[8];
           param[0] = new SqlParameter(CONSTANT.SUB_CATEGORY_ID, product.Sub_Cat_Id);
           param[1] = new SqlParameter(CONSTANT.BRAND_ID, product.Brand_Id);
           //param[2] = new SqlParameter(CONSTANT.PRODUCT_ID, product.Prod_Id);
           param[2] = new SqlParameter(CONSTANT.PRODUCT_NAME, product.Prod_Name);
           param[3] = new SqlParameter(CONSTANT.PRODUCT_IMAGE, product.Prod_Img);
           param[4] = new SqlParameter(CONSTANT.PRICE, product.Price);
           param[5] = new SqlParameter(CONSTANT.OFFER_PRICE, product.Offer_Price);
           param[6] = new SqlParameter(CONSTANT.PURCHASE_PRICE, product.Purchase_Price);
           param[7] = new SqlParameter(CONSTANT.PACKING_QUANITY, product.Prod_Quantity);

           int res = helper.InsertQuery(PROCEDURES.INSERT_PRODUCT, param);
           return res;

       }

       public int updateProduct(ProductDetails product)
       {
           DBHelper helper = new DBHelper();
           SqlParameter[] param = new SqlParameter[9];
           param[0] = new SqlParameter(CONSTANT.SUB_CATEGORY_ID, product.Sub_Cat_Name);
           param[1] = new SqlParameter(CONSTANT.BRAND_ID, product.Brand_Name);
           param[2] = new SqlParameter(CONSTANT.PRODUCT_ID, product.Prod_Id);
           param[3] = new SqlParameter(CONSTANT.PRODUCT_NAME, product.Prod_Name);
           param[4] = new SqlParameter(CONSTANT.PRODUCT_IMAGE, product.Prod_Img);
           param[5] = new SqlParameter(CONSTANT.PRICE, product.Price);
           param[6] = new SqlParameter(CONSTANT.OFFER_PRICE, product.Offer_Price);
           param[7] = new SqlParameter(CONSTANT.PURCHASE_PRICE, product.Purchase_Price);
           param[8] = new SqlParameter(CONSTANT.PACKING_QUANITY, product.Prod_Quantity);

           int res = helper.InsertQuery(PROCEDURES.UPDATE_PRODUCTS, param);
           return res;

       }


    }
}
