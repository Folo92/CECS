using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    /// <summary>
    /// 负责创建DbSession实例，保证线程内唯一
    /// </summary>
    public class DbSessionFactory
    {
        public static IDbSession CreateDbSession()
        {
            IDbSession DbSession = (IDbSession)CallContext.GetData("dbSession");
            if (DbSession == null)
            {
                DbSession = new DbSession();
                CallContext.SetData("dbSession", DbSession);
            }
            return DbSession;
        }
    }
}
