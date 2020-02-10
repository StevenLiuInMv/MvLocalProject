using System;
using System.Data;
using System.Windows.Forms;
using MvLocalProject.Bo;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using System.Threading;
using System.Text.RegularExpressions;
using MvLocalProject.Model;
using MvLocalProject.Controller;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.Net.Sockets;

namespace MvLocalProject.Viewer
{
    public partial class frmItCisco : Form
    {
        public frmItCisco()
        {
            InitializeComponent();
        }


        private async void btnGetPorts_Click(object sender, EventArgs e)
        {
            clearAllText();
            MvItCiscoBo bo = new MvItCiscoBo();
            SplashScreenManager.ShowDefaultWaitForm();
            DataTable dt = null;
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            dt = await bo.getPortStatusTable(GlobalConstant.MvMisCiscoIpList);
            SocketException se = null;
            se = bo.getSocketException();
            if (se != null)
            {
                MessageBox.Show(se.Message + Environment.NewLine + Environment.NewLine + "持續載入資訊中 ......");
            }

            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnDebugGetPorts_Click(object sender, EventArgs e)
        {
            clearAllText();
            SplashScreenManager.ShowDefaultWaitForm();
            MvItCiscoBo bo = new MvItCiscoBo();
            string result = await bo.getPortStatus(GlobalConstant.MvMisCiscoIpList);
            richTextBox1.Text = result.ToString();
            richTextBox1.Refresh();
            SplashScreenManager.CloseForm(false);
        }

        private void frmItCisco_Load(object sender, EventArgs e)
        {
            // initial Bar

            BarManager barManager = new BarManager();
            barManager.Form = this;
            barManager.BeginUpdate();
            Bar bar1 = new Bar(barManager, "My MainMenu");
            bar1.DockStyle = BarDockStyle.Top;
            barManager.MainMenu = bar1;

            // Create bar items for the bar1 and bar2
            BarSubItem subMenuLoad = new BarSubItem(barManager, "Load");
            BarSubItem subMenuViewLogs = new BarSubItem(barManager, "Logs");
            BarSubItem subMenuFile = new BarSubItem(barManager, "File");

            BarButtonItem buttonLoadAllPorts = new BarButtonItem(barManager, "Load all ports");
            BarButtonItem buttonLoadAllPortsIncludeMac = new BarButtonItem(barManager, "Load all ports include mac");
            BarButtonItem buttonLoadAllPortsFromDb = new BarButtonItem(barManager, "Load all ports from database");
            BarButtonItem buttonLoadAllPortsToDebug = new BarButtonItem(barManager, "Load to debug");

            BarButtonItem buttonLog_51_11 = new BarButtonItem(barManager, "Show log in C2960X-01");
            BarButtonItem buttonLog_51_12 = new BarButtonItem(barManager, "Show log in C2960X-02");
            BarButtonItem buttonLog_51_13 = new BarButtonItem(barManager, "Show log in C2960X-03");
            BarButtonItem buttonLog_51_14 = new BarButtonItem(barManager, "Show log in C2960X-04");
            BarButtonItem buttonLog_51_15 = new BarButtonItem(barManager, "Show log in C2960X-05");
            BarButtonItem buttonLog_51_16 = new BarButtonItem(barManager, "Show log in C2960X-06");

            BarButtonItem buttonSaveExcel = new BarButtonItem(barManager, "Save as Excel ...");

            subMenuLoad.AddItems(new BarItem[] { buttonLoadAllPorts, buttonLoadAllPortsIncludeMac, buttonLoadAllPortsFromDb, buttonLoadAllPortsToDebug });
            subMenuViewLogs.AddItems(new BarItem[] { buttonLog_51_11, buttonLog_51_12, buttonLog_51_13,
                buttonLog_51_14, buttonLog_51_15, buttonLog_51_16 });
            subMenuFile.AddItems(new BarItem[] { buttonSaveExcel });

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subMenuLoad, subMenuViewLogs, subMenuFile });

            // A handler to process clicks on bar items
            barManager.ItemClick += new ItemClickEventHandler(barManager_ItemClick);

            barManager.EndUpdate();

            // initial textedit
            teIp.Properties.ReadOnly = true;
            tePort.Properties.ReadOnly = true;
            teName.Properties.ReadOnly = true;
            teStatus.Properties.ReadOnly = true;

            // initial button
            disableAllComponent();
        }

        void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarSubItem subMenu = e.Item as BarSubItem;
            if (subMenu != null) { return; }

