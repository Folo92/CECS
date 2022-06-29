using Bigdesk2015;
using Bigdesk2015.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace CECSBack.WebService
{
    /****************************************************
     * 
     * Copyright(C)2022 xx 版权所有
     * 
     * 说    明：xx
     *
     * 作    者：吴云
     * 
     * 创建日期：2022年02月26日
     * 
     ***************************************************/
    /// <summary>
    /// WebService_Query 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] //防止java调用时出现无法识别 soapAction的问题
    public class WebService_Query : System.Web.Services.WebService
    {
        //private DBOperator DB = DBOperatorFactory.GetDBOperator(DesDecrypt(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString), "SQLSERVER2008");
        private DBOperator DB = DBOperatorFactory.GetDBOperator(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString, "SQLSERVER2008");

        /// <summary>
        /// 查询工程量平均数据
        /// </summary>
        //[WebMethod(Description = "查询工程量平均数据，返回json")]
        //public void GetQuantityAverage(string strGroupBy)
        //{
        //    string result = "";
        //    try
        //    {
        //        string tableName = " Quantity left join Building on Quantity.BuildingID=Building.BuildingID ";
        //        string strAvgFields = "UnitSteel,UnitConcrete,UnitShuttering";
        //        //string strGroupBy = "BuildingType,StructureSystem,Floors,SeismicIntensity,SiteClassification,SeismicGrade,Regularity,ColumnSpan";
        //        string strWhere = "";//"1=1 and BuildingType='住宅'";
        //        DataSet dsQuantity = DataHandler.GetAverage(tableName, strAvgFields, strGroupBy, strWhere);
        //        DataTable dt = dsQuantity.Tables[0];
        //        if (dt.HasData()) //成功
        //        {
        //            result = RetJsonBase64(1, "成功", "", dt);
        //        }
        //        else
        //        {
        //            result = RetJsonBase64(0, "失败", "", new DataTable("t"));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result = RetJsonBase64(0, "失败", "接口错误:" + e.ToString(), new DataTable("t"));
        //    }
        //    finally
        //    {
        //        Context.Response.Charset = "UTF-8"; //设置字符集类型  
        //        Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        //        Context.Response.Write(result);
        //        Context.Response.End();
        //    }
        //}

        /// <summary>
        /// 查询工程量综合数据
        /// </summary>
        [WebMethod(Description = "查询工程量综合数据，返回json")]
        public void GetQuantity()
        {
            string result = "";
            try
            {
//                string sql = @"select SUM(Jnje-Tjqje) 民工工资保证金总额  from (select isnull(Jnje,0) Jnje,qyID from GHNJ_BZJ_jnxxb where DataState=40 ) as a
//inner join (select isnull(Tjqje,0) Tjqje,qyID from GHNJ_BZJ_TJxxb  where DataState=80 ) as b on a.qyID=b.qyID";

                string sql = @"select * from Building LEFT JOIN Quantity ON Building.BuildingID = Quantity.BuildingID";
                //sql += "where ";

                SqlParameterCollection sp = DB.CreateSqlParameterCollection();
                DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t");
                if (dt.HasData()) //成功
                {
                    result = RetJsonBase64(1, "成功", "", dt);
                }
                else
                {
                    result = RetJsonBase64(0, "失败", "", new DataTable("t"));
                }
            }
            catch (Exception e)
            {
                result = RetJsonBase64(0, "失败", "接口错误:" + e.ToString(), new DataTable("t"));
            }
            finally
            {
                Context.Response.Charset = "UTF-8"; //设置字符集类型  
                Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Context.Response.Write(result);
                Context.Response.End();
            }
        }

        /// <summary>
        /// 查询建筑单体信息数据
        /// </summary>
        [WebMethod(Description = "查询建筑单体信息，返回json")]
        public void GetBuilding()
        {
            string result = "";
            try
            {
                string sql = @"select * from Building";

                SqlParameterCollection sp = DB.CreateSqlParameterCollection();
                DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t");
                if (dt.HasData()) //成功
                {
                    result = RetJsonBase64(1, "成功", "", dt);
                }
                else
                {
                    result = RetJsonBase64(0, "失败", "", new DataTable("t"));
                }
            }
            catch (Exception e)
            {
                result = RetJsonBase64(0, "失败", "接口错误:" + e.ToString(), new DataTable("t"));
            }
            finally
            {
                Context.Response.Charset = "UTF-8"; //设置字符集类型  
                Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Context.Response.Write(result);
                Context.Response.End();
            }
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        [WebMethod(Description = "查询项目信息，返回json")]
        public void GetProject()
        {
            string result = "";
            try
            {
                string sql = @"select * from Project";

                SqlParameterCollection sp = DB.CreateSqlParameterCollection();
                DataTable dt = DB.ExeSqlForDataTable(sql, sp, "t");
                if (dt.HasData()) //成功
                {
                    result = RetJsonBase64(1, "成功", "", dt);
                }
                else
                {
                    result = RetJsonBase64(0, "失败", "", new DataTable("t"));
                }
            }
            catch (Exception e)
            {
                result = RetJsonBase64(0, "失败", "接口错误:" + e.ToString(), new DataTable("t"));
            }
            finally
            {
                Context.Response.Charset = "UTF-8"; //设置字符集类型  
                Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Context.Response.Write(result);
                Context.Response.End();
            }
        }

        /// <summary>
        /// 格式化带返回值返回
        /// </summary>
        /// <param name="ResultState"></param>
        /// <param name="ResultMessage"></param>
        /// <param name="ResultReason"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string RetJsonBase64(int ResultState, string ResultMessage, string ResultReason, DataTable dt)
        {
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("ResultState", Type.GetType("System.Int32"));
            dtTable.Columns.Add("ResultMessage", Type.GetType("System.String"));
            dtTable.Columns.Add("ResultReason", Type.GetType("System.String"));
            dtTable.Columns.Add("data", Type.GetType("System.String"));
            dtTable.Rows.Add(dtTable.NewRow());
            dtTable.Rows[0]["ResultState"] = ResultState;
            dtTable.Rows[0]["ResultMessage"] = ResultMessage;
            dtTable.Rows[0]["ResultReason"] = ResultReason;
            dtTable.Rows[0]["data"] = ToJson(dt);
            string result = ToJson(dtTable).Replace("\"[", "[").Replace("]\"", "]");
            return (result.Substring(1, result.Length - 2)).ToBase64String();   //转换为Base64加密字符串
            //return result;
        }

        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetMD5_32(string msg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(msg));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));  //转化为小写的16进制，输出两位（不足的两位的前面补0，如 0x0A 如果没有2,就只会输出0xA）
            }
            return sb.ToString();
        }


        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DesEncrypt(string str)
        {
            if (str.IsEmpty())
            {
                return "";
            }
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            byKey = Encoding.UTF8.GetBytes("spark123");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray()).Replace("+", "%2B");
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string DesDecrypt(string strText)
        {
            try
            {
                if (strText.IsEmpty())
                {
                    return "";
                }
                byte[] byKey = null;
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] inputByteArray = new Byte[strText.Length];

                byKey = Encoding.UTF8.GetBytes("spark123");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch
            {

            }
            return "";
        }

        /// <summary>    
        /// Datatable转换为Json    
        /// </summary>    
        /// <param name="dt">Datatable对象</param>    
        /// <returns>Json字符串</returns>    
        public string ToJson(DataTable dt)
        {
            string jsonString = string.Empty;
            jsonString = Common.SerializeHelper.SerializeToString(dt);
            //jsonString = JsonHelper.CreateJsonParameters(dt);
            return jsonString.Replace("\\", "");
        }

        /// <summary>    
        /// Json字符串转换为DataTable
        /// </summary>    
        /// <param name="jsonString">Json字符串</param>    
        /// <returns>DataTable</returns>    
        public DataTable JsonToDataTable(string jsonString)
        {
            DataTable dt = new DataTable("t");
            dt = Common.SerializeHelper.DeserializeToObject<DataTable>(jsonString);
            return dt;
        }

    }
}
