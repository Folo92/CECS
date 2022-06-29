using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    /// <summary>
    /// 数据会话层：就是一个工厂类，负责完成所有数据操作类实例DAL的创建，然后业务层通过数据会话层来获取要操作数据类的实例。
    /// 数据会话层将业务层与数据层解耦。
    /// </summary>
    public partial class DbSession : IDbSession
    {
        //CECSDbEntities Db = new CECSDbEntities();
        public DbContext Db
        {
            get
            {
                return DbContextFactory.CreateDbContext();
            }
        }

        //下面注释掉的内容另外通过文字模板生成
        //private IAdminDAL _AdminDAL;
        //public IAdminDAL AdminDAL
        //{
        //    get
        //    {
        //        if (_AdminDAL == null)
        //        {
        //            //_AdminDAL = new AdminDAL();
        //            _AdminDAL = AbstractFactory.CreateAdminDAL();//通过抽象工厂封装了类的实例的创建
        //        }
        //        return _AdminDAL;
        //    }
        //    set
        //    {
        //        _AdminDAL = value;
        //    }
        //}

        /// <summary>
        /// 实现多表操作一次性保存数据。
        /// 一个事务中经常涉及到对多张表的操作，我们希望只连接一次数据库，避免多次连接数据库占用数据链接池，从而提高性能。 
        /// （工作单元模式）
        /// </summary>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }

        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        public int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            return Db.Database.ExecuteSqlCommand(sql, pars);
        }

        /// <summary>
        /// 直接执行SQL语句查询，返回查询到的对象列表
        /// </summary>
        public List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars)
        {
            return Db.Database.SqlQuery<T>(sql, pars).ToList();
        }

    }
}
