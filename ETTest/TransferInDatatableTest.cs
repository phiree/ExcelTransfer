using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ETBiz;
using System.Data;
namespace ETTest
{
    [TestFixture]
    public class TransferInDatatableTest
    {
        [Test]
        public void JoinXslToDataTableTest()
        {
            TransferInDatatable td = new TransferInDatatable();
            string erp = Environment.CurrentDirectory + @"\testresource\标准格式物料.XLS";

            //string baojia = Environment.CurrentDirectory + @"\testresource\NTS产品报价单.xls";
            //string bianma = Environment.CurrentDirectory + @"\testresource\NTS编码表.xls";
            //string gongyingshang = Environment.CurrentDirectory + @"\testresource\供应商编码.xls";

            //    string erpOut = Environment.CurrentDirectory + @"\testresource\out标准格式物料.XLS";

            //DataTable dt = td.JoinXslToDataTable(baojia, bianma, gongyingshang, erp);
            //Assert.AreEqual(2, dt.Rows.Count);
            //td.CreateXslFromDataTable(dt, erp, erpOut);

            string baojia1756 = Environment.CurrentDirectory + @"\testresource\NTS产品报价单1756.xls";
            //string bianma2130 = Environment.CurrentDirectory + @"\testresource\NTS编码表2130.xls";
            string gongyingshang104 = Environment.CurrentDirectory + @"\testresource\供应商编码104.xls";
            string erpOut1756 = Environment.CurrentDirectory + @"\testresource\物料.XLS";

            DataTable dt1728 = td.JoinXslToDataTable(baojia1756, gongyingshang104, erp);
            Assert.AreEqual(1750, dt1728.Rows.Count);
            td.CreateXslFromDataTable(dt1728, erp, erpOut1756);



        }
        [Test]
        public void CreateFromXslTest()
        {
            TransferInDatatable td = new TransferInDatatable();
           DataTable dt1= td.CreateFromXsl(Environment.CurrentDirectory + @"\testresource\NTS编码表.xls");
           Assert.AreEqual(1,dt1.Rows.Count);

           DataTable dt2 = td.CreateFromXsl(Environment.CurrentDirectory + @"\testresource\NTS产品报价单.xls");
           Assert.AreEqual(1, dt2.Rows.Count);

           DataTable dt3 = td.CreateFromXsl(Environment.CurrentDirectory + @"\testresource\标准格式物料.XLS");
           Assert.AreEqual(0, dt3.Rows.Count);

           DataTable dt4 = td.CreateFromXsl(Environment.CurrentDirectory + @"\testresource\供应商编码.xls");
           Assert.AreEqual(1, dt4.Rows.Count);
        }
     
        [Test]
        public void BuildNtsCodeTest()
        {
            TransferInDatatable td = new TransferInDatatable();
            Assert.AreEqual("01.012.0000400001", td.BuildNtsCode("01.012", "00004"));
            
        }

    }
}
