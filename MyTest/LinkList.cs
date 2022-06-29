using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    class LinkList<T> : IListDS<T>
    {
        private Node<T> head;//存储一个头结点
        public LinkList()
        {
            head = null;
        }
        //public int SeqList():this(10)//默认构造函数容量是10
        //{
        //}
        public T this[int index]
        {
            get
            {
                Node<T> temp = head;
                for (int i = 0; i < index; i++)//搜索到删除的位置
                {
                    temp = temp.Next;//让temp向后移动一个位置
                }
                return temp.Data;
            }
        }
        public void Add(T item)//添加数据
        {
            Node<T> newNode = new Node<T>(item);//根据新的数据创建一个新的结点
            //如果头部结点为空，那么这个新的结点就是头结点
            if (head == null)
            {
                head = newNode;
            }
            else//把新结点放到链表的尾部，要访问到链表的尾结点
            {
                Node<T> temp = head;//临时结点，从头结点开始搜索到尾结点
                while (true)
                {
                    if (temp.Next != null)
                    {
                        temp = temp.Next;
                    }
                    else
                    {
                        break;
                    }
                }
                temp.Next = newNode;//原来的尾结点指向新的结点
            }
        }
        public void Insert(T item, int index)//插入数据
        {
            Node<T> newNode = new Node<T>(item);
            if (index == 0)//插入到头结点
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node<T> temp = head;
                for (int i = 0; i < index; i++)//搜索到插入的位置
                {
                    temp = temp.Next;//让temp向后移动一个位置
                }
                Node<T> preNode = temp;
                Node<T> currentNode = temp.Next;
                preNode.Next = newNode;
                newNode.Next = currentNode;
            }
        }
        public T Delete(int index)//根据索引删除一条数据并返回
        {
            T data = default(T);
            if (index == 0)//删除头结点
            {
                head = head.Next;
            }
            else
            {
                Node<T> temp = head;
                for (int i = 0; i < index; i++)//搜索到删除的位置
                {
                    temp = temp.Next;//让temp向后移动一个位置
                }
                Node<T> preNode = temp;
                Node<T> currentNode = temp.Next;
                data = currentNode.Data;
                Node<T> nextNode = temp.Next.Next;
                preNode.Next = nextNode;
            }
            return data;
        }
        public T GetElement(int index)//获取元素
        {
            return this[index];
        }
        public int Locate(T value)//返回元素值第一次出现的位置
        {
            Node<T> temp = head;
            if(temp != null)
            {
                int index = 0;
                while (true)
                {
                    if (temp.Data.Equals(value))
                    {
                        return index;
                    }
                    else
                    {
                        if(temp.Next != null)
                        {
                            temp=temp.Next;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return -1;
            }
            return -1;//值不存在返回-1
        }
        public int GetLength()//返回单链表的长度
        {
            if (head == null) return 0;
            Node<T> temp = head;
            int count = 1;
            while (true)
            {
                if (temp.Next != null)
                {
                    count++;
                    temp = temp.Next;
                }
                else
                {
                    break;
                }
            }
            return count;
        }
        public void Clear()//清空单链表
        {
            head = null;
        }
        public bool IsEmpty()//判断单链表是否为空
        {
            return (head == null);
        }
    }
}
