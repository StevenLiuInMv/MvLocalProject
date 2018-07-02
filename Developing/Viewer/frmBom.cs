using MvLocalProject.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MvLocalProject.Bo;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;
using MvLocalProject.Model;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using NCalc;

namespace MvLocalProject.Viewer
{
    public partial class frmBom : Form
    {
        bool isInitialBomList = true;
        Hashtable[] hashTreeListBackColor = new Hashtable[3];
        public frmBom()
        {
            InitializeComponent();
        }

        private void sbtnGetBomList_Click(object sender, EventArgs e)
        {
            if (isInitialBomList == true)
            {
                // 第一次取得BomList
                DataTable tmpDt = null;
                tmpDt = MvDbDao.collectData_BomP09_BomList();
                cboBomType.DataSource = tmpDt;
                cboBomType.ValueMember = "MC001";
                cboBomType.DisplayMember = "MB0012";
                cboBomType.Text = "";
                cboBomType.SelectedValue = "";
                isInitialBomList = false;
                return;
            }

            string result = cboBomType.SelectedValue == null ? "" : cboBomType.SelectedValue.ToString();
            if (result.Length == 0)
            {
                MessageBox.Show("Please choice the bom");
                return;
            }
            // show wait process
            SplashScreenManager.ShowDefaultWaitForm();

            MvBomCompareBo bo = new MvBomCompareBo();
            DataSet sourceDs;

            // get source data set
            //sourceDs = bo.GetBomP07InfoByDev(result, false).Copy();
            sourceDs = bo.GetBomP07ThinInfoByDev(result, false).Copy();
            DataTable sourceDt = sourceDs.Tables[result].Copy();
            DataTable filterDt = sourceDs.Tables[result + "_Filter"].Copy();
            DataTable mocDt;

            // convert bom to moc
            mocDt = bo.convertBomToMoc(filterDt);

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            treeList1.DataSource = sourceDt.Clone();
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, sourceDt, ref hashTreeListBackColor[0], false);
            setColumnsCaption(ref treeList1);

            // 第2個Table的特別處理
            filterDt.Columns.Remove("AmountSpace");
            treeList2.DataSource = filterDt.Clone();
            hashTreeListBackColor[1].Clear();
            //showTreeListByLevel(treeList2, filterDt, ref hashTreeListBackColor[1], true);
            showTreeListByLevel(treeList2, filterDt, ref hashTreeListBackColor[1], false);
            setColumnsCaption(ref treeList2);

            // 第3個Table的特別處理
            mocDt.Columns.Remove("MD006");
            mocDt.Columns["RealAmount"].SetOrdinal(5);
            mocDt.Columns["ModuleLv1"].SetOrdinal(7);
            treeList3.DataSource = mocDt.Clone();
            hashTreeListBackColor[2].Clear();
            //showTreeListByLevel(treeList3, mocDt, ref hashTreeListBackColor[2], true);
            showTreeListByLevel(treeList3, mocDt, ref hashTreeListBackColor[2], false);
            setColumnsCaption(ref treeList3);

            xtraTabControl1.TabPages[0].Text = sourceDt.TableName;
            xtraTabControl1.TabPages[1].Text = sourceDt.TableName + "_標準製令";

            // Sheet1,2 不開放編輯功能, treeList 1,2,3
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;
            treeList3.OptionsBehavior.ReadOnly = true;

            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;
            treeList3.OptionsBehavior.Editable = false;

            //Close Wait Form
            SplashScreenManager.CloseForm(false);

        }

        private void showTreeListByLevel(TreeList tl, DataTable dt, ref Hashtable hashTreeListBackColor, bool isChangeLv)
        {
            TreeListNode[] parentNodeList = new TreeListNode[8];
            TreeListNode parentNode = null;
            int preLV = 0;
            int nowLv = 1;
            int changeColorIndex = 0;
            string strNowLv = string.Empty;
            string buyType = string.Empty;
            string A8 = string.Empty;


            foreach (DataRow dr in dt.Rows)
            {
                strNowLv = dr["LV"].ToString().Trim().Replace(".", "");
                nowLv = int.Parse(strNowLv);
                buyType = dr["MB025X"].ToString().Trim();
                A8 = dr["A8"].ToString().Trim();

                if (A8.Equals("21502121"))
                {
                    Console.WriteLine("test");
                }

                if (isChangeLv == true)
                {
                    dr["LV"] = "L" + strNowLv.ToString();
                }

                // 記錄是否要改變顏色
                if (buyType.Equals("模組"))
                {
                    hashTreeListBackColor[changeColorIndex] = true;
                }

                if (preLV < nowLv) // 1 -> 2
                {
                    // 要加入Node
                    parentNode = parentNodeList[preLV];
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                }
                else if (preLV == nowLv)
                {
                    // parentNode 要設定上一層
                    parentNode = parentNodeList[nowLv - 1];
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                }
                else if (preLV > nowLv) // 2 -> 1
                {
                    // parentNode 要設定Parent的上一層, 並清空該層
                    parentNode = parentNodeList[nowLv - 1];
                    parentNodeList[preLV] = null;
                    parentNodeList[nowLv] = tl.AppendNode(dr.ItemArray, parentNode);
                    //tl.AppendNode(dr.ItemArray, parentNode);
                    
                }
                preLV = nowLv;
                changeColorIndex++;
            }
        }

