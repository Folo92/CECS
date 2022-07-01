using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTest.Mock.Tests
{
    [TestClass()]
    public class GeneratorTests
    {
        [TestMethod()]
        public void Test()
        {
            //AddressGenerator.Test();
            DataGenerator.Test();
            //NameGenerator.Test();
            //ProverbGenerator.Test();
            for (int i = 0; i < 20; i++) Console.WriteLine(DataGenerator.GetRandomMobileNumber());
            
            //Assert.Fail();
        }
    }
}