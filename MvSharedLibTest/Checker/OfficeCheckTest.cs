using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvSharedLib.Checker;

namespace MvSharedLibTest
{
    [TestClass]
    public class OfficeCheckTest
    {
        [TestMethod]
        public void TestOfficeCheck()
        {
            string OfficeVersion = "";
            bool result = false;
            result = OfficeCheck.OfficeIsInstall(out OfficeVersion);
            Console.WriteLine("result = " + result + "; version = " + OfficeVersion);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestIsExcelInstalled()
        {
            bool result = false;
            result = OfficeCheck.IsExcelInstalled();
            Console.WriteLine("result = " + result);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestGetMSApplicationVersion()
        {
            int result = int.MinValue;
            string[] resultArray = null;

            result = OfficeCheck.GetMSOutlookVersion();
            Console.WriteLine("Outlook Version = " + result);
            Assert.AreNotEqual(int.MinValue, result);

            result = OfficeCheck.GetMSExcelVersion();
            Console.WriteLine("Excel Version = " + result);
            Assert.AreNotEqual(int.MinValue, result);

            result = OfficeCheck.GetMSWordVersion();
            Console.WriteLine("Word Version = " + result);
            Assert.AreNotEqual(int.MinValue, result);

            result = OfficeCheck.GetMSPowerPointVersion();
            Console.WriteLine("Word Version = " + result);
            Assert.AreNotEqual(int.MinValue, result);

            resultArray = OfficeCheck.GetMSOutlookPstPaths();
            Assert.AreNotEqual(null, resultArray);
        }

        [TestMethod]
        public void TestGetMSOutlookApplicationVersion()
        {
            CollectOutlookPSTPathStatus status = CollectOutlookPSTPathStatus.StartGetVersion;
            OfficeCheck officeCheck = new OfficeCheck();

            status = officeCheck.ExceuteCollectOutlookPSTPath();
            Assert.AreNotEqual(CollectOutlookPSTPathStatus.StartGetVersion, status);
        }
    }
}