        private void frmBom_Load(object sender, EventArgs e)
        {
            isInitialBomList = true;
            // initial Bar

            BarManager barManager = new BarManager();
            barManager.Form = this;
            barManager.BeginUpdate();
            Bar bar1 = new Bar(barManager, "My MainMenu");
            bar1.DockStyle = BarDockStyle.Top;
            barManager.MainMenu = bar1;

            // Create bar items for the bar1 and bar2
            BarSubItem subMenuFile = new BarSubItem(barManager, "File");
            BarSubItem subMenuView = new BarSubItem(barManager, "View");

            BarButtonItem buttonSaveOrgGrid = new BarButtonItem(barManager, "Save Orginal Bom as Excel ...");
            BarButtonItem buttonSaveFilterGrid = new BarButtonItem(barManager, "Save Filter Bom as Excel ...");
            BarButtonItem buttonSaveAll = new BarButtonItem(barManager, "Save All as Excel ...");

            BarButtonItem buttonExpandAll = new BarButtonItem(barManager, "ExpandAll");
            BarButtonItem buttonCollapseAll = new BarButtonItem(barManager, "CollapseAll");

            subMenuFile.AddItems(new BarItem[] { buttonSaveOrgGrid, buttonSaveFilterGrid });
            subMenuView.AddItems(new BarItem[] { buttonExpandAll, buttonCollapseAll });

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subMenuFile, subMenuView });

            // A handler to process clicks on bar items
            barManager.ItemClick += new ItemClickEventHandler(barManager_ItemClick);

            barManager.EndUpdate();

            // initial BomList
            sbtnGetBomList_Click(sender, e);

        }

        void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarSubItem subMenu = e.Item as BarSubItem;
            if (subMenu != null) { return; }

            TreeList tmpTl = null;
            if (e.Item.Caption.Equals("Save Orginal Bom as Excel ..."))
            {
                tmpTl = treeList1;
            }
            else if (e.Item.Caption.Equals("Save Filter Bom as Excel ..."))
            {
                tmpTl = treeList2;
            }
            else if (e.Item.Caption.Equals("Save All as Excel ..."))
            {
                // unfinished
            }
            else if (e.Item.Caption.Equals("ExpandAll"))
            {
                treeList1.ExpandAll();
                treeList2.ExpandAll();
                treeList3.ExpandAll();
                return;
            }
            else if (e.Item.Caption.Equals("CollapseAll"))
            {
                treeList1.CollapseAll();
                treeList2.CollapseAll();
                treeList3.CollapseAll();
                return;
            }


            // 往下跑, 目前只有存檔功能, 用tmpTreeList看有沒有指定來判斷
            if (tmpTl != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel 活頁簿 |*.xlsx";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName == "") { return; }
                string filePathAndName = saveFileDialog1.FileName;

                try
                {
                    tmpTl.ExportToXlsx(filePathAndName);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, filePathAndName));
                    return;
                }
                MessageBox.Show(string.Format("Save as path {0}{1}", Environment.NewLine, filePathAndName));
            }
        }

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if(hashTreeListBackColor[0].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void treeList2_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[1].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void treeList3_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[2].Contains(treeListIndex))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }


        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            string workingDirectory = @"D:\99_TempArea\";
            string fileNameAndPath = string.Format("{0}{1}_{2}.xlsx", workingDirectory, DefinedReport.ErpReportType.BomP09_RD.ToString(),  DateTime.Now.ToString("yyyyMMdd"));
            try
            {
                treeList1.ExportToXlsx(fileNameAndPath);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("please close the file first, then do save excel again.{0}{1}", Environment.NewLine, fileNameAndPath));
                return;
            }
            MessageBox.Show(string.Format("Save the file in below path {0}{1}", Environment.NewLine, fileNameAndPath));
        }

        private void setColumnsCaption(ref TreeList tl)
        {
            tl.Columns[0].Caption = "階次";
            tl.Columns[1].Caption = "元件品號";
            tl.Columns[2].Caption = "品號屬性";
            tl.Columns[3].Caption = "品名";
            tl.Columns[4].Caption = "單位";
            tl.Columns[5].Caption = "數量";
            tl.Columns[6].Caption = "SOP圖號";
            if (tl.Columns.Count > 7)
            {
                tl.Columns[7].Caption = "模組";
            }
        }

        private void cboBomType_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboBomType.SelectedIndex < 0) { return; }
            if (e.KeyCode == Keys.Enter) { sbtnGetBomList_Click(sender, e); }
        }

        private void frmBom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
