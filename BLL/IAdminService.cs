using Model;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface IAdminService : IBaseService<Admin>
    {
        bool DeleteEntities(List<int> list);
        //IQueryable<Admin> LoadSearchEntities(AdminSearch adminSearch, short delFlag);
        //bool SetUserRoleInfo(int userId, List<int> roleIdList);
        //bool SetUserActionInfo(int actionId, int userId, bool isPass);
    }
}
