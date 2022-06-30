using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Demo
{
    public static class LinqDemo
    {
        #region 测试：Linq
        public static void ShowLinqResult()
        {
            var query = Linq4();

            Console.WriteLine(query);

            //foreach (var x in query)
            //{
            //    Console.WriteLine(x);
            //}
            //同下面写法
            query.ToList().ForEach(x => Console.WriteLine(x));

            Console.ReadKey();
        }
        #endregion

        #region 测试：Linq表达式

        static IEnumerable<int> Linq5() //Join的使用
        {
            int[] arr1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] arr2 = new int[] { 0, 2, 4, 5, 6, 8, 9 };
            var query = from a in arr1
                        where a < 7
                        join b in arr2 on a equals b
                        select a;
            return query;
        }
        static List<dynamic> Linq4() //匿名对象及其返回方法
        {
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 22, 18, 45, 66, 88 };
            var query = from num in arr
                        let n = num % 2
                        let index = arr.ToList().IndexOf(num)   //i记录元素在数组中的位置
                        where n == 0
                        select new
                        {
                            id = index,
                            name = num.ToString()
                        };
            return query.ToList<dynamic>();
        }
        static IEnumerable<int> Linq3() //多表综合
        {
            int[] arr1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] arr2 = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            var query = from a in arr1
                        from b in arr2
                        let n = a % 3
                        where n == 0 && b > 15 && b < 18
                        orderby a descending
                        select a + b;
            return query;
        }
        static IEnumerable<int> Linq2() //let与into的使用
        {
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = from num in arr
                        let n = num % 2
                        where num > 0 && num < 9
                        group num by n into arr1
                        from num1 in arr1
                        select num1;
            return query;
        }
        static IEnumerable<int> Linq1()
        {
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = from n in arr
                        where n > 4
                        select n;
            return query;
        }

        #endregion

    }
}
