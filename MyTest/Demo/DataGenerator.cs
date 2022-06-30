using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Demo
{
    /// <summary>
    /// 实体数据生成器
    /// </summary>
    public static class DataGenerator
    {
        public static void TestGenerator()
        {
            Model.Building data = GenerateData<Model.Building>();
            //Console.WriteLine(SerializeHelper.SerializeToString(data));
        }
        public static T GenerateData<T>() where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            var instance = Activator.CreateInstance(type);
            //type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead).ToList().ForEach(x => { x.SetValue(target, x.ToString()); });

            foreach (PropertyInfo p in props)
            {
                Console.Write(p.Name + ": "); //返回属性的名称
                //Console.Write(p.PropertyType); //返回属性的数据类型
                Type propType = p.PropertyType;
                if (propType == typeof(int) || propType == typeof(int?))
                {
                    p.SetValue(instance, RandomInt());
                }
                else if (propType == typeof(string))
                {
                    p.SetValue(instance, RandomString(10));
                }
                else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                {
                    p.SetValue(instance, RandomDateTime());
                }
                Console.WriteLine(p.GetValue(instance));
            }
            //Console.WriteLine(instance);
            return (T)instance;
        }
        public static int RandomInt()
        {
            return Random.Shared.Next(10001, 100000);
        }
        public static string RandomString(int maxLen)
        {
            int len = Random.Shared.Next(4, maxLen);
            string str = "";
            for (int i = 0; i < len; i++)
            {
                //str += Convert.ToChar((byte)Random.Shared.Next(32, 127));
                if(Random.Shared.Next(0, 2) == 0) str += Convert.ToChar((byte)Random.Shared.Next(65, 91));
                else str += Convert.ToChar((byte)Random.Shared.Next(97, 123));
            }
            return str;
        }
        public static DateTime RandomDateTime()
        {
            return DateTime.Now
                .AddYears(-Random.Shared.Next(0, 20))
                .AddMonths(-Random.Shared.Next(0, 12))
                .AddDays(-Random.Shared.Next(0, 28))
                .AddMinutes(-Random.Shared.Next(1, 1440));
        }

    }
}
