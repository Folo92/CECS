using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Mock
{
    public partial class TestEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public byte Age { get; set; }
        public sbyte Flag { get; set; }
        public string? Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public uint PostCode { get; set; }
        public string Email { get; set; }
        public long MobilePhone { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? RegTime { get; set; }
        public TestEntity2 Blog { get; set; }
        public Model.Href HrefLink { get; set; }
    }
}
