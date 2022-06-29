using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class AdminService : BaseService<Admin>, IAdminService
    {

        //public override void SetCurrentDAL()
        //{
        //    CurrentDAL = CurrentDbSession.AdminDAL;
        //}
        //public void SetAdmin(Admin admin)
        //{
        //    CurrentDbSession.AdminDAL.AddEntity(admin);
        //    CurrentDbSession.AdminDAL.DeleteEntity(admin);
        //    CurrentDbSession.AdminDAL.EditEntity(admin);
        //    CurrentDbSession.SaveChanges();
        //}
        /// <summary>
        /// 批量删除多条用户数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var adminList = CurrentDAL.LoadEntities(u => list.Contains(u.ID));
            foreach (var admin in adminList)
            {
                CurrentDAL.DeleteEntity(admin);
            }
            return CurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 完成用户信息的搜索
        /// </summary>
        /// <param name="AdminSearch">封装的搜索条件数据</param>
        /// <returns></returns>
        //public IQueryable<Admin> LoadSearchEntities(Model.Search.AdminSearch AdminSearch, short delFlag)
        //{
        //    var temp = CurrentDAL.LoadEntities(c => c.DelFlag == delFlag);
        //    //根据用户名来搜索
        //    if (!string.IsNullOrEmpty(AdminSearch.UserName))
        //    {
        //        temp = temp.Where<Admin>(u => u.UserName.Contains(AdminSearch.UserName));
        //    }
        //    if (!string.IsNullOrEmpty(AdminSearch.UserRemark))
        //    {
        //        temp = temp.Where<Admin>(u => u.Remark.Contains(AdminSearch.UserRemark));
        //    }
        //    AdminSearch.TotalCount = temp.Count();
        //    return temp.OrderBy<Admin, int>(u => u.ID).Skip<Admin>((AdminSearch.PageIndex - 1) * AdminSearch.PageSize).Take<Admin>(AdminSearch.PageSize);
        //}

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleIdList">要分配的角色的编号</param>
        /// <returns></returns>
        //public bool SetUserRoleInfo(int userId, List<int> roleIdList)
        //{
        //    var admin = CurrentDAL.LoadEntities(u => u.ID == userId).FirstOrDefault();//根据用户的编号查找用户的信息
        //    if (admin != null)
        //    {
        //        admin.RoleInfo.Clear();
        //        foreach (int roleId in roleIdList)
        //        {
        //            var roleInfo = CurrentDbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
        //            admin.RoleInfo.Add(roleInfo);
        //        }
        //        return CurrentDbSession.SaveChanges();
        //    }
        //    return false;

        //}

        /// <summary>
        /// 完成用户权限的分配
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="userId"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        //public bool SetUserActionInfo(int actionId, int userId, bool isPass)
        //{
        //    //判断userId以前是否有了该actionId,如果有了只需要修改isPass状态，否则插入。
        //    var r_admin_actionInfo = CurrentDbSession.R_Admin_ActionInfoDal.LoadEntities(a => a.ActionInfoID == actionId && a.AdminID == userId).FirstOrDefault();
        //    if (r_admin_actionInfo == null)
        //    {
        //        R_Admin_ActionInfo adminActionInfo = new R_Admin_ActionInfo();
        //        adminActionInfo.ActionInfoID = actionId;
        //        adminActionInfo.AdminID = userId;
        //        adminActionInfo.IsPass = isPass;
        //        CurrentDbSession.R_Admin_ActionInfoDal.AddEntity(adminActionInfo);
        //    }
        //    else
        //    {
        //        r_admin_actionInfo.IsPass = isPass;
        //        CurrentDbSession.R_Admin_ActionInfoDal.EditEntity(r_admin_actionInfo);
        //    }
        //    return CurrentDbSession.SaveChanges();

        //}

    }
}
