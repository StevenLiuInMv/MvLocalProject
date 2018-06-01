using SevenZip;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MvLocalProject.Controller;
using Excel = Microsoft.Office.Interop.Excel;

namespace MvLocalProject.Bo
{
    internal class MvSecurityScanBo
    {
        class DefinedParamter
        {
            public static string[] extensions = new string[] { ".cpp", ".h", ".zip", ".7z", ".rar" };
            public static string[] compressFileExtensions = new string[] { ".zip", ".7z", ".rar" };
            public static string[] nonCompressFileExtensions = new string[] { ".cpp", ".h" };
            public static string[] mappingFileExtensions = new string[] { ".cpp", ".h" };

            public static string workingDirectory = @"D:\" + Environment.MachineName + Path.DirectorySeparatorChar;
            public static string compressDetailDirectoryName = "compress_detail";
            public static string copyTargetDirectoryName = "copyTo";
            public static string severnZipLibrary = @"C:\Program Files (x86)\7-Zip\7z.dll";
            //public static string severnZipLibrary = @"7z.dll";
            public static string copyListFileName = string.Format(@"{0}.copy.txt", Environment.MachineName);
            public static string copyScriptFileName = string.Format(@"{0}.copy.ps1", Environment.MachineName);
        }

        class DefinedFileType
        {
            public static String RawDataName { get { return ".raw"; } }
            public static String SummaryName { get { return ".summary"; } }
            public static String ReportName { get { return ".report"; } }
        }

