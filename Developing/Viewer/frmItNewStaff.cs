using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrimS.Telnet;

namespace MvLocalProject.Viewer
{
    public partial class frmItNewStaff : Form
    {
        public frmItNewStaff()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TreeListNode[] parentNodeList = new TreeListNode[10];

            //treeList1.OptionsBehavior.AutoNodeHeight = true;
            //treeList1.DataSource = new ViewModel().NewStaffSops;

            //treeList1.ExpandAll();
            //treeList1.OptionsBehavior.AutoNodeHeight = true;
            //treeList1.OptionsView.AutoWidth = true;
            

            //this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit() };


        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmItNewStaff_Load(object sender, EventArgs e)
        {
            labelControl1.Text = "1. 開啟進入(牧德) Mail Server 管理介面";
            memoEdit1.Text = "帳/密：(admin/ms1652)";

            labelControl2.Text = "2. 建立使用者帳號" + Environment.NewLine + "      >使用者管理" + Environment.NewLine + "      >帳號管理" + Environment.NewLine + "      >新增帳號，建立使用者帳號/密碼";
            labelControl3.Text = "1. 帳號 (英名 + 英姓皆小寫)" + Environment.NewLine + "    範例：mingwang" + Environment.NewLine
                + "2.姓名 (英名 + 英姓皆開頭大寫 + 中文全名 + _ + 部門代號)" + Environment.NewLine + "    範例：MingWang王小明_220" + Environment.NewLine
                + "    部門代號 - 華東部門CE、華南部門CS、華中部門CC" + Environment.NewLine
                + "3.密碼 (同AD密碼，英文代號 + 隨機產生五碼)" + Environment.NewLine + "    範例：產品研發部 r12345" + Environment.NewLine
                + "    密碼-華東華南華中 - 生日八碼" + Environment.NewLine
                + "4.勾選 功能> WM";
            pictureEdit1.Image = ResourceNewStaff.MailServerExample;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://192.168.222.16:88");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string strCommand = string.Empty;
            using (Client client = new Client("192.168.151.13", 23, new System.Threading.CancellationToken()))
            {
                string strResult = await client.ReadAsync();
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync();
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "en";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync();
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync();
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "conf t";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 10));
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "do sh int status";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 10));
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 10));
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 10));
                richTextBox1.Text += strResult;
                richTextBox1.Refresh();


            }

        }
    }

    public class NewStaffSop
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public string No { get; set; }
        public string Item { get; set; }
        public string ServerUrl { get; set; }
        public string UserInfo { get; set; }

        public string Remark { get; set; }

        public static List<NewStaffSop> GetNewStaffSops()
        {
            List<NewStaffSop> sops = new List<NewStaffSop>();
            sops.Add(new NewStaffSop() { ID = 1, ParentID = 0, No = "1", Item = "接獲 HR Mail 通知人員報到資訊", Remark = "必要項目：基本資料(工號、姓名、英文名、部門、報到日期、職稱)", ServerUrl = "", UserInfo = "" });
            sops.Add(new NewStaffSop() { ID = 2, ParentID = 0, No = "2", Item = "Mail Server 帳號建立作業", Remark = ""});
            sops.Add(new NewStaffSop() { ID = 21, ParentID = 2, No = "2.1", Item = "使用者帳號建立", Remark = ""});
            sops.Add(new NewStaffSop() { ID = 22, ParentID = 2, No = "2.2", Item = "LDAP資料庫更新(維護)作業", Remark = ""});


            sops.Add(new NewStaffSop() { ID = 211, ParentID = 21, No = "2.1.1", Item = "開啟進入(牧德) Mail Server 管理介面", Remark = "", ServerUrl = @"https://192.168.222.16:88", UserInfo = "admin/ms1652" });
            sops.Add(new NewStaffSop() { ID = 212, ParentID = 21, No = "2.1.2", Item = "建立使用者帳號"+ Environment.NewLine + "使用者管理> 帳號管理> 新增帳號，建立使用者帳號/密碼", Remark = "", ServerUrl = "", UserInfo = "" });
            return sops;
        }
    }

    public class Employee
    {
        

        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { ID = 1, ParentID = 0, Name = "Gregory S. Price", Department = "", Position = "President" });
            employees.Add(new Employee() { ID = 2, ParentID = 1, Name = "Irma R. Marshall", Department = "Marketing", Position = "Vice President" });
            employees.Add(new Employee() { ID = 3, ParentID = 1, Name = "John C. Powell", Department = "Operations", Position = "Vice President" });
            employees.Add(new Employee() { ID = 4, ParentID = 1, Name = "Christian P. Laclair", Department = "Production", Position = "Vice President" });
            employees.Add(new Employee() { ID = 5, ParentID = 1, Name = "Karen J. Kelly", Department = "Finance", Position = "Vice President" });
            employees.Add(new Employee() { ID = 6, ParentID = 2, Name = "Brian C. Cowling", Department = "Marketing", Position = "Manager" });
            employees.Add(new Employee() { ID = 7, ParentID = 2, Name = "Thomas C. Dawson", Department = "Marketing", Position = "Manager" });
            employees.Add(new Employee() { ID = 8, ParentID = 2, Name = "Angel M. Wilson", Department = "Marketing", Position = "Manager" });
            employees.Add(new Employee() { ID = 9, ParentID = 2, Name = "Bryan R. Henderson", Department = "Marketing", Position = "Manager" });
            employees.Add(new Employee() { ID = 10, ParentID = 3, Name = "Harold S. Brandes", Department = "Operations", Position = "Manager" });
            employees.Add(new Employee() { ID = 11, ParentID = 3, Name = "Michael S. Blevins", Department = "Operations", Position = "Manager" });
            employees.Add(new Employee() { ID = 12, ParentID = 3, Name = "Jan K. Sisk", Department = "Operations", Position = "Manager" });
            employees.Add(new Employee() { ID = 13, ParentID = 3, Name = "Sidney L. Holder", Department = "Operations", Position = "Manager" });
            employees.Add(new Employee() { ID = 14, ParentID = 4, Name = "James L. Kelsey", Department = "Production", Position = "Manager" });
            employees.Add(new Employee() { ID = 15, ParentID = 4, Name = "Howard M. Carpenter", Department = "Production", Position = "Manager" });
            employees.Add(new Employee() { ID = 16, ParentID = 4, Name = "Jennifer T. Tapia", Department = "Production", Position = "Manager" });
            employees.Add(new Employee() { ID = 17, ParentID = 5, Name = "Judith P. Underhill", Department = "Finance", Position = "Manager" });
            employees.Add(new Employee() { ID = 18, ParentID = 5, Name = "Russell E. Belton", Department = "Finance", Position = "Manager" });
            return employees;
        }
    }
    class ViewModel
    {
        public ViewModel()
        {
            Employees = Employee.GetEmployees();
            NewStaffSops = NewStaffSop.GetNewStaffSops();
        }
        public List<Employee> Employees { get; set; }
        public List<NewStaffSop> NewStaffSops { get; set; }
    }
}
