using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class BuildingService : BaseService<Building>, IBuildingService
    {
        /// <summary>
        /// 批量删除多条Building数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var buildingList = CurrentDAL.LoadEntities(u => list.Contains(u.BuildingID));
            foreach (var building in buildingList)
            {
                CurrentDAL.DeleteEntity(building);
            }
            return CurrentDbSession.SaveChanges();
        }
        /// <summary>
        /// 批量逻辑删除多条Building数据（用了反射）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntitiesLogically(List<int> list)
        {
            var buildingList = CurrentDAL.LoadEntities(u => list.Contains(u.BuildingID));
            foreach (var building in buildingList)
            {
                if (Common.ReflectionHelper<Building>.SetPropertyVal(building, "DelFlag", 1))
                {
                    CurrentDAL.EditEntity(building);
                }
            }
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 完成Building信息的搜索
        /// </summary>
        /// <param name="model">封装的搜索条件数据</param>
        /// <returns></returns>
        public IQueryable<Building> LoadSearchEntities(Building model,int pageIndex, int pageSize, out int totalCount, bool isAsc)
        {
            //var temp = CurrentDAL.LoadEntities(c => c.DelFlag == delFlag);
            var temp = CurrentDAL.LoadEntities(c => 1 == 1);
            if (!string.IsNullOrEmpty(model.BuildingName)) temp = temp.Where(u => u.BuildingName.Contains(model.BuildingName));
            if (!string.IsNullOrEmpty(model.BuildingType)) temp = temp.Where(u => u.BuildingType.Contains(model.BuildingType));
            if (!string.IsNullOrEmpty(model.StructureSystem)) temp = temp.Where(u => u.StructureSystem.Contains(model.StructureSystem));

            totalCount = temp.Count();
            temp = isAsc
                ? temp.OrderBy(u => u.BuildingID).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : temp.OrderByDescending(u => u.BuildingID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return temp;
        }

    }
}
