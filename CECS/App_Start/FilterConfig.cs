using CECS.Models;
using System.Web;
using System.Web.Mvc;

namespace CECS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());//默认的错误处理过滤器
            filters.Add(new MyExceptionAttribute());//自定义的错误处理过滤器
        }
    }
}
