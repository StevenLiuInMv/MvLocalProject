using ICSharpCode.SharpZipLib.Zip;
using SevenZip;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using MvLocalProject.Controller;
using System.Runtime.InteropServices;

namespace MvLocalProject.Viewer
{
    public partial class frmTestExtract : Form
    {
        public frmTestExtract()
        {
            InitializeComponent();
        }

        private void btnTestList_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //string sourceFileName = @"D:\temp\PC-00268.compress.zip.txt";
            string sourceFileName = @"D:\temp\PC-00268.compress.other.txt";

            if (File.Exists(sourceFileName) == false)
            {
                return;
            }

            string destFilePath = Path.Combine(Path.GetDirectoryName(sourceFileName), "compress_detail" + Path.DirectorySeparatorChar);
            string destFilePathAndName = "";

            StringBuilder sb = new StringBuilder();

            if (File.Exists(destFilePathAndName) == true)
            {
                File.Delete(destFilePathAndName);
            }

            if (Directory.Exists(destFilePath) == false)
            {
                Directory.CreateDirectory(destFilePath);
            }

            // 移除所有detail檔
            DirectoryInfo di = new DirectoryInfo(destFilePath);
            FileInfo[] files = di.GetFiles("*.txt")
                                 .Where(p => p.Extension == ".txt").ToArray();
            foreach (FileInfo file in files)
            { 
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
            }

            // 取得檔案內所有壓縮檔清單
            string[] fileList = File.ReadAllLines(sourceFileName);

            foreach (string fileName in fileList)
            {
                sb.Clear();
                try
                {
                    using (ZipFile zf = new ZipFile(fileName))
                    {
                        foreach (ZipEntry ze in zf)
                        {
                            sb.AppendLine(ze.Name);
                        }
                    }
                }
                catch (DirectoryNotFoundException) { }
                catch (ZipException) { }
                catch (FileNotFoundException) { }
                destFilePathAndName = fileName.Replace(":\\","%%%");
                destFilePathAndName = destFilePathAndName.Replace("\\", "%%");
                destFilePathAndName += ".detail.txt";
                File.AppendAllText(Path.Combine(destFilePath, destFilePathAndName), sb.ToString());
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");

        }

        private void btn7Zip_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            SevenZipBase.SetLibraryPath(@"C:\Program Files (x86)\7-Zip\7z.dll");

            string workingPath = @"D:\" + Environment.MachineName + "\\";

            if (Directory.Exists(workingPath) == false)
            {
                Directory.CreateDirectory(workingPath);
            }

            // 要處理的source
            string[] soruceExtensions = new string[] { ".zip", ".7z", ".rar" };
            // 要比對的副檔名
            string[] matchExtensionPatterns =  new string[] { ".exe" , ".dll"};
            // 需要copy檔案的檔名
            string needToCopyFileName = string.Format(@"{0}{1}.compress.copy.txt", workingPath, Environment.MachineName);

            string sourceFileName = "";

            // 設定處理compress file的目錄
            string destFilePath = Path.Combine(Path.GetDirectoryName(workingPath), "compress_detail" + Path.DirectorySeparatorChar);
            // 移除所有detail.txt檔案
            DirectoryInfo di = new DirectoryInfo(destFilePath);
            FileInfo[] files = di.GetFiles("*.txt")
                                 .Where(p => p.Extension == ".txt").ToArray();
            foreach (FileInfo file in files)
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
            }


