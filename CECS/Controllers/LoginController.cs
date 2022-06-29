using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login


        //private IAdminService _AdminService;
        //IAdminService AdminService
        //{
        //    get
        //    {
        //        if (_AdminService == null) return new AdminService();
        //        return _AdminService;
        //    }
        //    set { _AdminService = value; }
        //}如果不用容器，可以用上面注释掉的内容代替
        IAdminService AdminService { get; set; }  //通过Spring.Net容器实例化
        public ActionResult Index()
        {
            return View();
            //return RedirectToAction("Contact", "Home");
        }

        #region 完成用户登录
        public ActionResult Login()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                //return Content("<script>alert('验证码错误！');history.go(-1);</script>");
                return Content("no:验证码错误！");
            }
            Session["validateCode"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误！");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var admin = AdminService.LoadEntities(u => u.UserName == userName && u.PassWord == userPwd).FirstOrDefault();//根据用户名找用户
            if (admin != null)
            {
                //Session["admin"] = admin;
                //System.Web.Script.Serialization.JavaScriptSerializer
                string sessionId = Guid.NewGuid().ToString();//产生一个GUID值作为Memache的键.
                MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(admin), DateTime.Now.AddMinutes(20));//将登录用户信息存储到Memcache中，过期时间20分钟
                Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以Cookie的形式返回给浏览器。
                Session["username"] = admin.UserName;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:账号或密码错误！");
            }
        }
        #endregion

        #region 用户退出登录
        public ActionResult Logout()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                MemcacheHelper.Delete(sessionId);//根据sessionId删除对应memcache
                Session["username"] = null;
            }
            return Redirect("/Home/Index");
        }
        #endregion

        #region 显示验证码
        public ActionResult ShowValidateCode()
        {
            ValidateCode vliateCode = new ValidateCode();
            string code = vliateCode.CreateValidateCode(4);//产生验证码
            Session["validateCode"] = code;
            byte[] buffer = vliateCode.CreateValidateGraphic(code);//将验证码画到画布上.
            return File(buffer, "image/jpeg");
        }
        #endregion
    }
}