using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Mock
{
    public partial class TestEntity2
    {
        public int BlogID { get; set; }
        public string BlogName { get; set; }
        public string IP { get; set; }
        public Model.Article Article { get; set; }
    }
}
