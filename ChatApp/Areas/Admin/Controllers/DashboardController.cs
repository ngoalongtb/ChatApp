using ChatApp.Areas.Admin.Models;
using ChatApp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private ChatDB db = new ChatDB();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            model.ChuDecNo = db.ChuDes.Count();
            model.TaiKhoanNo = db.TaiKhoans.Count();
            model.DanhMucNo = db.DanhMucs.Count();
            model.NhomNo = db.Nhoms.Count();
            return View(model);
        }
    }
}