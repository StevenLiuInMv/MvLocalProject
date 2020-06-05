using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvSharedLib.Controller
{
    public class MvLogger
    {
        public static void write(string format, params object[] arg)
        {
            write(string.Format(format, arg));
        }

        public static void write(string message)
        {
            string filePath = Directory.GetCurrentDirectory();
            string filename = filePath +
                string.Format("\\{0:yyyyMM}\\{0:yyyy-MM-dd}.txt", DateTime.Now);

            FileInfo finfo = new FileInfo(filename);
            if (finfo.Directory.Exists == false) { finfo.Directory.Create(); }
            string writeString = string.Format("{0:yyyy/MM/dd HH:mm:ss} {1}",
                DateTime.Now, message) + Environment.NewLine;
            File.AppendAllText(filename, writeString, Encoding.Unicode);
        }
    }
}
