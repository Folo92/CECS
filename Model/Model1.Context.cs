//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CECSDbEntities : DbContext
    {
        public CECSDbEntities()
            : base("name=CECSDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<BlogUser> BlogUser { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<dtproperties> dtproperties { get; set; }
        public virtual DbSet<Href> Href { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Quantity> Quantity { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
    
        public virtual int pro_avgQuantity(string tblName, string strAvgFields, string strGroupBy, string strWhere)
        {
            var tblNameParameter = tblName != null ?
                new ObjectParameter("tblName", tblName) :
                new ObjectParameter("tblName", typeof(string));
    
            var strAvgFieldsParameter = strAvgFields != null ?
                new ObjectParameter("strAvgFields", strAvgFields) :
                new ObjectParameter("strAvgFields", typeof(string));
    
            var strGroupByParameter = strGroupBy != null ?
                new ObjectParameter("strGroupBy", strGroupBy) :
                new ObjectParameter("strGroupBy", typeof(string));
    
            var strWhereParameter = strWhere != null ?
                new ObjectParameter("strWhere", strWhere) :
                new ObjectParameter("strWhere", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pro_avgQuantity", tblNameParameter, strAvgFieldsParameter, strGroupByParameter, strWhereParameter);
        }
    
        public virtual int pro_pageList(string tblName, string strGetFields, string fldName, Nullable<int> pageSize, Nullable<int> pageIndex, Nullable<bool> doCount, Nullable<bool> orderType, string strWhere)
        {
            var tblNameParameter = tblName != null ?
                new ObjectParameter("tblName", tblName) :
                new ObjectParameter("tblName", typeof(string));
    
            var strGetFieldsParameter = strGetFields != null ?
                new ObjectParameter("strGetFields", strGetFields) :
                new ObjectParameter("strGetFields", typeof(string));
    
            var fldNameParameter = fldName != null ?
                new ObjectParameter("fldName", fldName) :
                new ObjectParameter("fldName", typeof(string));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var doCountParameter = doCount.HasValue ?
                new ObjectParameter("doCount", doCount) :
                new ObjectParameter("doCount", typeof(bool));
    
            var orderTypeParameter = orderType.HasValue ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(bool));
    
            var strWhereParameter = strWhere != null ?
                new ObjectParameter("strWhere", strWhere) :
                new ObjectParameter("strWhere", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pro_pageList", tblNameParameter, strGetFieldsParameter, fldNameParameter, pageSizeParameter, pageIndexParameter, doCountParameter, orderTypeParameter, strWhereParameter);
        }
    }
}
