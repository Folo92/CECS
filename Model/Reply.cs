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
    using System.Collections.Generic;
    
    public partial class Reply
    {
        public int ReplyID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public Nullable<int> BlogID { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string IP { get; set; }
        public Nullable<int> VisitorID { get; set; }
        public string VisitorName { get; set; }
    }
}
