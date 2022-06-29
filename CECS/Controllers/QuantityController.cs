using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CECS.Controllers
{
    public class QuantityController : BaseController
    {
        // GET: Quantity
        public QuantityController()
        {
            IsCheckedLogin = true;//校验用户是否登陆
        }
        IQuantityService QuantityService { get; set; }  //通过Spring.Net容器实例化

        public ActionResult Index()
        {
            return View();
        }

        #region 获取列表数据
        public ActionResult GetQuantityList()
        {
            //int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            //int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            //var quantityList = QuantityService.LoadPageEntities<int>(pageIndex, pageSize, out int totalCount, c => 1 == 1, c => c.QuantityID, false);
            //var temp = from u in quantityList select u;  
            //return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);//rows和total名称不能改

            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            
            //接收并构建搜索条件
            Quantity model = new Quantity();
            model.BuildingID = (Request.Form["BuildingID"] != "" && Request.Form["BuildingID"] != null) ? int.Parse(Request.Form["BuildingID"]) : 0;

            //定义删除标记，后面仅获取未被逻辑删除的
            //short delFlag = (short)DeleteEnumType.Normal;
            //根据构建好的搜索条件完成搜索
            var quantityList = QuantityService.LoadSearchEntities(model, pageIndex, pageSize, out int totalCount, false);
            var temp = from u in quantityList
                       select u;
            var json = Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            string str = Common.SerializeHelper.SerializeToString(json);//调试用

            return json;//rows和total名称不能改
        }
        #endregion

        #region 删除数据
        public ActionResult DeleteQuantity()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            //将list集合存储的的要删除的记录的编号传递到业务层.
            if (QuantityService.DeleteEntities(list))
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
        public ActionResult AddQuantity(Quantity quantity)
        {
            //quantity.DelFlag = 0;
            //quantity.ModifiedOn = DateTime.Now;
            //quantity.SubTime = DateTime.Now;
            QuantityService.AddEntity(quantity);
            return Content("ok");
        }
        #endregion

        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var quantity = QuantityService.LoadEntities(u => u.QuantityID == id).FirstOrDefault();
            return Json(quantity, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 完成数据更新
        public ActionResult EditQuantity(Quantity quantity)
        {
            //quantity.ModifiedOn = DateTime.Now;
            if (QuantityService.EditEntity(quantity))
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