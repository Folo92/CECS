using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EFDbContext
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        public static CECSDbEntities GetDbContext()
        {
            CECSDbEntities dbContext = CallContext.GetData("CECSDbEntities") as CECSDbEntities;
            if (dbContext == null)
            {
                dbContext = new CECSDbEntities();
                CallContext.SetData("CECSDbEntities", dbContext);
            }
            return dbContext;
        }
    }
}
