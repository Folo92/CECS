﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyTest.Mock
{
    public class AddressGenerator
    {
        public static void Test()
        {
            Console.WriteLine("-------- Start test of AddressGenerator: --------");
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(GetRandomAddress());
            }
            json = null;//清空json对象
            Console.WriteLine("VirtualAddress:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(GetRandomVirtualAddress());
            }
        }
        public static JsonArray json;
        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="filePath">JSON文件路径</param>
        /// <returns>JsonArray对象</returns>
        public static JsonArray ReadJsonFile(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                string jsonStr = file.ReadToEnd();
                return JsonNode.Parse(jsonStr).AsArray();

                //using (JsonTextReader reader = new JsonTextReader(file))
                //{
                //    return (JObject)JToken.ReadFrom(reader);//数据量较大时出错
                //}
            }
        }
        /// <summary>
        /// 生成随机地址
        /// </summary>
        /// <param name="level">地址的行政级别</param>
        /// <returns>随机地址字符串</returns>
        public static string GetRandomAddress(int level)
        {
            if (json == null) json = ReadJsonFile("../area_data_lite.json");//JSON文件路径

            int index1 = Random.Shared.Next(0, json.Count);
            string result = json[index1]["e"].ToString();
            if (level == 1) return result;

            int index2 = Random.Shared.Next(0, (json[index1]["c"].AsArray().Count));
            string temp = json[index1]["c"][index2]["e"].ToString();
            result += temp == result ? "" : temp;
            if (level == 2) return result;

            int index3 = Random.Shared.Next(0, json[index1]["c"][index2]["c"].AsArray().Count);
            result += json[index1]["c"][index2]["c"][index3]["e"].ToString();
            if (level == 3) return result;
            
            int index4 = Random.Shared.Next(0, json[index1]["c"][index2]["c"][index3]["c"].AsArray().Count);
            result += json[index1]["c"][index2]["c"][index3]["c"][index4]["e"].ToString();
            if (level == 4) return result;

            string[] home = { "金色家园", "耀江花园", "阳光翠竹苑", "东新大厦", "溢盈河畔别墅", "真新六街坊", "和亭佳苑", "协通公寓", "合泰公寓", "博泰新苑", "菊园五街坊", "住友嘉馨名园", "复华城市花园", "爱里舍花园", "清江苑", "胜利新村", "永泰花苑", "东城社区", "桃园社区", "中南悦府", "恒一华府", "西木社区", "振奋村" };

            string[] village = { "利民村", "王家庄", "平安村", "龙门村", "徐庄", "回马沟", "白杨村", "董家院", "罗湾村", "燕子坪", "上官村", "高家庄", "大北庄", "黄土坡", "黄土坡", "坪山凹", "联合村", "东平村", "西牛村", "光明村", "新庄", "清水塘", "姚埕", "岳家庄", "高庙", "黄树村", "青山坳", "张家堡", "林家寨", "青铜沟", "牌楼村" };

            if (result.Last().ToString() == "镇" || result.Last().ToString() == "乡")
            {
                result += village[Random.Shared.Next(village.Length)] + Random.Shared.Next(200) + "号";
            }
            else
            {
                result += Random.Shared.Next(200) + "号" + home[Random.Shared.Next(home.Length)];
            }
            if (level == 5) return result;

            return result;
        }
        public static string GetRandomAddress()
        {
            return GetRandomAddress(5);
        }

        /// <summary>
        /// 简易版的随机地址（虚拟）生成
        /// </summary>
        /// <returns></returns>
        public static string GetRandomVirtualAddress()
        {
            string[] province = { "河北省", "山西省", "辽宁省", "吉林省", "黑龙江省", "江苏省", "浙江省", "安徽省", "福建省", "江西省", "山东省", "河南省", "湖北省", "湖南省", "广东省", "海南省", "四川省", "贵州省", "云南省", "陕西省", "甘肃省", "青海省", "台湾省", };
            string[] city = { "安康市", "安庆市", "安顺市", "安阳市", "鞍山市", "巴彦淖尔市", "巴中市", "白城市", "白山市", "白银市", "百色市", "蚌埠市", "包头市", "宝鸡市", "保定市", "保山市", "北海市", "本溪市", "滨州市", "沧州市", "昌都地区", "长春市", "长沙市", "长治市", "常德市", "常州市", "巢湖市", "朝阳市", "潮州市", "郴州市", "成都市", "承德市", "池州市", "赤峰市", "崇左市", "滁州市", "达州市", "大连市", "大庆市", "大同市", "丹东市", "德阳市", "德州市", "定西市", "东莞市", "东营市", "鄂尔多斯市", "鄂州市", "防城港市", "佛山市", "福州市", "抚顺市", "抚州市", "阜新市", "阜阳市", "甘南州", "赣州市", "固原市", "广安市", "广元市", "广州市", "贵港市", "贵阳市", "桂林市", "哈尔滨市", "哈密地区", "海北藏族自治州", "海东地区", "海口市", "邯郸市", "汉中市", "杭州市", "毫州市", "合肥市", "河池市", "河源市", "菏泽市", "贺州市", "鹤壁市", "鹤岗市", "黑河市", "衡水市", "衡阳市", "呼和浩特市", "呼伦贝尔市", "湖州市", "葫芦岛市", "怀化市", "淮安市", "淮北市", "淮南市", "黄冈市", "黄山市", "黄石市", "惠州市", "鸡西市", "吉安市", "吉林市", "济南市", "济宁市", "佳木斯市", "嘉兴市", "嘉峪关市", "江门市", "焦作市", "揭阳市", "金昌市", "金华市", "锦州市", "晋城市", "晋中市", "荆门市", "荆州市", "景德镇市", "九江市", "酒泉市", "开封市", "克拉玛依市", "昆明市", "拉萨市", "来宾市", "莱芜市", "兰州市", "廊坊市", "乐山市", "丽江市", "丽水市", "连云港市", "辽阳市", "辽源市", "聊城市", "临沧市", "临汾市", "临沂市", "柳州市", "六安市", "六盘水市", "龙岩市", "陇南市", "娄底市", "泸州市", "吕梁市", "洛阳市", "漯河市", "马鞍山市", "茂名市", "眉山市", "梅州市", "绵阳市", "牡丹江市", "内江市", "南昌市", "南充市", "南京市", "南宁市", "南平市", "南通市", "南阳市", "宁波市", "宁德市", "攀枝花市", "盘锦市", "平顶山市", "平凉市", "萍乡市", "莆田市", "濮阳市", "普洱市", "七台河市", "齐齐哈尔市", "钦州市", "秦皇岛市", "青岛市", "清远市", "庆阳市", "曲靖市", "衢州市", "泉州市", "日照市", "三门峡市", "三明市", "三亚市", "汕头市", "汕尾市", "商洛市", "商丘市", "上饶市", "韶关市", "邵阳市", "绍兴市", "深圳市", "沈阳市", "十堰市", "石家庄市", "石嘴山市", "双鸭山市", "朔州市", "四平市", "松原市", "苏州市", "宿迁市", "宿州市", "绥化市", "随州市", "遂宁市", "台州市", "太原市", "泰安市", "泰州市", "唐山市", "天水市", "铁岭市", "通化市", "通辽市", "铜川市", "铜陵市", "铜仁市", "吐鲁番地区", "威海市", "潍坊市", "渭南市", "温州市", "乌海市", "乌兰察布市", "乌鲁木齐市", "无锡市", "吴忠市", "芜湖市", "梧州市", "武汉市", "武威市", "西安市", "西宁市", "锡林郭勒盟", "厦门市", "咸宁市", "咸阳市", "湘潭市", "襄樊市", "孝感市", "忻州市", "新乡市", "新余市", "信阳市", "兴安盟", "邢台市", "徐州市", "许昌市", "宣城市", "雅安市", "烟台市", "延安市", "盐城市", "扬州市", "阳江市", "阳泉市", "伊春市", "伊犁哈萨克自治州", "宜宾市", "宜昌市", "宜春市", "益阳市", "银川市", "鹰潭市", "营口市", "永州市", "榆林市", "玉林市", "玉溪市", "岳阳市", "云浮市", "运城市", "枣庄市", "湛江市", "张家界市", "张家口市", "张掖市", "漳州市", "昭通市", "肇庆市", "镇江市", "郑州市", "中山市", "中卫市", "舟山市", "周口市", "株洲市", "珠海市", "驻马店市", "资阳市", "淄博市", "自贡市", "遵义市", };
            string[] area = { "伊春区", "带岭区", "南岔区", "金山屯区", "西林区", "美溪区", "乌马河区", "翠峦区", "友好区", "新青区", "上甘岭区", "五营区", "红星区", "汤旺河区", "乌伊岭区", "榆次区" };
            string[] road = { "爱国路", "安边路", "安波路", "安德路", "安汾路", "安福路", "安国路", "安化路", "安澜路", "安龙路", "安仁路", "安顺路", "安亭路", "安图路", "安业路", "安义路", "安远路", "鞍山路", "鞍山支路", "澳门路", "八一路", "巴林路", "白城路", "白城南路", "白渡路", "白渡桥", "白兰路", "白水路", "白玉路", "百安路（方泰镇）", "百官街", "百花街", "百色路", "板泉路", "半淞园路", "包头路", "包头南路", "宝安公路", "宝安路", "宝昌路", "宝联路", "宝林路", "宝祁路", "宝山路", "宝通路", "宝杨路", "宝源路", "保德路", "保定路", "保屯路", "保屯路", "北艾路", };
            string[] home = { "金色家园", "耀江花园", "阳光翠竹苑", "东新大厦", "溢盈河畔别墅", "真新六街坊", "和亭佳苑", "协通公寓", "博泰新苑", "菊园五街坊", "住友嘉馨名园", "复华城市花园", "爱里舍花园" };

            int randomProvinceNum = Random.Shared.Next(province.Length);
            int randomCityNum = Random.Shared.Next(city.Length);
            int randomAreaNum = Random.Shared.Next(area.Length);
            int randomRoadNum = Random.Shared.Next(road.Length);
            int randomHomeNum = Random.Shared.Next(home.Length);
            int num = Random.Shared.Next(200);
            return province[randomProvinceNum] + city[randomCityNum] + area[randomAreaNum] + road[randomRoadNum] + num + "号" + home[randomHomeNum];
        }

        /// <summary>  
        /// Unicode字符串转为正常字符串  例如：1的Unicode为 u0031
        /// </summary>  
        /// <param name="srcText"></param>  
        /// <returns></returns>  
        //public static string UnicodeToString(string srcText)
        //{
        //    string dst = "";
        //    string src = srcText;
        //    int len = srcText.Length / 6;
        //    for (int i = 0; i <= len - 1; i++)
        //    {
        //        string str = "";
        //        str = src.Substring(0, 6).Substring(2);
        //        src = src.Substring(6);
        //        byte[] bytes = new byte[2];
        //        bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
        //        bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
        //        dst += Encoding.Unicode.GetString(bytes);
        //    }
        //    return dst;
        //}
    }
}
