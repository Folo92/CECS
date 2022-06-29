using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //BaseDAL不需要实现IBaseDAL，因为BaseDAL需要被继承使用。
    //继承BaseDAL的类会继承其对应的接口，该接口会继承IBaseDAL。
    //例如：
    //AdminDAL : BaseDAL<Admin>, IAdminDAL
    //IAdminDAL : IBaseDAL<Admin>
    //使用AdminDAL时IBaseDAL已被实现，不需要重复实现
    public class BaseDAL<T> where T : class, new()
    {
        //CECSDbEntities Db = new CECSDbEntities();//未使用工厂方法时在这里创建EF数据操作上下文实例
        DbContext db = DbContextFactory.CreateDbContext();//使用工厂方法在这里创建EF数据操作上下文实例

        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where<T>(whereLambda);//
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
            //return Db.SaveChanges() > 0;  //此处注释掉，在数据会话层统一保存
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            //return Db.SaveChanges() > 0;  //此处注释掉，在数据会话层统一保存
            return true;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            db.Set<T>().Add(entity);
            //Db.SaveChanges();  //此处注释掉，在数据会话层统一保存
            return entity;

        }
    }
}
