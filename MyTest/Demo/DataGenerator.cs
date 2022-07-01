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
            TestEntity data = GenerateData<TestEntity>();
            //Console.WriteLine(SerializeHelper.SerializeToString(data));
        }
        public static T GenerateData<T>() where T : class
        {
            Type type = typeof(T);
            var instance = Activator.CreateInstance(type);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                SetRandomValue(instance, prop);
            }
            return (T)instance;
        }

        private static void SetRandomValue(object instance, PropertyInfo prop)
        {
            Console.Write(prop.Name + ": "); //输出属性名称
            if (instance == null) return;
            Type propType = prop.PropertyType;

            //if (propType == typeof(int) || propType == typeof(int?))
            //{
            //    Console.WriteLine(typeof(DataGenerator).GetMethod("SetRandomValue").ToString());
            //    MethodInfo mi = instance.GetType().GetMethod("RandomInt").MakeGenericMethod(new Type[] { propType });
            //    prop.SetValue(instance, mi.Invoke(null, new object[] { 0, 100 }));
            //}

            //根据数据类型生成随机数
            if (propType == typeof(int) || propType == typeof(int?))
                prop.SetValue(instance, RandomInt());

            else if (propType == typeof(uint) || propType == typeof(uint?))
                prop.SetValue(instance, RandomUInt());

            else if (propType == typeof(long) || propType == typeof(long?))
                prop.SetValue(instance, RandomLong());

            else if (propType == typeof(ulong) || propType == typeof(ulong?))
                prop.SetValue(instance, RandomULong());

            else if (propType == typeof(short) || propType == typeof(short?))
                prop.SetValue(instance, RandomShort());

            else if (propType == typeof(ushort) || propType == typeof(ushort?))
                prop.SetValue(instance, RandomUShort());

            else if (propType == typeof(decimal) || propType == typeof(decimal?))
                prop.SetValue(instance, RandomDecimal());

            else if (propType == typeof(double) || propType == typeof(double?))
                prop.SetValue(instance, RandomDouble());

            else if (propType == typeof(float) || propType == typeof(float?))
                prop.SetValue(instance, RandomFloat());

            else if (propType == typeof(byte) || propType == typeof(byte?))
                prop.SetValue(instance, RandomByte());

            else if (propType == typeof(sbyte) || propType == typeof(sbyte?))
                prop.SetValue(instance, RandomSByte(1));

            else if (propType == typeof(bool))
                prop.SetValue(instance, RandomBoolean());

            else if (propType == typeof(string))
                prop.SetValue(instance, RandomString("Name"));

            else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                prop.SetValue(instance, RandomDateTime());

            else if (propType.IsClass)
            {
                Console.Write("{\n");//输出
                var propInst = Activator.CreateInstance(propType);
                PropertyInfo[] myProps = propType.GetProperties();
                foreach (PropertyInfo p in myProps)
                {
                    SetRandomValue(propInst, p);
                }
                prop.SetValue(instance, propInst);//给实体属性赋值
                Console.Write("}");//输出
            }

            Console.WriteLine(propType.IsClass ? "" : prop.GetValue(instance));//输出
        }

        /// <summary>
        /// 生成 Int32 (int) 类型的随机数据
        /// </summary>
        public static int RandomInt(int minValue, int maxValue)
        {
            return Random.Shared.Next(minValue, maxValue);
        }
        public static int RandomInt(int maxValue)
        {
            return RandomInt(0, maxValue);
        }
        public static int RandomInt()
        {
            return RandomInt(0, 100);
        }
        /// <summary>
        /// 生成 UInt32 (uint) 类型的随机数据
        /// </summary>
        public static uint RandomUInt(uint minValue, uint maxValue)
        {
            return (uint)Random.Shared.Next((int)minValue, (int)maxValue);
        }
        public static uint RandomUInt(uint maxValue)
        {
            return RandomUInt(0, maxValue);
        }
        public static uint RandomUInt()
        {
            return RandomUInt(0, 100);
        }
        /// <summary>
        /// 生成 Int64 (long) 类型的随机数据
        /// </summary>
        public static long RandomLong(long minValue, long maxValue)
        {
            return Random.Shared.NextInt64(minValue, maxValue);
        }
        public static long RandomLong(long maxValue)
        {
            return RandomLong(0, maxValue);
        }
        public static long RandomLong()
        {
            return RandomLong(0, 100);
        }
        /// <summary>
        /// 生成 UInt64 (ulong) 类型的随机数据
        /// </summary>
        public static ulong RandomULong(ulong minValue, ulong maxValue)
        {
            return (ulong)Random.Shared.NextInt64((long)minValue, (long)maxValue);
        }
        public static ulong RandomULong(ulong maxValue)
        {
            return RandomULong(0, maxValue);
        }
        public static ulong RandomULong()
        {
            return RandomULong(0, 100);
        }
        /// <summary>
        /// 生成 Int16 (short) 类型的随机数据
        /// </summary>
        public static short RandomShort(short minValue, short maxValue)
        {
            return (short)Random.Shared.Next(minValue, maxValue);
        }
        public static short RandomShort(short maxValue)
        {
            return RandomShort(0, maxValue);
        }
        public static short RandomShort()
        {
            return RandomShort(0, 10);
        }
        /// <summary>
        /// 生成 UInt16 (ushort) 类型的随机数据
        /// </summary>
        public static ushort RandomUShort(ushort minValue, ushort maxValue)
        {
            return (ushort)Random.Shared.Next(minValue, maxValue);
        }
        public static ushort RandomUShort(ushort maxValue)
        {
            return RandomUShort(0, maxValue);
        }
        public static ushort RandomUShort()
        {
            return RandomUShort(0, 10);
        }
        /// <summary>
        /// 生成 decimal 类型的随机数据
        /// </summary>
        public static decimal RandomDecimal(double minValue, double maxValue, int len)
        {
            return (decimal)(Math.Round(Random.Shared.NextDouble() * (maxValue - minValue) + minValue, len));
        }
        public static decimal RandomDecimal(double minValue, double maxValue)
        {
            return RandomDecimal(minValue, maxValue, 2);
        }
        public static decimal RandomDecimal(double maxValue)
        {
            return RandomDecimal(0, maxValue, 2);
        }
        public static decimal RandomDecimal()
        {
            return RandomDecimal(0, 100, 2);
        }
        /// <summary>
        /// 生成 double 类型的随机数据
        /// </summary>
        public static double RandomDouble(double minValue, double maxValue, int len)
        {
            return Math.Round(Random.Shared.NextDouble() * (maxValue - minValue) + minValue, len);
        }
        public static double RandomDouble(double minValue, double maxValue)
        {
            return RandomDouble(minValue, maxValue, 2);
        }
        public static double RandomDouble(double maxValue)
        {
            return RandomDouble(0, maxValue, 2);
        }
        public static double RandomDouble()
        {
            return RandomDouble(0, 100, 2);
        }
        /// <summary>
        /// 生成 float 类型的随机数据
        /// </summary>
        public static float RandomFloat(float minValue, float maxValue, int len)
        {
            return (float)Math.Round((Random.Shared.NextSingle() * (maxValue - minValue) + minValue), len);
        }
        public static float RandomFloat(float minValue, float maxValue)
        {
            return RandomFloat(minValue, maxValue, 2);
        }
        public static float RandomFloat(float maxValue)
        {
            return RandomFloat(0, maxValue, 2);
        }
        public static float RandomFloat()
        {
            return RandomFloat(0, 100, 2);
        }
        /// <summary>
        /// 生成 byte 类型的随机数据
        /// </summary>
        public static byte RandomByte(byte minValue, byte maxValue)
        {
            return (byte)Random.Shared.Next(minValue, maxValue + 1);
        }
        public static byte RandomByte(byte maxValue)
        {
            return RandomByte(0, maxValue);
        }
        public static byte RandomByte()
        {
            return RandomByte(0, 255);
        }
        /// <summary>
        /// 生成 sbyte 类型的随机数据
        /// </summary>
        public static sbyte RandomSByte(sbyte minValue, sbyte maxValue)
        {
            return (sbyte)Random.Shared.Next(minValue, maxValue + 1);
        }
        public static sbyte RandomSByte(sbyte maxValue)
        {
            return RandomSByte((sbyte)-maxValue, maxValue);
        }
        public static sbyte RandomSByte()
        {
            return RandomSByte(-128, 127);
        }
        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        public static bool RandomBoolean()
        {
            return Random.Shared.Next(0, 2) == 1 ? true : false;
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="minLen">生成字符串的最小长度</param>
        /// <param name="maxLen">生成字符串的最大长度</param>
        /// <returns></returns>
        public static string RandomString(int minLen, int maxLen)
        {
            int len = Random.Shared.Next(minLen, maxLen + 1);//长度范围包含最大值
            string str = "";
            for (int i = 0; i < len; i++)
            {
                //str += Convert.ToChar((byte)Random.Shared.Next(32, 127));
                if (Random.Shared.Next(0, 2) == 0) str += Convert.ToChar((byte)Random.Shared.Next(65, 91));
                else str += Convert.ToChar((byte)Random.Shared.Next(97, 123));
            }
            return str;
        }
        public static string RandomString(int maxLen)
        {
            return RandomString(1, maxLen);
        }
        public static string RandomString(string flag)
        {
            return flag switch
            {
                "Name" => NameGenerator.GenerateSurname(),
                _ => RandomString(),
            };
        }
        public static string RandomString(string[] arr)
        {
            return arr[Random.Shared.Next(0, arr.Length)];
        }
        public static string RandomString()
        {
            return RandomString(1, 9);
        }
        /// <summary>
        /// 生成随机时间
        /// </summary>
        public static DateTime RandomDateTime(int maxYearsAgo, int maxDaysAgo)
        {
            return DateTime.Now
                .AddYears(-Random.Shared.Next(0, maxYearsAgo))
                .AddMonths(-Random.Shared.Next(0, 12))
                .AddDays(-Random.Shared.Next(0, maxDaysAgo))
                .AddHours(-Random.Shared.Next(0, 24))
                .AddMinutes(-Random.Shared.Next(1, 60));
        }
        public static DateTime RandomDateTime(int maxYearsAgo)
        {
            return RandomDateTime(maxYearsAgo, 30);
        }
        public static DateTime RandomDateTime()
        {
            return RandomDateTime(10, 12);
        }

        /// <summary>
        /// 利用C#反射机制，将type作为泛型T传入方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="methodData"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        //public static T CallFunc<T>(object param, object methodData, string methodName)
        //{
        //    Type type = param.GetType();
        //    MethodInfo mi = methodData.GetType().GetMethod(methodName).MakeGenericMethod(new Type[] { type });
        //    var ret = mi.Invoke(methodData, new object[] { param });
        //    return (T)ret;
        //}
        //public static T CallStaticFunc<T>(object param, Type methodType, string methodName)
        //{
        //    Type type = param.GetType();
        //    MethodInfo mi = methodType.GetMethod(methodName).MakeGenericMethod(new Type[] { type });
        //    var ret = mi.Invoke(null, new object[] { param });
        //    return (T)ret;
        //}
    }
}
