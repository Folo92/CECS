using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Demo
{
    internal class AsyncDemo
    {
        internal static async Task TestAsync()
        {
            ThreadPool.QueueUserWorkItem(async (obj) =>
            {
                File.Delete(@"..\1.txt");
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);//打印当前线程ID
                using (HttpClient client = new HttpClient())
                {
                    for (int i = 0; i < 1; i++)
                    {
                        string html = await client.GetStringAsync("http://www.baidu.com");
                        await File.AppendAllTextAsync(@"..\1.txt", html);
                    }
                }
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);//打印当前线程ID
                Console.WriteLine("Finished!");
            });
        }
        internal static async Task<int> RandomSumAsync(int n)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    //Console.WriteLine(Thread.GetCurrentProcessorId());//打印当前线程所在处理器ID
                    Console.WriteLine($"线程ID：{Thread.CurrentThread.ManagedThreadId}");//打印当前线程ID
                    int result = 0;
                    Random rand = new Random();
                    for (var i = 0; i < n; i++)
                    {
                        result += rand.Next(100);
                        //Console.WriteLine(result);
                        Console.WriteLine($"线程ID：{Thread.CurrentThread.ManagedThreadId}，当前Result：{result}");
                        await Task.Delay(500);
                    }
                    Console.WriteLine($"线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    return result;
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    throw;
                }
                finally
                {
                    Console.WriteLine("=========任务完成=========");
                }
            });
        }
    }
}
