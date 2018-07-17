using System;
using System.Collections.Generic;

namespace MvLocalProject.Model
{

    internal interface IHeader
    {
        string Name();
        string AliasName();
    }

    public abstract class Header : IHeader
    {

        protected string _name = "NA";
        protected string _aliasName = "NA";

        public Header(string name, string aliasName)
        {
            _name = name;
            _aliasName = aliasName;
        }

        public string Name()
        {
            return _name;
        }

        public string AliasName()
        {
            return _aliasName;
        }
    }

    public class MvHeader : Header
    {
        public MvHeader(string name, string aliasName) : base("NA", "NA")
        {
            _name = name;
            _aliasName = aliasName;
        }
    }

    public sealed class DefinedHeader
    {
        public static readonly string[] Bom09HeaderAliasName = new string[] { "階次", "取替代料", "元件品號", "品號屬性", "二階", "三階", "四階", "品名", "材料", "尺寸", "後製程", "型號", "規格", "單位", "數量", "SOP圖號", "備料順序", "備註", "單價", "金額", "最近廠商", "前一版", "CNC", "3年前舊價", "國外廠商", "獨家廠商", "核價單" };
        public static readonly string[] Bom09HeaderAliasNameNoPrice = new string[] { "階次", "取替代料", "元件品號", "品號屬性", "二階", "三階", "四階", "品名", "材料", "尺寸", "後製程", "型號", "規格", "單位", "數量", "SOP圖號", "備料順序", "備註", "最近廠商", "前一版", "CNC", "3年前舊價", "國外廠商", "獨家廠商", "核價單" };
        public static readonly string[] Bom09SummaryHeaderNameNoPrice = new string[] { "No", "Add", "Delete", "Change", "CompareA8", "A8", "Column4", "OrgMD006", "MD006" };
        public static readonly string[] Bom09SummaryHeaderAliasNameNoPrice = new string[] { "項目", "新增", "刪除", "變更", "原料號", "變更/新增號", "品名", "原數量", "變更需求" };

        public static readonly string[] BomCompareHeaderName = new string[] { "CompareLV", "CompareA8", "CompareMD006", "LV", "A8", "MB025X", "Column4", "MB004", "MD006", "Column9", "ModuleLv1"};
        public static readonly string[] BomCompareHeaderAliasName = new string[] { "比對階層", "比對品號", "比對數量", "階次", "元件品號", "品號屬性", "品名", "單位", "數量", "SOP圖號", "模組" };
        public static readonly string[] BomCompareReportHeaderName = new string[] { "No", "Add", "Delete", "Change","CompareLV", "OrgA8", "NewA8", "Column4", "OrgMD006", "NewMD006", "IsNewBuid", "RDConfirm", "Owner", "ModuleLv1", "Remark" };
        public static readonly string[] BomCompareReportHeaderAliasName = new string[] { "項目", "新增", "刪除", "變更", "階層", "原料號", "變更/新增號", "品名", "原數量", "變更需求", "是否新建", "RD確認文件上傳", "負責人員", "模組", "備註" };
        public static readonly string[] BomCompareReportHeaderAliasNameNoLv = new string[] { "項目", "新增", "刪除", "變更", "原料號", "變更/新增號", "品名", "原數量", "變更需求", "是否新建", "RD確認文件上傳", "負責人員", "模組", "備註" };
        public static readonly int Bom09MaxLv = 7;
        public static readonly int Bom09MinLv = 1;
        public static readonly int BomP07_MaxSUBPN = 7;
        public static readonly string[] BomP07ReportHeaderAliasName = new string[] { "階次", "取替代料", "元件品號", "品號屬性", "品名", "單位", "數量", "SOP圖號", "優先耗用" };
    }


    public sealed class CompareBom09Header
    {
        public static readonly MvHeader CompareLV = new MvHeader("CompareLV", "比對階層");
        public static readonly MvHeader CompareA8 = new MvHeader("CompareA8", "比對品號");
        public static readonly MvHeader CompareMD006 = new MvHeader("CompareMD006", "比對數量");
        public static readonly MvHeader LV = new MvHeader("LV", "階次");
        public static readonly MvHeader A8 = new MvHeader("A8", "元件品號");
        public static readonly MvHeader Column4 = new MvHeader("Column4", "品名");
        public static readonly MvHeader Column8 = new MvHeader("Column8", "型號");
        public static readonly MvHeader MB003 = new MvHeader("MB003", "規格");
        public static readonly MvHeader MB004 = new MvHeader("MB004", "單位");
        public static readonly MvHeader MD006 = new MvHeader("MD006", "數量");
        public static readonly List<MvHeader> Values = new List<MvHeader>() { CompareLV, CompareA8, CompareMD006, LV, A8, Column4, Column8, MB003, MB004, MD006 };
    }

    public class MvErpDataModule_A121
    {
        private string ta004 = string.Empty;        // 部門代號
        private string ta005 = string.Empty;        // 備註
        private string ta008 = string.Empty;        // 廠別代號
        private string ta015 = string.Empty;        // 確認者
        private string tb004 = string.Empty;        // 品號
        private int tb007 = int.MinValue;           // 數量
        private string tb008 = string.Empty;        // 單位
        private string tb012 = string.Empty;        // 轉出庫
        private string tb013 = string.Empty;        // 轉入庫

        /// <summary>
        /// 部門代號
        /// </summary>
        public string TA004
        { get { return ta004; } set { ta004 = value; } }
        /// <summary>
        /// 備註
        /// </summary>
        public string TA005
        { get { return ta005; } set { ta005 = value; } }
        /// <summary>
        /// 廠別代號
        /// </summary>
        public string TA008
        { get { return ta008; } set { ta008 = value; } }
        /// <summary>
        /// 確認者
        /// </summary>
        public string TA015
        { get { return ta015; } set { ta015 = value; } }
        /// <summary>
        /// 品號
        /// </summary>
        public string TB004
        { get { return tb004; } set { tb004 = value; } }
        /// <summary>
        /// 數量
        /// </summary>
        public int TB007
        { get { return tb007; } set { tb007 = value; } }
        /// <summary>
        /// 單位
        /// </summary>
        public string TB008
        { get { return tb008; } set { tb008 = value; } }
        /// <summary>
        /// 轉出庫
        /// </summary>
        public string TB012
        { get { return tb012; } set { tb012 = value; } }
        /// <summary>
        /// 轉入庫
        /// </summary>
        public string TB013
        { get { return tb013; } set { tb013 = value; } }
    }
}
