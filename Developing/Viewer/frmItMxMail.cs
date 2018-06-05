using System;
using System.Data;
using System.Windows.Forms;
using MvLocalProject.Controller;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.Text;
using System.IO;

namespace MvLocalProject.Viewer
{
    public partial class frmItMxMail : Form
    {
        public frmItMxMail()
        {
            InitializeComponent();
        }

        DataTable _majorDt = null;

        private void sbQueryUser_Click(object sender, EventArgs e)
        {

            if (deStart.Text.Length == 0 || deEnd.Text.Length == 0)
            {
                MessageBox.Show("請先選取日期區間");
                return;
            }

            // 判斷start date 不能比end date晚
            DateTime dStart = DateTime.Parse(deStart.Text);
            DateTime dEnd = DateTime.Parse(deEnd.Text);
            if (DateTime.Compare(dStart,dEnd) > 0)
            {
                MessageBox.Show("起始日不可大於結束日");
                return;
            }

            // 判斷區間不要超過一個月
            TimeSpan ts = dEnd - dStart;
            int differenceInDays = ts.Days;
            if(differenceInDays > 31)
            {
                MessageBox.Show("日期區間請不要超過1個月");
                return;
            }

            _majorDt = null;
            _majorDt = MvDbDao.collectData_ItMxMail(deStart.Text, deEnd.Text);
            gridControl1.DataSource = _majorDt;
            hideGridViewForAll(ref gridView1);
            showGridViewForQuery(ref gridView1);
            gridView1.OptionsBehavior.Editable = false;
            gridView1.RefreshData();
        }
        private void frmItMxMail_Load(object sender, EventArgs e)
        {
            deStart.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deEnd.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deStart.DateTime = DateTime.Today;
            deEnd.DateTime = DateTime.Today;

            //exportUser / exportLdap / exportAD註冊同一個Click事件
            this.sbExportUser.Click += new EventHandler(buttonClickAction);
            this.sbExportLdap.Click += new EventHandler(buttonClickAction);
            this.sbExportAD.Click += new EventHandler(buttonClickAction);
        }

        private void buttonClickAction(object sender, EventArgs e)
        {

            // 不是button事件就離開
            if (!(sender is SimpleButton))
            { return; }

            if (_majorDt == null)
            {
                MessageBox.Show("資料表為空值, 請先執行查詢後再匯出");
                return;
            }
            hideGridViewForAll(ref gridView1);
            showGridViewForUser(ref gridView1);
            gridView1.RefreshData();

            // 往下跑, 目前只有存檔功能, 用tmpTreeList看有沒有指定來判斷
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            SimpleButton but = (SimpleButton)sender;
            if (but.Name == "sbExportUser" || but.Name == "sbExportLdap")
            {
                saveFileDialog1.Filter = "CSV (逗號分隔) |*.csv";
                saveFileDialog1.Title = "Save an CSV File";
            }
            else if (but.Name == "sbExportAD")
            {
                saveFileDialog1.Filter = "批次檔案 |*.bat";
                saveFileDialog1.Title = "Save an Batch File";
            }
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName == "") { return; }
            string filePathAndName = saveFileDialog1.FileName;

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            // 介面上只顯示使用者的column, 其餘不顯示
            hideGridViewForAll(ref gridView1);
            if (but.Name == "sbExportUser")
            {
                showGridViewForUser(ref gridView1);
            }
            else if (but.Name == "sbExportLdap")
            {
                showGridViewForLdap(ref gridView1);
            }
            else if (but.Name == "sbExportAD")
            {
                showGridViewForAD(ref gridView1);
            }

