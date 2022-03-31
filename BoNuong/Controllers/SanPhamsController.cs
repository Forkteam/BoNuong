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
using PagedList;

namespace BoNuong.Controllers
{
    public class SanPhamsController : Controller
    {
        private BoNuongContext db = new BoNuongContext();

        // GET: SanPhams

        public ActionResult Index(int? page)
        {
            var loaiSP = db.LoaiSP.ToList();
            //if (page == null) page = 1;
            var all_sanPham = (from s in db.SanPham select s).OrderBy(m => m.MaSP);
            int pageSize = 12;
            int pageNum = page ?? 1;
            SanPhamViewModel sp = new SanPhamViewModel
            {
                LoaiSPs = loaiSP,
                SanPhams = (PagedList<SanPham>)all_sanPham.ToPagedList(pageNum, pageSize)
            };

            //ViewBag.AllProduct = all_sanPham.ToPagedList(pageNum, pageSize);

            return View(sp);
        }

        // GET: SanPhams Admin

        public ActionResult IndexAdmin(int? page)
        {
            var all_sanPham = db.SanPham.ToList();
            int pageSize = 5;
            int pageNum = page ?? 1;
            return View(all_sanPham.ToPagedList(pageNum, pageSize));
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            foreach (var i in sanPham.BinhLuan)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.MaKH);
                i.Name = user.Name;
            }
            return View(sanPham);
        }

        // GET: SanPhams/Details/5 Admin
        public ActionResult DetailsAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id)
        {
            BinhLuansController addbinhluan = new BinhLuansController();
            BinhLuan binhLuan = new BinhLuan();
            string content = Request["txtContent"].ToString() + " ";
            if (content == " ")
            {
                return RedirectToAction("Details");
            }
            addbinhluan.Create(content, id, binhLuan);
            return RedirectToAction("Details");
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaLoai,Ten,MoTa,Gia,SoLuong,DonVi,GiamGia,Hinh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaLoai,Ten,MoTa,Gia,SoLuong,DonVi,GiamGia,Hinh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
            return "/Content/img/" + file.FileName;
        }
    }
}
