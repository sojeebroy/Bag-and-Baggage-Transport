using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspDotNetSummerProject.Models.Db;

namespace AspDotNetSummerProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(user s)
        {
            if(ModelState.IsValid)
            {
                var db = new AspdotNetSummerDBEntities();
                var user = (from p in db.users where p.email.Equals(s.email) && p.password.Equals(s.password) select p).SingleOrDefault();
                if (user != null)
                {
                    Session["logged_user"] = user.user_id;
                    if(user.user_type == "customer")
                    {
                        return RedirectToAction("CustomerHome", "Customer");
                    }
                    else if (user.user_type == "admin")
                    {
                        return RedirectToAction("Home", "Admin");
                    }
                    else if (user.user_type == "manager")
                    {
                        return RedirectToAction("Home", "Manager");
                    }
                    else if (user.user_type == "labour")
                    {
                        return RedirectToAction("Home", "Labour");
                    }
                    else if (user.user_type == "driver")
                    {
                        return RedirectToAction("Home", "Driver");
                    }

                }
                else
                {
                    TempData["msg"] = "invalid Email or password";
                    return View(s);
                }
            }
            return View(s);
        }
    }
}