        /// <summary>
        /// 各PC的Summary資料
        /// </summary>
        public void executeSummaryReport()
        {
            this.buildSummaryExcelReport(@"D:\", @"D:\");
        }
        /// <summary>
        /// 執行MvSecurityScanBo 所有流程
        /// 預設輸出Log至執行目錄
        /// </summary>
        public void executeAllProcess()
        {
            executeAllProcess(true, true, true, true, true, true);
        }

        /// <summary>
        /// 執行MvSecurityScanBo 流程
        /// </summary>
        /// <param name="isScanFile">是否執行檔案掃描</param>
        /// <param name="isScanCompress">是否執行壓縮檔掃描</param>
        /// <param name="isBuildSummary">是否產生Local 端掃描清單</param>
        /// <param name="isBuildScript">是否產生power shell檔</param>
        /// <param name="isBuildLocalExcel">是否產生個人掃描excel資料</param>
        /// <param name="isBuildSummaryExcel">是否產生各PC的Summary資料</param>
        /// <param name="isShowInConsole">是否將結果output至Console</param>
        /// <param name="isOutputLog">是否輸出log檔</param>
        public void executeAllProcess(bool isScanFile, bool isScanCompress, bool isBuildSummary, bool isBuildScript, bool isBuildLocalExcel, bool isBuildSummaryExcel, bool isShowInConsole = false, bool isOutputLog = true)
        {
            
            Stopwatch watch = new Stopwatch();
            MvLogger.write("run {0}.{1}", new object[] { System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name });
            if (Directory.Exists(DefinedParamter.workingDirectory) == false)
            {
                Directory.CreateDirectory(DefinedParamter.workingDirectory);
            }

            if (isScanFile == true)
            {
                // 1. 掃描所有實體硬碟的檔案, 並輸出檔案至指定路徑
                watch.Start();
                this.scanAllFilesFromPhysicalDisk(DefinedParamter.workingDirectory);
                watch.Stop();
                Console.WriteLine("-- finished scanAllFilesFromPhysicalDisk : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished scanAllFilesFromPhysicalDisk");
            }

            if (isScanFile == true)
            {
                // 2. 掃描所有壓縮檔內容, 並將符合的detail清單, 輸出檔案至指定路徑
                watch.Restart();
                this.scanCompressFileContent(DefinedParamter.workingDirectory, DefinedParamter.severnZipLibrary, DefinedParamter.compressFileExtensions, DefinedParamter.mappingFileExtensions);
                watch.Stop();
                Console.WriteLine("-- finished scanCompressFileContent : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished scanCompressFileContent");
            }

            if (isBuildSummary == true)
            {
                // 3. 整理所有summary資料, 並輸出.summary.檔案至指定路徑, 包含要copy的清單
                watch.Restart();
                this.buildSummaryResult(DefinedParamter.workingDirectory, DefinedParamter.compressFileExtensions, DefinedParamter.nonCompressFileExtensions, DefinedParamter.mappingFileExtensions, true);
                watch.Stop();
                Console.WriteLine("-- finished buildSummaryResult : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished buildSummaryResult");
            }

            if (isBuildScript == true)
            {
                // 4. 依各目錄下的copylist產生powershell 檔案
                watch.Restart();
                this.buildCopyScript(DefinedParamter.workingDirectory);
                watch.Stop();
                Console.WriteLine("-- finished buildCopyScript : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished buildCopyScript");
            }

            if (isBuildLocalExcel == true)
            {
                // 5. 依所有summary資料, 輸出local disk 的excel資料
                watch.Restart();
                this.buildLocalScannedExcel(DefinedParamter.workingDirectory);
                watch.Stop();
                Console.WriteLine("-- finished buildLocalScannedExcel : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished buildLocalScannedExcel");
            }

            if (isBuildSummaryExcel == true)
            {
                // 6. 依所有PC資料, 輸出summary execl report
                watch.Restart();
                this.buildSummaryExcelReport(@"D:\", @"D:\");
                watch.Stop();
                Console.WriteLine("-- finished buildSummaryExcelReport : " + watch.ElapsedMilliseconds.ToString() + " milliseconds");
                MvLogger.write("finished buildSummaryExcelReport");
            }
        }

        public void scanAllFilesFromPhysicalDisk(string workingDirectory)
        {
            string[] extensions = DefinedParamter.extensions;

            string filePathAndName = "";
            // 1. 依extension找出符合的檔案
            foreach (string extension in extensions)
            {
                // 輸出的檔名 固定尾碼帶.txt
                filePathAndName = workingDirectory + Environment.MachineName + DefinedFileType.RawDataName + extension + ".txt";
                if (File.Exists(filePathAndName) == true)
                {
                    File.Delete(filePathAndName);
                }

                // 2. 找出所有磁碟, 準備掃描
                DriveInfo[] drivers = DriveInfo.GetDrives();
                try
                {
                    foreach (DriveInfo di in drivers)
                    {
                        //使用IsReady屬性判斷裝置是否就緒
                        if (di.IsReady == false)
                        {
                            continue;
                        }

                        if (di.DriveType == DriveType.Fixed)
                        {
                            string diskName = di.Name;
                            var fileLists = FileScanner.searchAccessibleFiles(diskName, extension, false);
                            bool result = Utility.writeToFile(filePathAndName, fileLists.ToArray<string>());
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            // release parameter
            extensions = null;
            filePathAndName = null;
        }

        public void scanCompressFileContent(string workingDirectory, string severnZipLibrary, string[] compressFileExtensions, string[] mappingFileExtensions)
        {
            SevenZipBase.SetLibraryPath(severnZipLibrary);

            if (Directory.Exists(workingDirectory) == false) { return; }

            // 需要copy檔案的檔名
            string needToCopyFileName = string.Format(@"{0}{1}.compress.copy.txt", workingDirectory, Environment.MachineName);
            // 設定處理compress file的目錄
            string destFilePath = Path.Combine(Path.GetDirectoryName(workingDirectory), DefinedParamter.compressDetailDirectoryName + Path.DirectorySeparatorChar);
            // 建立目錄或清除資料
            if(Directory.Exists(destFilePath) == false)
            {
                Directory.CreateDirectory(destFilePath);
            }
            else
            {
                // 移除所有檔案
                DirectoryInfo di = new DirectoryInfo(destFilePath);
                FileInfo[] files = di.GetFiles("*.txt").Where(p => p.Extension == ".txt").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        file.Attributes = FileAttributes.Normal;
                        File.Delete(file.FullName);
                    }
                    catch { }
                }
            }
            // 依match的附檔名, 開始parsing 內容
            string sourceFileName = "";
            string destFilePathAndName = "";
            foreach (string extension in compressFileExtensions)
            {
                sourceFileName = Path.Combine(workingDirectory, string.Format("{0}{1}{2}.txt", Environment.MachineName, DefinedFileType.RawDataName, extension));
                // 如檔案不存在, 直接離開
                if (File.Exists(sourceFileName) == false) { continue; }

                if (Directory.Exists(destFilePath) == false) { Directory.CreateDirectory(destFilePath); }

                // 取得檔案內所有壓縮檔清單
                string[] fileList = File.ReadAllLines(sourceFileName);
                StringBuilder sb = new StringBuilder();
                // 判斷是否需要列入copy清單
                bool isNeedToCopy = false;

                // 開始整理資料
                foreach (string pattern in mappingFileExtensions)
                {
                    isNeedToCopy = false;
                    foreach (string fileName in fileList)
                    {
                        sb.Clear();
                        try
                        {
                            using (SevenZipExtractor extractor = new SevenZipExtractor(fileName))
                            {
                                foreach (var name in extractor.ArchiveFileNames)
                                {
                                    if (name.ToString().EndsWith(pattern) == false)
                                    {
                                        continue;
                                    }
                                    sb.AppendLine(name.ToString());
                                }
                            }
                        }
                        catch (DirectoryNotFoundException) { continue; }
                        catch (FileNotFoundException) { continue; }
                        catch (SevenZipException) { continue; }
                        // 如果StringBuilder length 為0, 代表沒有找到符合的副檔名
                        if (sb.Length <= 0) { continue; }
                        // 設定輸出檔名
                        destFilePathAndName = fileName.Replace(":\\", "%%%");
                        destFilePathAndName = destFilePathAndName.Replace("\\", "%%");
                        destFilePathAndName += pattern + ".txt";
                        if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }

                        File.AppendAllText(Path.Combine(destFilePath, destFilePathAndName), sb.ToString());

                        // 此檔案代表有match的清單
                        if (isNeedToCopy == false)
                        {
                            File.AppendAllLines(needToCopyFileName, new string[] { fileName });
                            isNeedToCopy = true;
                        }
                    }
                }
            }

            // release parameter
            needToCopyFileName = null;
            destFilePath = null;
            sourceFileName = null;
            destFilePathAndName = null;
        }

        public void buildSummaryResult(string workingDirectory, string[] compressFileExtensions, string[] nonCompressFileExtensions, string[] mappingFileExtensions, bool needOutputCopyList = false)
        {
            if (Directory.Exists(workingDirectory) == false) { return; }

            string sourceFileName = "";
            string destFilePathAndName = "";
            string searchTerm = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder copyListSb = new StringBuilder();

            // 0. 不管有無要輸出copy list, 均先刪除該檔案
            destFilePathAndName = workingDirectory + DefinedParamter.copyListFileName;
            if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }

            // 1. 整理非壓縮檔部份的summary
            string[] soruceExtensions = nonCompressFileExtensions;
            foreach (string extension in soruceExtensions)
            {
                // 找出符合的檔案清單
                searchTerm = string.Format("{0}{1}", extension, ".txt");
                destFilePathAndName = Path.Combine(workingDirectory, string.Format("{0}{1}{2}{3}.txt", Environment.MachineName, DefinedFileType.SummaryName, extension, extension.Replace(".", "_")));
                sb.Clear();
                copyListSb.Clear();
                foreach (var fi in Directory.EnumerateFiles(workingDirectory).Where(m => m.EndsWith(searchTerm)))
                {
                    if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }

                    sourceFileName = fi.ToString();
                    // 取得檔案內所有Line Data
                    string[] fileList = File.ReadAllLines(fi.ToString());
                    foreach (string lineData in File.ReadAllLines(fi.ToString()))
                    {
                        sb.Append("1").Append('\t').Append(lineData).Append(System.Environment.NewLine);
                        // for copy list
                        copyListSb.AppendLine(lineData);
                    }
                    File.AppendAllText(Path.Combine(workingDirectory, destFilePathAndName), sb.ToString());
                    // for copy list
                    File.AppendAllText(Path.Combine(workingDirectory, DefinedParamter.copyListFileName), copyListSb.ToString());
                }
            }

            // 2. 整理壓縮檔部份的summary
            // 設定處理compress file的目錄
            string compressDetailRawFilePath = Path.Combine(Path.GetDirectoryName(workingDirectory), DefinedParamter.compressDetailDirectoryName + Path.DirectorySeparatorChar);
            // 如果目錄不存在, 建立目錄, 讓程式可以執行到 3. 整理FinalSummary
            if (Directory.Exists(compressDetailRawFilePath) == false) { Directory.CreateDirectory(compressDetailRawFilePath); }

            // 要處理的壓副檔名
            soruceExtensions = compressFileExtensions;
            // 依match的附檔名, 開始parsing 內容
            foreach (string extension in soruceExtensions)
            {
                foreach (string pattern in mappingFileExtensions)
                {
                    // 找出符合的檔案清單
                    searchTerm = string.Format("{0}{1}{2}", extension, pattern, ".txt");
                    destFilePathAndName = Path.Combine(workingDirectory, string.Format("{0}{1}{2}{3}.txt", Environment.MachineName, DefinedFileType.SummaryName, extension, pattern.Replace(".", "_")));
                    sb.Clear();
                    copyListSb.Clear();
                    foreach (var fi in Directory.EnumerateFiles(compressDetailRawFilePath).Where(m => m.EndsWith(searchTerm)))
                    {
                        if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }

                        sourceFileName = fi.ToString();
                        // 取得檔案內所有Line Data
                        string replaceFileName = sourceFileName.Replace("%%%", ":\\");
                        replaceFileName = replaceFileName.Replace("%%", "\\");
                        replaceFileName = replaceFileName.Replace(extension + pattern + ".txt", extension);
                        replaceFileName = replaceFileName.Replace(compressDetailRawFilePath, "");
                        string[] fileList = File.ReadAllLines(fi.ToString());

                        sb.Append(fileList.Length.ToString()).Append('\t').Append(replaceFileName).Append(System.Environment.NewLine);
                        // for copy list
                        copyListSb.AppendLine(replaceFileName);
                        File.AppendAllText(Path.Combine(workingDirectory, destFilePathAndName), sb.ToString());
                        // for copy list
                        File.AppendAllText(Path.Combine(workingDirectory, DefinedParamter.copyListFileName), copyListSb.ToString());
                    }
                }
            }

            // 3. 整理Final Summary 
            // 找出符合的summary檔案
            searchTerm = Environment.MachineName + DefinedFileType.SummaryName + ".";
            destFilePathAndName = Path.Combine(workingDirectory, string.Format("{0}{1}.txt", Environment.MachineName, DefinedFileType.ReportName));
            sb.Clear();
            if (File.Exists(destFilePathAndName) == true)
            {
                File.Delete(destFilePathAndName);
            }
            foreach (var fi in Directory.EnumerateFiles(workingDirectory).Where(m => m.StartsWith(workingDirectory + searchTerm)))
            {
                string tmpFileName = fi.ToString();
                string extensionName = "";
                string includeExtensionName = "";

                // 取出Extension
                tmpFileName = tmpFileName.Replace(workingDirectory + searchTerm, "");
                tmpFileName = tmpFileName.Replace(".txt", "");
                extensionName = tmpFileName.Substring(0, tmpFileName.IndexOf("_"));

                // 取出IncludeExtension
                includeExtensionName = tmpFileName.Substring(tmpFileName.LastIndexOf("_") + 1, tmpFileName.Length - tmpFileName.LastIndexOf("_") - 1);

                // 判斷 extensionName 與 includeExtensionName 是否相同
                // 相同 : 非壓縮檔
                // 不同 : 壓縮檔, 加入Compress字串
                if (extensionName.Equals(includeExtensionName) == false)
                {
                    extensionName = "comress_" + extensionName;
                }

                // 取得檔案內所有Line Data
                string[] fileList = File.ReadAllLines(fi.ToString());
                sb.Append(Environment.MachineName).Append('\t')
                    .Append(extensionName).Append('\t')
                    .Append(includeExtensionName).Append('\t')
                    .Append(fileList.Length.ToString()).AppendLine();
            }
            File.AppendAllText(Path.Combine(workingDirectory, destFilePathAndName), sb.ToString());

            // 判斷是否需保留copy list
            if (needOutputCopyList == false)
            {
                if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }
            }

