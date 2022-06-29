using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    /// <summary>
    /// 抽象工厂类
    /// 提供创建实例的统一方法和所有数据操作类DAL实例的创建方法
    /// </summary>
    public partial class AbstractDALFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"]; //从Web.config中读取设置：程序集
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];       //从Web.config中读取设置：命名空间

        /// <summary>
        /// 通过反射的形式创建类的实例
        /// </summary>
        /// <param name="className"></param>
        private static object CreateInstance(string className)
        {
            var assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(className);
        }

        //下面注释掉的内容另外通过文字模板生成
        //public static IAdminDAL CreateAdminDAL()
        //{
        //    string fullClassName = NameSpace + ".AdminDAL";
        //    return CreateInstance(fullClassName) as IAdminDAL;
        //}
    }
}
