using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 基于 EF CodeFirst 独立的针对 IBaseEntityService 方法的实现，其实际实现时通过继承的方式处理
    /// </summary>
    public abstract class BaseEntityService<T> where T : class
    {
        private readonly DbContext _Context;
        private readonly IDbSet<T> _DbSet;

        public BaseEntityService(DbContext context)
        {
            this._Context = context;
            _DbSet = _Context.Set<T>();
        }
        /// <summary>
        /// 根据条件判断是否数据存在
        /// </summary>
        /// <param name="where">判断条件</param>
        /// <returns></returns>
        public bool HasInstance(Expression<Func<T, bool>> where)
        {
            if (_DbSet.Where(where).FirstOrDefault() != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据条件判断是否数据存在
        /// </summary>
        /// <typeparam name="T2">需要判断实体</typeparam>
        /// <param name="where">判断条件</param>
        /// <returns></returns>
        public bool HasRelevanceInstance<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2)) as IQueryable<T2>; ;
            if (dbSet.Where(where).FirstOrDefault() != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据主键ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByIntID(int id)
        {
            return _DbSet.Find(id);
        }
        /// <summary>
        /// 根据主键ID获取实体
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public T GetByStringID(string oid)
        {
            return _DbSet.Find(oid);
        }
        /// <summary>
        /// 根据主键ID获取实体
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public T GetByGuid(Guid oid)
        {
            return _DbSet.Find(oid);
        }
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="where">刷选条件</param>
        /// <returns></returns>
        public T GetByLambda(Expression<Func<T, bool>> where)
        {

            return _DbSet.Where(where).FirstOrDefault<T>();
        }

        //根据Guid获取一条数据，如果该数据不存在将创建一条数据，然后返回
        public T GetOrCreatePersistence(Guid id)
        {
            var fObject = this.GetByGuid(id);
            if (fObject != null)
                return fObject;
            else
            {
                fObject = Activator.CreateInstance<T>();
                this.Add(fObject);
                return fObject;
            }
        }

        public T GetOrCreateNotPersistence(Guid id)
        {
            var fObject = this.GetByGuid(id);
            if (fObject != null)
                return fObject;
            else
            {
                fObject = Activator.CreateInstance<T>();
                return fObject;
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="bo">实体</param>
        public void Add(T bo)
        {
            _DbSet.Add(bo);
            _Context.SaveChanges();
        }
        /// <summary>
        /// 添加或者更新数据
        /// </summary>
        /// <param name="bo">实体</param>
        /// <param name="isForNewObject">是否新增</param>
        /// <param name="updatedProperties">过滤字段</param>
        public void AddOrUpdate(T bo, bool isForNewObject = true, params Expression<Func<T, object>>[] updatedProperties)
        {
            if (isForNewObject)
            {
                this.Add(bo);
            }
            else
            {
                this.Update(bo, updatedProperties);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="bo">实体</param>
        public void Delete(T bo)
        {
            _DbSet.Remove(bo);
            _Context.SaveChanges();
        }

        /// <summary>
        /// 根据stringID删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            _DbSet.Remove(GetByStringID(id));
            _Context.SaveChanges();
        }
        /// <summary>
        /// 根据主键ID删除数据（int）
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _DbSet.Remove(GetByIntID(id));
            _Context.SaveChanges();
        }

        //public void DeleteByLambda(Expression<Func<T, bool>> where)
        //{
        //    _DbSet.Delete(where);
        //    _Context.SaveChanges();
        //}
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <param name="relevanceBo"></param>
        public void DeleteRelevenceObject<T2>(T2 relevanceBo)
        {
            var dbSet = _Context.Set(typeof(T2));
            dbSet.Remove(relevanceBo);
            _Context.SaveChanges();
        }
        /// <summary>
        /// 根据刷选添加循环删除
        /// </summary>
        /// <typeparam name="T2">需要删除的实体</typeparam>
        /// <param name="where">删除条件</param>
        public void DeleteRelevenceObjectsByLambda<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2));

            var tempCollection = dbSet as IQueryable<T2>;
            foreach (var item in tempCollection.Where(where))
                dbSet.Remove(item);

            _Context.SaveChanges();
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="bo">实体</param>
        public void Update(T bo)
        {
            _DbSet.Attach(bo);
            _Context.Entry(bo).State = EntityState.Modified;
            _Context.SaveChanges();
        }
        /// <summary>
        /// 更新实体，并过滤不需要更新列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updatedProperties">过滤不需要更实体数组</param>
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            _DbSet.Attach(entity);
            var dbEntityEntry = _Context.Entry(entity);
            _Context.Entry(entity).State = EntityState.Modified;
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = false;
                }
            }

            _Context.SaveChanges();

        }
        /// <summary>
        /// 获取实体所有信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _DbSet as IQueryable<T>;
        }
        /// <summary>
        /// 获取实体信息并排序
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllOrdered(Expression<Func<T, bool>> order)
        {
            return _DbSet.OrderBy(order);
        }
        /// <summary>
        /// 筛选获取实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _DbSet.Where(where) as IQueryable<T>;
        }

        public IQueryable<T> GetMany(Expression<Func<T, object>> selector, object val)
        {
            //var queryBuilder = CustomLinq.Func((selector, val) =>
            //      from c in _DbSet.ToExpandable()
            //      where selector.Expand(c).IndexOf(val) != -1
            //      select c);
            return _DbSet as IQueryable<T>;
        }

        public IQueryable<T> GetManyOrdered<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return _DbSet.Where(where).OrderBy(order);
        }

        public IQueryable<T> GetAllOrderedPaged(string orderProeprtyName, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _DbSet as IQueryable<T>;
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            return searchResult;
        }

        public IQueryable<T> GetAllOrderedPaged<TKey>(Expression<Func<T, TKey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _DbSet as IQueryable<T>;
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            if (boAmount > pageAmount * pageSize)
                pageAmount = pageAmount + 1;
            return searchResult.OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> GetManyOrderedPaged<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _DbSet.Where(where) as IQueryable<T>;
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            if (boAmount > pageAmount * pageSize)
                pageAmount = pageAmount + 1;

            if (!isDesc)
                return searchResult.OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize);
            else
                return searchResult.OrderByDescending(order).Skip((page - 1) * pageSize).Take(pageSize);
        }

        ///// <summary>
        ///// 分页获取数据
        ///// </summary>
        ///// <param name="page">分页基础类</param>
        ///// <param name="dataAmount">输出总数量</param>
        ///// <param name="where">筛选条件</param>
        ///// <returns></returns>
        //public IQueryable<T> GetDataPaging(GridPager page, ref int dataAmount, Expression<Func<T, bool>> where = null)
        //{
        //    var searchResult = _DbSet as IQueryable<T>;
        //    if (where != null)
        //        searchResult = searchResult.Where(where);
        //    dataAmount = searchResult.Count();
        //    var isDesc = page.order == "desc";
        //    searchResult = searchResult.OrderBy(page.sort, isDesc).Skip(page.thisCount).Take(page.rows);
        //    return searchResult;
        //}

        public IQueryable<T> GetManyOrderedByProertyNamePaged(
            Expression<Func<T, bool>> where,
            string orderProeprtyName,
            int page,
            int pageSize,
            ref int pageAmount,
            ref int boAmount,
            ref bool isDesc
            )
        {
            if (where != null)
            {
                var searchResult = _DbSet.Where(where) as IQueryable<T>;
                boAmount = searchResult.Count();
                pageAmount = boAmount / pageSize;
                if (boAmount > pageAmount * pageSize)
                    pageAmount = pageAmount + 1;

                return searchResult;
            }
            else
            {
                var searchResult = _DbSet as IQueryable<T>;
                boAmount = searchResult.Count();
                pageAmount = boAmount / pageSize;
                if (boAmount > pageAmount * pageSize)
                    pageAmount = pageAmount + 1;

                return searchResult;
            }

        }


        public IQueryable<T> GetAllCommonWay(
            ref int pageAmount,
            ref int boAmount,
            Expression<Func<T, bool>> where = null,
            Expression<Func<T, bool>> order = null,
            bool? isDesc = null,
            int? page = null,
            int? pageSize = null)
        {
            pageAmount = 0;
            boAmount = 0;
            var searchResult = _DbSet as IQueryable<T>;


            if (where != null && order != null)
            {
                searchResult = searchResult.Where(where);
                if (isDesc != null)
                    searchResult = searchResult.OrderByDescending(order);
                else
                    searchResult = searchResult.OrderBy(order);
            }
            else
            {
                if (where != null)
                {
                    searchResult = searchResult.Where(where);
                }
                else
                {
                    if (order != null)
                    {
                        if (isDesc != null)
                            searchResult = searchResult.OrderByDescending(order);
                        else
                            searchResult = searchResult.OrderBy(order);
                    }
                }
            }

            boAmount = searchResult.Count();
            if (pageAmount == -1)
            {
                pageAmount = -1;
            }

            if (page != null)
            {
                var p = (int)page;
                var ps = (int)pageSize;
                pageAmount = searchResult.Count() / ps;
                searchResult = searchResult.Skip((p - 1) * ps).Take(ps);
            }

            return searchResult;
        }

        public bool CanAdd(Expression<Func<T, bool>> where)
        {
            var count = _DbSet.Where(where).Count();
            if (count > 0)
                return false;
            else
                return true;
        }

        public T2 GetRelevanceObject<T2>(Guid id)
        {
            var dbSet = _Context.Set(typeof(T2));
            return (T2)dbSet.Find(id);
        }

        public T2 GetRelevanceObjectBySqlQuery<T2>(string SqlQureyString)
        {
            var id = _Context.Database.SqlQuery<Guid>(SqlQureyString).FirstOrDefault();
            if (id != null)
            {
                return GetRelevanceObject<T2>(id);
            }
            else
                return default(T2);
        }

        public T2 GetRelevanceObject<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2)) as IQueryable<T2>;
            return (T2)dbSet.Where(where).FirstOrDefault();
        }

        public IQueryable<T2> GetRelevanceObjects<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2));
            var searchResult = dbSet as IQueryable<T2>;

            return searchResult.Where(where);
        }

        public IQueryable<T2> GetRelevanceObjects<T2>(
            Expression<Func<T2, bool>> where = null,
            Expression<Func<T2, bool>> order = null,
            bool? isDesc = null)
        {
            var dbSet = _Context.Set(typeof(T2));
            var searchResult = dbSet as IQueryable<T2>;
            if (where != null && order != null)
            {
                searchResult = searchResult.Where(where);
                if (isDesc != null)
                    searchResult = searchResult.OrderByDescending(order);
                else
                    searchResult = searchResult.OrderBy(order);
            }
            else
            {
                if (where != null)
                {
                    searchResult = searchResult.Where(where);
                }
                else
                {
                    if (order != null)
                    {
                        if (isDesc != null)
                            searchResult = searchResult.OrderByDescending(order);
                        else
                            searchResult = searchResult.OrderBy(order);
                    }
                }
            }

            return searchResult;
        }

        public IQueryable<T2> GetManyRelevanceObjectsOrderedByProertyNamePaged<T2>(
            Expression<Func<T2, bool>> where,
            string orderProeprtyName,
            int page,
            int pageSize,
            ref int pageAmount,
            ref int boAmount,
            ref bool isDesc)
        {
            var dbSet = _Context.Set(typeof(T2));

            if (where != null)
            {
                var searchResult = (dbSet as IQueryable<T2>).Where(where) as IQueryable<T2>;
                boAmount = searchResult.Count();
                pageAmount = boAmount / pageSize;
                if (boAmount > pageAmount * pageSize)
                    pageAmount = pageAmount + 1;

                return searchResult;
            }
            else
            {
                var searchResult = dbSet as IQueryable<T2>;
                boAmount = searchResult.Count();
                pageAmount = boAmount / pageSize;
                if (boAmount > pageAmount * pageSize)
                    pageAmount = pageAmount + 1;

                return searchResult;
            }

        }

        public void AddRelevanceObject<T2>(T2 relevanceBo)
        {
            var dbSet = _Context.Set(typeof(T2));
            dbSet.Add(relevanceBo);
            _Context.SaveChanges();
        }

        public void UpdateRelevanceObject<T2>(T2 relevanceBo)
        {
            var tempBo = Activator.CreateInstance(typeof(T2));
            tempBo = relevanceBo;
            var dbSet = _Context.Set(typeof(T2));
            dbSet.Attach(tempBo);
            _Context.Entry(tempBo).State = EntityState.Modified;
            _Context.SaveChanges();
        }

    }

}
