using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExecptionQueue = new Queue<Exception>();
        /// <summary>
        /// 可以捕获异常数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);//先执行父类中的该方法
            Exception ex = filterContext.Exception;
            //写到队列
            ExecptionQueue.Enqueue(ex);
            //跳转到错误页面.
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}