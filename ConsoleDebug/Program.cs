using MvSharedLib.Checker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            //DebugExceuteCollectOutlookPSTPath();
            DebugImportOutlookPSTPathsToDB();
        }

        private static void DebugExceuteCollectOutlookPSTPath()
        {
            CollectOutlookPSTPathStatus status = CollectOutlookPSTPathStatus.StartGetVersion;
            OfficeCheck officeCheck = new OfficeCheck();

            status = officeCheck.ExceuteCollectOutlookPSTPath();
        }

        private static void DebugImportOutlookPSTPathsToDB()
        {

            bool status = false;
            string pstPath = @"\\mv2\public\StevenLiu\CollectOutlookPSTPaths";
            OfficeCheck officeCheck = new OfficeCheck();
            status = OfficeCheck.ImportOutlookPSTPathsToDB(pstPath);
        }
    }
}
