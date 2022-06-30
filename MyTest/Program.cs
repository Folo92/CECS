using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MyTest.Demo;

namespace MyTest
{
    class Program
    {
        public delegate void MyDelegate(int[] array, int low, int high);    //委托声明
        static void Main(string[] args)
        {
            //Question1();
            //TestGetCharNum("abcABCzxcvQWERTYASDFGHzxcvQQQQQQQQQQQQ");
            //TestList();//如果test1不是static，则必须加上Program，即：Program.test1();
            //TestReflect();
            //TestThread();
            //TestByTimes();
            //TestLinq();
            DataGenerator.TestGenerator();
        }

        #region 测试：代码运行时间
        static void TestByTimes()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start(); //开始监视代码运行时间
            //需要测试的代码 ....
            for (int i = 0; i < 100; i++)   //循环若干遍
            {
                TestSort2();    //在这里输入要测试的方法
            }
            stopwatch.Stop(); //停止监视
            TimeSpan timespan = stopwatch.Elapsed; //获取当前实例测量得出的总时间
            double hours = timespan.TotalHours; //总小时
            double minutes = timespan.TotalMinutes;  //总分钟
            double seconds = timespan.TotalSeconds;  //总秒数
            double milliseconds = timespan.TotalMilliseconds;  //总毫秒数
            Console.WriteLine("运行总时间：{0}秒", seconds);
            Console.ReadLine();
        }
        #endregion

        #region 测试：Linq
        static void TestLinq()
        {
            LinqDemo.ShowLinqResult();
        }
        #endregion

        #region 测试：反射
        static void TestReflect()
        {
            ReflectDemo.ShowMethodInfo();
            //ReflectDemo.InvokeMeth();
            //ReflectDemo.InvokeCons();
            //ReflectDemo.ShowClassInfo();
        }
        #endregion

