using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Data;
namespace MVCApp.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        public static string user_Id;
       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(BLL.User_Details user)
        {
            
            if (ModelState.IsValid)
            {
                LoginBLL bll = new LoginBLL();
                string str = bll.LoginCheck(user);
                if (str == "Admin")
                {
                    //ViewBag.LoginID = user.User_ID;
                    Session["loginDetails"] = TempData["LoginId"] = user_Id = user.User_ID;
                    return RedirectToAction( "NewUserRegistration","Admin");
                }
                else if (str == "Prod_Admin")
                {
                    Session["loginDetails"] = user_Id = user.User_ID; ;
                    return RedirectToAction("ProductAdminHome", "ProdAdmin");
                }
                else if (str == "User")
                {

                    return RedirectToAction("User", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }


        public ActionResult ProductAdmin()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }


        [HttpGet]
        public ActionResult NewUserRegistration()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = user_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult NewUserRegistration(BLL.User_Details user)
        {
            if (Session["logindetails"] != null)
            {
                TempData["LoginId"] = user_Id;
                SuperAdminBLL bll = new SuperAdminBLL();
                if (ModelState.IsValid)
                {

                    int res = bll.createNewUser(user, user.Role);
                    if (res > 0)
                    {
                        ViewBag.Message = "Sucess";
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult ShowUser()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = user_Id;
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult DeleteUser()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = user_Id;
                SuperAdminBLL bll = new SuperAdminBLL();
                List<string> lst = bll.GetUserID("1");
                List<SelectListItem> slst = new List<SelectListItem>();
                foreach (string item in lst)
                {
                    slst.Add(new SelectListItem { Text = item, Value = item });
                }

                ViewBag.DropDown = new SelectList(lst);
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(FormCollection frm)
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = user_Id;
                string user_id = frm["ddluser"].ToString();
                SuperAdminBLL bll = new SuperAdminBLL();
                List<string> lst = bll.GetUserID("2", user_id);
                if (lst.Contains("Delete"))
                {
                    return RedirectToAction("DeleteUser");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["loginDetails"] != null)
            {
                TempData["LoginId"] = user_Id;

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
                TempData["LoginId"] = user_Id;




                if (frm["txtNewPassword"].ToString() == frm["txtReTypePassword"].ToString())
                {

                    SuperAdminBLL bll = new SuperAdminBLL();
                    string re = bll.PasswordExist(user_Id, frm["txtCurrentPassword"].ToString());
                    if (re == "1")
                    {
                        int res = bll.ChangePassword(user_Id, frm["txtReTypePassword"].ToString());
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

        public string GetShowUser()
        {
            TempData["LoginId"] = user_Id;
            SuperAdminBLL bll = new SuperAdminBLL();
            return bll.getUserDetais();
        }



       



    }
}
