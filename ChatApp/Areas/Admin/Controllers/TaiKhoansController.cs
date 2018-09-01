using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatApp.EF;

namespace ChatApp.Areas.Admin.Controllers
{
    public class TaiKhoansController : Controller
    {
        private ChatDB db = new ChatDB();

        // GET: Admin/TaiKhoans
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.LoaiTaiKhoan);
            return View(taiKhoans.ToList());
        }

        // GET: Admin/TaiKhoans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoans, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTaiKhoan,MatKhau,TenHienThi,HinhAnh,SoDienThoai,MaLoaiTaiKhoan")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoans, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.MaLoaiTaiKhoan);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoans, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.MaLoaiTaiKhoan);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenTaiKhoan,MatKhau,TenHienThi,HinhAnh,SoDienThoai,MaLoaiTaiKhoan")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiTaiKhoan = new SelectList(db.LoaiTaiKhoans, "MaLoaiTaiKhoan", "TenLoaiTaiKhoan", taiKhoan.MaLoaiTaiKhoan);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
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
