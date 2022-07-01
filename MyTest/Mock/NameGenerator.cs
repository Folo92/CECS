using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyTest.Mock
{
    public class NameGenerator
    {
        public static void Test()
        {
            Console.WriteLine("-------- Start test of NameGenerator: --------");
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(GetRandomChineseName());
            }
        }
        /// <summary>
        /// 随机产⽣英文名
        /// </summary>
        /// <returns></returns>
        public static string GetRandomEnglishName()
        {
            string name = string.Empty;
            string[] currentConsonant;
            string[] vowels = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
            string[] commonConsonants = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
            string[] averageConsonants = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
            string[] middleConsonants = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(','); //Can't start
            string[] rareConsonants = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
            Random rng = new Random(Guid.NewGuid().GetHashCode()); //http://codebetter.com/blogs/59496.aspx
            int[] lengthArray = new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 4 }; //Favor shorter names but allow longer ones
            int length = lengthArray[rng.Next(lengthArray.Length)];
            for (int i = 0; i < length; i++)
            {
                int letterType = rng.Next(1000);
                if (letterType < 775) currentConsonant = commonConsonants;
                else if (letterType < 875 && i > 0) currentConsonant = middleConsonants;
                else if (letterType < 985) currentConsonant = averageConsonants;
                else currentConsonant = rareConsonants;
                name += currentConsonant[rng.Next(currentConsonant.Length)];
                name += vowels[rng.Next(vowels.Length)];
                if (name.Length > 4 && rng.Next(1000) < 800) break; //Getting long, must roll to save
                if (name.Length > 6 && rng.Next(1000) < 950) break; //Really long, roll again to save
                if (name.Length > 7) break; //Probably ridiculous, stop building and add ending
            }
            int endingType = rng.Next(1000);
            if (name.Length > 6)
                endingType -= (name.Length * 25); //Don't add long endings if already long
            else
                endingType += (name.Length * 10); //Favor long endings if short
            if (endingType < 400) { } // Ends with vowel
            else if (endingType < 775) name += commonConsonants[rng.Next(commonConsonants.Length)];
            else if (endingType < 825) name += averageConsonants[rng.Next(averageConsonants.Length)];
            else if (endingType < 840) name += "ski";
            else if (endingType < 860) name += "son";
            else if (Regex.IsMatch(name, "(.+)(ay|e|ee|ea|oo)$") || name.Length < 5)
            {
                name = "Mc" + name.Substring(0, 1).ToUpper() + name.Substring(1);
                return name;
            }
            else name += "ez";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1); //Capitalize first letter
            return name;
        }
        /// <summary>
        /// 随机产⽣中文名
        /// </summary>
        /// <returns></returns>
        public static string GetRandomChineseName()
        {
            string[] dicts = { FAMILY_NAME_0, FAMILY_NAME_1, FAMILY_NAME_2, FAMILY_NAME_3, FAMILY_NAME_4, FAMILY_NAME_5, FAMILY_NAME_6 };//姓氏字典集
            int[] keys = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 6 };
            int key = keys[Random.Shared.Next(keys.Length)];
            string[] dict = dicts[key].Split(',');//选中字典
            return dict[Random.Shared.Next(dict.Length)] + GetRandomHanzi(Random.Shared.Next(1,3));//返回 姓氏 + 1~2个随机汉字
        }

        /// <summary>
        /// 随机产⽣常⽤汉字
        /// </summary>
        /// <param name="count">要产⽣汉字的个数</param>
        /// <returns>常⽤汉字</returns>
        public static string GetRandomHanzi(int count)
        {
            string chineseWords = "";
            Random rm = new Random();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//在使用编码方法之前，对编码进行注册
            Encoding gb = Encoding.GetEncoding("gb2312");
            for (int i = 0; i < count; i++)
            {
                // 获取区码(常⽤汉字的区码范围为16-55)
                int regionCode = rm.Next(16, 56);
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }
                // 转换区位码为机内码
                int regionCode_Machine = regionCode + 160;// 160即为⼗六进制的20H+80H=A0H
                int positionCode_Machine = positionCode + 160;// 160即为⼗六进制的20H+80H=A0H
                                                              // 转换为汉字
                byte[] bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                chineseWords += gb.GetString(bytes);
            }
            return chineseWords;
        }
        public static readonly string FAMILY_NAME_0 = "赵,孙,李,陈,何,周,吴,王,张,郑,朱,许,杨,沈,马,徐,丁,刘,林";
        public static readonly string FAMILY_NAME_1 = "赵,孙,李,陈,何,周,吴,王,张,郑,朱,许,杨,沈,马,徐,丁,刘,林,钱,冯,褚,卫,蒋,韩,秦,尤,吕,施,孔,曹,严,华,金,魏,陶,姜,戚,谢,邹,苏,潘,葛,范,彭,方,俞,任,袁,柳,史,唐,费,汤,余,顾,孟,黄,姚,汪,戴,宋,庞,纪,项,祝,董,梁,杜,季,贾,江,郭,梅,盛,高,夏,蔡,田,万,薛,雷,贺,罗,毕,齐,萧,毛,章,胡,程,叶";
        public static readonly string FAMILY_NAME_2 = "向,习,牛,丰,聂,舒,邱,耿,楚,岳,关,庄,乔,卓,白,喻,柏,水,窦,云,奚,郎,鲁,韦,昌,苗,凤,花,鲍,廉,岑,倪,滕,殷,郝,邬,安,常,乐,于,时,傅,皮,卞,康,伍,元,卜,平,和,穆,尹,邵,湛,祁,禹,狄,米,贝,明,臧,计,伏,成,谈,茅,熊,屈,阮,蓝,闵,席,麻,强,路,娄,危,童,颜,刁,钟,骆,樊,凌,霍,虞,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,龚,嵇,邢,裴,陆,荣,翁,甄,家,封,芮,储,靳,糜,松,井,段,富,巫,乌,焦,牧,山,谷,车,侯,全,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,厉,戎,祖,武,符,景,詹,束,龙,闻,温,艾,晏,柴";
        public static readonly string FAMILY_NAME_3 = "巴,弓,宓,蓬,寇,佟,年,谭,赖,文,易,辛,燕,冀,郏,浦,尚,农,瞿,阎,荀,羊,於,惠,滑,郗,羿,墨,古";
        public static readonly string FAMILY_NAME_4 = "邰,蔺,咸,师,晁,勾,敖,融,冷,訾,阚,那,简,饶,空,曾,毋,沙,从,言,福,司马,上官,欧阳,夏侯,诸葛,闻人,东方,皇甫,尉迟,公孙,令狐,宇文,长孙,慕容,司徒,司空,南宫,西门,拓跋,哈,谯,伯,边,扈,桑,桂,商,归,海,佘,牟,游,竺,权,慕,连,爱,阳,容,匡,国,广,禄";
        public static readonly string FAMILY_NAME_5 = "钮,钭,隗,汲,邴,酆,麴,幸,司,韶,郜,黎,蓟,薄,印,宿,怀,蒲,鄂,索,籍,屠,蒙,池,阴,欎,胥,能,苍,双,莘,党,翟,贡,劳,逄,姬,申,扶,堵,冉,宰,郦,雍,舄,璩,濮,寿,通,别,充,茹,宦,鱼,慎,戈,廖,庾,终,暨,居,衡,步,都,满,弘,阙,东,殴,殳,沃,利,蔚,越,夔,隆,巩,厍,乜,养,鞠,须,巢,蒯,相,查,後,荆,红,逯,盖,益,桓,公,万俟,赫连,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,申屠,仲孙,轩辕,钟离,鲜于,闾丘,亓官,司寇,仉,督,子车,颛孙,端木,巫马,公西,漆雕,乐正,壤驷,公良,夹谷,宰父,谷梁,晋,闫,法,汝,鄢,涂,钦,段干,百里,东郭,南门,呼延,羊舌,微生,帅,缑,亢,况,后,有,琴,梁丘,左丘,东门,佴,赏,笪,第五";
        public static readonly string FAMILY_NAME_6 = "寸,卓,蔺,屠,蒙,池,乔,阳,郁,胥,能,苍,双,闻,莘,党,翟,谭,贡,劳,逄,姬,申,扶,堵,冉,宰,郦,雍,却,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,僪,浦,尚,农,温,别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,庾,终,暨,居,衡,步都,耿,满,弘,匡,国,文,寇,广,禄,阙,东欧,殳,沃,利,蔚,越,夔,隆,师,巩,厍,聂晁,勾,敖,融,冷,訾,辛,阚,那,简,饶,空曾,毋,沙,乜,养,鞠,须,丰,巢,关,蒯,相查,后,荆,红,游,竺,权,逮,盍,益,桓,公,唱,万俟,司马,上官,欧阳,夏侯,诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,申屠,公孙,仲孙,轩辕,令狐,钟离,宇文,长孙,慕容,司徒,司空,召,有,舜,丛,岳,寸,贰,皇,侨,彤,竭,端,赫,实,甫,集,象,翠,狂,辟,典,良,函,芒,苦,其,京,中,夕,之,蹇,称,诺,来,多,繁,戊,朴,回,毓,税,荤,靖,绪,愈,硕,牢,买,但,巧,枚,撒,泰,秘,亥,绍,以,壬,森,斋,释,奕,姒,朋,求,羽,用,占,真,穰,翦,闾,漆,贵,代,贯,旁,崇,栋,告,休,褒,谏,锐,皋,闳,在,歧,禾,示,是,委,钊,频,嬴,呼,大,威,昂,律,冒,保,系,抄,定,化,莱,校,么,抗,祢,綦,悟,宏,功,庚,务,敏,捷,拱,兆,丑,丙,畅,苟,随,类,卯,俟,友,答,乙,允,甲,留,尾,佼,玄,乘,裔,延,植,环,矫,赛,昔,侍,度,旷,遇,偶,前,由,咎,塞,敛,受,泷,袭,衅,叔,圣,御,夫,仆,镇,藩,邸,府,掌,首,员,焉,戏,可,智,尔,凭,悉,进,笃,厚,仁,业,肇,资,合,仍,九,衷,哀,刑,俎,仵,圭,夷,徭,蛮,汗,孛,乾,帖,罕,洛,淦,洋,邶,郸,郯,邗,邛,剑,虢,隋,蒿,茆,菅,苌,树,桐,锁,钟,机,盘,铎,斛,玉,线,针,箕,庹,绳,磨,蒉,瓮,弭,刀,疏,牵,浑,恽,势,世,仝,同,蚁,止,戢,睢,冼,种,凃肖,己,泣,潜,卷,脱,谬,蹉,赧,浮,顿,说,次,错,念,夙,斯,完,丹,表,聊,源,姓,吾,寻,展,出,不,户,闭,才,无,书,学,愚,本,性,雪,霜,烟,寒,少,字,桥,板,斐,独,千,诗,嘉,扬,善,揭,祈,析,赤,紫,青,柔,刚,奇,拜,佛,陀,弥,阿,素,长,僧,隐,仙,隽,宇,祭,酒,淡,塔,琦,闪,始,星,南,天,接,波,碧,速,禚,腾,潮,镜,似,澄,潭,謇,纵,渠,奈,风,春,濯,沐,茂,英,兰,檀,藤,枝,检,生,折,登,驹,骑,貊,虎,肥,鹿,雀,野,禽,飞,节,宜,鲜,粟,栗,豆,帛,官,布,衣,藏,宝,钞,银,门,盈,庆,喜,及,普,建,营,巨,望,希,道,载,声,漫,犁,力,贸,勤,革,改,兴,亓,睦,修,信,闽,北,守,坚,勇,汉,练,尉,士,旅,五,令,将,旗,军,行,奉,敬,恭,仪,母,堂,丘,义,礼,慈,孝,理,伦,卿,问,永,辉,位,让,尧,依,犹,介,承,市,所,苑,杞,剧,第,零,谌,招,续,达,忻,六,鄞,战,迟,候,宛,励,粘,萨,邝,覃,辜,初,楼,城,区,局,台,原,考,妫,纳,泉,老,清,德,卑,过,麦,曲,竹,百,福,言,第五,佟,爱,年,笪,谯,哈,墨,南宫,赏,伯,佴,佘,牟,商,西门,东门,左丘,梁丘,琴,后,况,亢,缑,帅,微生,羊舌,海,归,呼延,南门,东郭,百里,钦,鄢,汝,法,闫,楚,晋,谷梁,宰父,夹谷,拓跋,壤驷,乐正,漆雕,公西,巫马,端木,颛孙,子车,督,仉,司寇,亓官,鲜于,锺离,盖,逯,库,郏,逢,阴,薄,厉,稽,闾丘,公良,段干,开,光,操,瑞,眭,泥,运,摩,伟,铁,迮,荔菲,辗迟";
    }
}
