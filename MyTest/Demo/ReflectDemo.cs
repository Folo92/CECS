using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MyTest.Demo
{
    public static class ReflectDemo
    {
        #region 测试：使用反射从程序集获得类型
        public static void ShowClassInfo()
        {
            Assembly asm = Assembly.LoadFrom(@"MyTest");  //加载指定的程序集
            Type[] alltype = asm.GetTypes();  //获取程序集中的所有类型列表
            foreach (Type temp in alltype)
            {
                Console.Write(temp.Name + " , " + temp.GetType() + "\n");  //打印出程序集中的所有类型名称
                ShowMethodInfo(temp);
            }
            Console.ReadKey();
        }
        #endregion

        #region 测试：使用反射获取方法的相关信息
        public static void ShowMethodInfo(Type t)
        {
            //Type t = typeof(temp);
            Console.WriteLine("=== Analyzing methods in " + t.Name + " ===");

            //MethodInfo[] mi = t.GetMethods();  //MethodInfo对象在System.Reflection命名空间下。
            MethodInfo[] mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);  //不获取继承方法，为实例方法，为公开的
            foreach (MethodInfo m in mi) //遍历mi对象数组
            {
                Console.Write(m.ReturnType.Name); //返回方法的返回类型
                Console.Write(" " + m.Name + "("); //返回方法的名称
                ParameterInfo[] pi = m.GetParameters();  //获取方法参数列表并保存在ParameterInfo对象数组中
                for (int i = 0; i < pi.Length; i++)
                {
                    Console.Write(pi[i].ParameterType.Name); //方法的参数类型名称
                    Console.Write(" " + pi[i].Name);  // 方法的参数名
                    if (i + 1 < pi.Length)
                    {
                        Console.Write(", ");
                    }
                }
                Console.Write(")");
                Console.WriteLine(); //换行
            }
            Console.WriteLine("=== Finished ===\n"); //换行
            //Console.ReadKey();
        }
        public static void ShowMethodInfo()
        {
            Type t = typeof(SeqList<string>);
            //Type t = typeof(MyClass);   //获取描述MyClass类型的Type对象，MyClass可以换成其他类
            Console.WriteLine("=== Analyzing methods in " + t.Name + " ===");  //t.Name="MyClass"

            //MethodInfo[] mi = t.GetMethods();  //MethodInfo对象在System.Reflection命名空间下。
            MethodInfo[] mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);  //不获取继承方法，为实例方法，为公开的
            /*  BindingFlags是一个枚举，枚举值有（很多，只列出5个）：
                1、DeclareOnly: 仅获取指定类定义的方法，而不获取所继承的方法；
                2、Instance: 获取实例方法
                3、NonPublic: 获取非公有方法
                4、Public: 获取共有方法
                5、Static: 获取静态方法
             */

            foreach (MethodInfo m in mi) //遍历mi对象数组
            {
                Console.Write(m.ReturnType.Name); //返回方法的返回类型
                Console.Write(" " + m.Name + "("); //返回方法的名称
                ParameterInfo[] pi = m.GetParameters();  //获取方法参数列表并保存在ParameterInfo对象数组中
                for (int i = 0; i < pi.Length; i++)
                {
                    Console.Write(pi[i].ParameterType.Name); //方法的参数类型名称
                    Console.Write(" " + pi[i].Name);  // 方法的参数名
                    if (i + 1 < pi.Length)
                    {
                        Console.Write(", ");
                    }
                }
                Console.Write(")");
                Console.WriteLine(); //换行
            }
            Console.WriteLine("=== Finished ==="); //换行
            Console.ReadKey();
        }
        //注意：这里输出的除了MyClass类定义的所有方法外，也会显示object类定义的共有非静态方法。这是因为c#中的所有类型都继承于object类。
        //另外，这些信息实在程序运行时动态获得的，并不需要预先知道MyClass类的定义
        #endregion

        #region 测试：使用反射调用运行时动态创建的对象的方法
        //先获取一个构造函数列表，然后调用列表中的某个构造函数，创建一个该类型的实例。
        //通过这种机制，可以在运行时实例化任意类型的对象，而不必在声明语句中指定类型。
        public static void InvokeCons()
        {
            Type t = typeof(MyClass);
            int val;
            ConstructorInfo[] ci = t.GetConstructors();  //使用这个方法获取构造函数列表

            int x;
            for (x = 0; x < ci.Length; x++)
            {
                ParameterInfo[] pi = ci[x].GetParameters(); //获取当前构造函数的参数列表
                if (pi.Length == 2) break;    //如果当前构造函数有2个参数，则跳出循环
            }

            if (x == ci.Length)
            {
                return;
            }

            object[] consargs = new object[2];
            consargs[0] = 10;
            consargs[1] = 20;
            object reflectOb = ci[x].Invoke(consargs);  //实例化一个这个构造函数有两个参数的类型对象,如果参数为空，则为null

            //实例化后，调用MyClass中的方法。获取全部方法（MethodInfo）并遍历，直到方法名匹配，使用Invoke()方法调用获取到的指定方法
            //MethodInfo[] mi = t.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            //foreach (MethodInfo m in mi)
            //{
            //    if (m.Name.Equals("sum", StringComparison.Ordinal))
            //    {
            //        val = (int)m.Invoke(reflectOb, null);  //由于实例化类型对象的时候是用的两个参数的构造函数，所以这里返回的结构为30
            //        Console.WriteLine(" sum is " + val);  //输出 sum is 30
            //    }
            //}
            val = (int)t.GetMethod("sum").Invoke(reflectOb, null);  //这样使用更简洁
            Console.WriteLine(" sum is " + val);  //输出 sum is 30

            Console.ReadKey();
        }
        #endregion

        #region 测试：使用反射调用显式创建的对象的方法
        //先通过反射获取到要调用的方法，然后使用Invoke()方法，调用获取到的指定方法
        public static void InvokeMeth()
        {
            Type t = typeof(MyClass);
            MyClass reflectOb = new MyClass(10, 20);
            reflectOb.Show();  //输出为： x:10, y:20
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                ParameterInfo[] pi = m.GetParameters();
                if (m.Name.Equals("Set", StringComparison.Ordinal) && pi[0].ParameterType == typeof(int))
                {
                    object[] args = new object[2];
                    args[0] = 9;
                    args[1] = 10;
                    //参数reflectOb,为一个对象引用，将调用他所指向的对象上的方法，如果为静态方法这个参数必须设置为null
                    //参数args，为调用方法的参数数组，如果不需要参数为null
                    m.Invoke(reflectOb, args);   //调用MyClass类中的参数类型为int的Set方法，输出为Inside set(int,int).x:9, y:10
                }
            }
            reflectOb.Show();
            Console.ReadKey();
        }
        #endregion

    }

    #region 测试类：用于反射
    class MyClass
    {
        int x;
        int y;
        public MyClass(int i, int j)
        {
            x = i;
            y = j;
        }
        public int sum()
        {
            return x + y;
        }
        public bool IsBetween(int i)
        {
            if (x < i && i < y) return true;
            else return false;
        }
        public void Set(int a, int b)
        {
            x = a;
            y = b;
        }
        public void Set(double a, double b)
        {
            x = (int)a;
            y = (int)b;
        }
        public void Show()
        {
            Console.WriteLine("x:{0},y:{1}", x, y);
        }
    }
    #endregion

}
