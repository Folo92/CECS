using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class SystemController : BaseController
    {
        // GET: System
        public SystemController()
        {
            IsCheckedLogin = true;//校验用户是否登陆
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InfoSystem()
        {
            return View();
        }
    }
}