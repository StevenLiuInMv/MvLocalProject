using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using MvLocalProject.Controller;
using MvLocalProject.Model;

namespace MvLocalProject.Viewer

{
    public partial class frmReport : Form
    {

        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        string workingDirectory = @"D:\99_TempArea\";
        string fileNameAndPath = "";

        public frmReport()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            bool blnResult = false;
            // get the report start's date and end date
            startDate = dateTimePicker1.Value;
            endDate = dateTimePicker2.Value;


            if (startDate.CompareTo(endDate) > 0)
            {
                if(DateTime.Compare(startDate.Date, endDate.Date) != 0)
                {
                    MessageBox.Show("結束日期需大於等於起始日期");
                    return;
                }
            }

            string result = cboReportType.SelectedValue == null ? "" : cboReportType.SelectedValue.ToString();
            if (result.Length == 0)
            {
                MessageBox.Show("Please choice the report type");
                return;
            }

            // Find the report type
            DefinedReport.ErpReportType reportList = DefinedReport.ErpReportType.NONE;

            try
            {
                reportList = (DefinedReport.ErpReportType)Enum.Parse(typeof(DefinedReport.ErpReportType), result);
            }
            catch (ArgumentException)
            { }

            MvExcelReport report = new MvExcelReport();
            switch (reportList)
            {
                case DefinedReport.ErpReportType.MocP10Auto:

                    endDate = System.DateTime.Now;
                    startDate = DateTime.Parse(endDate.AddMonths(-4).ToShortDateString());
                    fileNameAndPath = string.Format("{0}{1}_{2}.xlsx", workingDirectory, DefinedReport.ErpReportType.MocP10Auto.ToString(), endDate.ToString("yyyyMMdd"));

                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start report");
                    ds = MvDbDao.collectData_MocP10Auto(System.DateTime.Now);

                    blnResult = report.generateMocP10Auto(fileNameAndPath, ds);
                    if (blnResult == false)
                    { MessageBox.Show("generate MocP10Auto fail"); }
                    else
                    { MessageBox.Show(string.Format("generate {0} done {1}{2}", DefinedReport.ErpReportType.MocP10Auto.ToString(), Environment.NewLine, fileNameAndPath)); }

                    break;
                case DefinedReport.ErpReportType.BomP09:

                    result =  cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
                    if (result.Length == 0)
                    {
                        MessageBox.Show("Please choice the BOM type");
                        return;
                    }

                    ds = MvDbDao.collectData_Bom09(new string[] { result });
                    fileNameAndPath = string.Format("{0}{1}_{2}.xlsx", workingDirectory, DefinedReport.ErpReportType.BomP09.ToString(), endDate.ToString("yyyyMMdd"));
                    blnResult = report.generateBomP09_NoPrice(fileNameAndPath, ds);
                    if (blnResult == false)
                    { MessageBox.Show("generate MocP10Auto fail"); }
                    else
                    { MessageBox.Show(string.Format("generate {0} done {1}{2}", DefinedReport.ErpReportType.BomP09.ToString(), Environment.NewLine, fileNameAndPath)); }

                    break;
                case DefinedReport.ErpReportType.BomP09_Multi:

                    if (chkBomListBox.CheckedItems.Count <= 0)
                    {
                        MessageBox.Show("please checked more than one");
                        return;
                    }

                    List<string> selectedList = new List<string>();
                    foreach (var itemChecked in chkBomListBox.CheckedItems)
                    {
                        var row = (itemChecked as DataRowView).Row;
                        selectedList.Add(row["MC001"].ToString());
                    }

                    ds = MvDbDao.collectData_Bom09(selectedList.ToArray<string>());
                    fileNameAndPath = string.Format("{0}{1}_{2}.xlsx", workingDirectory, DefinedReport.ErpReportType.BomP09_Multi.ToString(), endDate.ToString("yyyyMMdd"));
                    blnResult = report.generateBomP09_NoPrice(fileNameAndPath, ds);

                    if (blnResult == false)
                    { MessageBox.Show("generate MocP10Auto fail"); }
                    else
                    { MessageBox.Show(string.Format("generate {0} done {1}{2}", DefinedReport.ErpReportType.BomP09_Multi.ToString(), Environment.NewLine, fileNameAndPath)); }

                    break;
                default:

                    MessageBox.Show("Can't find the report type as " + result);
                    break;
            }
        }

        
        private DataTable initialReportList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportId");
            dt.Columns.Add("ReportName");

            DataRow row = null;

            foreach(string name in Enum.GetNames(typeof(DefinedReport.ErpReportType)))
            {
                row = dt.NewRow();
                row["ReportId"] = name;
                row["ReportName"] = name.ToString();
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            cboReportType.DataSource = initialReportList();
            cboReportType.ValueMember = "ReportId";
            cboReportType.DisplayMember = "Value.DisplayName";
            cboReportType.Text = "";
            cboReportType.SelectedValue = "";

            //預設關閉元件
            enableObject(false, false, false);
        }


        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 如果沒被選取, 則直接離開
            if (cboReportType.SelectedIndex < 0) { return; }
            string result = cboReportType.SelectedValue.ToString();
            // 如果有值再判斷是否要intial value
            if (result.Length == 0)
            { return; }

            // Find the report type
            DataTable tmpDt = null;
            DefinedReport.ErpReportType reportList = DefinedReport.ErpReportType.NONE;
            try
            {
                reportList = (DefinedReport.ErpReportType)Enum.Parse(typeof(DefinedReport.ErpReportType), result);
            }
            catch (ArgumentException)
            { }

            switch (reportList)
            {
                case DefinedReport.ErpReportType.MocP10Auto:
                    groupBom.Enabled = false; 
                    chkBomListBox.Enabled = false;
                    groupDate.Enabled = true;
                    enableObject(true, false, false);

                    break;
                case DefinedReport.ErpReportType.BomP09:
                    // initial button value
                    tmpDt = MvDbDao.collectData_BomP09_BomList();
                    cboBomType.DataSource = tmpDt;
                    cboBomType.ValueMember = "MC001";
                    cboBomType.DisplayMember = "MB0012";
                    cboBomType.Text = "";
                    cboBomType.SelectedValue = "";
                    enableObject(false, true, false);

                    break;
                case DefinedReport.ErpReportType.BomP09_Multi:
                    // initial button value
                    tmpDt = MvDbDao.collectData_BomP09_BomList();
                    chkBomListBox.DataSource = tmpDt;
                    chkBomListBox.ValueMember = "MC001";
                    chkBomListBox.DisplayMember = "MB0012";
                    enableObject(false, false, true);

                    break;
                default:

                    enableObject(false, false, false);
                    // do nothing
                    return;
            }
        }

        private void enableObject(bool isGroupDate, bool isGroupBom, bool isCheckBoxListBox)
        {
            groupDate.Enabled = isGroupDate;
            groupBom.Enabled = isGroupBom;
            chkBomListBox.Enabled = isCheckBoxListBox;
        }


        private void chkBomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (((CheckedListBox)sender).SelectedIndex < 0) return;

            if (((CheckedListBox)sender).CheckedItems.Count > 2)
            {
                ((CheckedListBox)sender).SetItemCheckState(((CheckedListBox)sender).SelectedIndex, CheckState.Unchecked);
                ((CheckedListBox)sender).SetSelected(((CheckedListBox)sender).SelectedIndex, false);
                MessageBox.Show("至多只能選2個");
            }
        }

        private void frmReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
