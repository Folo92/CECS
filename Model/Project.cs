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
    
    public partial class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public string OwnerName { get; set; }
        public string OwnerType { get; set; }
        public Nullable<decimal> ArchitectureArea { get; set; }
        public Nullable<decimal> FloorArea { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string ProjectAddress { get; set; }
        public Nullable<int> InvestmentAmount { get; set; }
    }
}
