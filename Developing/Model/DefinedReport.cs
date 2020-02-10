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
            excelDt.Columns.Add("TB043");   // PURTB
            excelDt.Columns.Add("TA012");   // PURTA
            excelDt.Columns.Add("TB004");   // PURTB
            excelDt.Columns.Add("TB005");   // PURTB
            excelDt.Columns.Add("TB006");   // PURTB
            excelDt.Columns.Add("TB009");   // PURTB
            excelDt.Columns.Add("TB008");   // PURTB
            excelDt.Columns.Add("TB011");   // PURTB
            excelDt.Columns.Add("TB010");   // PURTB
            excelDt.Columns.Add("TB012");   // PURTB
            excelDt.Columns.Add("TB029");   // PURTB
            excelDt.Columns.Add("TB201");   // PURTB
            excelDt.Columns.Add("TA202");   // PURTA
            excelDt.Columns.Add("MM002");   // INVMM
            excelDt.Columns.Add("MM003");   // INVMM
            excelDt.Columns.Add("MB004");   // INVMB
            excelDt.Columns.Add("TA201");   // MOCTA
            return excelDt;
        }
    }
}
