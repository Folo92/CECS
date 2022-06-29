using Model;
using Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial interface IQuantityService : IBaseService<Quantity>
    {
        bool DeleteEntities(List<int> list);
        bool DeleteEntitiesLogically(List<int> list);
        IQueryable<Quantity> LoadSearchEntities(Quantity model, int pageIndex, int pageSize, out int totalCount, bool isAsc);
    }
}
