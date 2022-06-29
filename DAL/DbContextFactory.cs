using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 负责创建数据操作上下文实例，必须保证线程内唯一（单例模式）
    /// </summary>
    public class DbContextFactory
    {
        //线程槽
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new CECSDbEntities();   //创建EF框架的数据操作上下文实例
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
