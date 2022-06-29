using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Demo
{
    internal class ThreadDemo
    {
        internal void ShowThread()
        {
            Thread childThread = new Thread(new ThreadStart(ChildThreadMethod));
            childThread.Start();
            while (true)
            {
                Console.WriteLine("MainThread - 执行任务A");
                Thread.Sleep(500);
            }
        }
        private static void ChildThreadMethod()
        {
            while (true)
            {
                Console.WriteLine("ChildThread - 执行任务B");
                Thread.Sleep(1000);
            }
        }
    }
}