            gridView1.RefreshData();
            SplashScreenManager.CloseForm(false);
            // 開始後製csv檔案
            try
            {
                if (but.Name == "sbExportUser" || but.Name == "sbExportLdap")
                {
                    gridView1.ExportToCsv(filePathAndName);
                }
                else if (but.Name == "sbExportAD")
                {
                    exportToBatchAd(_majorDt, filePathAndName);
                }
            }
            catch (System.IO.IOException)
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(string.Format("please close the file first, then do save file again.{0}{1}", Environment.NewLine, filePathAndName));
                return;
            }
            //Close Wait Form
            SplashScreenManager.CloseForm(false);
            MessageBox.Show(string.Format("Save as path {0}{1}", Environment.NewLine, filePathAndName));

        }

        private void showGridViewForQuery(ref GridView gridView)
        {
            gridView.Columns["建立日期"].Visible = true;
            gridView.Columns["到職日"].Visible = true;
            gridView.Columns["工號"].Visible = true;
            gridView.Columns["姓名"].Visible = true;
            gridView.Columns["使用者帳號"].Visible = true;
            gridView.Columns["使用者密碼"].Visible = true;
            gridView.Columns["使用者姓名"].Visible = true;
            gridView.Columns["LDAP姓氏"].Visible = true;
            gridView.Columns["LDAP名字"].Visible = true;
            gridView.Columns["電子郵件"].Visible = true;
            gridView.Columns["公司"].Visible = true;
            gridView.Columns["職稱"].Visible = true;
            gridView.Columns["部門"].Visible = true;
            gridView.Columns["辦公室"].Visible = true;

            gridView.Columns["建立日期"].VisibleIndex = 0;
            gridView.Columns["到職日"].VisibleIndex = 1;
            gridView.Columns["工號"].VisibleIndex = 2;
            gridView.Columns["姓名"].VisibleIndex = 3;
            gridView.Columns["使用者帳號"].VisibleIndex = 4;
            gridView.Columns["使用者密碼"].VisibleIndex = 5;
            gridView.Columns["使用者姓名"].VisibleIndex = 6;
            gridView.Columns["LDAP姓氏"].VisibleIndex = 7;
            gridView.Columns["LDAP名字"].VisibleIndex = 8;
            gridView.Columns["電子郵件"].VisibleIndex = 9;
            gridView.Columns["公司"].VisibleIndex = 10;
            gridView.Columns["職稱"].VisibleIndex = 11;
            gridView.Columns["部門"].VisibleIndex = 12;
            gridView.Columns["辦公室"].VisibleIndex = 13;
            gridView.RefreshData();
        }

        private void showGridViewForUser(ref GridView gridView)
        {
            gridView.Columns["使用者帳號"].Visible = true;
            gridView.Columns["使用者密碼"].Visible = true;
            gridView.Columns["使用者姓名"].Visible = true;
            gridView.Columns["QUOTA"].Visible = true;
            gridView.Columns["WM"].Visible = true;
            gridView.Columns["WH"].Visible = true;
            gridView.Columns["SMB"].Visible = true;
            gridView.Columns["FTP"].Visible = true;
            gridView.Columns["WEB"].Visible = true;
            gridView.Columns["SQL"].Visible = true;
            gridView.Columns["EXP"].Visible = true;

            gridView.Columns["使用者帳號"].VisibleIndex = 0;
            gridView.Columns["使用者密碼"].VisibleIndex = 1;
            gridView.Columns["使用者姓名"].VisibleIndex = 2;
            gridView.Columns["QUOTA"].VisibleIndex = 3;
            gridView.Columns["WM"].VisibleIndex = 4;
            gridView.Columns["WH"].VisibleIndex = 5;
            gridView.Columns["SMB"].VisibleIndex = 6;
            gridView.Columns["FTP"].VisibleIndex = 7;
            gridView.Columns["WEB"].VisibleIndex = 8;
            gridView.Columns["SQL"].VisibleIndex = 9;
            gridView.Columns["EXP"].VisibleIndex = 10;
            gridView.RefreshData();
        }
        private void showGridViewForLdap(ref GridView gridView)
        {
            gridView.Columns["LDAP姓氏"].Visible = true;
            gridView.Columns["LDAP名字"].Visible = true;
            gridView.Columns["電子郵件"].Visible = true;
            gridView.Columns["電話"].Visible = true;
            gridView.Columns["傳真"].Visible = true;
            gridView.Columns["行動電話"].Visible = true;
            gridView.Columns["地址"].Visible = true;
            gridView.Columns["網頁"].Visible = true;
            gridView.Columns["公司"].Visible = true;
            gridView.Columns["職稱"].Visible = true;
            gridView.Columns["部門"].Visible = true;
            gridView.Columns["辦公室"].Visible = true;
            gridView.Columns["辦公室電話"].Visible = true;
            gridView.Columns["辦公室傳真"].Visible = true;
            gridView.Columns["IP電話"].Visible = true;
            gridView.Columns["呼叫器"].Visible = true;
            gridView.Columns["國家"].Visible = true;
            gridView.Columns["郵遞區號"].Visible = true;
            gridView.Columns["縣/市"].Visible = true;
            gridView.Columns["鄉/鎮/市/區"].Visible = true;
            gridView.Columns["街名"].Visible = true;
            gridView.Columns["其它"].Visible = true;

            gridView.Columns["LDAP姓氏"].VisibleIndex = 0;
            gridView.Columns["LDAP名字"].VisibleIndex = 1;
            gridView.Columns["電子郵件"].VisibleIndex = 2;
            gridView.Columns["電話"].VisibleIndex = 3;
            gridView.Columns["傳真"].VisibleIndex = 4;
            gridView.Columns["行動電話"].VisibleIndex = 5;
            gridView.Columns["地址"].VisibleIndex = 6;
            gridView.Columns["網頁"].VisibleIndex = 7;
            gridView.Columns["公司"].VisibleIndex = 8;
            gridView.Columns["職稱"].VisibleIndex = 9;
            gridView.Columns["部門"].VisibleIndex = 10;
            gridView.Columns["辦公室"].VisibleIndex = 11;
            gridView.Columns["辦公室電話"].VisibleIndex = 12;
            gridView.Columns["辦公室傳真"].VisibleIndex = 13;
            gridView.Columns["IP電話"].VisibleIndex = 14;
            gridView.Columns["呼叫器"].VisibleIndex = 15;
            gridView.Columns["國家"].VisibleIndex = 16;
            gridView.Columns["郵遞區號"].VisibleIndex = 17;
            gridView.Columns["縣/市"].VisibleIndex = 18;
            gridView.Columns["鄉/鎮/市/區"].VisibleIndex = 19;
            gridView.Columns["街名"].VisibleIndex = 20;
            gridView.Columns["其它"].VisibleIndex = 21;
            gridView.RefreshData();
        }

        private void hideGridViewForAll(ref GridView gridView)
        {
            gridView.Columns["建立日期"].Visible = false;
            gridView.Columns["到職日"].Visible = false;
            gridView.Columns["工號"].Visible = false;
            gridView.Columns["姓名"].Visible = false;
            gridView.Columns["使用者帳號"].Visible = false;
            gridView.Columns["使用者密碼"].Visible = false;
            gridView.Columns["使用者姓名"].Visible = false;
            gridView.Columns["QUOTA"].Visible = false;
            gridView.Columns["WM"].Visible = false;
            gridView.Columns["WH"].Visible = false;
            gridView.Columns["SMB"].Visible = false;
            gridView.Columns["FTP"].Visible = false;
            gridView.Columns["WEB"].Visible = false;
            gridView.Columns["SQL"].Visible = false;
            gridView.Columns["EXP"].Visible = false;
            gridView.Columns["LDAP姓氏"].Visible = false;
            gridView.Columns["LDAP名字"].Visible = false;
            gridView.Columns["電子郵件"].Visible = false;
            gridView.Columns["電話"].Visible = false;
            gridView.Columns["傳真"].Visible = false;
            gridView.Columns["行動電話"].Visible = false;
            gridView.Columns["地址"].Visible = false;
            gridView.Columns["網頁"].Visible = false;
            gridView.Columns["公司"].Visible = false;
            gridView.Columns["職稱"].Visible = false;
            gridView.Columns["部門"].Visible = false;
            gridView.Columns["辦公室"].Visible = false;
            gridView.Columns["辦公室電話"].Visible = false;
            gridView.Columns["辦公室傳真"].Visible = false;
            gridView.Columns["IP電話"].Visible = false;
            gridView.Columns["呼叫器"].Visible = false;
            gridView.Columns["國家"].Visible = false;
            gridView.Columns["郵遞區號"].Visible = false;
            gridView.Columns["縣/市"].Visible = false;
            gridView.Columns["鄉/鎮/市/區"].Visible = false;
            gridView.Columns["街名"].Visible = false;
            gridView.Columns["其它"].Visible = false;
            gridView.RefreshData();
        }

        private void showGridViewForAD(ref GridView gridView)
        {
            gridView.Columns["姓名"].Visible = false;
            gridView.Columns["使用者帳號"].Visible = false;
            gridView.Columns["使用者密碼"].Visible = false;
            gridView.Columns["部門"].Visible = false;

            gridView.Columns["姓名"].VisibleIndex = 0;
            gridView.Columns["使用者帳號"].VisibleIndex = 1;
            gridView.Columns["使用者密碼"].VisibleIndex = 2;
            gridView.Columns["部門"].VisibleIndex = 3;
            gridView.RefreshData();
        }

        private void exportToBatchAd(DataTable dt, string filePathAndName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                // 取得姓名
                string name = dr["姓名"].ToString();
                string firstName = name.Substring(0, 1);
                string lastName = name.Substring(1, name.Length - 1);
                // 取得AD部門
                string dept = dr["部門"].ToString();
                string mappedDept = getMappingDept(dept);
                // 如果沒有找到mapping, 預設為TempUsers
                if(mappedDept.Length==0) { mappedDept = "TempUsers"; }


                sb.AppendLine(string.Format("dsadd user cn={0},ou={3},dc=office,dc=machvision,dc=com,dc=tw -upn {1}@office.machvision.com.tw -pwd {2} -display {0} -fn {5} -ln {4} -email {1}@machvision.com.tw -mustchpwd no -pwdneverexpires yes -disabled no -samid {1}", dr["姓名"].ToString(), dr["使用者帳號"].ToString(), dr["使用者密碼"].ToString(), mappedDept, firstName, lastName));
                // 當mappedDept不是TempUsers時, 代表有mapping到AD的部門, 再帶入部門權限設定
                if(mappedDept.Equals("TempUsers")==false)
                {
                    sb.AppendLine(string.Format("net group {0} {1} /add /domain", mappedDept, dr["使用者帳號"].ToString()));
                }
            }
            sb.AppendLine("@pause");


            File.WriteAllText(filePathAndName, sb.ToString(), Encoding.Default);
        }

        private string getMappingDept(string deptName)
        {
            string deptNo = deptName.Substring(0, 3);

            int n;
            if (int.TryParse(deptNo, out n) == false)
            {
                return string.Empty;
            }

            n = int.Parse(deptNo);

            if (n >= 100 && n <= 220)
            {
                // 總經理室到資訊部
                if (n == 100)
                {
                    return "100總經理室";
                }
                else if (n == 105)
                {
                    return "105文管中心";
                }
                else if (n == 111)
                {
                    return "111台灣業務部";
                }
                else if (n == 210 || n == 211)
                {
                    return "210行政部";
                }
                else if (n == 220)
                {
                    return "220資訊安全部";
                }
            }
            else if (n >= 300 && n <= 399)
            {
                return "310生產部";

            }
            else if (n >= 410 && n <= 419)
            {
                return "410產品研發部";
            }
            else if (n >= 420 && n <= 429)
            {
                return "420研發部光機電";
            }
            else if (n >= 470 && n <= 479)
            {
                return "470專案研發部";
            }
            else if (n >= 480 && n <= 489)
            {
                return "480研發前測中心";
            }
            else if (n >= 500 && n <= 599)
            {
                return "510業務客服部";
            }
            else if (n >= 610 && n <= 619)
            {
                return "610財務部";
            }
            else if (n >= 720 && n <= 729)
            {
                return "720採購部";
            }
            else if (n >= 730 && n <= 739)
            {
                return "730料控中心";
            }
            else if (n >= 811 && n <= 816)
            {
                return "114華南區";
            }
            else if (n >= 821 && n <= 824)
            {
                return "115華東區";
            }

            // 上面條件都沒成立, 不設定部門
            return string.Empty;
        }

        private void sbLinkMvMxMail_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MvAdConnector.MxMailUrlMachvision);
        }

        private void sbLinkSiGoldMxMail_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MvAdConnector.MxMailUrlSiGold);
        }

        private void sbExportAD_Click(object sender, EventArgs e)
        {

        }

        private void frmItMxMail_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
