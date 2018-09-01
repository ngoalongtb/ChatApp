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
    public class ChuDesController : Controller
    {
        private ChatDB db = new ChatDB();

        // GET: Admin/ChuDes
        public ActionResult Index()
        {
            var chuDes = db.ChuDes.Include(c => c.DanhMuc);
            return View(chuDes.ToList());
        }

        // GET: Admin/ChuDes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDes/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/ChuDes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuDe,TenChuDe,MaDanhMuc")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(chuDe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", chuDe.MaDanhMuc);
            return View(chuDe);
        }

        // GET: Admin/ChuDes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", chuDe.MaDanhMuc);
            return View(chuDe);
        }

        // POST: Admin/ChuDes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuDe,TenChuDe,MaDanhMuc")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuDe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", chuDe.MaDanhMuc);
            return View(chuDe);
        }

        // GET: Admin/ChuDes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            return View(chuDe);
        }

        // POST: Admin/ChuDes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuDe chuDe = db.ChuDes.Find(id);
            db.ChuDes.Remove(chuDe);
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
