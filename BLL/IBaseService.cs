using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IBaseService<T> where T : class, new()
    {
        DALFactory.IDbSession CurrentDbSession { get; }
        DAL.IBaseDAL<T> CurrentDAL { get; set; }
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);
        bool DeleteEntity(T entity);
        bool DeleteEntityLogically(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);
    }
}
