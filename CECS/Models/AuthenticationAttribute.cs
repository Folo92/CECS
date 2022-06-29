using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CECS.Models
{

    //// 登录认证特性
    //public class AuthenticationAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Session["username"] == null)
    //            filterContext.Result = new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", Request.Url.ToString() } });

    //        base.OnActionExecuting(filterContext);
    //    }
    //}


    ///// <summary>
    ///// 表示需要用户登录才可以使用的特性
    ///// 如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //public class AuthenAdminAttribute : ActionFilterAttribute, IAuthorizationFilter  //继承这两个
    //{

    //    //下面两个方法都可以用作验证，只不过执行顺序不同
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        RequestContext requestContext = new RequestContext();

    //        if (filterContext.HttpContext.Session[LoginUser.uid.ToString()] == null)
    //            //filterContext.Result = new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", Request.Url.ToString() } }); 
    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Home" }));
    //        base.OnActionExecuting(filterContext);
    //    }
    //    public void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        //RouteValueDictionary res = filterContext.RouteData.Values;
    //        //string ViewName = res.Values.ToList()[1].ToString();
    //        bool isLogin = Authentication.isLogin();
    //        if (!isLogin)
    //        {
    //            filterContext.Result = new RedirectResult("/admin/home/login");
    //            // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "home", controller = "login" }));
    //        }
    //    }

    //}

}