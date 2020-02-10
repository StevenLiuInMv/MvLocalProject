using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvLocalProject
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MvLocalProject.Viewer.frmLogin());

            //var start = new MvLocalProject.Viewer.frmLogin();
            //var start = new MvLocalProject.Viewer.frmMainDev();
            //var start = new Viewer.frmItMxMail();

            //var start = new Viewer.frmItCisco();
            //var start = new MvLocalProject.Viewer.frmTestParser();
            //var start = new MvLocalProject.Viewer.frmTestMisGetBom();
            //var start = new MvLocalProject.Viewer.frmBomToMocCompare();

            //var start = new Viewer.frmBomCompareDev();
            //var start = new Viewer.frmMoc();
            //var start = new Viewer.frmBom();

            //var start = new Viewer.frmErpCreatePR();
            //var start = new Viewer.frmReportFinance();
            //var start = new Viewer.frmTestMvDao();

            //2018/10/05 using
            //var start = new Viewer.frmMcBatchJob();
            //var start = new Viewer.frmTestParser();
            var start = new Viewer.frmLogin();

            //var start = new Viewer.frmDiskScan();
            //var start = new Viewer.frmItCisco();
            //var start = new Viewer.frmItMxMail();
            start.FormClosed += WindowClosed;
            start.Show();
            Application.Run();
        }

        static void WindowClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
            else Application.OpenForms[0].FormClosed += WindowClosed;
        }
    }
}
