using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MvLocalProject.Controller;
using MvLocalProject.Model;
using MvLocalProject.Bo;

namespace MvLocalProject.Viewer
{
    public partial class frmBomCompare : Form
    {
        frmProgress progress = new frmProgress();

        public frmBomCompare()
        {
            InitializeComponent();
        }

        private void frmBomCompare_Load(object sender, EventArgs e)
        {
            enableObject(true, false);

            // Lambda語法
            AsyncTemplate.OnInvokeStarting =
                () => { progress.ShowDialog(); };

            AsyncTemplate.OnInvokeEnding =
                () =>
                {
                    if (progress.InvokeRequired) { progress.Invoke(new MethodInvoker(() => { progress.Close(); })); }
                    else { progress.Close(); }
                };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (BomView.Items.Count > 1)
            {
                MessageBox.Show("Please remove item first");
                return;
            }

            string result = cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
            if (result.Length == 0) { return; }

            ListViewItem lvi = new ListViewItem(result);
            lvi.SubItems.Add(cboBomType.Text.Remove(0, result.Length + 1));
            BomView.Items.Add(lvi);
            BomView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            BomView.Refresh();
        }

        private void btnGetBomList_Click(object sender, EventArgs e)
        {
            DataTable tmpDt = null;
            tmpDt = MvDbDao.collectData_BomList();
            cboBomType.DataSource = tmpDt;
            cboBomType.ValueMember = "MC001";
            cboBomType.DisplayMember = "MB0012";
            cboBomType.Text = "";
            cboBomType.SelectedValue = "";

            BomView.Columns.Add("BomId");
            BomView.Columns.Add("BomName");
            BomView.View = View.Details;

            enableObject(true, false);
            btnGetBomList.Visible = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (BomView.Items.Count < 1) { return; }
            for (int i = 0; i < BomView.Items.Count; i++)
            {
                if (BomView.Items[i].Selected)
                {
                    BomView.Items[i].Remove();
                    i--;
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            bool isFinishedCompressProcess = false;
            int cacheErrorResult = int.MaxValue;

            // initial dataGridView
            initialDataGridView();
            initialTabControl();

            if (BomView.Items.Count < 2)
            {
                MessageBox.Show("請選擇2個bom表");
                return;
            }
            enableObject(false, true);

            // 取得已選取的list
            List<string> selectedList = new List<string>();
            foreach (var item in BomView.Items)
            {
                string row = (item as ListViewItem).Text;
                selectedList.Add(row);
            }

            // 顯示progress bar
            DataSet sourceDs = new DataSet();
            DataSet compareAB = new DataSet();
            DataSet compareBA = new DataSet();
            DataTable illegalData = new DataTable();
            DataTable summaryDt = new DataTable();
            progress.Message = "In progress, please wait... ";

            AsyncTemplate.DoWorkAsync(
                () =>
                {
                    MvBomCompareBo bo = new MvBomCompareBo();
                    return bo.CompareProcess(selectedList.ToArray<string>(), ref sourceDs, ref compareAB, ref compareBA, ref illegalData, ref summaryDt);
                },
                (result) =>
                {
                    cacheErrorResult = result;
                    switch (result)
                    {
                        case 0:
                            //成功
                            isFinishedCompressProcess = true;
                            break;
                        case -1:
                            MessageBox.Show(string.Format("One or all data from bom's id is empty {0}Bom1 Id : {1}{0}Bom2 Id : {2}{0}", Environment.NewLine, selectedList[0], selectedList[1]));
                            break;
                        case -2:
                        case -3:
                            MessageBox.Show("compare error");
                            break;
                        default:
                            MessageBox.Show("undefined error");
                            break;
                    }
                    
                },
                (exception) =>
                {
                    //error handling
                    MessageBox.Show(exception.Message);
                });

            // 如果前段取資料有問題, 直接return
            if (isFinishedCompressProcess == false)
            {
                enableObject(true, true);
                return;
            }
            // 填入資料與後製畫面
            tabControl1.TabPages[0].Text = sourceDs.Tables[0].TableName;
            tabControl1.TabPages[1].Text = sourceDs.Tables[1].TableName;
            tabControl1.TabPages[2].Text = string.Format("{0} -> {1}", sourceDs.Tables[0].TableName, sourceDs.Tables[1].TableName);
            tabControl1.TabPages[3].Text = string.Format("{0} <- {1}", sourceDs.Tables[0].TableName, sourceDs.Tables[1].TableName);
            tabControl1.TabPages[4].Text = "Summary";

            // 填入 A比對B 的結果
            dgvABSame.DataSource = compareAB.Tables["Same"];
            dgvABDiff.DataSource = compareAB.Tables["Different"];
            // 填入 B比對A 的結果
            dgvBASame.DataSource = compareBA.Tables["Same"];
            dgvBADiff.DataSource = compareBA.Tables["Different"];

            // 填入 source 及 illegal
            dgvBomA.DataSource = sourceDs.Tables[0];
            dgvBomB.DataSource = sourceDs.Tables[1];
            dgvIllegal.DataSource = illegalData;

            // 填入 summary
            dgvSummary.DataSource = summaryDt;
            dgvSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvSummary.Columns["項目"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSummary.Columns["項目"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSummary.Columns["新增"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSummary.Columns["刪除"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSummary.Columns["變更"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 啟用TabControl
            enableObject(true, true);
            initialDataGridView(true);
        }




        private void CompareProcess_Org(string[] selectedList)
        {
            DataTable illegalData = null;
            DataSet sourceDs = new DataSet();
            DataSet tmpDs = new DataSet();

            MvBomCompareBo bo = new MvBomCompareBo();
            sourceDs = bo.CollectSourceDsProcess_BomP09_VB6(selectedList);
            if (sourceDs.Tables.Count <= 1)
            {
                MessageBox.Show(string.Format("One or all data from bom's id is empty {0}Bom1 Id : {1}{0}Bom2 Id : {2}{0}", Environment.NewLine, selectedList[0], selectedList[1]));
                return;
            }

            // initial tablControl pages name
            tabControl1.TabPages[0].Text = sourceDs.Tables[0].TableName;
            tabControl1.TabPages[1].Text = sourceDs.Tables[1].TableName;
            tabControl1.TabPages[2].Text = string.Format("{0} -> {1}", sourceDs.Tables[0].TableName, sourceDs.Tables[1].TableName);
            tabControl1.TabPages[3].Text = string.Format("{0} <- {1}", sourceDs.Tables[0].TableName, sourceDs.Tables[1].TableName);
            tabControl1.TabPages[4].Text = "Summary";

            // 比對 A -> B
            tmpDs = bo.compareBomByRulePur(sourceDs.Tables[0], sourceDs.Tables[1]);
            if ((tmpDs == null) || (tmpDs.Tables.Count == 0))
            {
                MessageBox.Show("compare error");
                enableObject(true, true);
                return;
            }

            // rename table's column name
            int i = 0;
            foreach (MvHeader header in CompareBom09Header.Values)
            {
                tmpDs.Tables["Same"].Columns[i].ColumnName = header.AliasName();
                tmpDs.Tables["Different"].Columns[i].ColumnName = header.AliasName();
                i++;
            }

            // 開始填入A 比對B的結果
            dgvABSame.DataSource = tmpDs.Tables["Same"];
            dgvABDiff.DataSource = tmpDs.Tables["Different"];
            illegalData = tmpDs.Tables["Illegal"].Copy();

            // 比對 B -> A
            tmpDs = bo.compareBomByRulePur(sourceDs.Tables[1], sourceDs.Tables[0], true);
            if ((tmpDs == null) || (tmpDs.Tables.Count == 0))
            {
                MessageBox.Show("compare error");
                enableObject(true, true);
                return;
            }

            // Merge illegal的talbe 資料
            illegalData.Merge(tmpDs.Tables["Illegal"].Copy());

            // rename table's column name
            i = 0;
            foreach (MvHeader header in CompareBom09Header.Values)
            {
                tmpDs.Tables["Same"].Columns[i].ColumnName = header.AliasName();
                tmpDs.Tables["Different"].Columns[i].ColumnName = header.AliasName();
                i++;
            }

            // 開始填入B 比對A的結果
            dgvBASame.DataSource = tmpDs.Tables["Same"];
            dgvBADiff.DataSource = tmpDs.Tables["Different"];

            // rename source table and illegal talbe column's name
            i = 0;
            foreach (string header in DefinedHeader.Bom09HeaderAliasNameNoPrice)
            {
                sourceDs.Tables[0].Columns[i].ColumnName = header;
                sourceDs.Tables[1].Columns[i].ColumnName = header;
                illegalData.Columns[i].ColumnName = header;
                i++;
            }

            // 開始填入 source
            dgvBomA.DataSource = sourceDs.Tables[0];
            dgvBomB.DataSource = sourceDs.Tables[1];
            dgvIllegal.DataSource = illegalData;
            return;
        }


        private void enableObject(bool enableGroup, bool enableTabControl)
        {
            groupBom.Enabled = enableGroup;
            tabControl1.Enabled = enableTabControl;
        }

        private void initialDataGridView(bool onlyReresh = false)
        {
            if (onlyReresh ==false)
            {
                // initial dataGridView
                dgvBomA.DataSource = null;
                dgvBomB.DataSource = null;
                dgvABSame.DataSource = null;
                dgvABDiff.DataSource = null;
                dgvBASame.DataSource = null;
                dgvBADiff.DataSource = null;
                dgvIllegal.DataSource = null;
                dgvSummary.DataSource = null;
                dgvBomA.ReadOnly = true;
                dgvBomB.ReadOnly = true;
                dgvABSame.ReadOnly = true;
                dgvABDiff.ReadOnly = true;
                dgvBASame.ReadOnly = true;
                dgvBADiff.ReadOnly = true;
                dgvIllegal.ReadOnly = true;
                dgvSummary.ReadOnly = true;
            }

            dgvBomA.ScrollBars = ScrollBars.Both;
            dgvBomB.ScrollBars = ScrollBars.Both;
            dgvABSame.ScrollBars = ScrollBars.Both;
            dgvABDiff.ScrollBars = ScrollBars.Both;
            dgvBASame.ScrollBars = ScrollBars.Both;
            dgvBADiff.ScrollBars = ScrollBars.Both;
            dgvIllegal.ScrollBars = ScrollBars.Both;
            dgvSummary.ScrollBars = ScrollBars.Both;

            dgvBomA.Refresh();
            dgvBomB.Refresh();
            dgvABSame.Refresh();
            dgvABDiff.Refresh();
            dgvBASame.Refresh();
            dgvBADiff.Refresh();
            dgvIllegal.Refresh();
            dgvSummary.Refresh();
        }

        private void initialTabControl()
        {
            for (int i = 0; i < 5; i++)
            {
                tabControl1.TabPages[i].Text = string.Format("tabPage{0}", i + 1);
            }
            tabControl1.Refresh();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = (DataTable) dgvABDiff.DataSource;

            ds.Tables.Add(dt);
        }
    }
}
