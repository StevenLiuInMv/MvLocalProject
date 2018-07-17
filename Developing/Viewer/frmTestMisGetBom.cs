using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MvLocalProject.Bo;
using MvLocalProject.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmTestMisGetBom : Form
    {
        public frmTestMisGetBom()
        {
            InitializeComponent();
        }

        bool isInitialBomList = true;
        Hashtable[] hashTreeListBackColor = new Hashtable[3];

        private void sbtnGetBomList_Click_org(object sender, EventArgs e)
        {
            if (isInitialBomList == true)
            {
                // 第一次取得BomList
                DataTable tmpDt = null;
                tmpDt = MvDbDao.collectData_BomList();
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

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            // get source data set
            DataSet sourceDs;
            sourceDs = bo.GetDevDataSet_BomP07_VB6(result, false).Copy();
            DataTable sourceDt = sourceDs.Tables[result].Copy();

            treeList1.DataSource = sourceDt.Clone();
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, sourceDt, ref hashTreeListBackColor[0], false);
            setColumnsCaption(ref treeList1);

            // get source data set by 易全哥
            DataSet sourceDsThin;
            sourceDsThin = bo.GetDevDataSet_BomP09_Thin(result, false).Copy();
            DataTable sourceDtThin = sourceDsThin.Tables[result].Copy();

            treeList2.DataSource = sourceDtThin.Clone();
            hashTreeListBackColor[1].Clear();
            showTreeListByLevel(treeList2, sourceDtThin, ref hashTreeListBackColor[1], false);
            setColumnsCaption(ref treeList2);

            // Sheet1,2 不開放編輯功能, treeList 1,2,3
            treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;
            
            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;

            //Close Wait Form
            SplashScreenManager.CloseForm(false);

        }

        private void sbtnGetBomList_Click(object sender, EventArgs e)
        {
            if (isInitialBomList == true)
            {
                // 第一次取得BomList
                DataTable tmpDt = null;
                tmpDt = MvDbDao.collectData_BomList();
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

            // initial hash tables
            for (int i = 0; i < hashTreeListBackColor.Length; i++)
            {
                hashTreeListBackColor[i] = new Hashtable();
            }

            // get source data set by stored procedure
            DataSet sourceDs;
            sourceDs = bo.GetDevDataSet_BomP09_Thin(result, false).Copy();
            DataTable sourceDt = sourceDs.Tables[result].Copy();

            treeList1.DataSource = sourceDt.Clone();
            hashTreeListBackColor[0].Clear();
            showTreeListByLevel(treeList1, sourceDt, ref hashTreeListBackColor[0], false);
            setColumnsCaption(ref treeList1);

            // get source data set by optional data
            DataSet sourceDsOptional;
            sourceDsOptional = bo.GetDevDataSet_BomP09_Thin(result, false).Copy();
            DataTable sourceDtOptional = sourceDsOptional.Tables[result].Copy();

            treeList2.DataSource = sourceDtOptional.Clone();
            hashTreeListBackColor[1].Clear();
            showTreeListByLevel(treeList2, sourceDtOptional, ref hashTreeListBackColor[1], false);
            setColumnsCaption(ref treeList2);

            // Sheet1,2 不開放編輯功能, treeList 1,2,3
            //treeList1.OptionsBehavior.ReadOnly = true;
            treeList2.OptionsBehavior.ReadOnly = true;

            treeList1.OptionsBehavior.Editable = false;
            treeList2.OptionsBehavior.Editable = false;

            // Sheet1 show checkbox
            treeList1.OptionsView.ShowCheckBoxes = true;
            
            treeList1.OptionsBehavior.AllowRecursiveNodeChecking = true;

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

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int treeListIndex = e.Node.Id;
            if (hashTreeListBackColor[0].Contains(treeListIndex))
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

        private void setColumnsCaption(ref DevExpress.XtraTreeList.TreeList tl)
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

        private void sbDebug_Click(object sender, EventArgs e)
        {
            List<int> numbers = new List<int>() { 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

            IEnumerable<int> queryFactorsOfFour =
                from num in numbers
                where num % 4 == 0
                select num;


            // Store the results in a new variable
            // without executing a foreach loop.
            List<int> factorsofFourList = queryFactorsOfFour.ToList();

            // Keep the console window open in debug mode.
            foreach(int result in factorsofFourList)
            {
                Console.WriteLine(result);
            }
        }

        private void frmTestMisGetBom_Load(object sender, EventArgs e)
        {
            isInitialBomList = true;
            sbtnGetBomList_Click(sender, e);
        }

        private void sbConvertToMoc_Click(object sender, EventArgs e)
        {
            foreach (TreeListNode node in treeList1.Nodes)
            {
                showNodes(node);
            }
        }


        private void showNodes(TreeListNode sourceNode)
        {
            bool hasChildren = sourceNode.HasChildren;
            TreeListNodes nodes;

            Console.WriteLine(sourceNode.Checked + " " + sourceNode[0].ToString() + " " + sourceNode[1].ToString() + " " + sourceNode[2].ToString() + " " + sourceNode[3].ToString() + " " + sourceNode[4].ToString() + " " + sourceNode[5].ToString());
            if (hasChildren == false)
            {
                return;
            }

            nodes = sourceNode.Nodes;
            foreach (TreeListNode node in nodes)
            {
                showNodes(node);
            }
            return;
        }

        private void treeList2_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.C)
            //{
            //    TreeList treeList = (TreeList)sender;
            //    Clipboard.SetText(treeList.FocusedNode.GetDisplayText(treeList.FocusedColumn));
            //    e.Handled = true;
            //}
        }

        private void sbCopyCell_Click(object sender, EventArgs e)
        {
            TreeList treeList = treeList2;
            Clipboard.SetText(treeList.FocusedNode.GetDisplayText(treeList.FocusedColumn));
            textBox1.Text = Clipboard.GetText();
        }
    }
}
