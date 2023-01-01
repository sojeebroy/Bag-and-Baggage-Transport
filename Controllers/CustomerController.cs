using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspDotNetSummerProject.Models;
using AspDotNetSummerProject.Models.Db;
using Newtonsoft.Json;

namespace AspDotNetSummerProject.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(user u,customer c)
        {
            if (ModelState.IsValid)
            {
                var db = new AspdotNetSummerDBEntities();
                u.user_type = "customer";
                db.users.Add(u);
                db.SaveChanges();
                var user = (from p in db.users where u.email.Equals(p.email) select p).SingleOrDefault();
                Session["logged_user"] = user.user_id;
                c.user_id = user.user_id;
                c.customer_name = "null";
                c.dob = new DateTime(1999,02,11);
                c.address = "null";
                c.phone = 0000;
                db.customers.Add(c);
                db.SaveChanges();
                return RedirectToAction("profile", "Customer");
            }
            else
            {
                return View(u);

            }       
        }
        public ActionResult CustomerHome()
        {
            ViewBag.userId = Session["logged_user"];
            return View();
        }
        public ActionResult info()
        {
            return View();
        }
        public async Task<ActionResult> test()
        {
            getLocationJSN model = new getLocationJSN();
            GeoHelper geoHelper = new GeoHelper();
            var result = await geoHelper.GetGeoInfo();
            model = JsonConvert.DeserializeObject<getLocationJSN>(result);

            return View(model);

        }
        public ActionResult search()
        {
            return View();
        }
        public ActionResult profile()
        {
            var tempuser_id = ((int)Session["logged_user"]);


            var db = new AspdotNetSummerDBEntities();
            var user = (from p in db.customers where p.user_id == tempuser_id select p).SingleOrDefault();
            var usersinfo = (from n in db.users where n.user_id == tempuser_id select n).SingleOrDefault();
            ViewBag.email= usersinfo.email;
            ViewBag.pasword = usersinfo.password;

            return View(user);
        }
        [HttpGet]
        public ActionResult edit()
        {
                return View();
        }
        [HttpPost]
        public ActionResult edit(user u,customer c)
        {
            var tempuser_id = ((int)Session["logged_user"]);
            var db = new AspdotNetSummerDBEntities();
            var user = (from p in db.customers where p.user_id == tempuser_id select p).SingleOrDefault();
            var usersinfo = (from n in db.users where n.user_id == tempuser_id select n).SingleOrDefault();
            ViewBag.email = usersinfo.email;
            ViewBag.pasword = usersinfo.password;

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Remove("logged_user");
            return RedirectToAction("Login","Home");
        }
    }
}