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
                TaiKhoan taikhoan =  db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                if (Request.Files.Count > 0 && Request.Files[0].FileName.Trim() != "")
                {
                    string[] _arr = Request.Files[0].FileName.Split('.');
                    string type = _arr[_arr.Length - 1];

                    taikhoan.HinhAnh = taikhoan.TenTaiKhoan + "." + type;
                    Request.Files[0].SaveAs(Server.MapPath("~/Public/Upload/Img_TaiKhoan/") + taikhoan.HinhAnh);
                }
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
                if (Request.Files.Count > 0 && Request.Files[0].FileName.Trim() != "")
                {
                    string[] _arr = Request.Files[0].FileName.Split('.');
                    string type = _arr[_arr.Length - 1];

                    taiKhoan.HinhAnh = taiKhoan.TenTaiKhoan + "." + type;
                    Request.Files[0].SaveAs(Server.MapPath("~/Public/Upload/Img_TaiKhoan/") + taiKhoan.HinhAnh);
                }
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
