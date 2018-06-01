using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;

namespace MvLocalProject.Controller
{
    public class FileScanner
    {
        public static DataTable scanFilesByExtension(DirectoryInfo diInfo, string extension)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileSize");

            try
            {
                DataRow dr = null;
                foreach (var fi in diInfo.EnumerateFiles("*." + extension))
                {
                    dr = dt.NewRow();
                    dr["FileName"] = fi.FullName;
                    dr["FileSize"] = fi.Length.ToString();
                    dt.Rows.Add(dr);
                }


                foreach (var di in diInfo.EnumerateDirectories("*", SearchOption.AllDirectories))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*." + extension))
                        {
                            dr = dt.NewRow();
                            dr["FileName"] = fi.FullName;
                            dr["FileSize"] = fi.Length.ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthFile)
                    {
                        Console.WriteLine("UnAuthDir: {0}", UnAuthFile.Message);
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
            return dt;
        }

        public static DataTable searchAccessibleFiles(DataTable dt, DirectoryInfo diInfo, string extension)
        {

            DataRow dr = null;
            foreach (var fi in diInfo.EnumerateFiles("*." + extension))
            {
                dr = dt.NewRow();
                dr["FileName"] = fi.FullName;
                dr["FileSize"] = fi.Length.ToString();
                dt.Rows.Add(dr);
            }

            foreach (var di in diInfo.EnumerateDirectories("*"))
            {
                try
                {
                    dt.Merge(searchAccessibleFiles(dt, di, extension));
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (PathTooLongException)
                {
                }
                catch (DirectoryNotFoundException)
                {
                }
            }
            return dt;
        }
        public static DataTable searchAccessibleFiles(DirectoryInfo diInfo, string extension)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileSize");

            DataRow dr = null;
            foreach (var fi in diInfo.EnumerateFiles("*." + extension))
            {
                dr = dt.NewRow();
                dr["FileName"] = fi.FullName;
                dr["FileSize"] = fi.Length.ToString();
                dt.Rows.Add(dr);
            }

            foreach (var di in diInfo.EnumerateDirectories("*"))
            {
                try
                {
                    dt.Merge(searchAccessibleFiles(di, extension));
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (PathTooLongException)
                {
                }
                catch (DirectoryNotFoundException)
                {
                }
            }

            return dt;
        }


        /// <summary>
        /// 取得root路徑下的所有檔案
        /// </summary>
        /// <param name="root"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IEnumerable<string> searchAccessibleFiles(string root, string searchTerm, bool includeFileSize = false)
        {
            var files = new List<string>();

            foreach (var fi in Directory.EnumerateFiles(root).Where(m => m.EndsWith(searchTerm)))
            {
                if(includeFileSize == true)
                {
                    files.Add(fi + "\t" + new FileInfo(fi).Length.ToString());
                } else
                {
                    files.Add(fi);
                }
                
            }
            foreach (var subDir in Directory.EnumerateDirectories(root))
            {
                try
                {
                    files.AddRange(searchAccessibleFiles(subDir, searchTerm, includeFileSize));
                }
                catch (UnauthorizedAccessException)
                {
                    // continue search files by sub directories
                }
            }

            return files;
        }
    }
}
