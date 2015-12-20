using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BLL;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
namespace MVCApp.Controllers
{
    public class ProdAdminController : Controller
    {

        public static string user_Id;
        public static DataTable dt = new DataTable();


        public static List<SelectListItem> ddlSubCategory = null;
        public static List<SelectListItem> ddlbrand = null;

        public void getDdlsForProduct()
        {
            ProductAdminBLL bll = new ProductAdminBLL();
            Dictionary<string, string> lst = bll.GetSubcategory();
            List<string> str = new List<string>();
            List<SelectListItem> lstitm = new List<SelectListItem>();
            foreach (var item in lst)
            {
                lstitm.Add(new SelectListItem { Text = item.Value, Value = item.Key });
                str.Add(item.Value);
            }

            ddlSubCategory = lstitm;
            //ViewBag.ddlSubCategory = new SelectList(lstitm, "Value", "Text");
           // ViewBag.SubCategory = new SelectList(lstitm, "Value", "Text").ToList();

            lstitm = new List<SelectListItem>();
           
            List<Brand> lstbrand = bll.GetBrands();
            Dictionary<string, string> lstbrands = new Dictionary<string, string>();
            foreach (Brand item in lstbrand)
            {
                lstitm.Add(new SelectListItem { Text = item.Brand_Name, Value =Convert.ToString(item.Brand_Id) });
            }

            ddlbrand = lstitm;
           // ViewBag.DDLBrand = lstbrand;

        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult ProductAdminHome()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = Session["loginDetails"];
                getDdlsForProduct();
                ViewBag.DDLBrand = new SelectList(ddlbrand, "Value", "Text").ToList();
                ViewBag.SubCategory = new SelectList(ddlSubCategory, "Value", "Text").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
            
            
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ProductAdminHome(Product product)
        {
            // getDdlsForProduct();
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                getDdlsForProduct();
                ViewBag.DDLBrand = new SelectList(ddlbrand, "Value", "Text").ToList();
                ViewBag.SubCategory = new SelectList(ddlSubCategory, "Value", "Text").ToList();

                if (ModelState.IsValid)
                {
                    int res = bll.insertProduct(product);
                    if (res > 0)
                    {

                        return RedirectToAction("ProductAdminHome");
                    }
                }
                return View();
            }

            else
            {
                return RedirectToAction("Error");
            }
        }






