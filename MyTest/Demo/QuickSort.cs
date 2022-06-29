using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Demo
{
    /// <summary>
    /// 快速排序类
    /// </summary>
    class QuickSort
    {
        ///<summary>
        ///快速排序
        ///</summary>
        ///<param name="array">排序数组</param>
        ///<param name="low">排序起始位置</param>
        ///<param name="high">排序结束位置</param>
        public void Sort(int[] array, int low, int high)
        {
            if (low >= high)
                return;
            /*完成一次单元排序*/
            int index = SortUnit(array, low, high);
            /*对左边单元进行排序*/
            Sort(array, low, index - 1);
            /*对右边单元进行排序*/
            Sort(array, index + 1, high);
        }

        /// <summary>
        ///一次排序单元，完成此方法，key左边都比key小，key右边都比key大。
        ///<para>array 排序数组</para>
        ///<para>low 排序起始位置</para>
        ///<para>high 排序结束位置</para>
        ///<para>返回排序结束位置</para>
        /// </summary>
        private static int SortUnit(int[] array, int low, int high)
        {
            int key = array[low];
            while (low < high)
            {
                /*从后向前搜索比key小的值*/
                while (array[high] >= key && high > low)
                    --high;
                /*比key小的放左边*/
                array[low] = array[high];
                /*从前向后搜索比key大的值，比key大的放右边*/
                while (array[low] <= key && high > low)
                    ++low;
                /*比key大的放右边*/
                array[high] = array[low];
            }
            /*左边都比key小，右边都比key大。//将key放在游标当前位置。//此时low等于high */
            array[low] = key;
#if false
            foreach (int i in array)
            {
                Console.Write("{0}\t", i);
            }
            Console.WriteLine();
#endif
            return high;
        }
    }

    class MyRandom
    {
        ///<summary>
        ///生成随机数组
        ///<para>array 输出的随机数组</para>
        ///<para>n 数组的元素数量</para>
        ///</summary>
        public void RandomArray(out int[] array, int n)
        {
            Random rd = new Random();
            int[] tmpArray = new int[n];
            for (int i = 0; i < n; i++)
            {
                tmpArray[i] = rd.Next(1, 99);
            }
            array = tmpArray;
        }
    }
}
