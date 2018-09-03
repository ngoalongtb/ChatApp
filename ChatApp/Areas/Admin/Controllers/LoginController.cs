using ChatApp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private ChatDB db = new ChatDB();
        // GET: Admin/Login
        public ActionResult Index()
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
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                TempData["Message"] = "Tài khoản hoặc mật khẩu không chính xác";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("LoginAccount");
            return Redirect("/");
        }
    }
}