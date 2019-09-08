using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KolizjeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(false);
        }
    }
    
}

