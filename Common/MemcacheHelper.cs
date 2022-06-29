using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MemcacheHelper
    {
        private static readonly MemcachedClient mc = null;
        private static readonly string ServerList = ConfigurationManager.AppSettings["ServerList"]; //从Web.config中读取设置
        static MemcacheHelper()
        {
            //string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };//最好放在配置文件中
            string[] serverlist = ServerList.Replace(" ", "").Split(',');
            
            SockIOPool pool = SockIOPool.GetInstance();//初始化池

            pool.SetServers(serverlist);//设置服务器列表
            //pool.SetWeights(new int[] { 1 });//各服务器之间负载均衡的设置比例

            pool.InitConnections = 1;//初始化时创建连接数
            pool.MinConnections = 1;//最小连接数
            pool.MaxConnections = 5;//最大连接数

            //pool.MaxIdle = 1000 * 60 * 60 * 6;//连接的最大空闲时间，此处设置为6个小时（单位ms），超过这个设置时间，连接会被释放掉
            pool.SocketConnectTimeout = 1000;//socket连接的超时时间，设置为0时表示不超时（单位ms），即一直保持链接状态
            pool.SocketTimeout = 3000;//通讯的超时时间，此处设置为3秒（单位ms），.Net版本没有实现
            pool.MaxBusy = 1000 * 10;//socket单次任务的最大时间（单位ms），超过这个时间socket会被强行中断掉，当前任务失败。
            pool.MaintenanceSleep = 30;//维护线程的间隔激活时间，此处设置为30秒（单位s），设置为0时表示不启用维护线程

            pool.Failover = true;//设置SocketIO池的故障标志
            //pool.Nagle = false;//是否对TCP/IP通讯使用nalgle算法，.Net版本没有实现

            pool.Initialize();

            mc = new MemcachedClient();// 获得客户端实例
            mc.EnableCompression = false;//是否启用压缩数据：如果启用了压缩，数据压缩长于门槛的数据将被储存在压缩的形式
            //mc.CompressionThreshold = 1024 * 1024;//压缩设置，超过指定大小的都压缩 
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set(string key, object value)
        {
            return mc.Set(key, value);
        }
        public static bool Set(string key, object value, DateTime time)
        {
            return mc.Set(key, value, time);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
                return mc.Delete(key);
            }
            return false;

        }
    }
}
