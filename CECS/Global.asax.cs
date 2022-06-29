using CECS.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CECS
{
    public class MvcApplication : SpringMvcApplication //原来默认继承的是System.Web.HttpApplication，使用Spring.Net要改为SpringMvcApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取配置文件中关于Log4Net配置信息
            //IndexManager.GetInstance().StartThread();//开始线程扫描LuceneNet对应的数据队列。

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string filePath = Server.MapPath("/Logs/");
            //开启一个线程，扫描异常信息队列。
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //判断一下队列中是否有数据
                    if (MyExceptionAttribute.ExecptionQueue.Count() > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExecptionQueue.Dequeue();
                        if (ex != null)
                        {
                            //用自定义方法将异常信息写到日志文件中。
                            //string fileName = "Logs_" + DateTime.Now.ToString("yyyy-MM-dd");
                            //File.AppendAllText(filePath + fileName + ".txt", ex.ToString(), System.Text.Encoding.UTF8);
                            //通过log4net将异常信息写到日志文件中（异步多线程）
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            //如果队列中没有数据，休息
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        //如果队列中没有数据，休息
                        Thread.Sleep(3000);
                    }
                }
            }, filePath);//filePath是传入lambda表达式的参数，不用filePath可省略
        }
        //异常处理的过滤器
    }
}