            // release parameter
            sb = null;
            copyListSb = null;
            soruceExtensions = null;
            sourceFileName = null;
            destFilePathAndName = null;
            searchTerm = null;
            compressDetailRawFilePath = null;
        }

        public void buildLocalScannedExcel(string workingDirectory)
        {
            if (Directory.Exists(workingDirectory) == false) { return; }

            DataSet sheetsData = new DataSet("SheetsData");
            // 1. 設定處理目錄
            DataTable summaryDt = new DataTable("Summary");

            summaryDt.Columns.Add("Extension");
            summaryDt.Columns.Add("IncludeExtension");
            summaryDt.Columns.Add("Amount");

            // 2. 整理summary內容
            // 找出符合的summary檔案
            string searchTerm = Environment.MachineName + DefinedFileType.SummaryName + ".";
            foreach (var fi in Directory.EnumerateFiles(workingDirectory).Where(m => m.StartsWith(workingDirectory + searchTerm)))
            {
                string tmpFileName = fi.ToString();
                string extensionName = "";
                string includeExtensionName = "";

                DataRow summaryDr = summaryDt.NewRow();
                // 取出Extension
                tmpFileName = tmpFileName.Replace(workingDirectory + searchTerm, "");
                tmpFileName = tmpFileName.Replace(".txt", "");
                extensionName = tmpFileName.Substring(0, tmpFileName.IndexOf("_"));
                // 取出IncludeExtension
                includeExtensionName = tmpFileName.Substring(tmpFileName.LastIndexOf("_") + 1, tmpFileName.Length - tmpFileName.LastIndexOf("_") - 1);

                // 判斷 extensionName 與 includeExtensionName 是否相同
                // 相同 : 非壓縮檔
                // 不同 : 壓縮檔, 加入Compress字串
                if (extensionName.Equals(includeExtensionName) == false)
                {
                    extensionName = "comress_" + extensionName;
                }

                summaryDr["Extension"] = extensionName;

                summaryDr["IncludeExtension"] = includeExtensionName;

                // 取得檔案內所有Line Data
                string[] fileList = File.ReadAllLines(fi.ToString());
                DataTable tempDt = new DataTable(extensionName + "_Include_" + includeExtensionName);
                tempDt.Columns.Add("Amount");
                tempDt.Columns.Add("FileName");
                foreach (string lineDate in fileList)
                {
                    DataRow dr = tempDt.NewRow();
                    string[] tmpString = lineDate.Split('\t');
                    dr["Amount"] = tmpString[0];
                    dr["FileName"] = tmpString[1];
                    tempDt.Rows.Add(dr);
                }
                sheetsData.Tables.Add(tempDt);
                summaryDr["Amount"] = fileList.Length;
                summaryDt.Rows.Add(summaryDr);
            }
            sheetsData.Tables.Add(summaryDt);

            // 3. 輸出excel
            string outputFilePathAndName = workingDirectory + Environment.MachineName + ".summary_local.xlsx";

            if (File.Exists(outputFilePathAndName) == true)
            {
                // 如果無法刪除, 就變更檔名
                try
                {
                    File.Delete(outputFilePathAndName);
                }
                catch (IOException)
                {
                    outputFilePathAndName = workingDirectory + "summary_local_" + new Random(DateTime.Now.Millisecond).Next().ToString() + ".xlsx";
                }
            }

            MvExcelReport.writeExcelByDataSet(sheetsData, outputFilePathAndName);

            // release parameter
            sheetsData.Dispose();
            summaryDt.Dispose();

            sheetsData = null;
            summaryDt = null;
            searchTerm = null;
            outputFilePathAndName = null;
        }


