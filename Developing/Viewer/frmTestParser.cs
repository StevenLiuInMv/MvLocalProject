using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraSplashScreen;
using MvLocalProject.Bo;
using MvLocalProject.Controller;
using DevExpress.XtraGrid.Views.Grid;
using MvLocalProject.Model;
using System.Collections;
using System.Transactions;
using System.Data.SqlClient;
using MvSharedLib.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmTestParser : Form
    {

        int differentCellCount = 0;

        public frmTestParser()
        {
            InitializeComponent();
        }

        private void sbParserRdDiffBomExcel_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            Workbook workbook = new Workbook();
            bool isLoad = false;
            //isLoad = workbook.LoadDocument(@"D:\\1.xlsx", DocumentFormat.Xlsx);
            //Console.WriteLine("result = " + isLoad);

            ArrayList itemList = new ArrayList();


            DataTable dt = new DataTable();
            using (FileStream stream = new FileStream(@"D:\99_TempForProgram\\差異料清單表_OK_AOI4.0 R167.xlsx", FileMode.Open))
            {
                isLoad = workbook.LoadDocument(stream, DocumentFormat.Xlsx);
                if (isLoad == false)
                {
                    MessageBox.Show("請確認EXCEL檔案是否已解密");
                    return;
                }

                // 判斷Sheet只能有一個, 等要release再打開
                //if(workbook.Sheets.Count != 1)
                //{
                //    MessageBox.Show(string.Format("請確認EXCEL檔案內只有一個sheet{0}該檔現有個sheets : {1}", Environment.NewLine, workbook.Sheets.Count));
                //    return;
                //}

                // 定義column
                dt = new DataTable();
                dt.Columns.Add("Item");
                dt.Columns.Add("Add");
                dt.Columns.Add("Delete");
                dt.Columns.Add("Change");
                dt.Columns.Add("OrgA8");
                dt.Columns.Add("NewA8");
                dt.Columns.Add("A8Name");
                dt.Columns.Add("OrgCount");
                dt.Columns.Add("NewCount");
                dt.Columns.Add("IsNew");
                dt.Columns.Add("IsRDConfirm");
                dt.Columns.Add("Owner");
                dt.Columns.Add("Module");
                dt.Columns.Add("Remark");
                // 取得sheet 內容
                Worksheet sheet = workbook.Worksheets[workbook.Sheets.Count - 1];


                Range range = sheet.GetUsedRange();
                Console.WriteLine("column count = " + range.ColumnCount);
                Console.WriteLine("row count = " + range.RowCount);

                // 取得欄位標題資訊
                int j = 1;
                textBox1.Text = sheet.GetCellValue(1, 1).ToString();
                textBox2.Text = sheet.GetCellValue(1, 2).ToString();
                textBox3.Text = sheet.GetCellValue(j, 3).ToString();
                textBox4.Text = sheet.GetCellValue(j, 4).ToString();
                textBox5.Text = sheet.GetCellValue(j, 5).ToString();
                textBox6.Text = sheet.GetCellValue(j, 6).ToString();

                // 取得欄位修改內容
                j = 2;
                for (int i = 9; i <= range.RowCount; i++)
                {
                    if (sheet.GetCellValue(j, i).IsEmpty == true)
                    {
                        break;
                    }
                    j = 1;
                    DataRow dr = dt.NewRow();
                    dr["Item"] = sheet.GetCellValue(j, i);
                    dr["Add"] = sheet.GetCellValue(++j, i);
                    dr["Delete"] = sheet.GetCellValue(++j, i);
                    dr["Change"] = sheet.GetCellValue(++j, i);
                    dr["OrgA8"] = sheet.GetCellValue(++j, i);

                    if(dr["OrgA8"].ToString().Equals("N/A")==false)
                    {
                        itemList.Add(dr["OrgA8"].ToString());
                    }

                    dr["NewA8"] = sheet.GetCellValue(++j, i);
                    if (dr["NewA8"].ToString().Equals("N/A") == false)
                    {
                        itemList.Add(dr["NewA8"].ToString());
                    }

                    dr["A8Name"] = sheet.GetCellValue(++j, i);
                    dr["OrgCount"] = sheet.GetCellValue(++j, i);
                    dr["NewCount"] = sheet.GetCellValue(++j, i);
                    dr["IsNew"] = sheet.GetCellValue(++j, i);
                    dr["IsRDConfirm"] = sheet.GetCellValue(++j, i);
                    dr["Owner"] = sheet.GetCellValue(++j, i);
                    dr["Module"] = sheet.GetCellValue(++j, i);
                    dr["Remark"] = sheet.GetCellValue(++j, i);
                    dt.Rows.Add(dr);
                }
            }

            
            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.RefreshData();


            // 判斷品號是否有不存在DB的資料, 如果有, 直接跳出訊息說明
            bool result = MvDbDao.checkData_hasIllegalItemInInvmb((string[])itemList.ToArray(typeof(string)));
            MessageBox.Show("resut is " + result);
        }

        private void sbGetBomP07_Click(object sender, EventArgs e)
        {
            differentCellCount = 0;
            string bomId = cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
            if (bomId.Length == 0)
            {
                MessageBox.Show("Please choice the bom");
                return;
            }

            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            DataSet sourceDs = new DataSet("SourceDs");
            MvBomCompareBo bo = new MvBomCompareBo();

            // collect bom source by bom id and show
            sourceDs = bo.CollectSourceDsProcess_BomP07_Thin(new string[] { bomId }).Copy();
            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            //bo.filterBomP07ColumnByPdRule(ref sourceDt1);
            gridControl1.DataSource = sourceDt1;

            // calculate different between 品號 and 優先耗用
            foreach(DataRow dr in sourceDt1.Rows)
            {
                //if(!string.IsNullOrEmpty(dr["Column12"].ToString()) && !dr["Column12"].Equals(dr["A8"]))
                if(!string.IsNullOrEmpty(dr["MB004_FIRST"].ToString()) && !dr["MB004_FIRST"].Equals(dr["MD003"]))
                {
                    differentCellCount += 1;
                }
            }

            // update columns caption
            setGridViewColumnsCaption(ref gridView1);

            // close Wait Form
            SplashScreenManager.CloseForm(false);
            labelControl1.Text = string.Format("相異總數 : {0}", differentCellCount);
        }

        private void frmTestParser_Load(object sender, EventArgs e)
        {
            // 第一次取得BomList
            DataTable tmpDt = null;
            tmpDt = MvDbDao.collectData_BomList();
            cboBomType.DataSource = tmpDt.Copy();
            cboBomType.ValueMember = "MC001";
            cboBomType.DisplayMember = "MB0012";
            cboBomType.Text = "";
            cboBomType.SelectedValue = "";

            // 調整GridControl 為不可編輯
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.RefreshData();

            // 第一次取得單別
            tmpDt = MvDbDao.collectData_Cmsmq();
            cboOrderType.DataSource = tmpDt.Copy();
            cboOrderType.ValueMember = "MQ001";
            cboOrderType.DisplayMember = "MQ001";
            cboOrderType.Text = "";
            cboOrderType.SelectedValue = "";

            deStart.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deEnd.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            deStart.DateTime = DateTime.Today;
            deEnd.DateTime = DateTime.Today;

        }

        private void setGridViewColumnsCaption(ref GridView view)
        {
            if (view.Columns.Count != 9)
            {
                return;
            }

            int index = 0;
            foreach (string header in DefinedHeader.BomP07ReportHeaderAliasName)
            {
                view.Columns[index].Caption = header;
                index++;
            }
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (View.Columns.Count != 12)
            {
                return;
            }
            if (e.RowHandle >= 0)
            {
                string A8 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MD003"]);
                string Column12 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["MB004_FIRST"]);
                if ((Column12.Length != 0) && !(A8.Equals(Column12)))
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void frmTestParser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void sbCheckInvmb_Click(object sender, EventArgs e)
        {

            string[] tmpString = new string[3];
            tmpString[0] = "123";
            tmpString[1] = "1231";
            tmpString[2] = "1234";

            bool result = MvDbDao.checkData_hasIllegalItemInInvmb(tmpString);
            MessageBox.Show("resut is " + result);

        }

        private void sbGetSumByOrderType_Click(object sender, EventArgs e)
        {
            if (deStart.Text.Length == 0 || deEnd.Text.Length == 0)
            {
                MessageBox.Show("請先選取日期區間");
                return;
            }

            // 判斷start date 不能比end date晚
            DateTime dStart = DateTime.Parse(deStart.Text);
            DateTime dEnd = DateTime.Parse(deEnd.Text);
            if (DateTime.Compare(dStart, dEnd) > 0)
            {
                MessageBox.Show("起始日不可大於結束日");
                return;
            }

            // 判斷區間不要超過一個月
            TimeSpan ts = dEnd - dStart;
            int differenceInDays = ts.Days;
            if (differenceInDays > 180)
            {
                MessageBox.Show("日期區間請不要超過6個月");
                return;
            }

            // 判斷單別必需要有值
            if (cboOrderType.Text.Length == 0)
            {
                MessageBox.Show("請選取單別");
                return;
            }

            // 清除grid controll 資料
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridView1.RefreshData();

            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            // 取得單別的統計資料 
            DataTable dt = null;
            dt = MvDbDao.collectData_GetOrderTypeSummary(cboOrderType.Text, deStart.Text, deEnd.Text);

            if(dt == null)
            {
                MessageBox.Show("此單別尚未列入計算清單內, 或是該單別為已停用單別");
                return;
            }

            gridControl1.DataSource = dt;
            // close Wait Form
            SplashScreenManager.CloseForm(false);
        }

        private void sbCreateA121_Click(object sender, EventArgs e)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();


            //using (TransactionScope scope = new TransactionScope())
            //{
            //    SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MVTEST;
            //    SqlCommand command = connection.CreateCommand();
            //    try
            //    {
            //        command.CommandTimeout = 3;
            //        connection.Open();

            //        // 新增INVTA
            //        sb.AppendLine(string.Format(@"SELECT TOP 0 * from MVTEST.dbo.INVTA"));
            //        command.CommandText = sb.ToString();
            //        majorData = MvDbConnector.queryDataBySql(command);

            //        //做完以後
            //        scope.Complete();
            //    }
            //    catch (SqlException se)
            //    {
            //        //發生例外時，會自動rollback
            //        throw se;
            //    }
            //    finally
            //    {
            //        command.Dispose();
            //        connection.Close();
            //        connection.Dispose();
            //    }
            //}

            // using sql bulkcopy
            DataRow dr = null;

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MVTEST;

                try
                {
                    connection.Open();

                    // Get Data schema INVTA
                    sb.AppendLine(string.Format(@"SELECT TOP 0 * from MVTEST.dbo.INVTA"));
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());

                    // add simulate data
                    dr = MvDbDao.simulateData_INVTA(majorData);
                    majorData.Rows.Add(dr);

                    // use BulkCopy insert to DB
                    using (SqlBulkCopy sbc = new SqlBulkCopy(connection))
                    {
                        // set number of records to be processed
                        sbc.BatchSize = 300;
                        sbc.DestinationTableName = ErpTableName.INVTA.ToString();

                        // Add column mappings
                        foreach (DataColumn column in majorData.Columns)
                        {
                            sbc.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                        }

                        // write to server
                        sbc.WriteToServer(majorData);
                    }

                    // Get Data schema INVTB
                    sb.Clear();
                    sb.AppendLine(string.Format(@"SELECT TOP 0 * from MVTEST.dbo.INVTB"));
                    majorData.Clear();
                    majorData.Columns.Clear();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());

                    // add simulate data
                    dr = MvDbDao.simulateData_INVTB(connection, majorData, "10101001");
                    majorData.Rows.Add(dr);

                    // use BulkCopy insert to DB
                    using (SqlBulkCopy sbc = new SqlBulkCopy(connection))
                    {
                        // set number of records to be processed
                        sbc.BatchSize = 300;
                        sbc.DestinationTableName = ErpTableName.INVTB.ToString();

                        // Add column mappings
                        foreach (DataColumn column in majorData.Columns)
                        {
                            sbc.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                        }

                        // write to server
                        sbc.WriteToServer(majorData);
                    }

                    scope.Complete();
                }
                catch (SqlException se)
                {
                    // 發生例外時，會自動rollback
                    throw se;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            sb = null;
        }
    }
}
