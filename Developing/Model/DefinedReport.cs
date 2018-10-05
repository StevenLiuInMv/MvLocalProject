using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvLocalProject.Model
{
    internal static class DefinedReport
    {
        public enum ErpReportType
        {
            NONE,
            MocP10Auto,
            BomP09,
            BomP09_Multi,
            BomP09_RD
        }
    }

    public enum ErpNoteHead
    {
        H_3101 = 3101,
        H_3102 = 3102,
        H_3103 = 3103,
        H_3104 = 3104,
        H_3105 = 3105,
        H_3109 = 3109,
        H_A121 = 121,
        H_A12E = 1214,
        None = 9487
    }

    public static class CustomStructure
    {
        public static readonly string[] ExcelPrDtCaption = new string[] { "專案代號", "請購人員", "品號", "品名", "規格/型號", "需求數量", "進貨倉別", "需求日期", "供應商", "備註", "來源單號", "預計儲位", "急件" };
        /// <summary>
        /// William哥原開立PR使用的Excel格式
        /// </summary>
        /// <returns></returns>
        public static DataTable cloneExcelPrDtColumn()
        {
            DataTable excelDt = new DataTable();
            excelDt.Columns.Add("TB043");
            excelDt.Columns.Add("TA012");
            excelDt.Columns.Add("TB004");
            excelDt.Columns.Add("TB005");
            excelDt.Columns.Add("TB006");
            excelDt.Columns.Add("TB009");
            excelDt.Columns.Add("TB008");
            excelDt.Columns.Add("TB011");
            excelDt.Columns.Add("TB010");
            excelDt.Columns.Add("TB012");
            excelDt.Columns.Add("TB029");
            excelDt.Columns.Add("TB201");
            excelDt.Columns.Add("TA202");
            return excelDt;
        }
        
    }
}
