using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Mock
{
    public class ProverbGenerator
    {
        public static void Test()
        {
            Console.WriteLine("-------- Start test of ProverbGenerator: --------");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(GetRandomProverb());
            }
        }
        public static async Task<string> GetRandomProverbAsync()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string html = await client.GetStringAsync("https://api.uixsj.cn/hitokoto/get?type=social");
                        await File.AppendAllTextAsync(@"..\proverbs.txt", html + "\r\n");//写入本地文件
                        //string res = await File.ReadAllTextAsync(@"..\proverbs.txt");
                        return html;
                    }
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            });
        }
        public static string GetRandomProverb()
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //hitokoto(一言)、en(中英文)、social(社会语录)、soup(毒鸡汤)、fart(彩虹屁)、zha(渣男语录)
                        string html = await client.GetStringAsync("https://api.uixsj.cn/hitokoto/get?type=en");
                        await File.AppendAllTextAsync(@"..\proverbs.txt", html + "\r\n");//写入本地文件
                        //string res = await File.ReadAllTextAsync(@"..\proverbs.txt");
                        return html;
                    }
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }).Result;
        }
    }
}