        #region 测试：多线程
        static void TestThread()
        {
            ThreadDemo threadDemo = new ThreadDemo();
            threadDemo.ShowThread();
        }
        static void TestTask2()
        {
            var list = new ConcurrentBag<string>();
            string[] dirNames = { ".", ".." };
            List<Task> tasks = new List<Task>();
            foreach (var dirName in dirNames)
            {
                Task t = Task.Run(() =>
                {
                    foreach (var path in Directory.GetFiles(dirName))
                        list.Add(path);
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
            foreach (Task t in tasks)
                Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

            Console.WriteLine("Number of files read: {0}", list.Count);
        }
        static void TestTask1()
        {
            /*
            默认情况下，Task 是有线程池中的异步线程执行，是否执行完成，可以通过Task的的属性IsCompleted 来判断，
            如果想在子线程工作完成之后，在进行后续主线程工作可以通过调用task.Wait() 来等待线程完成，
            调用Wait后，当前线程会被阻塞，直到到子线程完成。
            */
            ShowThreadInfo("Application");
            var task = Task.Run(() =>
            {
                //此处要写一些耗时的操作,如果没有就写
                Thread.Sleep(1000); //去掉会导致cpu占用高
                ShowThreadInfo("Task");
                Thread.Sleep(1000);
            });
            Console.WriteLine("t.IsCompleted=" + task.IsCompleted);
            task.Wait();
            Console.WriteLine("t.IsCompleted=" + task.IsCompleted);
            task.GetAwaiter().OnCompleted(() =>
            {
                Console.Write("这里是执行完后自动的回调事件");
            });
        }
        static void ShowThreadInfo(String s)
        {
            Console.WriteLine("{0} Thread ID: {1}", s, Thread.CurrentThread.ManagedThreadId);
        }
        #endregion

        #region 测试：数据结构
        static void TestList()
        {
            //SeqList<string> seqList = new SeqList<string>();//顺序表
            LinkList<string> seqList = new LinkList<string>();//单链表
            seqList.Add("123");
            seqList.Add("456");
            seqList.Add("789");
            Console.WriteLine(seqList.GetElement(0));
            Console.WriteLine(seqList[0]);
            seqList.Insert("abc", 1);
            for (int i = 0; i < seqList.GetLength(); i++)
            {
                Console.Write(seqList[i] + " ");
            }
            Console.WriteLine();
            seqList.Delete(0);
            for (int i = 0; i < seqList.GetLength(); i++)
            {
                Console.Write(seqList[i] + " ");
            }
            Console.WriteLine();
            seqList.Clear();
            Console.WriteLine(seqList.GetLength());
            Console.ReadKey();
        }
        #endregion

        #region 测试：获取字符数量
        static void TestGetCharNum(string strTest)
        {
            string strAll = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";//包含全部拉丁字母(A-Z、a-z)的对照字符串
            string div = "  ";
            Console.Write("\n测试字符串：\n");
            PrintArray(strTest.ToArray(), div);
            Console.Write("\n对照字符串：\n");
            PrintArray(strAll.ToArray(), div);
            Console.Write("\n统计结果：\n");
            PrintArray(GetCharCount(strTest), div);
            Console.ReadLine();
        }

        /// <summary>
        /// 返回字符串中各拉丁字母(A-Z、a-z)的数量
        /// </summary>
        static int[] GetCharCount(string str)
        {
            int[] charCount = new int[52];
            //ASCII值：A=65，Z=90，a=97,
            foreach (char chr in str) { charCount[(int)chr > 90 ? (int)chr - 71 : (int)chr - 65]++; }
            return charCount;
        }

        /// <summary>
        /// 依次输出数组中的元素
        /// </summary>
        static void PrintArray<T>(T[] array, string div = "\t")
        {
            foreach (T i in array) { Console.Write("{0}" + div, i); }
            Console.Write("\n");
        }
        #endregion

        #region 测试：算法题目一
        static void Question1()
        {
            int[] arr = new int[] { 0, 7, 2, 3, 4, 5, 6, 7, 1 };    //初始化数组
            //int[] arr = new int[] { 2, 2, 2, 2 };    //初始化数组
            Console.Write("原数组：\n");
            PrintArray<int>(arr);
            int count = arr.GetLength(0);  //获取数组的元素数量
            Array.Sort(arr);    //数组元素按从小到大排序
            int arrSum = GetArraySum(arr);
            int remainder = arrSum % 3;
            long result = 0;
            int index = 0;
            if (remainder == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    result += arr[i] * (int)Math.Pow(10, i);
                    //Console.Write("result={0}\n", result.ToString());
                }
            }
            else
            {
                int[] arr0 = ArrayRemainder(arr, 0);
                int[] arr1 = ArrayRemainder(arr, 1);
                int[] arr2 = ArrayRemainder(arr, 2);
                int count0 = arr0.GetLength(0);  //获取数组的元素数量
                int count1 = arr1.GetLength(0);  //获取数组的元素数量
                int count2 = arr2.GetLength(0);  //获取数组的元素数量
                if (remainder == 1)
                {
                    if (count1 > 0) //去除一个余数为1的数字后得到结果
                    {
                        index = Array.IndexOf(arr, arr1[0]);
                        for (int i = 0; i < index; i++)
                        {
                            result += arr[i] * (int)Math.Pow(10, i);
                        }
                        for (int i = index + 1; i < count; i++)
                        {
                            result += arr[i] * (int)Math.Pow(10, i - 1);
                        }
                    }
                    else
                    {
                        //去除两个余数为2的数字后得到结果
                    }
                }
                else
                {
                    if (count2 > 0) //去除一个余数为2的数字后得到结果
                    {
                        index = Array.IndexOf(arr, arr2[0]);
                        for (int i = 0; i < index; i++)
                        {
                            result += arr[i] * (int)Math.Pow(10, i);
                        }
                        for (int i = index + 1; i < count; i++)
                        {
                            result += arr[i] * (int)Math.Pow(10, i - 1);
                        }
                    }
                    else
                    {
                        //去除两个余数为1的数字后得到结果
                    }
                }
            }

            int[] resultArr = TrimArray(arr, index);
            Console.Write("处理后的数组：\n");
            PrintArray<int>(resultArr);
            Console.Write("结果：\n{0}\n{0}/3={1}", result.ToString(), (result / 3).ToString());
            Console.ReadLine();
        }

        /// <summary>
        /// 从指定数组中筛选除以3余数为remainder的数字，形成新的数组
        /// </summary>
        /// <param name="arr">原数组</param>
        /// <param name="remainder">余数值</param>
        /// <returns>新的数组</returns>
        static int[] ArrayRemainder(int[] arr, int remainder)
        {
            if (remainder > 2)
            {
                Console.WriteLine("错误\n");
                return new int[0];
            }
            int count = arr.GetLength(0);  //获取数组的元素数量
            ArrayList arrayList1 = new ArrayList();//定义动态数组
            for (int i = 0; i < count; i++)
            {
                if (arr[i] % 3 == remainder)
                {
                    arrayList1.Add(arr[i]);
                }
            }
            return (int[])arrayList1.ToArray(Type.GetType("System.Int32"));
        }


        static bool TrimArrayN(int[] arr)
        {
            int count = arr.GetLength(0);
            int index = GetTrimArrayDivisible(arr);
            if (index != -1)
            {
                int[] newArr = TrimArray(arr, index);
            }
            else
            {
                int[] newArr = TrimArray(arr, index);
                if (TrimArrayN(newArr))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        static int GetTrimArrayDivisible(int[] arr)
        {
            long count = arr.GetLength(0);

            for (int i = 0; i < count; i++)
            {
                int[] newArr = TrimArray(arr, i);
                if (GetDivisible(newArr))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 从指定位置处减少一个元素，形成新的数组
        /// </summary>
        static int[] TrimArray(int[] arr, int index)
        {
            int count = arr.GetLength(0);
            int[] newArr = new int[count - 1];    //定义新数组，长度减少1
            for (int i = 0; i != index; i++)
            {
                newArr[i] = arr[i];
            }
            for (int i = index + 1; i < count; i++)
            {
                newArr[i - 1] = arr[i];
            }
            return newArr;
        }

        /// <summary>
        /// 判断数组之和能否被3整除
        /// </summary>
        static bool GetDivisible(int[] arr)
        {
            if (GetArraySum(arr) % 3 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 数组求和
        /// </summary>
        static int GetArraySum(int[] arr)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                sum += i;
            }
            return sum;
        }
        #endregion

        #region 测试：排序算法
        /// <summary>
        /// 测试快速排序
        /// </summary>
        static void TestSort1()
        {
            int[] array = { 49, 38, 65, 97, 76, 13, 27, 55, 88, 41, 36, 75 };

            //int[] array;
            ////生成随机数组
            //MyRandom rd = new MyRandom();
            //rd.RandomArray(out array, 9);

            QuickSort qs = new QuickSort();
            MyDelegate myDelegate1 = new MyDelegate(qs.Sort);
            myDelegate1(array, 0, array.Length - 1);
            //qs.Sort(array, 0, array.Length - 1);
            foreach (int i in array)
            {
                Console.Write("{0}\t", i);
            }
            Console.WriteLine();
            //Console.ReadLine();
        }

        /// <summary>
        /// 测试系统自带的冒泡排序
        /// </summary>
        static void TestSort2()
        {
            int[] array = { 49, 38, 65, 97, 76, 13, 27, 55, 88, 41, 36, 75 };
            Array.Sort(array);
            foreach (int i in array)
            {
                Console.Write("{0}\t", i);
            }
            Console.WriteLine();
            //Console.ReadLine();
        }
        #endregion

    }

}



