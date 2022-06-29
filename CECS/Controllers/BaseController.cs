using BLL;
using Common;
using Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheckedLogin { get; set; }
        public Admin LoginUser { get; set; }
        /// <summary>
        /// 执行控制器中的方法之前先执行该方法。
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);//先执行父类中的该方法
            
            Response.Cache.SetCacheability(HttpCacheability.NoCache);//禁止页面被缓存
            Response.Cache.SetExpires(DateTime.Today.AddYears(-2));//设置缓存过期时间（-2表示两年前过期）

            //if (Session["admin"] == null)
            bool isSucess = false;
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj = MemcacheHelper.Get(sessionId);//根据sessionId查Memcache
                if (obj != null)
                {
                    Admin admin = SerializeHelper.DeserializeToObject<Admin>(obj.ToString());
                    LoginUser = admin;
                    Session["username"] = admin.UserName;
                    isSucess = true;
                    MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));//模拟出滑动过期时间，即延后过期时间（留一个后门方便测试）

                    //    if (LoginUser.UserName == "itcast")
                    //    {
                    //        return;
                    //    }
                    //    //完成权限校验。
                    //    //获取用户请求的URL地址.
                    //    string url = Request.Url.AbsolutePath.ToLower();
                    //    //获取请求的方式.
                    //    string httpMehotd = Request.HttpMethod;
                    //    //根据获取的URL地址与请求的方式查询权限表。
                    //    IApplicationContext ctx = ContextRegistry.GetContext();
                    //    IBLL.IActionInfoService ActionInfoService = (IBLL.IActionInfoService)ctx.GetObject("ActionInfoService");
                    //    var actionInfo = ActionInfoService.LoadEntities(a => a.Url == url && a.HttpMethod == httpMehotd).FirstOrDefault();
                    //    if (actionInfo != null)
                    //    {
                    //        filterContext.Result = Redirect("/Error.html");
                    //        return;
                    //    }

                    //    //判断用户是否具有所访问的地址对应的权限
                    //    IAdminService AdminService = (IAdminService)ctx.GetObject("AdminService");
                    //    var loginAdmin = AdminService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
                    //    //1:可以先按照用户权限这条线进行过滤。
                    //    var isExt = (from a in loginAdmin.R_Admin_ActionInfo
                    //                 where a.ActionInfoID == actionInfo.ID
                    //                 select a).FirstOrDefault();
                    //    if (isExt != null)
                    //    {
                    //        if (isExt.IsPass)
                    //        {
                    //            return;
                    //        }
                    //        else
                    //        {
                    //            filterContext.Result = Redirect("/Error.html");
                    //            return;
                    //        }
                    //    }
                    //    //2：按照用户角色权限这条线进行过滤。
                    //    var loginUserRole = loginAdmin.RoleInfo;
                    //    var count = (from r in loginUserRole
                    //                 from a in r.ActionInfo
                    //                 where a.ID == actionInfo.ID
                    //                 select a).Count();
                    //    if (count < 1)
                    //    {
                    //        filterContext.Result = Redirect("/Error.html");
                    //        return;
                    //    }
                }
            }
            if (!isSucess && IsCheckedLogin)//如果用户未登录且需要登录
            {
                //MVC中请求一个方法必须要返回ActionRusult
                //filterContext.HttpContext.Response.Redirect("/Login/Index");//没给Result，会继续执行方法后面的代码
                filterContext.Result = Redirect("/Login/Index");//给了Result，直接跳转到登录页面
            }
        }
    }
}