using DAL;
using DALFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IDbSession CurrentDbSession
        {
            get
            {
                //return new DbSession();//注释掉，使用数据工厂创建实例
                return DbSessionFactory.CreateDbSession();
            }
        }
        public IBaseDAL<T> CurrentDAL { get; set; }
        public abstract void SetCurrentDAL();//定义一个抽象方法
        public BaseService()
        {
            SetCurrentDAL();//子类一定要实现抽象方法。详见Manager模板创建的子类
        }
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return CurrentDAL.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDAL.DeleteEntity(entity);
            return CurrentDbSession.SaveChanges();
        }
        ///// <summary>
        ///// 批量物理删除多条Building数据（利用反射）
        ///// </summary>
        //public bool DeleteEntities(List<int> list)
        //{
        //    string pKeyProp = Common.ReflectionHelper<T>.GetPKeyName();
        //    var entityList = CurrentDAL.LoadEntities(u => list.Contains(Common.ReflectionHelper<T>.GetPropertyVal<int>(u, pKeyProp))).ToList();
        //    //此方法失败了，利用反射获取的属性用于Lambda表达式是不合适的，反射的方法无法转换为存储表达式
        //    foreach (var entity in entityList)
        //    {
        //        CurrentDAL.DeleteEntity(entity);
        //    }
        //    return CurrentDbSession.SaveChanges();
        //}
        /// <summary>
        /// 逻辑删除（利用反射）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntityLogically(T entity)
        {
            if (Common.ReflectionHelper<T>.SetPropertyVal(entity, "DelFlag", 1))//利用反射将entity的DelFlag属性改为1，即标志为已删除
            {
                CurrentDAL.EditEntity(entity);
                return CurrentDbSession.SaveChanges();
            }
            return false;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            CurrentDAL.EditEntity(entity);
            return CurrentDbSession.SaveChanges();
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            CurrentDAL.AddEntity(entity);
            CurrentDbSession.SaveChanges();
            return entity;
        }
    }
}