            if (e.Item.Caption.Equals("Load all ports"))
            { btnGetPorts_Click(sender, e); }
            else if (e.Item.Caption.Equals("Load all ports include mac"))
            { btnGetPortsIncludeMac_Click(sender, e); }
            else if (e.Item.Caption.Equals("Load to debug"))
            { btnDebugGetPorts_Click(sender, e); }
            else if (e.Item.Caption.Equals("Load all ports from database"))
            { btnGetPortsFromDB_Click(sender, e); }
            else if (e.Item.Caption.Equals("Show log in C2960X-01"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.11"); }
            else if (e.Item.Caption.Equals("Show log in C2960X-02"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.12"); }
            else if (e.Item.Caption.Equals("Show log in C2960X-03"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.13"); }
            else if (e.Item.Caption.Equals("Show log in C2960X-04"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.14"); }
            else if (e.Item.Caption.Equals("Show log in C2960X-05"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.15"); }
            else if (e.Item.Caption.Equals("Show log in C2960X-06"))
            { btnGetLogsByIp_Click(sender, e, "192.168.151.16"); }
            else if (e.Item.Caption.Equals("Save as Excel ..."))
            { btnExportToExcel_Click(sender, e); }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string tmpString = string.Empty;
            foreach (int i in gridView1.GetSelectedRows())
            {
                tmpString = string.Empty;
                DataRow row = gridView1.GetDataRow(i);
                if (gridView1.Columns[0].FieldName.Equals("IP"))
                {
                    teIp.Text = row[0].ToString();
                    tePort.Text = row[1].ToString();
                    teName.Text = row[2].ToString();
                    teStatus.Text = row[3].ToString();
                }
                else if (gridView1.Columns[0].FieldName.Equals("DeviceIP"))
                {
                    teIp.Text = row[0].ToString();
                    tePort.Text = row[1].ToString();
                    teName.Text = row[4].ToString();
                    teStatus.Text = "disable";
                }
                tmpString = row[5].ToString();
                if (gridView1.Columns[5].FieldName.Equals("MacAddress"))
                {
                    tmpString = row[5].ToString();
                    if(!tmpString.Equals("N/A"))
                    {
                        teMacAddress.Text = row[5].ToString();
                    } 
                    else
                    {
                        teMacAddress.Text = string.Empty;
                    }

                    tmpString = row[6].ToString();
                    if (!tmpString.Equals("N/A") && !tmpString.Equals("0"))
                    {
                        teMacNumber.Text = row[6].ToString();
                    }
                    else
                    {
                        teMacNumber.Text = string.Empty;
                    }
                }
            }

            if (teStatus.Text.Length == 0)
            {
                disableAllComponent();
            }
            else if (teStatus.Text.IndexOf("err-disable") == 0)
            {
                btnEnablePort.Enabled = false;
                btnDisablePort.Enabled = true;
                btnGetPortInfo.Enabled = true;
                groupMacEdit.Enabled = true;
                groupMacNumberEdit.Enabled = true;
            }
            else if (teStatus.Text.IndexOf("disable") >= 0)
            {
                disableAllComponent();
                btnEnablePort.Enabled = true;
                btnGetPortInfo.Enabled = true;
            }
            else
            {
                btnEnablePort.Enabled = false;
                btnDisablePort.Enabled = true;
                btnGetPortInfo.Enabled = true;
                groupMacEdit.Enabled = true;
                groupMacNumberEdit.Enabled = true;
            }
        }

        private void btnEnablePort_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0)
            {
                return;
            }
            // 不可以使用static 的寫法, 會被cache
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                bo.enablePortByTelnet(teIp.Text, tePort.Text);
            }
            // 跑太快抓回來的資訊會不正確
            Thread.Sleep(300);
            clearAllText();

            btnGetPorts_Click(sender, e);
            disableAllComponent();
        }

        private void btnDisablePort_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0)
            {
                return;
            }
            // 不可以使用static 的寫法, 會被cache
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                bo.disablePortByTelnet(teIp.Text, tePort.Text);
            }
            Thread.Sleep(300);
            // 跑太快抓回來的資訊會不同步
            clearAllText();
            btnGetPorts_Click(sender, e);
            disableAllComponent();
        }

        private void clearAllText()
        {
            teIp.Text = string.Empty;
            tePort.Text = string.Empty;
            teName.Text = string.Empty;
            teStatus.Text = string.Empty;
            teMacAddress.Text = string.Empty;
            teMacNumber.Text = string.Empty;
        }

        private void disableAllComponent()
        {
            btnDisablePort.Enabled = false;
            btnEnablePort.Enabled = false;
            btnGetPortInfo.Enabled = false;
            groupMacEdit.Enabled = false;
            groupMacNumberEdit.Enabled = false;
        }

        private async void btnGetMac_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0 || teStatus.Text.Length == 0)
            {
                return;
            }

            richTextBox1.Text = string.Empty;
            // Get Mac Address
            string macAddress = string.Empty;
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                macAddress = await bo.getMacAddressOnPortByTelnet(teIp.Text, tePort.Text);
            }

            richTextBox1.Text = macAddress;
        }

