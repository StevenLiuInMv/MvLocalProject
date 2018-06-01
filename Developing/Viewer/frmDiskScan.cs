using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MvLocalProject.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmDiskScan : Form
    {
        public frmDiskScan()
        {
            InitializeComponent();
        }

        private void btnCheckDisk_Click(object sender, EventArgs e)
        {
            DriveInfo[] ListDrivesInfo = DriveInfo.GetDrives();
            StringBuilder sb = new StringBuilder();

            try
            {
                foreach (DriveInfo vListDrivesInfo in ListDrivesInfo)
                {
                    //使用IsReady屬性判斷裝置是否就緒
                    if (vListDrivesInfo.IsReady)
                    {
                        sb.Append("磁碟代號:" + vListDrivesInfo.Name).Append(Environment.NewLine)
                            .Append("磁碟標籤:" + vListDrivesInfo.VolumeLabel).Append(Environment.NewLine)
                            .Append("磁碟類型:" + vListDrivesInfo.DriveType.ToString()).Append(Environment.NewLine)
                            .Append("磁碟格式:" + vListDrivesInfo.DriveFormat).Append(Environment.NewLine)
                            .Append("磁碟大小:" + vListDrivesInfo.TotalSize.ToString()).Append(Environment.NewLine)
                            .Append("剩餘空間:" + vListDrivesInfo.AvailableFreeSpace.ToString()).Append(Environment.NewLine)
                            .Append("總剩餘空間(含磁碟配碟):" + vListDrivesInfo.TotalFreeSpace.ToString()).Append(Environment.NewLine)
                            .Append("===================================").Append(Environment.NewLine);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            rtb_ListDriveInfo.Text = sb.ToString();
        }

        private void btnScanFiles_Click(object sender, EventArgs e)
        {
            DateTime startTime = System.DateTime.Now;
            DirectoryInfo diTop = new DirectoryInfo(@"C:\");
            string extension = "xlsx";
            DataTable dt = null;
            
            dt = FileScanner.scanFilesByExtension(diTop, extension);

            DateTime endTime = System.DateTime.Now;
            TimeSpan ts = endTime - startTime;

            Console.WriteLine("Total : {0}", string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds));

            string filePathAndName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "test2.txt";
            bool result = Utility.writeDataTableToFile(filePathAndName, dt, true);
            if(result == true)
            {
                MessageBox.Show("done");
            } else
            {
                MessageBox.Show("error");
            }
        }

        private void btnScanFilesForAllDisk_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string[] extensions = new string[] { ".txt", ".xlsx", ".xls", ".zip", ".7z", ".rar" };
            string filePathAndName = "";
            // 1. 依extension找出符合的檔案
            foreach (string extension in extensions)
            {
                filePathAndName = System.IO.Path.GetTempPath() + Path.DirectorySeparatorChar + Environment.MachineName + ".raw" + extension + ".txt";
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");

            }
        }

        private void btnScanFilesForAllDisk_Click_bak(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // 1. 找出非壓縮檔的extension檔案
            string filePathAndName = System.IO.Path.GetTempPath() + Path.DirectorySeparatorChar + Environment.MachineName + ".normal.txt";
            //string filePathAndName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + Environment.MachineName + ".normal.txt";


            //string[] extensions = { ".cpp", ".h", ".zip", ".rar", ".7z" };
            string[] extensions = new string[] { ".txt", ".xlsx", ".xls" };

            if (File.Exists(filePathAndName) == true)
            {
                File.Delete(filePathAndName);
            }
            
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

                    if(di.DriveType == DriveType.Fixed)
                    {

                        foreach (string extension in extensions)
                        {
                            string diskName = di.Name;
                            var fileLists = FileScanner.searchAccessibleFiles(diskName, extension , false);

                            bool result = Utility.writeToFile(filePathAndName, fileLists.ToArray<string>());
                        }
                    }
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");



            // 2. 找出所有zip壓縮檔的清單
            watch.Restart();
            filePathAndName = System.IO.Path.GetTempPath() + Path.DirectorySeparatorChar + Environment.MachineName + ".compress.zip.txt";
            extensions = new string[] { ".zip" };

            if (File.Exists(filePathAndName) == true)
            {
                File.Delete(filePathAndName);
            }

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

                        foreach (string extension in extensions)
                        {
                            // 取得file 清單
                            var fileLists = FileScanner.searchAccessibleFiles(di.Name, extension, false);
                            // 將清單資料寫入檔案
                            bool result = Utility.writeToFile(filePathAndName, fileLists.ToArray<string>());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");

            // 3. 找出所有壓縮檔的清單(不含zip)
            watch.Restart();
            filePathAndName = System.IO.Path.GetTempPath() + Path.DirectorySeparatorChar + Environment.MachineName + ".compress.other.txt";
            extensions = new string[] { ".7z", "*.rar" };

            if (File.Exists(filePathAndName) == true)
            {
                File.Delete(filePathAndName);
            }

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

                        foreach (string extension in extensions)
                        {
                            // 取得file 清單
                            var fileLists = FileScanner.searchAccessibleFiles(di.Name, extension, false);
                            // 將清單資料寫入檔案
                            bool result = Utility.writeToFile(filePathAndName, fileLists.ToArray<string>());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString() + " milliseconds, end function");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            DirectoryInfo diInfo = new DirectoryInfo(@"c:\");
            string extension = "txt";

            try
            {
                foreach (var fi in diInfo.EnumerateFiles("*." + extension))
                {
                    Console.WriteLine("{0} {1}", fi.FullName, fi.Length.ToString());
                }

                foreach (var di in diInfo.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*." + extension, SearchOption.AllDirectories))
                        {
                            Console.WriteLine("{0} {1}", fi.FullName, fi.Length.ToString());
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        //Console.WriteLine("UnAuthDir: {0}", UnAuthFile.Message);
                    }
                }
            }
            catch (DirectoryNotFoundException DirNotFound)
            {
                Console.WriteLine("DirNotFound: {0}", DirNotFound.Message);
            }
            catch (UnauthorizedAccessException UnAuthDir)
            {
                Console.WriteLine("UnAuthDir: {0}", UnAuthDir.Message);
            }
            catch (PathTooLongException LongPath)
            {
                Console.WriteLine("{0}", LongPath.Message);
            }
            Console.WriteLine("{0} ", "fisihed");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start function");
            //string[] extensions = { ".cpp", ".h", ".zip", ".rar", ".7z" };
            string[] extensions = { "txt", "xls" };
            DriveInfo[] drivers = DriveInfo.GetDrives();

            string filePathAndName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "test4.txt";

            if (File.Exists(filePathAndName) == true)
            {
                File.Delete(filePathAndName);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileSize");


            foreach (DriveInfo di in drivers)
            {
                //使用IsReady屬性判斷裝置是否就緒
                if (di.IsReady == false)
                {
                    continue;
                }

                if (di.DriveType == DriveType.Fixed)
                {
                    foreach (string extension in extensions)
                    {
                        dt = FileScanner.searchAccessibleFiles(new DirectoryInfo(di.Name), extension);

                        bool result = Utility.writeDataTableToFile(filePathAndName, dt, true);
                    }
                }
            }
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " end function");
        }

        private void frmDiskScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
