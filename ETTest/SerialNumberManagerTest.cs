using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ETBiz;
namespace ETTest
{
    [TestFixture]
   public class SerialNumberManagerTest
    {
        SerialNumberManager snm = new SerialNumberManager();
       
        [Test]
        public void SerialNumberTest()
        {
            Assert.AreEqual(1,snm.GetSerialNo("01.012.00001",false));
           
            Assert.AreEqual(2, snm.GetSerialNo("01.012.00001",false));
            snm.WriteSerialNumberFile();
        }
    }
}
