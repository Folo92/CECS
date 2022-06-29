using Model;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface IProjectService : IBaseService<Project>
    {
        bool DeleteEntities(List<int> list);
        bool DeleteEntitiesLogically(List<int> list);
        IQueryable<Project> LoadSearchEntities(Project model, int pageIndex, int pageSize, out int totalCount, bool isAsc);
    }
}
