using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windup.SerialTalker;

namespace Windup.SerialTalkerTest
{
    using NUnit.Framework;

    [TestFixture]
    public class SerialListTest
    {
        [Test]
        public void ReturnSerialListTest()
        {
            int i = 1;
            string[] returnList = SerialList.ReturnSerialList();
            foreach (var s in returnList) {
                string temp = "COM" + i.ToString();
                Assert.AreEqual(s.ToString(), temp);
                i++;
            }
        }
    }
}