            // 依match的附檔名, 開始parsing 內容
            foreach (string extension in soruceExtensions)
            {
                //string sourceFileName = @"D:\temp\PC-00268.source.zip.txt";
                sourceFileName = Path.Combine(workingPath, string.Format("{0}.{1}{2}.txt", Environment.MachineName, "raw", extension));
                if (File.Exists(sourceFileName) == false)
                {
                    continue; 
                }

                string destFilePathAndName = "";


                if (Directory.Exists(destFilePath) == false)
                {
                    Directory.CreateDirectory(destFilePath);
                }

                if (File.Exists(destFilePathAndName) == true)
                {
                    File.Delete(destFilePathAndName);
                }


                // 取得檔案內所有壓縮檔清單
                string[] fileList = File.ReadAllLines(sourceFileName);
                StringBuilder sb = new StringBuilder();
                // 判斷是否需要列入copy清單
                bool isNeedToCopy = false;
                foreach (string pattern in matchExtensionPatterns)
                {
                    isNeedToCopy = false;
                    foreach (string fileName in fileList)
                    {
                        watch.Restart();
                        sb.Clear();
                        try
                        {
                            using (SevenZipExtractor extractor = new SevenZipExtractor(fileName))
                            {
                                foreach (var name in extractor.ArchiveFileNames)
                                {
                                    if (name.ToString().EndsWith(pattern) == true)
                                    {
                                        sb.AppendLine(name.ToString());
                                    }
                                }
                            }
                        }
                        catch (DirectoryNotFoundException) { continue; }
                        catch (FileNotFoundException) { continue; }
                        catch (SevenZipException) { continue; }
                        // 如果StringBuilder length 為0, 代表沒有找到符合的副檔名
                        if (sb.Length <= 0)
                        {
                            continue;
                        }
                        destFilePathAndName = fileName.Replace(":\\", "%%%");
                        destFilePathAndName = destFilePathAndName.Replace("\\", "%%");
                        destFilePathAndName += pattern + ".txt";

                        File.AppendAllText(Path.Combine(destFilePath, destFilePathAndName), sb.ToString());

                        // 此檔案代表有match的清單
                        if (isNeedToCopy == false)
                        {
                            File.AppendAllLines(needToCopyFileName, new string[] { fileName });
                            isNeedToCopy = true;
                        }
                    }
                    watch.Stop();
                    Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");
                }
            }
        }

        private void btnBuildSummaryData_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            SevenZipBase.SetLibraryPath(@"C:\Program Files (x86)\7-Zip\7z.dll");

            string workingPath = @"D:\" + Environment.MachineName + "\\";

            if (Directory.Exists(workingPath) == false)
            {
                Directory.CreateDirectory(workingPath);
            }

            // 1. 設定處理compress file的目錄
            string rawFilePath = Path.Combine(Path.GetDirectoryName(workingPath), "compress_detail" + Path.DirectorySeparatorChar);
            if(Directory.Exists(rawFilePath) == false)
            {
                Directory.CreateDirectory(rawFilePath);
            }

            // 2. 整理壓縮檔部份的summary
            // 要處理的壓副檔名
            string[] soruceExtensions = new string[] { ".zip", ".7z", ".rar" };

            // 要比對壓縮檔內含的副檔名
            string[] matchExtensionPatterns = new string[] { ".exe", ".dll" };

            string sourceFileName = "";
            string destFilePathAndName = "";
            string searchTerm = "";
            // 依match的附檔名, 開始parsing 內容
            StringBuilder sb = new StringBuilder();
            foreach (string extension in soruceExtensions)
            {
                //string sourceFileName = @"D:\temp\PC-00268.source.zip.txt";
                foreach (string pattern in matchExtensionPatterns)
                {
                    // 找出符合的檔案清單
                    searchTerm = string.Format("{0}{1}{2}", extension, pattern, ".txt");
                    destFilePathAndName = Path.Combine(workingPath, string.Format("{0}.{1}{2}{3}.txt", Environment.MachineName, "summary", extension, pattern.Replace(".", "_")));
                    sb.Clear();
                    foreach (var fi in Directory.EnumerateFiles(rawFilePath).Where(m => m.EndsWith(searchTerm)))
                    {
                        if (File.Exists(destFilePathAndName) == true)
                        {
                            File.Delete(destFilePathAndName);
                        }

                        sourceFileName = fi.ToString();
                        // 取得檔案內所有Line Data
                        string replaceFileName = sourceFileName.Replace("%%%", ":\\");
                        replaceFileName = replaceFileName.Replace("%%", "\\");
                        replaceFileName = replaceFileName.Replace(extension + pattern + ".txt", extension);
                        replaceFileName = replaceFileName.Replace(rawFilePath, "");
                        string[] fileList = File.ReadAllLines(fi.ToString());

                        sb.Append(fileList.Length.ToString()).Append('\t').Append(replaceFileName).Append(System.Environment.NewLine);
                        File.AppendAllText(Path.Combine(workingPath, destFilePathAndName), sb.ToString());
                    }
                }
            }

            // 2. 整理非壓縮檔部份的summary
            soruceExtensions = new string[] { ".txt", ".xls", ".xlsx" };
            
            foreach (string extension in soruceExtensions)
            {
                // 找出符合的檔案清單
                searchTerm = string.Format("{0}{1}", extension, ".txt");
                destFilePathAndName = Path.Combine(workingPath, string.Format("{0}.{1}{2}{3}.txt", Environment.MachineName, "summary", extension, extension.Replace(".", "_")));
                sb.Clear();
                foreach (var fi in Directory.EnumerateFiles(workingPath).Where(m => m.EndsWith(searchTerm)))
                {
                    if (File.Exists(destFilePathAndName) == true)
                    {
                        File.Delete(destFilePathAndName);
                    }

                    sourceFileName = fi.ToString();
                    // 取得檔案內所有Line Data
                    string[] fileList = File.ReadAllLines(fi.ToString());
                    foreach (string lineData in File.ReadAllLines(fi.ToString()))
                    {
                        sb.Append("1").Append('\t').Append(lineData).Append(System.Environment.NewLine);
                    }
                    File.AppendAllText(Path.Combine(workingPath, destFilePathAndName), sb.ToString());
                }
            }

            // 3. 整理Final Summary 
            // 找出符合的summary檔案
            searchTerm = Environment.MachineName + ".summary.";
            destFilePathAndName = Path.Combine(workingPath, string.Format("{0}.{1}.txt", Environment.MachineName, "report"));
            sb.Clear();
            if (File.Exists(destFilePathAndName) == true)
            {
                File.Delete(destFilePathAndName);
            }
            foreach (var fi in Directory.EnumerateFiles(workingPath).Where(m => m.StartsWith(workingPath + searchTerm)))
            {
                string tmpFileName = fi.ToString();
                string extensionName = "";
                string includeExtensionName = "";

                // 取出Extension
                tmpFileName = tmpFileName.Replace(workingPath + searchTerm, "");
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
            File.AppendAllText(Path.Combine(workingPath, destFilePathAndName), sb.ToString());
        }

        private void btnGetExcel_Click(object sender, EventArgs e)
        {
            string workingPath = @"D:\" + Environment.MachineName + "\\";
            if (Directory.Exists(workingPath) == false)
            {
                MessageBox.Show("have no directory in " + workingPath);
                return;
            }

            DataSet sheetsData = new DataSet("SheetsData");
            // 1. 設定處理目錄
            DataTable summaryDt = new DataTable("Summary");

            summaryDt.Columns.Add("Extension");
            summaryDt.Columns.Add("IncludeExtension");
            summaryDt.Columns.Add("Amount");

            //sheetsData.Tables.Add(summaryDt);

            // 2. 整理summary內容
            // 副檔名
//            string[] soruceExtensions = new string[] { ".zip", ".7z", ".rar", ".txt", ".xls", ".xlsx" };

            // 找出符合的summary檔案
            string searchTerm = Environment.MachineName + ".summary.";
            foreach (var fi in Directory.EnumerateFiles(workingPath).Where(m => m.StartsWith(workingPath + searchTerm)))
            {
                string tmpFileName = fi.ToString();
                string extensionName = "";
                string includeExtensionName = "";

                DataRow summaryDr = summaryDt.NewRow();
                // 取出Extension
                tmpFileName = tmpFileName.Replace(workingPath + searchTerm, "");
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
            string outputFilePathAndName = workingPath + Environment.MachineName + ".summary.xlsx";

            if (File.Exists(outputFilePathAndName) == true)
            {
                // 如果無法刪除, 就變更檔名
                try
                {
                    File.Delete(outputFilePathAndName);
                }
                catch(IOException)
                {
                    outputFilePathAndName = workingPath + "summary_" + new Random(DateTime.Now.Millisecond).Next().ToString() +".xlsx";
                }
                
            }

            generateResultExcel(sheetsData, outputFilePathAndName);
        }

        private void generateResultExcel(DataSet ds, string filePathAndName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
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

            int sheetCount = 1;
            foreach (DataTable dt in ds.Tables)
            {
                xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                xlWorkBook.Worksheets[sheetCount].Name = dt.TableName;

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[dt.TableName];
                xlWorkSheet.Cells.NumberFormatLocal = "@";
                xlWorkSheet.Cells.Font.Size = 10;

                // fill content to sheet name "Detail"
                MvExcelReport.fastFillDataIntoExcel(dt, xlWorkSheet, "A1", true);
            }

            // save file to xlsx
            xlApp.DisplayAlerts = false;

            try
            {
                xlWorkBook.SaveAs(filePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + filePathAndName);
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
        }
        private void generatBatchFile(DataTable dt, string filePathAndName)
        {

        }

        private void btnGetSummaryExcel_Click(object sender, EventArgs e)
        {
            string workingPath = @"D:\";
            string searchTerm = "PC-0";

            StringBuilder sb = new StringBuilder();

            DataTable dt = new DataTable("SummaryReport");
            dt.Columns.Add("MachineName");
            dt.Columns.Add("ExtensionType");
            dt.Columns.Add("IncludeExtension");
            dt.Columns.Add("Amount");

            foreach (var fi in Directory.EnumerateDirectories(workingPath).Where(m => m.StartsWith(workingPath + searchTerm)))
            {
                string machineName = fi.Replace(workingPath, "");
                string reportFileName = string.Format("{0}.report.txt", Path.Combine(fi.ToString(), machineName));

                if (File.Exists(reportFileName) == false)
                {
                    continue;
                }

                string[] fileList = File.ReadAllLines(reportFileName);
                
                foreach(string lineData in fileList)
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

            string destFilePathAndName = workingPath + "SummaryReport.xlsx";
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
        }

        private void frmTestExtract_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
