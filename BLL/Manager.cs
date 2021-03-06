//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
	
    public partial class AdminService : BaseService<Admin>, IAdminService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.AdminDAL;
        }
    }
	
    public partial class ArticleService : BaseService<Article>, IArticleService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.ArticleDAL;
        }
    }
	
    public partial class BlogUserService : BaseService<BlogUser>, IBlogUserService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.BlogUserDAL;
        }
    }
	
    public partial class BuildingService : BaseService<Building>, IBuildingService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.BuildingDAL;
        }
    }
	
    public partial class dtpropertiesService : BaseService<dtproperties>, IdtpropertiesService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.dtpropertiesDAL;
        }
    }
	
    public partial class HrefService : BaseService<Href>, IHrefService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.HrefDAL;
        }
    }
	
    public partial class MessageService : BaseService<Message>, IMessageService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.MessageDAL;
        }
    }
	
    public partial class ProjectService : BaseService<Project>, IProjectService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.ProjectDAL;
        }
    }
	
    public partial class QuantityService : BaseService<Quantity>, IQuantityService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.QuantityDAL;
        }
    }
	
    public partial class ReplyService : BaseService<Reply>, IReplyService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentDbSession.ReplyDAL;
        }
    }
	
}