using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 反射工具类（使用反射前应考虑到性能方面的影响）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReflectionHelper<T> where T : class
    {
        /// <summary>
        /// 利用反射判断对象是否包含某个属性
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="propertyName">需要判断的属性</param>
        /// <returns>是否包含</returns>
        public static bool ContainProperty(T instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                //Type entityType = typeof(T);
                //PropertyInfo _PropertyInfo = entityType.GetProperty(propertyName);
                PropertyInfo _PropertyInfo = instance.GetType().GetProperty(propertyName);
                return (_PropertyInfo != null);
            }
            return false;
        }
        /// <summary>
        /// 利用反射获取对象某个属性的值
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static s GetPropertyVal<s>(T instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _PropertyInfo = instance.GetType().GetProperty(propertyName);
                if (_PropertyInfo != null)
                {
                    var result = _PropertyInfo.GetValue(instance);
                    if (result != null)
                    {
                        return (s)result;
                    }
                }
            }
            return default;
        }
        /// <summary>
        /// 利用反射设置对象某个属性的值
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool SetPropertyVal<s>(T instance, string propertyName, s val)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _PropertyInfo = instance.GetType().GetProperty(propertyName);
                if (_PropertyInfo != null)
                {
                    _PropertyInfo.SetValue(instance, val);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 利用反射获取主键的名称
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetPKeyName()
        {
            //获取主键的 PropertyInfo
            PropertyInfo pKeyProp = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();
            return pKeyProp.Name ?? "";//返回主键名称
        }
        /// <summary>
        /// 利用反射获取主键的属性
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static PropertyInfo GetPKeyProp()
        {
            return typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();
        }
    }
}
