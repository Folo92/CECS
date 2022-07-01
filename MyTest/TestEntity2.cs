using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    public partial class TestEntity2
    {
        public int EntityID { get; set; }
        public string EntityName { get; set; }
        public Model.Article Entity { get; set; }
    }
}
