using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerUI.BLL;

namespace CustomerUI.Models
{
    public class ProductCategoryController : Controller
    {
        //
        // GET: /ProductCategory/

        public ActionResult Staples()
        {
            return GetProducts();
        }

        private ActionResult GetProducts()
        {

            string CategoryName = Request.QueryString["cat_name"];

            CategoryBLL bll = new CategoryBLL();
            List<ProductCategory> productDetails = bll.getCategory(CategoryName);

            return View(productDetails);
        }

        public ActionResult BrandedFoods()
        {
            return GetProducts();
        }

        public ActionResult Beverage()
        {
            return GetProducts();
        }


        public ActionResult DairyBreadEggs()
        {
            return GetProducts();
        }


        public ActionResult HouseHold()
        {
            return GetProducts();
        }


        public ActionResult ComboAndOffers()
        {
            return View();
        }


        public ActionResult PersonalCare()
        {
            return GetProducts();
        }

        public ActionResult PetCare()
        {
            return View();
        }

        public ActionResult Frozen()
        {
            return View();
        }

        public ActionResult AyurvedicandHerbals()
        {
            return View();
        }

        public ActionResult Gifts()
        {
            return GetProducts();
        }
    }
}
