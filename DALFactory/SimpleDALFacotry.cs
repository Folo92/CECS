﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using DAL;
using System;
using System.Collections.Generic;

namespace DALFactory
{
    //抽象工厂创建具体工厂，提供所有数据操作类DAL实例的创建方法（简单工厂）
    public partial class AbstractDALFactory
    {
		
        public static IAdminDAL CreateAdminDAL()
        {
            string fullClassName = NameSpace + ".AdminDAL";
            return CreateInstance(fullClassName) as IAdminDAL;
        }
		
        public static IArticleDAL CreateArticleDAL()
        {
            string fullClassName = NameSpace + ".ArticleDAL";
            return CreateInstance(fullClassName) as IArticleDAL;
        }
		
        public static IBlogUserDAL CreateBlogUserDAL()
        {
            string fullClassName = NameSpace + ".BlogUserDAL";
            return CreateInstance(fullClassName) as IBlogUserDAL;
        }
		
        public static IBuildingDAL CreateBuildingDAL()
        {
            string fullClassName = NameSpace + ".BuildingDAL";
            return CreateInstance(fullClassName) as IBuildingDAL;
        }
		
        public static IdtpropertiesDAL CreatedtpropertiesDAL()
        {
            string fullClassName = NameSpace + ".dtpropertiesDAL";
            return CreateInstance(fullClassName) as IdtpropertiesDAL;
        }
		
        public static IHrefDAL CreateHrefDAL()
        {
            string fullClassName = NameSpace + ".HrefDAL";
            return CreateInstance(fullClassName) as IHrefDAL;
        }
		
        public static IMessageDAL CreateMessageDAL()
        {
            string fullClassName = NameSpace + ".MessageDAL";
            return CreateInstance(fullClassName) as IMessageDAL;
        }
		
        public static IProjectDAL CreateProjectDAL()
        {
            string fullClassName = NameSpace + ".ProjectDAL";
            return CreateInstance(fullClassName) as IProjectDAL;
        }
		
        public static IQuantityDAL CreateQuantityDAL()
        {
            string fullClassName = NameSpace + ".QuantityDAL";
            return CreateInstance(fullClassName) as IQuantityDAL;
        }
		
        public static IReplyDAL CreateReplyDAL()
        {
            string fullClassName = NameSpace + ".ReplyDAL";
            return CreateInstance(fullClassName) as IReplyDAL;
        }
    }
	
}