        public void buildSummaryExcelReport(string workingDirectory, string outputDirectory)
        {
            string searchTerm = "PC-0";

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable("SummaryReport");
            dt.Columns.Add("MachineName");
            dt.Columns.Add("ExtensionType");
            dt.Columns.Add("IncludeExtension");
            dt.Columns.Add("Amount");

            foreach (var fi in Directory.EnumerateDirectories(workingDirectory).Where(m => m.StartsWith(workingDirectory + searchTerm)))
            {
                string machineName = fi.Replace(workingDirectory, "");
                string reportFileName = string.Format("{0}.report.txt", Path.Combine(fi.ToString(), machineName));

                if (File.Exists(reportFileName) == false)
                {
                    continue;
                }

                string[] fileList = File.ReadAllLines(reportFileName);

                foreach (string lineData in fileList)
                {
                    DataRow dr = dt.NewRow();
                    string[] tempData = lineData.Split('\t');

                    dr["MachineName"] = tempData[0];
                    dr["ExtensionType"] = tempData[1];
                    dr["IncludeExtension"] = tempData[2];
                    dr["Amount"] = tempData[3];
                    dt.Rows.Add(dr);
                }
            }

            // 產生Summary excel
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;

            // 建立excel 文件
            xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            // 建立Sheets and name
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);

