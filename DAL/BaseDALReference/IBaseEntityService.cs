using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    #region 封装定义数据操作接口 IBaseEntityService<T>
    /// <summary>
    /// 封装定义数据操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseEntityService<T> where T : class
    {
        /// <summary>
        /// 检查是否存在持久实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool HasInstance(Expression<Func<T, bool>> where);
        bool HasRelevanceInstance<T2>(Expression<Func<T2, bool>> where);


        T GetByIntID(int id);
        /// <summary>
        /// 提取单个对象的方法(ID)
        /// </summary>
        /// <param name="oid">ID</param>
        /// <returns></returns>
        T GetByStringID(string oid);
        /// <summary>
        ///提取单个对象的方法(GID)
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        T GetByGuid(Guid oid);
        T GetByLambda(Expression<Func<T, bool>> where);

        //! 根据 id ，提取实例，如果没有，创建一个新的实例，并执行持久化
        T GetOrCreatePersistence(Guid id);
        //! 根据 id ，提取实例，如果没有，创建一个新的实例，但不执行持久化
        T GetOrCreateNotPersistence(Guid id);

        //! 添加对象的方法
        void Add(T bo);
        void AddOrUpdate(T bo, bool isForNewObject, params Expression<Func<T, object>>[] updatedProperties);

        //! 删除对象的方法
        void Delete(T bo);

        void DeleteRelevenceObject<T2>(T2 relevanceBo);
        void DeleteRelevenceObjectsByLambda<T2>(Expression<Func<T2, bool>> where);

        //! 更新对象的方法
        void Update(T bo);

        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);

        //! 提取对象集合的方法
        IQueryable<T> GetAll();
        IQueryable<T> GetAllOrdered(Expression<Func<T, bool>> order);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, object>> selector, object val);
        IQueryable<T> GetManyOrdered<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order);

        //! 按照分页方式提取对象集合的方法
        IQueryable<T> GetAllOrderedPaged(string orderProeprtyName, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);
        IQueryable<T> GetAllOrderedPaged<TKey>(Expression<Func<T, TKey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);

        //IQueryable<T> GetDataPaging(GridPager page, ref int dataAmount, Expression<Func<T, bool>> where = null);
        IQueryable<T> GetManyOrderedPaged<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);
        IQueryable<T> GetManyOrderedByProertyNamePaged(Expression<Func<T, bool>> where, string orderProeprtyName, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);
        IQueryable<T> GetAllCommonWay(
            ref int pageAmount,
            ref int boAmount,
            Expression<Func<T, bool>> where = null,
            Expression<Func<T, bool>> order = null,
            bool? isDesc = null,
            int? page = null,
            int? pageSize = null);

        //! 数据处理约束管理方法
        bool CanAdd(Expression<Func<T, bool>> where);

        //! 用于处理一些附加对象的方法
        T2 GetRelevanceObject<T2>(Guid id);
        T2 GetRelevanceObject<T2>(Expression<Func<T2, bool>> where);
        T2 GetRelevanceObjectBySqlQuery<T2>(string SqlQureyString);

        IQueryable<T2> GetRelevanceObjects<T2>(Expression<Func<T2, bool>> where);
        IQueryable<T2> GetRelevanceObjects<T2>(
            Expression<Func<T2, bool>> where = null,
            Expression<Func<T2, bool>> order = null,
            bool? isDesc = null);

        IQueryable<T2> GetManyRelevanceObjectsOrderedByProertyNamePaged<T2>(Expression<Func<T2, bool>> where, string orderProeprtyName, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);

        void AddRelevanceObject<T2>(T2 relevanceBo);
        void UpdateRelevanceObject<T2>(T2 relevanceBo);
    }
    #endregion
}
