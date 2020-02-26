using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
//using MvLocalProject.Controller;

namespace MvSharedLib.Checker
{

    public enum MSOfficeApplication
    {
        Word,
        Excel,
        PowerPoint,
        Outlook
    }

    public enum CollectOutlookPSTPathStatus
    {
        IllegalStatus = -1,
        StartGetVersion = 0,
        DoneByGetVersion = 1,
        DoneByGetPstPathList = 2,
        DoneByOutputFileToLocalDisk = 3,
        DoneByInsertIntoDB = 4,
        DoneByUploadFile = 5
    }

    public class OfficeCheck
    {
        /// <summary>
        /// 通過註冊表檢測office版本
        /// 版权声明：本文为CSDN博主「xiaojie119120」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
        /// 原文链接：https://blog.csdn.net/xiaojie119120/article/details/54581588
        /// </summary>
        /// <param name="OfficeVersion">儲存office版本的字符串</param>
        /// <returns></returns>
        public static bool OfficeIsInstall(out string OfficeVersion)
        {

            OfficeVersion = "";
            Microsoft.Win32.RegistryKey regKey = null;
            Microsoft.Win32.RegistryKey regSubKey1 = null;
            Microsoft.Win32.RegistryKey regSubKey2 = null;
            Microsoft.Win32.RegistryKey regSubKey3 = null;
            Microsoft.Win32.RegistryKey regSubKey4 = null;
            Microsoft.Win32.RegistryKey regSubKey5 = null;

            // OpenBaseKey
            regKey = OpenBaseKey(RegistryHive.LocalMachine);
            regSubKey1 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Common\InstallRoot", false);
            regSubKey2 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Common\InstallRoot", false);
            regSubKey3 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0\Common\InstallRoot", false);
            regSubKey4 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\15.0\Common\InstallRoot", false);
            regSubKey5 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\16.0\Common\InstallRoot", false);

            if (regSubKey5 != null && regSubKey5.GetValue("Path") != null)
            {
                OfficeVersion = "2016";
                return true;
            }
            else if (regSubKey4 != null && regSubKey4.GetValue("Path") != null)
            {
                OfficeVersion = "2013";
                return true;
            }
            else if (regSubKey3 != null && regSubKey3.GetValue("Path") != null)
            {
                OfficeVersion = "2010";
                return true;
            }
            else if (regSubKey2 != null && regSubKey2.GetValue("Path") != null)
            {
                OfficeVersion = "2007";
                return true;
            }
            else if (regSubKey1 != null && regSubKey1.GetValue("Path") != null)
            {
                OfficeVersion = "2003";
                return true;
            }

            else
            {

                regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                regSubKey1 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Common\InstallRoot", false);
                regSubKey2 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Common\InstallRoot", false);
                regSubKey3 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0\Common\InstallRoot", false);
                regSubKey4 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\15.0\Common\InstallRoot", false);
                regSubKey5 = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\16.0\Common\InstallRoot", false);
                if (regSubKey5 != null && regSubKey5.GetValue("Path") != null)
                {
                    OfficeVersion = "2016";
                    return true;
                }
                else if (regSubKey4 != null && regSubKey4.GetValue("Path") != null)
                {
                    OfficeVersion = "2013";
                    return true;
                }
                else if (regSubKey3 != null && regSubKey3.GetValue("Path") != null)
                {
                    OfficeVersion = "2010";
                    return true;
                }
                else if (regSubKey2 != null && regSubKey2.GetValue("Path") != null)
                {
                    OfficeVersion = "2007";
                    return true;
                }
                else if (regSubKey1 != null && regSubKey1.GetValue("Path") != null)
                {
                    OfficeVersion = "2003";
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static bool IsExcelInstalled()
        {
            Type type = Type.GetTypeFromProgID("Excel.Application");
            return type != null;
        }

        /// <summary>
        /// 取得 Office Outlook版本
        /// </summary>
        /// <returns>11/12/14/15/16</returns>
        /// <remarks>2003/2007/2010/2013/2016</remarks>
        public static int GetMSOutlookVersion()
        {
            return GetMSOfficeApplicationVersion(MSOfficeApplication.Outlook);
        }

        /// <summary>
        /// 取得 Office Excel版本
        /// </summary>
        /// <returns>11/12/14/15/16</returns>
        /// <remarks>2003/2007/2010/2013/2016</remarks>
        public static int GetMSExcelVersion()
        {
            return GetMSOfficeApplicationVersion(MSOfficeApplication.Excel);
        }

        /// <summary>
        /// 取得 Office PowerPoint版本
        /// </summary>
        /// <returns>11/12/14/15/16</returns>
        /// <remarks>2003/2007/2010/2013/2016</remarks>
        public static int GetMSPowerPointVersion()
        {
            return GetMSOfficeApplicationVersion(MSOfficeApplication.PowerPoint);
        }

        /// <summary>
        /// 取得 Office Word版本
        /// </summary>
        /// <returns>11/12/14/15/16</returns>
        /// <remarks>2003/2007/2010/2013/2016</remarks>
        public static int GetMSWordVersion()
        {
            return GetMSOfficeApplicationVersion(MSOfficeApplication.Word);
        }

        /// <summary>
        /// 取得 Office Application版本
        /// </summary>
        /// <param name="name"></param>
        /// <returns>11/12/14/15/16</returns>
        /// <remarks>2003/2007/2010/2013/2016</remarks>
        private static int GetMSOfficeApplicationVersion(MSOfficeApplication name)
        {
            string TempString = string.Empty;
            Microsoft.Win32.RegistryKey RegKey = null;
            Microsoft.Win32.RegistryKey RegSubKey = null;

            // 組出registry key 名稱
            TempString = GetMSOfficeApplicationName(name);
            if (string.IsNullOrEmpty(TempString)) return int.MinValue;
            TempString = string.Format(@"{0}.Application\CurVer", TempString);

            // OpenBaseKey
            RegKey = OpenBaseKey(RegistryHive.ClassesRoot);
            RegSubKey = RegKey.OpenSubKey(TempString, false);
            if (RegSubKey == null) return int.MinValue;

            object TempData = null;
            // Get default value
            TempData = RegSubKey.GetValue(null);
            if (TempData == null) return int.MinValue;

            // 取得末2碼版本代表號
            TempString = TempData.ToString();
            TempString = TempString.Substring(TempString.Length - 2);

            int n;
            if (int.TryParse(TempString, out n) == false)
            {
                return int.MinValue;
            }

            return int.Parse(TempString);
        }

        private static string GetMSOfficeApplicationName(MSOfficeApplication name)
        {
            foreach (MSOfficeApplication enumType in Enum.GetValues(typeof(MSOfficeApplication)))
            {
                if (enumType == name)
                {
                    return enumType.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// 利用RegistryKey取得Outlook PST檔案路徑清單
        /// </summary>
        /// <returns></returns>
        public static string[] GetMSOutlookPstPaths()
        {
            int OutlookVersion = int.MinValue;

            OutlookVersion = GetMSOutlookVersion();
            // 無法取到Outlook version, 直接回傳null
            if (OutlookVersion == int.MinValue) return null;
            return GetMSOutlookPstPaths(OutlookVersion);
        }

        /// <summary>
        /// 利用RegistryKey取得Outlook PST檔案路徑清單
        /// </summary>
        /// <param name="OutlookVersion"></param>
        /// <returns></returns>
        public static string[] GetMSOutlookPstPaths(int OutlookVersion)
        {
            string TempString = null;
            Microsoft.Win32.RegistryKey RegKey = null;
            Microsoft.Win32.RegistryKey RegSubKey = null;

            // OpenBaseKey
            RegKey = OpenBaseKey(RegistryHive.CurrentUser);
            if (RegKey == null) return null;

            // 組出Sub Registry key 名稱
            TempString = string.Format(@"Software\Microsoft\Office\{0}.0\Outlook\Search\Catalog", OutlookVersion);
            RegSubKey = RegKey.OpenSubKey(TempString, false);

            // 如取到的值為null, 再判斷另一個路徑, Outlook 2007版本
            if (RegSubKey == null)
            {
                TempString = string.Format(@"Software\Microsoft\Office\{0}.0\Outlook\Catalog", OutlookVersion);
                RegSubKey = RegKey.OpenSubKey(TempString, false);
                // 如果兩個路徑都取不到機碼, 回傳null
                if (RegSubKey == null) return null;
            }

            string[] nameList = RegSubKey.GetValueNames();
            foreach (string s in nameList)
            {
                Console.WriteLine("name = " + s);
            }

            // 過濾副檔名非.pst的路徑
            var query = from keyName in nameList
                        where keyName.EndsWith(".pst")
                        select keyName;

            return (query == null || query.Count() == 0) ? null : query.ToArray();
        }

        private static Microsoft.Win32.RegistryKey OpenBaseKey(RegistryHive rh) {
            Microsoft.Win32.RegistryKey RegKey = null;

            
            if (Environment.Is64BitOperatingSystem)
            {
                RegKey = RegistryKey.OpenBaseKey(rh, RegistryView.Registry64);
            }
            else
            {
                RegKey = RegistryKey.OpenBaseKey(rh, RegistryView.Registry32);
            }
            return RegKey;
        }

        /// <summary>
        /// 執行取得Outlook flag的function
        /// </summary>
        /// <returns></returns>
        public CollectOutlookPSTPathStatus ExceuteCollectOutlookPSTPath()
        {
            int OutlookVersion = int.MinValue;
            CollectOutlookPSTPathStatus status = CollectOutlookPSTPathStatus.StartGetVersion;
            string OutputPathAndName = string.Empty;
            string OutputFileName = string.Empty;
            string[] PstPaths = null;
            List<string> ExistPstPaths = null;

            
            // 執行檢查是否有取到Outlook Version
            OutlookVersion = GetMSOutlookVersion();
            if (OutlookVersion == int.MinValue) { return status; }
            status = CollectOutlookPSTPathStatus.DoneByGetVersion;

            // 執行檢查是否取得PST Search log
            PstPaths = GetMSOutlookPstPaths();
            if (PstPaths == null) { return status; }

            // 檢查檔案是否存在
            ExistPstPaths = new List<string>();
            long fileSize = 0;
            //const long limitSize = 8000000000;
            //const long limitSize = 1900000000;
            //int overLimitSize = 0;
            foreach (string path in PstPaths)
            {
                if (File.Exists(path) == false) { continue; }

                fileSize = new FileInfo(path).Length;
                //overLimitSize = fileSize > limitSize ? 1 : 0;
                ExistPstPaths.Add(string.Format("{0}\t{1}\t{2}", Environment.MachineName, Convert.ToString(fileSize), path)); 
            }
            status = CollectOutlookPSTPathStatus.DoneByGetPstPathList;
            
            // 執行檢查Output檔案到local
            OutputFileName = string.Format("{0}.txt", Environment.MachineName);
            OutputPathAndName = string.Format(@"D:\{0}", OutputFileName);
            try
            {
                File.WriteAllLines(OutputPathAndName, ExistPstPaths.ToArray(), Encoding.UTF8);
            }
            catch(DirectoryNotFoundException dnf)
            {
                // 當有找不到路徑的問題時, 強制寫入C:\ProgramData的目錄裡
                OutputPathAndName = string.Format(@"C:\ProgramData\{0}_Outlook.txt", Environment.MachineName);
                File.WriteAllText(OutputPathAndName, dnf.StackTrace, Encoding.UTF8);
                File.AppendAllText(OutputPathAndName, Environment.NewLine, Encoding.UTF8);
                File.AppendAllText(OutputPathAndName, dnf.Message, Encoding.UTF8);
                return CollectOutlookPSTPathStatus.IllegalStatus;
            }
            catch (IOException ioe)
            {
                OutputPathAndName = string.Format(@"C:\ProgramData\{0}_Outlook.txt", Environment.MachineName);
                File.WriteAllText(OutputPathAndName, ioe.StackTrace, Encoding.UTF8);
                File.AppendAllText(OutputPathAndName, Environment.NewLine, Encoding.UTF8);
                File.AppendAllText(OutputPathAndName, ioe.Message, Encoding.UTF8);
                return CollectOutlookPSTPathStatus.IllegalStatus;
            }

            status = CollectOutlookPSTPathStatus.DoneByOutputFileToLocalDisk;
            // 回寫路徑到DB
            
            //status = CollectOutlookPSTPathStatus.DoneByInsertIntoDB;
            // 無法回寫路徑時, 是否可上傳檔案到指定的路徑
            File.Copy(OutputPathAndName, @"\\mv2\public\StevenLiu\CollectOutlookPSTPaths\" + OutputFileName, true);
            // 已複製後
            File.Delete(OutputPathAndName);
            //File.Copy(OutputPathAndName, @"D:\99_TempArea\" + OutputFileName);
            status = CollectOutlookPSTPathStatus.DoneByUploadFile;
            
            return status;
        }

        public static bool ImportOutlookPSTPathsToDB(string destFilePath)
        {
            DirectoryInfo DInfo = new DirectoryInfo(destFilePath);
            // 如果路徑不存在, 不用再執行了
            if (DInfo.Exists == false) { return false; }

            // 列出目錄下的所有.txt檔案
            // 讀取所有的檔案內容
            var mergeDataList = new List<string>();
            FileInfo[] files = DInfo.GetFiles("*.txt").Where(p => p.Extension == ".txt").ToArray();
            foreach (FileInfo fi in files)
            {
                string[] fileList = File.ReadAllLines(string.Format(@"{0}\{1}", destFilePath, fi.ToString()));
                mergeDataList.AddRange(fileList);
            }

            // Clone該資料表的Table Schema
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();
            SqlConnection connection = MvDbConnector.Connection_ERPBK_Dot_IT;

            try
            {
                connection.Open();
                sb.AppendLine(string.Format(@"SELECT TOP 0 * from IT.dbo.CollectOutlookPstFileName"));
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                // 發生例外時，會自動rollback
                throw se;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            // 將txt的內容匯入Clone table
            string[] tempArray = null;
            foreach (string content in mergeDataList)
            {
                tempArray = content.Split('\t');

                var row = majorData.NewRow();
                row["ComputerName"] = tempArray[0];
                row["FileSize"] = tempArray[1];
                row["PstFileName"] = tempArray[2];
                row["CreateDate"] = DateTime.Now;
                majorData.Rows.Add(row);
            }


            // 使用SQLBulk將資料塞入


            using (TransactionScope scope = new TransactionScope())
            {
                connection = MvDbConnector.Connection_ERPBK_Dot_IT;

                try
                {
                    connection.Open();
                    // TRUNCATE 資料庫內Table的資料
                    SqlCommand command = null;
                    command = connection.CreateCommand();
                    command.CommandText = "Insert into IT.dbo.CollectOutlookPstFileNameHistory Select ComputerName, PstFileName, FileSize, CreateDate From IT.dbo.CollectOutlookPstFileName ";
                    command.ExecuteNonQuery();

                    command.CommandText = "Truncate Table IT.dbo.CollectOutlookPstFileName";
                    command.ExecuteNonQuery();

                    using (SqlBulkCopy sbc = new SqlBulkCopy(connection))
                    {
                        // set number of records to be processed
                        sbc.BatchSize = 300;
                        sbc.DestinationTableName = "CollectOutlookPstFileName";
                        // write to server
                        sbc.WriteToServer(majorData);
                    }
                    scope.Complete();
                }
                catch (SqlException se)
                {
                    // 發生例外時，會自動rollback
                    throw se;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return false;
        }

    }
}
