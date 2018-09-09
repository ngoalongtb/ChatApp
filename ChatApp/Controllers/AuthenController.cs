using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatApp.EF;

namespace ChatApp.Controllers
{
    public class AuthenController : Controller
    {
        private ChatDB db = new ChatDB();
        // GET: Authen
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var temp = db.TaiKhoans.Where(x => x.TenTaiKhoan == username && x.MatKhau == password);
            if (temp.Count() == 1)
            {
                TaiKhoan loginAccount = temp.SingleOrDefault();
                loginAccount.MatKhau = "";
                Session.Add("LoginAccount", loginAccount);
                if (loginAccount.MaLoaiTaiKhoan == 1)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else return Redirect("/");
            }
            else
            {
                TempData["Message"] = "Tài khoản hoặc mật khẩu không chính xác";
                return RedirectToAction("Index");
            }
        }

        // GET: /Authen/Authen
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Authen/Authen
        [HttpPost]
        public ActionResult Register(TaiKhoan taiKhoan)
        {
            try
            {
                taiKhoan.MaLoaiTaiKhoan = 2; //TODO HardCode is member
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                ViewBag["Message"] = "Tài khoản đã tồn tại";
                return View();
            }
            catch (Exception)
            {
                return View("Success", taiKhoan);
            }


        }

        // GET: /Authen/Logout
        public ActionResult Logout()
        {
            Session.Remove("LoginAccount");
            return Redirect("/");
        }
    }
}