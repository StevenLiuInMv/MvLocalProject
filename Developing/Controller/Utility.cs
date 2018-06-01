using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
