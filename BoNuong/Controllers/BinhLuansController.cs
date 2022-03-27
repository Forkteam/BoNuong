﻿using System;
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
using PagedList;

namespace BoNuong.Controllers
{
    public class BinhLuansController : Controller
    {
        private BoNuongContext db = new BoNuongContext();

        // GET: BinhLuans
        public ActionResult Index()
        {
            var binhLuan = db.BinhLuan.Include(b => b.SanPham);
            return View(binhLuan.ToList());
        }

        // GET: BinhLuans/Details/5
        public ActionResult Details(int? id, int? page)
        {
            if (page == null) page = 1;
            var all_cmt = (from s in db.BinhLuan select s).OrderBy(m => m.NgayTao);
            int pageSize = 3;
            int pageNum = page ?? 1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(all_cmt.ToPagedList(pageNum, pageSize));
        }

        // GET: BinhLuans/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "Ten");
            return View();
        }

        // POST: BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string content, int id, [Bind(Include = "MaBinhLuan,NoiDung,MaSP,MaKH,NgayTao")] BinhLuan binhLuan)
        {
            // khong xet valid MaKH vi bang user dang nhap
            ModelState.Remove("MaKH");
            if (!ModelState.IsValid)
            {
                ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "Ten", binhLuan.MaSP);
                return View(binhLuan);
            }

            // lay login user id
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            binhLuan.MaKH = user.Id;
            binhLuan.MaSP = id;
            binhLuan.NoiDung = content;
            binhLuan.NgayTao = DateTime.Now;
            db.BinhLuan.Add(binhLuan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "Ten", binhLuan.MaSP);
            return View(binhLuan);
        }

        // POST: BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBinhLuan,NoiDung,MaSP,MaKH,NgayTao")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "Ten", binhLuan.MaSP);
            return View(binhLuan);
        }

        // GET: BinhLuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            db.BinhLuan.Remove(binhLuan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
