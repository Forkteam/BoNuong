using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoNuong.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BoNuong.Controllers
{
    public class HomeController : Controller
    {
        private BoNuongContext db = new BoNuongContext();
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.LoaiSP);
            return View(sanPham.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Blogdetails()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Shopdetails()
        {
            return View();
        }
        public ActionResult Shopgrid()
        {
            return View();
        }
        public ActionResult Shopcart()
        {
            return View();
        }
    }
}