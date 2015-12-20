using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public struct PROCEDURES
    {
        //public static string CAT_INSERT = "Admin_InsertCategory";
        public static string GET_ALL_CATEGORIES = "GetAllCategories";
        public static string LOGIN_CHECK = "login_check_SP";
        public static string ADD_NEW_USER = "Add_New_User";
        public static string  GET_USER_DETAIS= "getUserDetais";
        public static string  DELETE_USER_OPERTAION= "delete_user";
        public static string CHANGE_PASSWORD ="Change_Password";
        public static string PASSWORD_EXISTS = "password_exist";


        public static string INSERT_BRAND = "Admin_InsertBrand";
        public static string INSERT_SUB_CATEGORY = "Admin_Insert_sub_category";
        public static string INSERT_CATEGORY = "Admin_InsertCategory";
        public static string GET_BRAND = "Admin_get_brand";
        public static string GET_SUB_CATEGORY = "Admin_get_subCategory";
        public static string GET_CATEGORY = "Admin_getCategory";
        public static string GET_ALL_BRANDS = "get_all_brands";
        public static string UPDATE_BRAND = "update_brand";
        public static string DELETE_BRAND = "delete_brand";
        public static string UPDATE_CATEGORY = "update_category";
        public static string DELETE_CATEGORY = "delete_category";

        public static string GET_ALL_CATEGORY = "getAllSubCategory";
        public static string UPDATE_SUB_CATEGORY = "updateSubCategory";
        public static string DELETE_SUB_CATEGORY = "deleteSubCategory";
        public static string INSERT_PRODUCT = "insertProduct";
        public static string GET_PRODUCTS = "Get_product_list";
        public static string UPDATE_PRODUCTS = "updateProduct";
        public static string DELETE_PRODUCT = "delete_product";

        //Customer UI

        public static string GET_CATEGORY_WISE_PRODUCT = "GetCategoryWiseProduct";

    }
    public struct CONSTANT
    {

        public static string CAT_ID = "@category_id";
        public static string UID = "@uid";
        public static string PASSWORD = "@pass";
        public static string DESIGNATION = "@User_Designation";
        public static string USER_NAME = "@user_name";
        public static string USER_EMAIL = "@user_Email";
        public static string  TYPE= "@Type";
        public static string MODE = "@mode";
        public static string USER_ID = "@user_id";
        public static string OLD_PASSWORD = "@old_pass";

        public static string BRAND_NAME = "@Brand_name";
        public static string SUB_CAT_NAME = "@SubCategory_name";
        public static string CATEGORY_NAME = "@category_name";
        public static string IMAGE = "@image_name";
        public static string SUB_CATEGORY_ID = "@sub_cat_id";
        public static string BRAND_ID = "@brand_id";
        public static string PRODUCT_ID = "@product_id";
        public static string PRODUCT_NAME = "@product_Name";
        public static string PRODUCT_IMAGE = "@product_img";
        public static string PRICE = "@price";
        public static string OFFER_PRICE = "@Offer_Price";
        public static string PURCHASE_PRICE = "@Purchase_Price";
        public static string PACKING_QUANITY = "@Packed_Quantity";


        //Customer UI



    }
}
