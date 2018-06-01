using System;
using System.Data;
using System.Windows.Forms;
using MvLocalProject.Bo;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;
using System.Threading;
using System.Text.RegularExpressions;
using MvLocalProject.Model;

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
            MvItCiscoBo bo = new MvItCiscoBo();
            SplashScreenManager.ShowDefaultWaitForm();

            DataTable dt = null;
            gridControl1.DataSource = null;
            dt = await bo.getPortStatusTable(DefinedParameter.MvMisCiscoIpList);
            //dt = await bo.getPortStatusTable(new string[] { "192.168.151.12" });
            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.RefreshData();
            SplashScreenManager.CloseForm(false);
        }

        private async void btnDebugGetPorts_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            MvItCiscoBo bo = new MvItCiscoBo();
            string result = await bo.getPortStatus(DefinedParameter.MvMisCiscoIpList);
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
            BarSubItem subMenuFile = new BarSubItem(barManager, "Load");
            BarSubItem subMenuViewLogs = new BarSubItem(barManager, "Logs");

            BarButtonItem buttonLoadAllPorts = new BarButtonItem(barManager, "Load all ports");
            BarButtonItem buttonLoadAllPortsToDebug = new BarButtonItem(barManager, "Load to debug");

            BarButtonItem buttonLog_51_11 = new BarButtonItem(barManager, "Show log in C2960X-01");
            BarButtonItem buttonLog_51_12 = new BarButtonItem(barManager, "Show log in C2960X-02");
            BarButtonItem buttonLog_51_13 = new BarButtonItem(barManager, "Show log in C2960X-03");
            BarButtonItem buttonLog_51_14 = new BarButtonItem(barManager, "Show log in C2960X-04");
            BarButtonItem buttonLog_51_15 = new BarButtonItem(barManager, "Show log in C2960X-05");
            BarButtonItem buttonLog_51_16 = new BarButtonItem(barManager, "Show log in C2960X-06");

            subMenuFile.AddItems(new BarItem[] { buttonLoadAllPorts, buttonLoadAllPortsToDebug });
            subMenuViewLogs.AddItems(new BarItem[] { buttonLog_51_11, buttonLog_51_12, buttonLog_51_13,
                buttonLog_51_14, buttonLog_51_15, buttonLog_51_16 });

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subMenuFile, subMenuViewLogs });

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
            else if (e.Item.Caption.Equals("Load to debug"))
            { btnDebugGetPorts_Click(sender, e); }
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

        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                teIp.Text = row[0].ToString();
                tePort.Text = row[1].ToString();
                teName.Text = row[2].ToString();
                teStatus.Text = row[3].ToString();
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
    }
}
