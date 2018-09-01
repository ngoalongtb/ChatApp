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
    public class ThongTinNhomsController : Controller
    {
        private ChatDB db = new ChatDB();

        // GET: Admin/ThongTinNhoms
        public ActionResult Index()
        {
            var thongTinNhoms = db.ThongTinNhoms.Include(t => t.Nhom);
            return View(thongTinNhoms.ToList());
        }

        // GET: Admin/ThongTinNhoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinNhom thongTinNhom = db.ThongTinNhoms.Find(id);
            if (thongTinNhom == null)
            {
                return HttpNotFound();
            }
            return View(thongTinNhom);
        }

        // GET: Admin/ThongTinNhoms/Create
        public ActionResult Create()
        {
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom");
            return View();
        }

        // POST: Admin/ThongTinNhoms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongTinNhom,TenTaiKhoan,MaNhom")] ThongTinNhom thongTinNhom)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinNhoms.Add(thongTinNhom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", thongTinNhom.MaNhom);
            return View(thongTinNhom);
        }

        // GET: Admin/ThongTinNhoms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinNhom thongTinNhom = db.ThongTinNhoms.Find(id);
            if (thongTinNhom == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", thongTinNhom.MaNhom);
            return View(thongTinNhom);
        }

        // POST: Admin/ThongTinNhoms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTinNhom,TenTaiKhoan,MaNhom")] ThongTinNhom thongTinNhom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinNhom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.Nhoms, "MaNhom", "TenNhom", thongTinNhom.MaNhom);
            return View(thongTinNhom);
        }

        // GET: Admin/ThongTinNhoms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinNhom thongTinNhom = db.ThongTinNhoms.Find(id);
            if (thongTinNhom == null)
            {
                return HttpNotFound();
            }
            return View(thongTinNhom);
        }

        // POST: Admin/ThongTinNhoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinNhom thongTinNhom = db.ThongTinNhoms.Find(id);
            db.ThongTinNhoms.Remove(thongTinNhom);
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
