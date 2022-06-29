using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    /// <summary>
    /// 顺序表实现方式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class SeqList<T> : IListDS<T>
    {
        private T[] data;   //用来存储数据
        private int count = 0;//表示存了多少个数据
        public SeqList(int size)//size表示最大容量
        {
            data = new T[size];
            count = 0;
        }
        public SeqList()
        {
            data = new T[10];//默认构造函数容量是10
            count = 0;
        }
        //public int SeqList():this(10)//默认构造函数容量是10
        //{
        //}
        public T this[int index] { get { return GetElement(index); } }
        public void Add(T item)//添加数据
        {
            if (count == data.Length)
            {
                Console.WriteLine("当前顺序表已存满，不允许再存入");
            }
            else
            {
                data[count++] = item;
            }
        }
        public void Insert(T item, int index)//插入数据
        {
            for (int i = count - 1; i >= index; i--)
            {
                data[i + 1] = data[i];//把数据向后移动
            }
            data[index] = item;
            count++;
        }
        public T Delete(int index)//根据索引删除一条数据并返回
        {
            T temp = data[index];
            for (int i = index + 1; i < count; i++)
            {
                data[i - 1] = data[i];//把数据向前移动
            }
            count--;
            return temp;
        }
        public T GetElement(int index)//获取元素
        {
            if (index >= 0 && index < count)
            {
                return data[index];
            }
            else
            {
                Console.WriteLine("索引不存在");
                return default(T);
            }
        }
        public int Locate(T value)//返回元素值第一次出现的位置
        {
            for (int i = 0; i < count; i++)
            {
                if (data[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;//值不存在返回-1
        }
        public int GetLength()//返回顺序表的长度
        {
            return count;
        }
        public void Clear()//清空顺序表
        {
            count = 0;
        }
        public bool IsEmpty()//判断顺序表是否为空
        {
            return (count == 0);
        }
    }
}
