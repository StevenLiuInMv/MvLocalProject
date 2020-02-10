using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Testing
{
    public class Utility
    {

        /// <summary>
        /// 取得目錄容量
        /// </summary>
        /// <param name="DirRoute"></param>
        /// <returns></returns>
        public static long CalculateDirectorySize(string DirRoute)
        {
            try
            {
                Type tp = Type.GetTypeFromProgID("Scripting.FileSystemObject");
                object fso = Activator.CreateInstance(tp);
                object fd = tp.InvokeMember("GetFolder", BindingFlags.InvokeMethod, null, fso, new object[] { DirRoute });
                long ret = Convert.ToInt64(tp.InvokeMember("Size", BindingFlags.GetProperty, null, fd, null));
                Marshal.ReleaseComObject(fso);
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Show 目錄的Size資訊
        /// </summary>
        /// <param name="diskPath"></param>
        /// <returns></returns>
        public static string ShowSubDirectoriesSizeInfo(string diskPath)
        {
            long fileSize = 0;
            StringBuilder sb = new StringBuilder();

            if (diskPath.EndsWith("\\") == false)
            {
                diskPath += "\\";
            }

            if (diskPath.StartsWith("C:") == true) { return "don't support system disk"; }

            string[] dirs = Directory.GetDirectories(diskPath);
            foreach (var item in dirs)
            {
                fileSize = 0;
                fileSize = Utility.CalculateDirectorySize(item);
                sb.AppendLine(item.ToString() + " " + fileSize);
            }

            return sb.ToString();
        }
    }
}