        [HttpGet]
        public ActionResult ProdEdit(string id)
        {
            if (Session["loginDetails"] != null)
            {
                getDdlsForProduct();
                ViewBag.DDLBrand = new SelectList(ddlbrand, "Value", "Text").ToList();
                ViewBag.SubCategory = new SelectList(ddlSubCategory, "Value", "Text").ToList();

                ProductAdminBLL bll = new ProductAdminBLL();
                ProductDetails productdetails = bll.Get_product_list().Single(b => b.Prod_Id == id);

                return View(productdetails);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        [ActionName("ProdEdit")]
        public ActionResult ProdEdit_post(ProductDetails productdetails)
        {
            if (Session["loginDetails"] != null)
            {
                getDdlsForProduct();
                ViewBag.DDLBrand = new SelectList(ddlbrand, "Value", "Text").ToList();
                ViewBag.SubCategory = new SelectList(ddlSubCategory, "Value", "Text").ToList();

                ProductAdminBLL bll = new ProductAdminBLL();
                int res = bll.updateProduct(productdetails);
                if (res > 0)
                {

                   return RedirectToAction("ProductList");
                }

                return View();
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult ProdDelete(string id)
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                ProductDetails productdetails = bll.Get_product_list().Single(b => b.Prod_Id == id);

                return View(productdetails);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        [ActionName("ProdDelete")]
        public ActionResult ProdDelete_post(string id)
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                int res = bll.deleteProduct(id);
                if (res > 0)
                {

                    RedirectToAction("ProductList");
                }
                return View();
            }
            else
            {

                return RedirectToAction("Error");
            }
        }





        [HttpGet]
        public ActionResult ProductList()
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                List<ProductDetails> products = bll.Get_product_list();
                return View(products);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Category()
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                Dictionary<string, string> dict = bll.GetCategory();
                List<Category> category = new List<Category>();
                foreach (var item in dict)
                {
                    Category cat = new Category();
                    cat.Cat_Id = Convert.ToInt32(item.Key);
                    cat.Cat_Name = item.Value;
                    category.Add(cat);

                }

                return View(category);
            }
            else
            {

                return RedirectToAction("Error");
            }
        
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            if (Session["loginDetails"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ActionName("CreateCategory")]
        public ActionResult CreateCategory_Post(Category cat)
        {

            if (Session["loginDetails"] != null)
            {
                if (ModelState.IsValid)
                {
                    ProductAdminBLL bll = new ProductAdminBLL();
                    int res = bll.InsertCategory(cat.Cat_Name);
                    if (res > 0)
                    {
                        return RedirectToAction("Category");
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {

            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                Dictionary<string, string> dict = bll.GetCategory();
                List<Category> category = new List<Category>();
                foreach (var item in dict)
                {
                    Category cat = new Category();
                    cat.Cat_Id = Convert.ToInt32(item.Key);
                    cat.Cat_Name = item.Value;
                    category.Add(cat);

                }

                Category cate = category.Single(b => b.Cat_Id == id);
                return View(cate);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }



        [HttpGet]
        public ActionResult EditSubCategory(int id)
        {

            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                List<SubCategoryDetails> list = bll.getAllSubCategory();

                //Dictionary<string, string> dict = bll.GetCategory();
                //List<Category> category = new List<Category>();
                //foreach (var item in dict)
                //{
                //    Category cat = new Category();
                //    cat.Cat_Id = Convert.ToInt32(item.Key);
                //    cat.Cat_Name = item.Value;
                //    category.Add(cat);

                //}
                GetSubCategoryDDL();
                SubCategoryDetails cate = list.Single(b => b.Sub_Cat_Id == id);
                return View(cate);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }




        [HttpPost]
        public ActionResult EditSubCategory(SubCategoryDetails details)
        {
            
             if (Session["loginDetails"] != null)
            {

            ProductAdminBLL bll = new ProductAdminBLL();
            GetSubCategoryDDL();
            int res = bll.updateSubCategory(details);
            if (res > 0)
            {
                return RedirectToAction("SubCategory", "ProdAdmin");
            }
           
            return View();
            }
             else
             {
                 return RedirectToAction("Error");
             }
        }


        [HttpGet]
        public ActionResult DeleteSubCategory(int  id)
        {


            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                //bll.GetSubcategory().ToList().Single(b => b.Brand_Id == id);
                SubCategoryDetails det = bll.getAllSubCategory().ToList().Single(b => b.Sub_Cat_Id == id);

                return View(det);
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ActionName("DeleteSubCategory")]
        public ActionResult DeleteSubCategory_post(int id)
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                int res = bll.deleteSubCategory(id);
                if (res > 0)
                {
                    return RedirectToAction("SubCategory", "ProdAdmin");
                }
                return View();
            }
            else
            {

                return RedirectToAction("Error");
            }
        }



        [HttpPost]
        public ActionResult EditCategory(Category cat)
        {
            if (Session["loginDetails"] != null)
            {


                if (ModelState.IsValid)
                {
                    ProductAdminBLL bll = new ProductAdminBLL();
                    int res = bll.UpdateCategory(cat);
                    if (res > 0)
                    {
                        return RedirectToAction("Category");
                    }
                }
                return View();
            }
            else
            {

                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {

            if (Session["loginDetails"] != null)
            {

                ProductAdminBLL bll = new ProductAdminBLL();
                Dictionary<string, string> dict = bll.GetCategory();
                List<Category> category = new List<Category>();
                foreach (var item in dict)
                {
                    Category cat = new Category();
                    cat.Cat_Id = Convert.ToInt32(item.Key);
                    cat.Cat_Name = item.Value;
                    category.Add(cat);

                }

                Category cate = category.Single(b => b.Cat_Id == id);
                return View(cate);
            }
            else
            {

                return RedirectToAction("Error");
            }

        }



        [HttpPost]
        [ActionName("DeleteCategory")]
        public ActionResult DeleteCategory_Post(int id)
        {
             if (Session["loginDetails"] != null)
            {
                    ProductAdminBLL bll = new ProductAdminBLL();

                    int res = bll.DeleteCategory(id);
                    if (res > 0)
                    {
                        return RedirectToAction("Category");
                    }
                    else
                    {
                        return RedirectToAction("Category");
                    }

            }
             else
             {

                 return RedirectToAction("Error");
             }
        }




        [HttpGet]
        public ActionResult SubCategory()
        {
             if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                List < SubCategoryDetails > subCategory= bll.getAllSubCategory();


                //GetSubCategoryDDL();

                return View(subCategory);
             }
             else
             {

                 return RedirectToAction("Error");
             }
        }

         [HttpGet]
        public ActionResult CreateSubCategory()
        {
           if (Session["loginDetails"] != null)
            {
            GetSubCategoryDDL();
            return View();
            }
            else
            {

                return RedirectToAction("Error");
            }
        }

         [HttpPost]
         public ActionResult CreateSubCategory(FormCollection frm)
         {

             //string str = EnterSubCat(Convert.ToInt32(frm[0]), frm[1]);
             if (Session["loginDetails"] != null)
             {
                 ProductAdminBLL bll = new ProductAdminBLL();
                 int str = bll.EnterSubCat(Convert.ToInt32(frm[0]), frm[1]);
                 //return str;
                 if (str > 0)
                 {

                     return RedirectToAction("SubCategory");
                 }
                 else
                 {
                     ViewBag.CatSuccess = "Fail to Insert";
                     GetSubCategoryDDL();
                     return View();

                 }

             }
             else
             {
                 return RedirectToAction("Error");
             }
         }

        public void GetSubCategoryDDL()
        {
            
                ProductAdminBLL bll = new ProductAdminBLL();
                Dictionary<string, string> lst = bll.GetCategory();
                List<string> str = new List<string>();
                List<SelectListItem> lstitm = new List<SelectListItem>();
                foreach (var item in lst)
                {
                    lstitm.Add(new SelectListItem { Text = item.Value, Value = item.Key });
                    str.Add(item.Value);
                }

                //ViewBag.ddlSubCategory = new SelectList(lstitm, "Value", "Text");
                ViewBag.SubCategory = new SelectList(lstitm, "Value", "Text").ToList();
           
        }

        [HttpPost]
        public ActionResult SubCategory(FormCollection frm)
        {

            //string str = EnterSubCat(Convert.ToInt32(frm[0]), frm[1]);
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                int str = bll.EnterSubCat(Convert.ToInt32(frm[0]), frm[1]);
                //return str;
                if (str > 0)
                {

                    return RedirectToAction("SubCategory");
                }
                else
                {
                    ViewBag.CatSuccess = "Fail to Insert";
                    GetSubCategoryDDL();
                    return View();

                }

            }
            else
            {
                return RedirectToAction("Error");
            }
          
        }

        


        /*Brand */

        [HttpGet]
        public ActionResult Brand()
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();
                List<Brand> lst = bll.GetBrands();
                return View(lst);

            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult CreateBrand()
        {
            if (Session["loginDetails"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ActionName("CreateBrand")]
        public ActionResult CreateBrand_Post()
        {
            if (Session["loginDetails"] != null)
            {
                if (ModelState.IsValid)
                {
                    Brand brand = new Brand();
                    TryUpdateModel<Brand>(brand);

                    ProductAdminBLL bll = new ProductAdminBLL();
                    int res = bll.InsertBrand(brand);
                    if (res > 0)
                    {
                       return RedirectToAction("Brand");
                    }

                }

                return View();

                
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult EditBrand(int id)
        {
            if (Session["loginDetails"] != null)
            {
            ProductAdminBLL bll = new ProductAdminBLL();
            Brand brand = bll.GetBrands().ToList().Single(b => b.Brand_Id == id);
                //bll.Employees.Single(emp => emp.ID == id);
            
            return View(brand);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        [ActionName("EditBrand")]
        public ActionResult EditBrand_Post(Brand brand)
        {
            if (Session["loginDetails"] != null)
            {
                ProductAdminBLL bll = new ProductAdminBLL();

                if (ModelState.IsValid)
                {
                    int res = bll.UpdateBrand(brand);
                    if (res > 0)
                    {
                        return RedirectToAction("Brand");
                    }
                    else
                    {
                        return View();
                    }


                }
                else
                {
                    return View();
                }

            }
            else
            {
                return RedirectToAction("Error");
            }

        }



        [HttpGet]
        public ActionResult DeleteBrand(int id)
        {
           if (Session["loginDetails"] != null)
            {
              
            ProductAdminBLL bll = new ProductAdminBLL();
            Brand brand = bll.GetBrands().ToList().Single(b => b.Brand_Id == id);
            //bll.Employees.Single(emp => emp.ID == id);

            return View(brand);
            }
           else
           {
               return RedirectToAction("Error");
           }
        }





        [HttpPost]
        [ActionName("DeleteBrand")]
        public ActionResult DeleteBrand_Post(int Brand_Id)
        {
            if (Session["loginDetails"] != null)
            {
              
            ProductAdminBLL bll = new ProductAdminBLL();

            int res = bll.DeleteBrand(Brand_Id);
            if (res > 0)
            {
                return RedirectToAction("Brand");
            }
            else
            {
                return View();
            }

            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        /*End of Brand*/
        
        public string EnterBrand(string Brand)
        {

            ProductAdminBLL bll = new ProductAdminBLL();
            string str = bll.EnterBrand(Brand);
            return str;


        }




        //public string EnterSubCat(int catID, string subCategoryName)
        //{
        //    ProductAdminBLL bll = new ProductAdminBLL();
        //    string str = bll.EnterSubCat(catID, subCategoryName);
        //    return str;
        //}


       


        public string GetBrand()
        {
            ProductAdminBLL bll = new ProductAdminBLL();
            string str = bll.GetBrand();
            return str;

        }


        //public string GetSubcategory()
        //{
        //    ProductAdminBLL bll = new ProductAdminBLL();
        //    string str = bll.GetSubcategory();
        //    return str;

        //}

        public string EnterProduct(string image, string product, string SubCategoryId, string BrandID)
        {
            string[] images = image.Split('\\');
            image = images[2];


            ProductAdminBLL bll = new ProductAdminBLL();
            string str = bll.EnterProduct(image, product, SubCategoryId, BrandID);
            return str;


        }


        //public string GetCategory()
        //{
        //    ProductAdminBLL bll = new ProductAdminBLL();
        //    string str = bll.GetCategory();
        //    return str;

        //}



        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Admin", "Home");
        }

        public string getdata()
        {
            ProductAdminBLL bll = new ProductAdminBLL();
            string str = bll.getdata();
            return str;
        }


        public ActionResult insertProduct()
        {



            return View();
        
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = Session["loginDetails"];

                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection frm)
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = Session["loginDetails"];
                if (frm["txtNewPassword"].ToString() == frm["txtReTypePassword"].ToString())
                {

                    SuperAdminBLL bll = new SuperAdminBLL();
                    string re = bll.PasswordExist(Session["loginDetails"].ToString(), frm["txtCurrentPassword"].ToString());
                    if (re == "1")
                    {
                        int res = bll.ChangePassword(Session["loginDetails"].ToString(), frm["txtReTypePassword"].ToString());
                        if (res > 0)
                        {
                            return RedirectToAction("Index", "Admin");
                            //ViewBag.ChangeMessage = "Password Change Sucessfully";

                        }
                        return View();
                    }
                    else
                    {
                        ViewBag.ChangeMessage = "Incorrect Old Password";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ChangeMessage = "Password doesnot Match ";

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        //private string GetConnectionString()
        //{

        //    var connectionString = ConfigurationManager.ConnectionStrings["ado_GroccersPointConnectionString"].ConnectionString;
        //    return connectionString;
        //    //    "Data Source=10.224.151.111;Initial Catalog=CSEDGE_PMO_MVC;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;";
        //}

       



        


        //public string EnterSubCat(int catID, string subCategoryName)
        //{
        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 
        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {
        //        string cmd = "exec Admin_Insert_sub_category " + "[" + catID + "]" + ",[" + subCategoryName + "]";
        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);
        //        DataSet tds = new DataSet();
        //        sqlDataAapter.Fill(tds);
        //        DataTable dt = tds.Tables[0];

        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }
        //        return serializer.Serialize(rows);
        //    }
        //}




        //public string EnterCategory(string categoryName)
        //{
        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {

        //        string cmd = "exec Admin_InsertCategory " + "[" + categoryName + "]";



        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);

        //        DataSet tds = new DataSet();



        //        sqlDataAapter.Fill(tds);

        //        DataTable dt = tds.Tables[0];



        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //        Dictionary<string, object> row;

        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            row = new Dictionary<string, object>();

        //            foreach (DataColumn col in dt.Columns)
        //            {

        //                row.Add(col.ColumnName, dr[col]);

        //            }

        //            rows.Add(row);

        //        }

        //        return serializer.Serialize(rows);



        //    }

        //}//end of insert category




        //public string GetBrand()
        //{

        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {

        //        string cmd = "exec Admin_get_brand ";



        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);

        //        DataSet tds = new DataSet();



        //        sqlDataAapter.Fill(tds);

        //        DataTable dt = tds.Tables[0];



        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //        Dictionary<string, object> row;

        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            row = new Dictionary<string, object>();

        //            foreach (DataColumn col in dt.Columns)
        //            {

        //                row.Add(col.ColumnName, dr[col]);

        //            }

        //            rows.Add(row);

        //        }

        //        return serializer.Serialize(rows);



        //    }
        //}



        //public string GetSubcategory()
        //{

        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {

        //        string cmd = "exec Admin_get_subCategory ";



        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);

        //        DataSet tds = new DataSet();



        //        sqlDataAapter.Fill(tds);

        //        DataTable dt = tds.Tables[0];



        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //        Dictionary<string, object> row;

        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            row = new Dictionary<string, object>();

        //            foreach (DataColumn col in dt.Columns)
        //            {

        //                row.Add(col.ColumnName, dr[col]);

        //            }

        //            rows.Add(row);

        //        }

        //        return serializer.Serialize(rows);



        //    }
        //}


        



        //public string GetCategory()
        //{

        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {

        //        string cmd = "exec Admin_getCategory ";
        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);
        //        DataSet tds = new DataSet();
        //        sqlDataAapter.Fill(tds);
        //        DataTable dt = tds.Tables[0];

        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);

        //        }
        //        return serializer.Serialize(rows);
        //    }

        //}
       
        //public string EnterProduct(string image, string product, string SubCategoryId, string BrandID)
        //{
        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {
        //        string[] images = image.Split('\\');
        //        image = images[2];
        //        string cmd = "exec Admin_EnterProduct" + "[" + image + "]" + ",[" + product + "]" + ",[" + SubCategoryId + "]" + ",[" + BrandID + "]";
        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);
        //        DataSet tds = new DataSet();
        //        sqlDataAapter.Fill(tds);
        //        DataTable dt = tds.Tables[0];

        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);

        //        }
        //        return serializer.Serialize(rows);
        //    }

        //}


        

        //public string getdata()
        //{

        //    string connectionString = GetConnectionString(); //= @"Provider=System.Data.SqlClient;Data Source=10.224.151.111;Initial Catalog=CSPMO_PUNE;Integrated Security=false;User ID=sa;Password=password@123;Timeout=700;"; 

        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    {

        //        string cmd = "exec Admin_getCategory ";
        //        SqlDataAdapter sqlDataAapter = new SqlDataAdapter(cmd, connectionString);
        //        DataSet tds = new DataSet();
        //        sqlDataAapter.Fill(tds);
        //        DataTable dt = tds.Tables[0];

        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);

        //        }
        //        return serializer.Serialize(rows);
        //    }


        //}

        //public string abc() {
        //    dt.Clear();
        //    ProductAdminBLL bll = new ProductAdminBLL();
        //    dt = bll.Categories();

        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        //    Dictionary<string, object> row;

        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        row = new Dictionary<string, object>();

        //        foreach (DataColumn col in dt.Columns)
        //        {

        //            row.Add(col.ColumnName, dr[col]);

        //        }

        //        rows.Add(row);

        //    }

        //    return serializer.Serialize(rows);
        //}
          

    }
}
