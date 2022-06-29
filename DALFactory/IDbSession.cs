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
    /// 业务层调用的是数据会话层的接口
    /// </summary>
    public partial interface IDbSession
    {
        DbContext Db { get; }
        //IAdminDAL AdminDAL { get; set; }
        bool SaveChanges();
        int ExecuteSql(string sql, params SqlParameter[] pars);
        List<T> ExecuteQuery<T>(string sql, params SqlParameter[] pars);
    }
}