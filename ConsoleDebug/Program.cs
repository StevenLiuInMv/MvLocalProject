using MvSharedLib.Checker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            // 要用的時候再自己打開, 後面要再寫一段, 之後都要用帶參數的方式
            //DebugExceuteCollectOutlookPSTPath();
            //DebugImportOutlookPSTPathsToDB();
            string commandString = @"d:\PW.exe";
            //System.Diagnostics.Process.Start("d:\\pw.exe");
            //System.Diagnostics.Process.Start("d:\\PW.exe > d:\\1.txt");
            InvokeExcute(commandString);
            //string result = CommandOutput("d:\\PW.exe");
            //Console.WriteLine(result);
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

        private static string InvokeExcute(string Command)
        {
            //Command = Command.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "d:\\pw.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系統shell啟動
                p.StartInfo.RedirectStandardInput = true;   //接受來自調用程序的輸入信息
                p.StartInfo.RedirectStandardOutput = true;  //由調用程序獲取輸出信息
                p.StartInfo.RedirectStandardError = true;   //重定向標准錯誤輸出
                p.StartInfo.CreateNoWindow = false;          //不顯示程序窗口
                p.Start();//啟動程序
                          //向cmd窗口寫入命令
                p.StandardInput.WriteLine("");
                p.StandardInput.AutoFlush = true;
                //獲取cmd窗口的輸出信息
                StreamReader reader = p.StandardOutput;//截取輸出流
                string str = reader.ReadToEnd();
                p.WaitForExit();//等待程序執行完退出進程
                p.Close();
                return str;
            }
        }

        public static string CommandOutput(string commandText)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false; //不跳出cmd視窗
            string strOutput = null;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();//匯出整個執行過程
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }
        
    }
}
