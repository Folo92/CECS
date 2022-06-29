using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class ProjectService : BaseService<Project>, IProjectService
    {
        /// <summary>
        /// 批量删除多条Project数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var projectList = CurrentDAL.LoadEntities(u => list.Contains(u.ProjectID));
            foreach (var project in projectList)
            {
                CurrentDAL.DeleteEntity(project);
            }
            return CurrentDbSession.SaveChanges();
        }
        /// <summary>
        /// 批量逻辑删除多条Project数据（用了反射）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntitiesLogically(List<int> list)
        {
            var projectList = CurrentDAL.LoadEntities(u => list.Contains(u.ProjectID));
            foreach (var project in projectList)
            {
                if (Common.ReflectionHelper<Project>.SetPropertyVal(project, "DelFlag", 1))
                {
                    CurrentDAL.EditEntity(project);
                }
            }
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 完成Project信息的搜索
        /// </summary>
        /// <param name="model">封装的搜索条件数据</param>
        /// <returns></returns>
        public IQueryable<Project> LoadSearchEntities(Project model,int pageIndex, int pageSize, out int totalCount, bool isAsc)
        {
            //var temp = CurrentDAL.LoadEntities(c => c.DelFlag == delFlag);
            var temp = CurrentDAL.LoadEntities(c => 1 == 1);
            if (model.ProjectID != null && model.ProjectID != 0) temp = temp.Where(u => u.ProjectID == model.ProjectID);
            if (!string.IsNullOrEmpty(model.ProjectName)) temp = temp.Where(u => u.ProjectName.Contains(model.ProjectName));

            totalCount = temp.Count();
            temp = isAsc
                ? temp.OrderBy(u => u.ProjectID).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : temp.OrderByDescending(u => u.ProjectID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return temp;
        }

    }
}
