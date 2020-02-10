using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvLocalProject.Controller
{
    public class Utility
    {
        public static void writeErrorMessage(string message)
        {
            try
            {
                using (var fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/errormessage.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var fw = new StreamWriter(fs))
                    {
                        fw.Write(message);
                        fw.Flush(); 
                    }
                }
            }
            catch (IOException ie)
            {
                Mailer.MailContent mc = new Mailer.MailContent();
                // make mail content
                mc.Subject = "[Error] write the message error";
                mc.Body = ie.Message;
                Mailer.sendMail(mc);
            }
        }

        public static bool writeDataTableToFile(string filePathAndName, DataTable dt, bool includeHead)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(filePathAndName)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePathAndName));
                }
                using (var fs = new FileStream(filePathAndName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var fw = new StreamWriter(fs))
                    {
                        if (includeHead == true)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                fw.Write(col.ToString() + "\t"); 
                            }
                            fw.WriteLine();
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                fw.Write((string)item + "\t");
                            }
                            fw.WriteLine();
                        }
                    }
                }
            }
            catch (IOException ie)
            {
                Mailer.MailContent mc = new Mailer.MailContent();
                // make mail content
                mc.Subject = "[Error] write the message error";
                mc.Body = ie.Message;
                Mailer.sendMail(mc);
            }

            return true;
        }

        public static bool writeDataTableToCsvFile(string filePathAndName, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(filePathAndName, sb.ToString());

            return true;
        }

        public static bool writeToFile(string filePathAndName, string[] dataResult)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(filePathAndName)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePathAndName));
                }
                using (var fs = new FileStream(filePathAndName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var fw = new StreamWriter(fs))
                    {
                        foreach (string row in dataResult)
                        {
                            fw.WriteLine(row);
                        }
                    }
                }
            }
            catch (IOException ie)
            {
                Mailer.MailContent mc = new Mailer.MailContent();
                // make mail content
                mc.Subject = "[Error] write the message error";
                mc.Body = ie.Message;
                Mailer.sendMail(mc);
            }
            return true;
        }


        public static void clearTextBox(ref TextBox[] textBoxArray)
        {
            foreach(TextBox box in textBoxArray) {
                box.Clear();
            }
        }

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

    public class AsyncTemplate
    {
        public static Action OnInvokeStarting { get; set; }

        public static Action OnInvokeEnding { get; set; }

        public static void DoWorkAsync(Action beginAction, Action endAction, Action<Exception> errorAction)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (o) =>
                {
                    try
                    {
                        beginAction();
                        endAction();
                    }
                    catch (Exception ex)
                    {
                        errorAction(ex);
                        return;
                    }
                    finally
                    {
                        if (OnInvokeEnding != null)
                        {
                            OnInvokeEnding();
                        }
                    }
                })
            , null);

            if (OnInvokeStarting != null)
            {
                OnInvokeStarting();
            }
        }

        public static void DoWorkAsync<TResult>(Func<TResult> beginAction, Action<TResult> endAction, Action<Exception> errorAction)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (o) =>
                {
                    TResult result = default(TResult);

                    try
                    {
                        result = beginAction();

                        endAction(result);
                    }
                    catch (Exception ex)
                    {
                        errorAction(ex);
                        return;
                    }
                    finally
                    {
                        if (OnInvokeEnding != null)
                        {
                            OnInvokeEnding();
                        }
                    }
                })
            , null);

            if (OnInvokeStarting != null)
            {
                OnInvokeStarting();
            }
        }
    }
}
