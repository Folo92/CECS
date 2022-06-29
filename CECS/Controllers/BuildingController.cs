using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class BuildingController : BaseController
    {
        // GET: Building
        public BuildingController()
        {
            IsCheckedLogin = true;//校验用户是否登陆
        }
        IBuildingService BuildingService { get; set; }  //通过Spring.Net容器实例化

        public ActionResult Index()
        {
            return View();
        }

        #region 获取列表数据
        public ActionResult GetBuildingList()
        {
            //int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            //int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            //var buildingList = BuildingService.LoadPageEntities<int>(pageIndex, pageSize, out int totalCount, c => 1 == 1, c => c.BuildingID, false);
            //var temp = from u in buildingList select u;  
            //return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);//rows和total名称不能改

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;

            //接收并构建搜索条件
            Building model = new Building();
            model.BuildingName = Request.Form["BuildingName"] != "" ? Request.Form["BuildingName"] : "";
            model.BuildingType = Request.Form["BuildingType"] != "" ? Request.Form["BuildingType"] : "";
            model.StructureSystem = Request.Form["StructureSystem"] != "" ? Request.Form["StructureSystem"] : "";
            //model.BuildingHeight = decimal.Parse(Request.Form["BuildingHeight"] != "" ? Request.Form["BuildingHeight"] : "");
            //model.Floors = short.Parse(Request.Form["Floors"] != "" ? Request.Form["Floors"] : "");
            //model.FloorHeight = decimal.Parse(Request.Form["FloorHeight"] != "" ? Request.Form["FloorHeight"] : "");
            //model.ArchitectureArea = decimal.Parse(Request.Form["ArchitectureArea"] != "" ? Request.Form["ArchitectureArea"] : "");
            //model.StructureArea = decimal.Parse(Request.Form["StructureArea"] != "" ? Request.Form["StructureArea"] : "");
            //model.SeismicIntensity = Request.Form["SeismicIntensity"] != "" ? Request.Form["SeismicIntensity"] : "";
            //model.SiteClassification = Request.Form["SiteClassification"] != "" ? Request.Form["SiteClassification"] : "";
            //model.SeismicGrade = Request.Form["SeismicGrade"] != "" ? Request.Form["SeismicGrade"] : "";
            //model.ReferenceWindPressure = decimal.Parse(Request.Form["ReferenceWindPressure"] != "" ? Request.Form["ReferenceWindPressure"] : "");
            //model.Regularity = Request.Form["Regularity"] != "" ? Request.Form["Regularity"] : "";
            //model.ColumnSpan = Request.Form["ColumnSpan"] != "" ? Request.Form["ColumnSpan"] : "";
            //model.TransferedFloors = byte.Parse(Request.Form["TransferedFloors"] != "" ? Request.Form["TransferedFloors"] : "");
            //model.ProjectID = Request.Form["ProjectID"] != "" ? int.Parse(Request.Form["ProjectID"]) : 0;
            //model.DesignerID = Request.Form["DesignerID"] != "" ? int.Parse(Request.Form["DesignerID"]) : 0;

            //定义删除标记，后面仅获取未被逻辑删除的
            //short delFlag = (short)DeleteEnumType.Normal;
            //根据构建好的搜索条件完成搜索
            var buildingList = BuildingService.LoadSearchEntities(model, pageIndex, pageSize, out int totalCount, false);
            var temp = from u in buildingList
                       select u;
            var json = Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            string str = Common.SerializeHelper.SerializeToString(json);//调试用

            return json;//rows和total名称不能改
        }
        #endregion

        #region 删除数据
        public ActionResult DeleteBuilding()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            //将list集合存储的的要删除的记录的编号传递到业务层.
            if (BuildingService.DeleteEntities(list))
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
        public ActionResult AddBuilding(Building building)
        {
            //building.DelFlag = 0;
            //building.ModifiedOn = DateTime.Now;
            //building.SubTime = DateTime.Now;
            BuildingService.AddEntity(building);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var building = BuildingService.LoadEntities(u => u.BuildingID == id).FirstOrDefault();
            return Json(building, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 完成数据更新
        public ActionResult EditBuilding(Building building)
        {
            //building.ModifiedOn = DateTime.Now;
            if (BuildingService.EditEntity(building))
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