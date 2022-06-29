using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace CECS.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            IsCheckedLogin = false;//不校验用户是否登陆
        }
        //public ActionResult LoginFul(Admin admin)
        //{
        //    try
        //    {
        //        using (CECSDbEntities db = new CECSDbEntities())
        //        {
        //            var list = db.Admin.Where(x => x.UserName == admin.UserName && x.PassWord == admin.PassWord).ToList();
        //            //db.Configuration.LazyLoadingEnabled = false;//关闭延迟加载模式
        //            if (list.Count > 0)
        //            {
        //                return View("Index");
        //            }
        //            else
        //            {
        //                return Content("<script>alert('账号或密码错误！！！');history.go(-1);</script>");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        //return Content("<script>alert('账号或密码错误！！！');history.go(-1);</script>");
        //        //return Content(admin.AdminNumber.ToString() + "<br>" + admin.AdminPwd);
        //        throw new Exception(exception.Message);
        //        //return RedirectToAction("Home", "Error");
        //    }
        //}

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserInfo()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}