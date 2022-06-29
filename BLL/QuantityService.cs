using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class QuantityService : BaseService<Quantity>, IQuantityService
    {
        /// <summary>
        /// 批量删除多条Quantity数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var quantityList = CurrentDAL.LoadEntities(u => list.Contains(u.QuantityID));
            foreach (var quantity in quantityList)
            {
                CurrentDAL.DeleteEntity(quantity);
            }
            return CurrentDbSession.SaveChanges();
        }
        /// <summary>
        /// 批量逻辑删除多条Quantity数据（用了反射）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntitiesLogically(List<int> list)
        {
            var quantityList = CurrentDAL.LoadEntities(u => list.Contains(u.QuantityID));
            foreach (var quantity in quantityList)
            {
                if (Common.ReflectionHelper<Quantity>.SetPropertyVal(quantity, "DelFlag", 1))
                {
                    CurrentDAL.EditEntity(quantity);
                }
            }
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 完成Quantity信息的搜索
        /// </summary>
        /// <param name="model">封装的搜索条件数据</param>
        /// <returns></returns>
        public IQueryable<Quantity> LoadSearchEntities(Quantity model, int pageIndex, int pageSize, out int totalCount, bool isAsc)
        {
            //var temp = CurrentDAL.LoadEntities(c => c.DelFlag == delFlag);
            var temp = CurrentDAL.LoadEntities(c => 1 == 1);
            if (model.BuildingID != null && model.BuildingID != 0) temp = temp.Where(u => u.BuildingID == model.BuildingID);
            totalCount = temp.Count();
            temp = isAsc
                ? temp.OrderBy(u => u.QuantityID).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : temp.OrderByDescending(u => u.QuantityID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return temp;
        }

    }
}
