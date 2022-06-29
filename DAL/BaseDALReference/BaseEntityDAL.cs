using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    #region BaseEntityDAL
    /// <summary>
    /// 基于 EF CodeFirst 独立的针对 IBaseEntityDAL 方法的实现，其实际实现时通过继承的方式处理
    /// 实现基础操作方法的基类
    /// </summary>
    public abstract class BaseEntityDAL<T> where T : class
    {
        protected readonly DbContext _Context;
        protected readonly IDbSet<T> _DbSet;

        public BaseEntityDAL(DbContext context)
        {
            this._Context = context;
            _DbSet = _Context.Set<T>();
        }

        /// <summary>
        /// 根据条件检查数据是否已存在
        /// </summary>
        /// <param name="where">Lamda表达式</param>
        /// <returns>true：为不存在</returns>
        public bool HasInstance(Expression<Func<T, bool>> where)
        {
            if (_DbSet.Where(where).FirstOrDefault() == null)
                return true;
            else
                return false;
        }

        public bool HasRelevanceInstance<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2)) as IQueryable<T2>; ;
            if (dbSet.Where(where).FirstOrDefault() != null)
                return true;
            else
                return false;
        }
        public T GetByStringID(string oid)
        {
            return _DbSet.Find(oid);
        }

        public T GetByGuid(Guid oid)
        {
            return _DbSet.Find(oid);
        }

        /// <summary>
        /// 根据Lambda表达式获取一条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetByLambda(Expression<Func<T, bool>> where)
        {
            try
            {
                return _DbSet.Where(where).FirstOrDefault<T>();
            }
            catch (Exception e)
            {
                return null;
            }
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

        public void Add(T bo)
        {
            _DbSet.Add(bo);
            _Context.SaveChanges();
        }
        public void DelayedAdd(T bo)
        {
            _DbSet.Add(bo);
        }
        public int AllSaveChanges()
        {
            return _Context.SaveChanges();

        }
        public void AddOrUpdate(T bo, bool isForNewObject, ref int returnPoint)
        {
            if (isForNewObject)
            {
                this.Add(bo);
                returnPoint = 1;
            }
            else
            {
                this.Update(bo);
            }
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="bo"></param>
        public void DeleteEntity(T bo)
        {
            _DbSet.Remove(bo);
            _Context.SaveChanges();
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="bo"></param>
        public void DeleteByIds(params object[] ids)
        {
            if (ids != null)
            {
                string strIds = string.Join(",", ids);
                _Context.Database.ExecuteSqlCommand(string.Format("delete {0}  where  ID  in ('{1}')  ", typeof(T).Name, strIds));
            }
        }
        /// <summary>
        /// 逻辑删除(使用时确保表中 含有用于标记逻辑删除的字段 IsDelete)
        /// </summary>
        /// <param name="bo"></param>
        public void Delete(T bo)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = properties.Length; i > 0; i--)
            {
                string fn = properties[i - 1].Name;
                PropertyInfo pinfo = typeof(T).GetProperty(fn);
                if ("IsDelete" == fn)
                {
                    pinfo.SetValue(bo, true, null);
                    break;
                }
            }
            _DbSet.Attach(bo);
            _Context.Entry(bo).State = EntityState.Modified;
            _Context.SaveChanges();
        }
        public void DeleteRelevenceObject<T2>(T2 relevanceBo)
        {
            var dbSet = _Context.Set(typeof(T2));
            dbSet.Remove(relevanceBo);
            _Context.SaveChanges();
        }

        public void DeleteRelevenceObjectsByLambda<T2>(Expression<Func<T2, bool>> where)
        {
            var dbSet = _Context.Set(typeof(T2));

            var tempCollection = dbSet as IQueryable<T2>;
            foreach (var item in tempCollection.Where(where))
                dbSet.Remove(item);

            _Context.SaveChanges();
        }

        public void Update(T bo)
        {
            _DbSet.Attach(bo);
            _Context.Entry(bo).State = EntityState.Modified;
            _Context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _DbSet as IQueryable<T>;
        }

        //获取所有数据，根据条件排序
        public IQueryable<T> GetAllOrdered(Expression<Func<T, bool>> order)
        {
            return _DbSet.OrderBy(order);
        }

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

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="dataTableModel">表格参数</param>
        /// <param name="dataAmount">总条数</param>
        /// <param name="orderProperty">要排序的对象名称</param>
        /// <param name="where">搜索条件</param>
        /// <returns></returns>
        //public IQueryable<T> GetDataPaging(DataTableModel dataTableModel, ref int dataAmount, string orderProperty, Expression<Func<T, bool>> where = null)
        //{
        //    var searchResult = _DbSet as IQueryable<T>;
        //    if (where != null)
        //        searchResult = searchResult.Where(where);

        //    dataAmount = searchResult.Count();
        //    var isDesc = dataTableModel.OrderDir == "desc";
        //    searchResult = searchResult.OrderBy(orderProperty, isDesc).Skip(dataTableModel.Start).Take(dataTableModel.Length);
        //    return searchResult;
        //}

        public IQueryable<T> GetAllOrderedPaged<TKey>(Expression<Func<T, TKey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _DbSet as IQueryable<T>;
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            if (boAmount > pageAmount * pageSize)
                pageAmount = pageAmount + 1;
            return searchResult.OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize);
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


        ///// <summary>
        ///// 从缓存中获取所有数据
        ///// </summary>
        ///// <returns></returns>
        //public ICollection<T> GetAllByCache()
        //{
        //    var key = typeof(T).Name + "List";
        //    var obj = this.GetByCache(key);
        //    if (obj == null)
        //    {
        //        obj = this.GetAll().ToList();
        //        this.SetInCache(key, obj);
        //    }
        //    return obj as ICollection<T>;
        //}
        ///// <summary>
        ///// 从缓存中获取单个实体
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public T GetByCache(Guid id)
        //{
        //    var key = id.ToString();
        //    var obj = this.GetByCache(key);
        //    if (obj == null)
        //    {
        //        obj = this.GetByGuid(id);
        //        this.SetInCache(key, obj);
        //    }
        //    return obj as T;
        //}

        ///// <summary>
        ///// 移除缓存中的数据（集合）
        ///// </summary>
        //public void RemoveAllCache()
        //{
        //    var key = typeof(T).Name + "List";
        //    this.RemoveCache(key);
        //}
        ///// <summary>
        ///// 移除缓存中的数据（单个实体）
        ///// </summary>
        ///// <param name="id"></param>
        //public void RemoveCache(Guid id)
        //{
        //    var key = id.ToString();
        //    this.RemoveCache(key);
        //}
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        //public void SetInCache(string key, object val)
        //{
        //    CacheHelper.Set(key, val);
        //}
        ///// <summary>
        ///// 从缓存中获取数据
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public object GetByCache(string key)
        //{
        //    return CacheHelper.Get(key);
        //}
        ///// <summary>
        ///// 从缓存中移除数据
        ///// </summary>
        ///// <param name="key"></param>
        //public void RemoveCache(string key)
        //{
        //    CacheHelper.Remove(key);
        //}
    }
    #endregion
}
