using ChatApp.Areas.Admin.Models;
using ChatApp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            if (loginAccount == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login/Index");
            }

            if (loginAccount != null && !ValidateRole(loginAccount))
            {
                filterContext.Result = new RedirectResult("~/Admin/Dashboard/Index");
            }
            //
            base.OnActionExecuted(filterContext);
        }

        public bool ValidateRole(TaiKhoan loginAccount)
        {
            if (loginAccount.LoaiTaiKhoan.TenLoaiTaiKhoan == Roles.ADMIN)
            {
                return true;
            }
            return false;
        }
    }