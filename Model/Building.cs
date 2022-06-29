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

    public partial class Building
    {
        public int BuildingID { get; set; }
        public string BuildingName { get; set; }
        public string BuildingType { get; set; }
        public string StructureSystem { get; set; }
        public Nullable<decimal> BuildingHeight { get; set; }
        public Nullable<short> Floors { get; set; }
        public Nullable<decimal> FloorHeight { get; set; }
        public Nullable<decimal> ArchitectureArea { get; set; }
        public Nullable<decimal> StructureArea { get; set; }
        public string SeismicIntensity { get; set; }
        public string SiteClassification { get; set; }
        public string SeismicGrade { get; set; }
        public Nullable<decimal> ReferenceWindPressure { get; set; }
        public string Regularity { get; set; }
        public string ColumnSpan { get; set; }
        public Nullable<byte> TransferedFloors { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> DesignerID { get; set; }
    }
}
