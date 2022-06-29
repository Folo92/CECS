using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    /// <summary>
    /// 顺序表接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IListDS<T>
    {
        T this[int index] { get; }
        void Add(T item);
        void Insert(T item, int index);
        T Delete(int index);
        T GetElement(int index);
        int Locate(T value);
        int GetLength();
        void Clear();
        bool IsEmpty();
    }
}
