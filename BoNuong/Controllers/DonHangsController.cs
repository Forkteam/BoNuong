using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoNuong.Models;
using BoNuong.Models.LinQ;
using PagedList;

namespace BoNuong.Controllers
{
    public class DonHangsController : Controller
    {
        private BoNuongContext db = new BoNuongContext();
        private MyDataDataContext data = new MyDataDataContext();

        // GET: DonHangs
        public ActionResult Index(int? page)
        {
            var all_donHang = data.DonHangs.ToList();
            int pageSize = 10;
            int pageNum = page ?? 1;
            return View(all_donHang.ToPagedList(pageNum, pageSize));
        }

        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.LinQ.DonHang donHang = data.DonHangs.FirstOrDefault(d => d.MaDH == id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(data.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,TrangThaiGiaoHang,NgayDat,NgayGiao,MaKH")] Models.DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHang.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(data.AspNetUsers, "Id", "Email");
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,TrangThaiGiaoHang,NgayDat,NgayGiao,MaKH")] Models.DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.LinQ.DonHang donHang = data.DonHangs.FirstOrDefault(d => d.MaDH == id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.DonHang donHang = db.DonHang.Find(id);
            db.DonHang.Remove(donHang);
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
