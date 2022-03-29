using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoNuong.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult error401()
        {
            return View();
        }
        public ActionResult error404()
        {
            return View();
        }
        public ActionResult error500()
        {
            return View();
        }
        public ActionResult charts()
        {
            return View();
        }
        public ActionResult layoutsidenavlight()
        {
            return View();
        }
        public ActionResult layoutstatic ()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult password()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
        public ActionResult tables()
        {
            return View();
        }
    }
}