            xlWorkBook.Worksheets[1].Name = dt.TableName;

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[dt.TableName];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;

            // fill content to sheet name "Detail"
            MvExcelReport.fastFillDataIntoExcel(dt, xlWorkSheet, "A1", true);

            // save file to xlsx
            xlApp.DisplayAlerts = false;

            string destFilePathAndName = outputDirectory + "SummaryReport.xlsx";
            try
            {
                xlWorkBook.SaveAs(destFilePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + destFilePathAndName);
            }
            finally
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;

            sb = null;
            dt.Dispose();
            dt = null;
        }

        public void buildCopyScript(string workingDirectory)
        {
            string sourceFilePathAndName = Path.Combine(workingDirectory, DefinedParamter.copyListFileName);
            string destFilePathAndName = Path.Combine(workingDirectory, DefinedParamter.copyScriptFileName);

            if (File.Exists(destFilePathAndName) == true) { File.Delete(destFilePathAndName); }

            StringBuilder sb = new StringBuilder();
            // 整理script 語法
            sb.Append("$sourceFilePathAndName = \"").Append(sourceFilePathAndName).Append("\"").AppendLine()
                .Append("$destinationDirectory = \"").Append(Path.Combine(workingDirectory, DefinedParamter.copyTargetDirectoryName)).AppendLine("\"").AppendLine()
                .AppendLine("if((Test-Path -path $sourceFilePathAndName) -eq $false)")
                .AppendLine("{")
                .AppendLine('\t' + "return")
                .AppendLine("}").AppendLine()
                .AppendLine("if((Test-Path -path $destinationDirectory) -eq $false)")
                .AppendLine("{")
                .AppendLine('\t' + "New-Item -Path $destinationDirectory -ItemType \"directory\"")
                .AppendLine("}").AppendLine()
                .AppendLine("$fileContentList = Get-Content $sourceFilePathAndName")
                .AppendLine("foreach($item in $fileContentList)")
                .AppendLine("{")
                .AppendLine('\t' + "Copy-Item $item -Destination $destinationDirectory")
                .AppendLine("}").AppendLine();

            // 輸出script
            File.AppendAllText(destFilePathAndName, sb.ToString());
        }
    }
}