        private async void btnAddMac_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0 || teStatus.Text.Length == 0 || teMacAddress.Text.Length == 0 || teName.Text.Length == 0)
            {
                MessageBox.Show("請輸入完整的Cisco及Mac資訊");
                return;
            }

            if (teStatus.Text.IndexOf("err-disable") == 0)
            {
                // err-disable 正常操作
            }
            else if (teStatus.Text.IndexOf("disable") >= 0)
            {
                MessageBox.Show("請先將該port enable後再重新操作");
                return;
            }

            // 比對是否符合網卡輸入規則
            Regex rgx = new Regex(@"[a-z0-9]{4}.[a-z0-9]{4}.[a-z0-9]{4}$");
            bool isMatch = rgx.IsMatch(teMacAddress.Text);
            if (isMatch == false)
            {
                MessageBox.Show("請輸入符合格式如下, 並請轉為小寫" + Environment.NewLine + "bxxe.bxxb.0xx4");
                return;
            }

            // 執行mac address add作業
            SplashScreenManager.ShowDefaultWaitForm();
            string result = string.Empty;
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                result = await bo.addMacAddressOnPortByTelnet(teIp.Text, tePort.Text, teMacAddress.Text, teName.Text, true);
            }

            if(result.IndexOf("has reached maximum limit") >0)
            {
                //代表已超過上線
                MessageBox.Show("已超過maximum limit上限, 該mac address無法加入" + Environment.NewLine + "請看debug output");
                richTextBox1.Text = result;
            } else
            {
                btnGetMac_Click(sender, e);
            }

            clearAllText();
            disableAllComponent();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnDelMac_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0 || teStatus.Text.Length == 0 || teMacAddress.Text.Length == 0 || teName.Text.Length == 0)
            {
                MessageBox.Show("請輸入完整的Cisco及Mac資訊");
                return;
            }

            if (teStatus.Text.IndexOf("err-disable") == 0)
            {
                // err-disable 正常操作
            }
            else if (teStatus.Text.IndexOf("disable") >= 0)
            {
                MessageBox.Show("請先將該port enable後再重新操作");
                return;
            }

            // 比對是否符合網卡輸入規則
            Regex rgx = new Regex(@"[a-z0-9]{4}.[a-z0-9]{4}.[a-z0-9]{4}$");
            bool isMatch = rgx.IsMatch(teMacAddress.Text);
            if (isMatch == false)
            {
                MessageBox.Show("請輸入符合格式如下, 並請轉為小寫" + Environment.NewLine + "bxxe.bxxb.0xx4");
                return;
            }

            // 執行mac address delete業
            SplashScreenManager.ShowDefaultWaitForm();
            string result = string.Empty;
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                result = await bo.removeMacAddressOnPortByTelnet(teIp.Text, tePort.Text, teMacAddress.Text, teName.Text, true);
            }
                
            btnGetMac_Click(sender, e);
            clearAllText();
            disableAllComponent();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnGetLogsByIp_Click(object sender, EventArgs e, string routerIp)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            string result = string.Empty;
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                result = await bo.getRouterLogsByTelnet(routerIp);
            }
            richTextBox1.Text = result.ToString();
            richTextBox1.Refresh();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnSetMacNumber_Click(object sender, EventArgs e)
        {
            if (teIp.Text.Length == 0 || tePort.Text.Length == 0 || teStatus.Text.Length == 0 || teName.Text.Length == 0 || teMacNumber.Text.Length == 0)
            {
                MessageBox.Show("請輸入完整的Cisco及Mac Number資訊");
                return;
            }

            if (teStatus.Text.IndexOf("err-disable") == 0)
            {
                // err-disable 正常操作
            }
            else if (teStatus.Text.IndexOf("disable") >= 0)
            {
                MessageBox.Show("請先將該port enable後再重新操作");
                return;
            }
            // 比對輸入是否為數字
            int n;
            if (int.TryParse(teMacNumber.Text, out n) == false)
            {
                MessageBox.Show("請輸入數字");
                return;
            }

            // 執行mac address number modify作業
            SplashScreenManager.ShowDefaultWaitForm();
            string result = string.Empty;
            using (MvItCiscoBo bo = new MvItCiscoBo())
            {
                result = await bo.setMacAddressNumberOnPortByTelnet(teIp.Text, tePort.Text, teMacNumber.Text, true);
            }

            if (result.IndexOf("Maximum is less than") >= 0)
            {
                //代表設定的數量比已存在的網卡少, 要先移除網卡
                MessageBox.Show("修改數量比已設定的網卡數量少, 請加大輸入數量或先移除網卡" + Environment.NewLine + "請看debug output");
                richTextBox1.Text = result;
            }
            else
            {
                btnGetMac_Click(sender, e);
            }

            clearAllText();
            disableAllComponent();
            SplashScreenManager.CloseForm(false);
        }

        private void frmItCisco_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void btnGetPortsIncludeMac_Click(object sender, EventArgs e)
        {
            clearAllText();
            MvItCiscoBo bo = new MvItCiscoBo();
            SplashScreenManager.ShowDefaultWaitForm();

            DataTable dt = null;
            DataTable dtFromDb = null;
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            //dt = await bo.getStatusTableIncludeMacAddress(new string[] { "192.168.151.11" });
            dt = await bo.getStatusTableIncludeMacAddress(GlobalConstant.MvMisCiscoIpList);
            dt.Columns.Add("IsInDB");
            dtFromDb = MvDbDao.collectData_ItNetworkDevice();

            Hashtable cacheResult = new Hashtable();
            string matchKey = string.Empty;
            // 將DB 資料放入HashTable
            foreach (DataRow drFromDb in dtFromDb.Rows)
            {
                matchKey = string.Format("{0}{1}{2}{3}", drFromDb["DeviceIP"], drFromDb["Port"], drFromDb["Mac"], drFromDb["No"]);
                cacheResult.Add(matchKey, true);
            }

            matchKey = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                matchKey = string.Format("{0}{1}{2}{3}", dr["IP"], dr["Port"], dr["MacAddress"], dr["Name"]);
                if (cacheResult.Contains(matchKey) == true)
                {
                    dr["IsInDB"] = "V";
                }
            }

            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnGetPortsFromDB_Click(object sender, EventArgs e)
        {
            clearAllText();
            MvItCiscoBo bo = new MvItCiscoBo();
            SplashScreenManager.ShowDefaultWaitForm();

            DataTable dt = null;
            DataTable dtFromCisco = null;
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            dt = MvDbDao.collectData_ItNetworkDevice();
            dt.Columns.Remove("SN");
            dt.Columns.Add("IsInCisco");

            //dtFromCisco = await bo.getStatusTableIncludeMacAddress(new string[] { "192.168.151.11" });
            dtFromCisco = await bo.getStatusTableIncludeMacAddress(GlobalConstant.MvMisCiscoIpList);

            Hashtable cacheResult = new Hashtable();
            string matchKey = string.Empty;
            // 將Cisco 資料放入HashTable
            foreach (DataRow drFromCisco in dtFromCisco.Rows)
            {
                matchKey = string.Format("{0}{1}{2}{3}", drFromCisco["IP"], drFromCisco["Port"], drFromCisco["MacAddress"], drFromCisco["Name"]);
                cacheResult.Add(matchKey, true);
            }

            matchKey = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                matchKey = string.Format("{0}{1}{2}{3}", dr["DeviceIP"], dr["Port"], dr["Mac"], dr["No"]);
                if (cacheResult.Contains(matchKey) == true)
                {
                    dr["IsInCisco"] = "V";
                }
            }

            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();

            SplashScreenManager.CloseForm(false);
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (View.Columns.Count < 8)
            {
                return;
            }
            // 由實體機器與DB比對的呈現邏輯
            if (View.Columns.Count == 8 && View.Columns[7].FieldName.Equals("IsInDB"))
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                string isInDb = View.GetRowCellDisplayText(e.RowHandle, View.Columns["IsInDB"]);
                if (isInDb.Length <= 0)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
            else if (View.Columns.Count == 11 && View.Columns[10].FieldName.Equals("IsInCisco"))
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                string isInCisco = View.GetRowCellDisplayText(e.RowHandle, View.Columns["IsInCisco"]);
                if (isInCisco.Length <= 0)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if(gridView1.Columns.Count <= 0)
            {
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel 活頁簿 |*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName == "") { return; }
            string filePathAndName = saveFileDialog1.FileName;

            try
            {
                gridView1.ExportToXlsx(filePathAndName);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, filePathAndName));
                return;
            }
            MessageBox.Show(string.Format("Save as path {0}{1}", Environment.NewLine, filePathAndName));
        }
    }
}
