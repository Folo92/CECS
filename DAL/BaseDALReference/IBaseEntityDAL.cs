using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    #region 封装定义数据操作接口 IBaseEntityDAL<T>
    /// <summary>
    /// 封装定义数据操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseEntityDAL<T> where T : class
    {
        //! 检查是否存在持久实体对象
        bool HasInstance(Expression<Func<T, bool>> where);
        bool HasRelevanceInstance<T2>(Expression<Func<T2, bool>> where);

        //! 提取单个对象的方法
        T GetByIntID(int id);
        T GetByStringID(string oid);
        T GetByGuid(Guid oid);
        T GetByLambda(Expression<Func<T, bool>> where);

        //! 根据 id ，提取实例，如果没有，创建一个新的实例，并执行持久化
        T GetOrCreatePersistence(Guid id);
        //! 根据 id ，提取实例，如果没有，创建一个新的实例，但不执行持久化
        T GetOrCreateNotPersistence(Guid id);

        //! 添加对象的方法
        void Add(T bo);
        void AddOrUpdate(T bo, bool isForNewObject, ref int returnPoint);

        //! 删除对象的方法
        void Delete(T bo);
        void DeleteRelevenceObject<T2>(T2 relevanceBo);
        void DeleteRelevenceObjectsByLambda<T2>(Expression<Func<T2, bool>> where);

        //! 更新对象的方法
        void Update(T bo);

        //! 提取对象集合的方法
        IQueryable<T> GetAll();
        IQueryable<T> GetAllOrdered(Expression<Func<T, bool>> order);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, object>> selector, object val);
        IQueryable<T> GetManyOrdered<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order);
        IQueryable<T> GetAllOrderedPaged<TKey>(Expression<Func<T, TKey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc);
        //IQueryable<T> GetDataPaging(DataTableModel dataTableModel, ref int dataAmount, string orderProperty, Expression<Func<T, bool>> where = null);
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

        void AddRelevanceObject<T2>(T2 relevanceBo);
        void UpdateRelevanceObject<T2>(T2 relevanceBo);
    }
    #endregion
}
