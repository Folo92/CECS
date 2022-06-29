using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Project
        public ProjectController()
        {
            IsCheckedLogin = true;//校验用户是否登陆
        }
        IProjectService ProjectService { get; set; }  //通过Spring.Net容器实例化

        public ActionResult Index()
        {
            return View();
        }

        #region 获取列表数据
        public ActionResult GetProjectList()
        {
            //int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            //int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            //var projectList = ProjectService.LoadPageEntities<int>(pageIndex, pageSize, out int totalCount, c => 1 == 1, c => c.ProjectID, false);
            //var temp = from u in projectList select u;  
            //return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);//rows和total名称不能改

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;

            //接收并构建搜索条件
            Project model = new Project();
            //model.ProjectID = (Request.Form["ProjectID"] != "" && Request.Form["ProjectID"] != null) ? int.Parse(Request.Form["ProjectID"]) : 0;
            model.ProjectName = Request.Form["ProjectName"] != "" ? Request.Form["ProjectName"] : "";

            //定义删除标记，后面仅获取未被逻辑删除的
            //short delFlag = (short)DeleteEnumType.Normal;
            //根据构建好的搜索条件完成搜索
            var projectList = ProjectService.LoadSearchEntities(model, pageIndex, pageSize, out int totalCount, false);
            var temp = from u in projectList
                       select u;
            var json = Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            string str = Common.SerializeHelper.SerializeToString(json);//调试用

            return json;//rows和total名称不能改
        }
        #endregion

        #region 删除数据
        public ActionResult DeleteProject()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            //将list集合存储的的要删除的记录的编号传递到业务层.
            if (ProjectService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加数据
        public ActionResult AddProject(Project project)
        {
            //project.DelFlag = 0;
            //project.ModifiedOn = DateTime.Now;
            //project.SubTime = DateTime.Now;
            ProjectService.AddEntity(project);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var project = ProjectService.LoadEntities(u => u.ProjectID == id).FirstOrDefault();
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 完成数据更新
        public ActionResult EditProject(Project project)
        {
            //project.ModifiedOn = DateTime.Now;
            if (ProjectService.EditEntity(project))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

    }
}