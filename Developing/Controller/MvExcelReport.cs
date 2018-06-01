using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Transactions;
using MvLocalProject.Model;

namespace MvLocalProject.Controller
{
    public class MvExcelReport
    {

        public bool generateMocP10Auto(string filePathAndName, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;

            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " create excel");
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            // 建立excel 文件
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return false;
            }

            // 建立Sheets and name
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);

            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            xlWorkBook.Worksheets[1].Name = "Detail";
            xlWorkBook.Worksheets[2].Name = "Summary";
            xlWorkBook.Worksheets[3].Name = "PR_Detail";
            xlWorkBook.Worksheets[4].Name = "PR_Summary";

            // 建立excel headers
            buildHeader_MocP10Auto(xlWorkBook);

            // fill content to sheet name "Detail"
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " create sheet Detail");
            dt = ds.Tables["Detail"];
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Detail"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dt, xlWorkSheet, "A5", false);

            // fill content to sheet name "Summary"
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " create sheet Summary");
            dt = ds.Tables["Summary"];
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Summary"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dt, xlWorkSheet, "A5", false);

            // fill content to sheet name "PR_Detail"
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " create sheet PR_Detail");
            dt = ds.Tables["PR_Detail"];
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Detail"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dt, xlWorkSheet, "A5", false);

            // fill content to sheet name "PR_Summary"
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " create sheet PR_Summary");
            dt = ds.Tables["PR_Summary"];
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Summary"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dt, xlWorkSheet, "A5", false);

            // save file to xlsx
            xlApp.DisplayAlerts = false;
            try
            {
                xlWorkBook.SaveAs(filePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + filePathAndName);
            }
            finally
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;

            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " end excel");
            return true;
        }

        public bool generateMocP10Auto(string filePathAndName)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dataTable = null;
            SqlConnection sqlConnection = null;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            // 建立excel 文件
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return false;
            }

            // 建立Sheets and name
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);

            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            xlWorkBook.Worksheets[1].Name = "Detail";
            xlWorkBook.Worksheets[2].Name = "Summary";
            xlWorkBook.Worksheets[3].Name = "PR_Detail";
            xlWorkBook.Worksheets[4].Name = "PR_Summary";

            // 建立excel headers
            Console.WriteLine("point 1 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            buildHeader_MocP10Auto(xlWorkBook);

            // fetch data from data base, sheet name "PR_Summary"
            sb.Clear();
            sb.Append(@"SELECT MACHNO, LOCATION, MOCNO, PRTOTAL, PRAPP, Round(PRAPP/PRTOTAL,3)*100, POTOTAL, POAPP, POP=CASE POTOTAL WHEN 0 THEN 0 ELSE Round(POAPP/POTOTAL,3)*100 END ")
                .Append("From TEMP.dbo.TMP_MOCP10PRSUM ORDER BY MACHNO ");

            try
            {
                sqlConnection = MvDbConnector.Connection_ERPDB2_Dot_TEMP;
                sqlConnection.Open();
                dataTable = MvDbConnector.queryDataBySql(sqlConnection, sb.ToString());
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.StackTrace);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }

            // fill content to sheet name "PR_Summary"
            Console.WriteLine("point 2 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Summary"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dataTable, xlWorkSheet, "A5", false);
            //for (int rowNdx = 0; rowNdx < dataTable.Rows.Count; rowNdx++)
            //{
            //    for (int colNdx = 0; colNdx < dataTable.Columns.Count; colNdx++)
            //    {
            //        xlWorkSheet.Cells[rowNdx + startRow, colNdx + startColumn] = GetString(dataTable.Rows[rowNdx][colNdx]);
            //    }
            //}
            Console.WriteLine("point 3 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));

            // =========================================================
            // fetch data from data base, sheet name "PR_Detail"

            sb.Clear();
            sb.Append(@"SELECT MACHNO, LOCATION, TB001, TB002, TB003, TA003, TA004, TA005, TA006, TB004, TB005, TB006, TB009, TB014, TB016, TB022, TB025, MOCSTATUS, POQTY, POSURE ")
                .Append("From TEMP.dbo.TMP_MOCP10PR WHERE MOCSTATUS <> 'Y' OR MOCSTATUS <>'y' ORDER BY MACHNO, TB004 ");

            try
            {
                sqlConnection = MvDbConnector.Connection_ERPDB2_Dot_TEMP;
                sqlConnection.Open();

                // read data from db
                dataTable = MvDbConnector.queryDataBySql(sqlConnection, sb.ToString());
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.StackTrace);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }

            Console.WriteLine("point 4 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            // fill content to sheet name "PR_Detail"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Detail"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dataTable, xlWorkSheet, "A5", false);
            Console.WriteLine("point 5 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            // =========================================================
            // fetch data from data base, sheet name "Summary"
            sb.Clear();
            sb.Append(@"SELECT TA201, TA200, TA024, TA025, '', '', TB012,'', TB004, TB005, FUNQTY, QTY_NOT, WKNODELV ")
                .Append("FROM ERPDB2.TEMP.dbo.TMP_MOCP10P ORDER BY TB003 ");

            try
            {
                sqlConnection = MvDbConnector.Connection_ERPDB2_Dot_TEMP;
                sqlConnection.Open();

                // read data from db
                dataTable = MvDbConnector.queryDataBySql(sqlConnection, sb.ToString());
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.StackTrace);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            Console.WriteLine("point 6 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            // fill content to sheet name "Summary"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Summary"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dataTable, xlWorkSheet, "A5", false);
            Console.WriteLine("point 7 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            // =========================================================
            // fetch data from data base, sheet name "Summary"
            sb.Clear();
            sb.Append(@"SELECT TA201, TA200, TA024, TA025, TA003, TA009,TA034, RTRIM(TB003) TB003, RTRIM(TB012), RTRIM(TB013), TB004, TB005, FUNQTY, QTY_NOT=CASE MB077 When '耗材' THEN 0 Else TB004-TB005-FUNQTY End, Rtrim(TB009), MB077, RVDATE, ASDATE ")
                .Append("FROM TEMP..TMP_MOCP10 WHERE TB003 <> '' ORDER BY TB003 ");

            try
            {
                sqlConnection = MvDbConnector.Connection_ERPDB2_Dot_TEMP;
                sqlConnection.Open();

                // read data from db
                dataTable = MvDbConnector.queryDataBySql(sqlConnection, sb.ToString());
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.StackTrace);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            Console.WriteLine("point 8 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
            // fill content to sheet name "Detail"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Detail"];
            xlWorkSheet.Cells.NumberFormatLocal = "@";
            xlWorkSheet.Cells.Font.Size = 10;
            fastFillDataIntoExcel(dataTable, xlWorkSheet, "A5", false);
            Console.WriteLine("point 9 : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));

            // =========================================================
            // save file to xlsx
            xlApp.DisplayAlerts = false;
            try
            {

                xlWorkBook.SaveAs(filePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + filePathAndName);
            }
            finally
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;

            return true;
        }


        
        private void buildHeader_MocP10Auto(Excel.Workbook xlWorkBook)
        {

            Excel.Worksheet xlWorkSheet;

            // build header for sheet name is "Detail"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Detail"];
            xlWorkSheet.Activate();
            xlWorkSheet.Cells[1, 6] = "MACHVISION";
            xlWorkSheet.Cells[2, 6] = "備料表入庫狀況表";
            string[] strArray = new string[] { "機號", "儲位", "單別", "單號", "開單日", "預計開工日", "產品名稱", "材料品號", "材料品名", "材料規格", "需領用量", "已領用量", "入庫量", "未到量", "倉別", "類別", "收料日", "調撥日" };
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 18]] = strArray;

            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[4, 18]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 18]].Font.Bold = false;

            // 設定凍結視窗
            xlWorkSheet.Application.ActiveWindow.SplitRow = 4;
            xlWorkSheet.Application.ActiveWindow.FreezePanes = true;
            // 設定自動篩選
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 18]].AutoFilter();

            // build header for sheet name is "Summary"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["Summary"];
            xlWorkSheet.Activate();
            xlWorkSheet.Cells[1, 6] = "MACHVISION";
            xlWorkSheet.Cells[2, 6] = "本日確認單據明細表";
            xlWorkSheet.Cells[3, 1] = "印表日期:";
            xlWorkSheet.Cells[3, 2] = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            strArray = new string[] { "機號", "儲位", "單別", "單號", "產品名稱", "材料品號", "材料品名", "材料規格", "需領用量", "已領用量", "入庫量", "未到量", "倉別" };
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 13]] = strArray;
            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[4, 13]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 13]].Font.Bold = false;


            // 設定凍結視窗
            xlWorkSheet.Application.ActiveWindow.SplitRow = 4;
            xlWorkSheet.Application.ActiveWindow.FreezePanes = true;
            // 設定自動篩選
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 12]].AutoFilter();

            // build header for sheet name is "PR_Detail"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Detail"];
            xlWorkSheet.Activate();
            xlWorkSheet.Cells[1, 6] = "MACHVISION";
            xlWorkSheet.Cells[2, 6] = "PR備料狀況表";
            xlWorkSheet.Cells[3, 1] = "印表日期:";
            xlWorkSheet.Cells[3, 2] = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            xlWorkSheet.Cells[3, 6] = "資料日期:";
            xlWorkSheet.Cells[3, 7] = "unfinished";
            strArray = new string[] { "機號", "儲位", "單別", "單號", "序號 ", "開單日", "部門", "母製令", "備註", "材料品號", "材料品名", "規格", " PR請購量 ", " PR採購量 ", "幣別", "採購單號", "PR確認", "製令狀態", "PO數量", "PO確認" };
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 20]] = strArray;
            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[4, 20]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 20]].Font.Bold = false;

            // 設定凍結視窗
            xlWorkSheet.Application.ActiveWindow.SplitRow = 4;
            xlWorkSheet.Application.ActiveWindow.FreezePanes = true;
            // 設定自動篩選
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 20]].AutoFilter();

            // build header for sheet name is PR_Summary
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets["PR_Summary"];
            xlWorkSheet.Activate();
            xlWorkSheet.Cells[1, 6] = "MACHVISION";
            xlWorkSheet.Cells[2, 6] = "本日確認單據明細表";
            xlWorkSheet.Cells[3, 1] = "印表日期:";
            xlWorkSheet.Cells[3, 2] = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            xlWorkSheet.Cells[3, 6] = "資料日期:";
            xlWorkSheet.Cells[3, 7] = "unfinished";
            strArray = new string[] { "機號", "儲位", "單別", "PR 筆數", "PR確認筆數", "PR比率%", "PO 筆數", "PO確認筆數", "PO比率%" };
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 9]] = strArray;
            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[4, 9]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 9]].Font.Bold = false;

            // 設定凍結視窗
            xlWorkSheet.Application.ActiveWindow.SplitRow = 4;
            xlWorkSheet.Application.ActiveWindow.FreezePanes = true;
            // 設定自動篩選
            xlWorkSheet.Range[xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 9]].AutoFilter();

        }

        private string GetString(object o)
        {
            if (o == null)
            {
                return "";
            }
            return o.ToString();
        }

        public static void fastFillDataIntoExcel(DataTable dt, Excel.Worksheet xlWorkSheet, string startCellName, bool includeColumnName)
        {
            // Copy the DataTable to an object array
            object[,] columnNameData = null;
            object[,] rawData = null;

            if (includeColumnName == true)
            {
                columnNameData = new object[1, dt.Columns.Count];
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    columnNameData[0, col] = dt.Columns[col].ColumnName;
                }
            }
            // Copy the column names to the first row of the object array
            rawData = new object[dt.Rows.Count, dt.Columns.Count];

            // Copy the values to the object array
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    rawData[row, col] = dt.Rows[row].ItemArray[col];
                }
            }

            // Calculate the final column letter
            string finalColLetter = string.Empty;
            string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int colCharsetLen = colCharset.Length;

            if (dt.Columns.Count > colCharsetLen)
            {
                finalColLetter = colCharset.Substring(
                    (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
            }

            finalColLetter += colCharset.Substring(
                    (dt.Columns.Count - 1) % colCharsetLen, 1);


            // Fast data export to Excel
            string strTemp = Regex.Replace(startCellName, "[^0-9]", "");
            int starCellColumnIndex = int.Parse(strTemp);

            if (includeColumnName == true)
            {
                // 貼header
                string excelRange = string.Format(startCellName + ":{0}{1}",
                    finalColLetter, starCellColumnIndex);
                xlWorkSheet.get_Range(excelRange, Type.Missing).Value2 = columnNameData;

                // 貼raw data
                strTemp = Regex.Replace(startCellName, "[0-9]", "");
                string tempCellName = strTemp + (starCellColumnIndex + 1).ToString();

                excelRange = string.Format(tempCellName + ":{0}{1}",
                    finalColLetter, dt.Rows.Count + starCellColumnIndex);
                xlWorkSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;
            }
            else
            {
                string excelRange = string.Format(startCellName + ":{0}{1}",
                    finalColLetter, dt.Rows.Count + starCellColumnIndex - 1);
                xlWorkSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;
            }
        }

        public static void writeExcelByDataSet(DataSet ds, string filePathAndName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet = null;

            // 建立excel 文件
            xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            // 建立Sheets and name
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);

            int sheetCount = 1;
            foreach (DataTable dt in ds.Tables)
            {
                xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                xlWorkBook.Worksheets[sheetCount].Name = dt.TableName;

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[dt.TableName];
                xlWorkSheet.Cells.NumberFormatLocal = "@";
                xlWorkSheet.Cells.Font.Size = 10;

                // fill content to sheet name "Detail"
                MvExcelReport.fastFillDataIntoExcel(dt, xlWorkSheet, "A1", true);
            }

            // save file to xlsx
            xlApp.DisplayAlerts = false;

            try
            {
                xlWorkBook.SaveAs(filePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + filePathAndName);
            }
            finally
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;
        }




        private void buildHeader_Bom09_NoPrice(Excel.Workbook xlWorkBook, string bomId)
        {

            Excel.Worksheet xlWorkSheet;

            // build header for sheet name is "Detail"
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[bomId];
            xlWorkSheet.Activate();

            xlWorkSheet.Cells[1, 3] = bomId;
            // BomName 要再想一下要怎麼帶進來
            xlWorkSheet.Cells[1, 4] = "";

            //string[] strArray = new string[] { "階次", "取替代料", "元件品號", "品號屬性", "二階", "三階", "四階", "品名", "材料", "尺寸", "後製程", "型號", "規格", "單位", "數量", "SOP圖號", "備料順序", "備註", "單價", "金額", "最近廠商", "前一版", "CNC", "3年前舊價", "國外廠商", "獨家廠商", "核價單" };
            string[] strArray = DefinedHeader.Bom09HeaderAliasNameNoPrice;

            xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 25]] = strArray;
            xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 25]].Font.Bold = true;

            // 設定自動篩選
            xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 25]].AutoFilter();
        }

        public bool generateBomP09_NoPrice(string filePathAndName, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet = null;

            // 建立excel 文件
            xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return false;
            }

            // 建立Sheets and name
            // !!! 經多次實驗, 需要先建立workbook 及worksheet之後才可以填入值
            // 如果邊建邊填入sheet, 目前實驗的結果只會填在最後的一頁, 還找不到是何原因造成
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);
            for (int index = 1; index < ds.Tables.Count; index++)
            {
                xlWorkBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }

            int sheetIndex = 1;
            foreach (DataTable dt1 in ds.Tables)
            {
                xlWorkBook.Worksheets[sheetIndex].Name = ds.Tables[sheetIndex - 1].TableName;
                sheetIndex += 1;
            }

            // 填入表格內容
            foreach (DataTable dt in ds.Tables)
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[dt.TableName];

                // 建立excel headers
                buildHeader_Bom09_NoPrice(xlWorkBook, dt.TableName);

                // fill content 
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[dt.TableName];
                xlWorkSheet.Cells.NumberFormatLocal = "@";
                xlWorkSheet.Cells.Font.Size = 10;
                fastFillDataIntoExcel(dt, xlWorkSheet, "A3", false);
            }

            // save file to xlsx
            xlApp.DisplayAlerts = false;
            try
            {
                xlWorkBook.SaveAs(filePathAndName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (COMException)
            {
                MessageBox.Show("Please make the file is not be opened. " + Environment.NewLine + filePathAndName);
            }
            finally
            {
                xlWorkBook.Close(true, Type.Missing, Type.Missing);
                xlApp.Quit();
            }

            if (xlWorkSheet != null) { Marshal.ReleaseComObject(xlWorkSheet); }
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;

            return true;
        }
    }
}
