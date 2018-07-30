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
            //var start = new MvLocalProject.Viewer.frmBomCompareDev();
            //var start = new MvLocalProject.Viewer.frmBom();
            //var start = new MvLocalProject.Viewer.frmMoc();

            //var start = new MvLocalProject.Viewer.frmMainDev();
            //var start = new Viewer.frmItCisco();
            //var start = new Viewer.frmItMxMail();
            //var start = new Viewer.frmTestMvDao();
            var start = new MvLocalProject.Viewer.frmTestParser();
            //var start = new MvLocalProject.Viewer.frmTestMisGetBom();
            //var start = new MvLocalProject.Viewer.frmBomToMocCompare();

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
