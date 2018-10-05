using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MvLocalProject.Model;
using MvLocalProject.Controller;

namespace MvLocalProject.Bo
{
    internal sealed class MvErpNoteBo : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 取得ERP Note Enum
        /// </summary>
        /// <param name="erpNoteTitle"></param>
        /// <returns>Enum ErpNoteHead</returns>
        public static ErpNoteHead getEprNoteEnum(string erpNoteTitle)
        {
            try
            {
                return (ErpNoteHead)Enum.Parse(typeof(ErpNoteHead), erpNoteTitle, false);
            }
            catch (ArgumentException)
            {
                return ErpNoteHead.None;
            }
        }

        /// <summary>
        /// 取得該單別的最大單號
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="noteHead"></param>
        /// <returns>ERP單身</returns>
        public static string getMaxErpNote(SqlConnection conn, ErpNoteHead noteHead)
        {

            DataTable tempDt = null;
            try
            {
                string sqlCommand = string.Empty;
                switch (noteHead)
                {
                    case ErpNoteHead.H_3101:
                    case ErpNoteHead.H_3102:
                    case ErpNoteHead.H_3103:
                    case ErpNoteHead.H_3104:
                    case ErpNoteHead.H_3105:
                    case ErpNoteHead.H_3109:
                        sqlCommand = string.Format("SELECT MAX(TA002) MAX_TA002 FROM PURTA WHERE TA001='{0}' AND (TA002 >= '{1}' AND TA002 < '{2}')", (int)noteHead, DateTime.Now.ToString("yyyyMM"), DateTime.Now.AddMonths(1).ToString("yyyyMM"));
                        //sqlCommand = string.Format("SELECT MAX(TA002) MAX_TA002 FROM PURTA WHERE TA001='{0}' AND (TA002 >= '{1}' AND TA002 < '{2}')", (int)noteHead, DateTime.Now.AddMonths(2).ToString("yyyyMM"), DateTime.Now.AddMonths(3).ToString("yyyyMM"));
                        break;
                    default:
                        break;
                }

                if (sqlCommand.Length == 0) { return null; }
                tempDt = MvDbConnector.queryDataBySql(conn, sqlCommand);

                switch (noteHead)
                {
                    case ErpNoteHead.H_3101:
                    case ErpNoteHead.H_3102:
                    case ErpNoteHead.H_3103:
                    case ErpNoteHead.H_3104:
                    case ErpNoteHead.H_3105:
                    case ErpNoteHead.H_3109:
                        // 如果是null, 直接回傳00000
                        return Convert.IsDBNull(tempDt.Rows[0]["MAX_TA002"]) ? DateTime.Today.ToString("yyyyMM") + "00000" : tempDt.Rows[0]["MAX_TA002"].ToString();
                    default:
                        break;
                }
            }
            catch (SqlException se)
            {
                //發生例外時，會自動rollback
                throw se;
            }
            finally
            {
                if (tempDt != null) { tempDt.Dispose(); }
            }

            return null;
        }


        /// <summary>
        /// 開立PR
        /// </summary>
        /// <param name="company"></param>
        /// <param name="conn"></param>
        /// <param name="noteHead"></param>
        /// <param name="excelDt"></param>
        /// <param name="requestDate"></param>
        /// <param name="adUserName"></param>
        /// <param name="userGroup"></param>
        /// <param name="testingNoteNumber"></param>
        /// <returns>開單後的DataSet for PURTA/PURTB</returns>
        public static DataSet createErpNote_PR(MvCompany company, SqlConnection conn, ErpNoteHead noteHead, DataTable excelDt, DateTime requestDate, string adUserName, string userGroup, string testingNoteNumber = "")
        {
            DataSet ds = new DataSet();
            DataTable resultDt = null;
            string noteHeadString = ((int)noteHead).ToString();

            // 先用宣告的方式產生temp table, 再直接於temp table產生資料
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @PURTB_Temp TABLE (COMPANY NCHAR(20), CREATOR NCHAR(10), USR_GROUP NCHAR(10), CREATE_DATE NCHAR(8), ")
                .Append("MODIFIER NCHAR(10), MODI_DATE NCHAR(8), FLAG NUMERIC(3,0), TB001 NCHAR(4), TB002 NCHAR(11), TB003 NCHAR(4), ")
                .Append("TB004 NVARCHAR(40), TB005 NVARCHAR(120), TB006 NVARCHAR(120), TB007 NVARCHAR(6), TB008 NVARCHAR(10), TB009 NUMERIC(16,3), ")
                .Append("TB010 NVARCHAR(10), TB011 NVARCHAR(8), TB012 NVARCHAR(255), TB013 NVARCHAR(10), TB014 NUMERIC(16,3), TB015 NVARCHAR(6), ")
                .Append("TB016 NVARCHAR(4), TB017 NUMERIC(21,6), TB018 NUMERIC(21,6), TB019 NVARCHAR(8), TB020 NVARCHAR(1), TB021 NVARCHAR(1), ")
                .Append("TB022 NVARCHAR(21), TB023 NVARCHAR(10), TB024 NVARCHAR(255), TB025 NVARCHAR(1), TB026 NVARCHAR(1), TB027 NVARCHAR(6), ")
                .Append("TB028 NVARCHAR(6), TB029 NVARCHAR(4), TB030 NVARCHAR(11), TB031 NVARCHAR(4), TB032 NVARCHAR(1), TB033 NVARCHAR(20), ")
                .Append("TB034 NUMERIC(16,3), TB035 NUMERIC(16,3), TB036 NVARCHAR(8), TB037 NVARCHAR(6), TB038 NVARCHAR(6), TB039 NVARCHAR(1), ")
                .Append("TB040 NVARCHAR(1), TB041 NVARCHAR(30), TB042 NVARCHAR(1), TB043 NVARCHAR(20), TB044 NUMERIC(20,9), TB045 NUMERIC(21,6), ")
                .Append("TB046 NVARCHAR(1), TB047 NVARCHAR(6), TB048 NVARCHAR(20), TB049 NUMERIC(21,6), TB050 NVARCHAR(4), TB051 NUMERIC(21,6), ")
                .Append("TB052 NUMERIC(21,6), TB053 NUMERIC(21,6), TB054 NVARCHAR(1), TB055 NVARCHAR(30), TB056 NVARCHAR(60), TB057 NVARCHAR(3), ")
                .Append("TB058 NVARCHAR(1), TB059 NVARCHAR(40), TB060 NVARCHAR(2), TB061 NVARCHAR(4), TB062 NVARCHAR(11), TB063 NUMERIC(8,5), TB064 NVARCHAR(1), ")
                .Append("TB500 NVARCHAR(255), TB501 NVARCHAR(4), TB502 NVARCHAR(11), TB503 NVARCHAR(4), TB550 NVARCHAR(1), TB551 NUMERIC(21,6), ")
                .Append("UDF01 NVARCHAR(255), UDF02 NVARCHAR(255), UDF03 NVARCHAR(255), UDF04 NVARCHAR(255), UDF05 NVARCHAR(255), UDF06 NUMERIC(21,6), ")
                .Append("UDF07 NUMERIC(21,6), UDF08 NUMERIC(21,6), UDF09 NUMERIC(21,6), UDF10 NUMERIC(21,6), TB200 NVARCHAR(255), TB201 NVARCHAR(10) ")
                .AppendLine(" );");

            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand command = null;
                DataTable tempDt = null;
                string createDate = DateTime.Now.ToString("yyyyMMdd");
                string isUrgent = string.Empty; // 只要excel 裡有一筆急件, 單頭帶入急件

                try
                {
                    command = conn.CreateCommand();

                    // 先取最大單號
                    //string noteNumber = getMaxErpNote(conn, noteHead);
                    // 測試用 要移掉
                    string noteNumber = string.Empty;
                    if (testingNoteNumber.Length > 0)
                    {
                        noteNumber = testingNoteNumber;
                    }
                    else
                    {
                        noteNumber = getMaxErpNote(conn, noteHead);
                    }
                    
                    // 測試用 要移掉

                    long n;
                    if (long.TryParse(noteNumber, out n) == false)
                    {
                        throw new ArgumentException("Un-support format");
                    }
                    else
                    {
                        n = long.Parse(noteNumber);
                        noteNumber = (n += 1).ToString();
                    }

                    int totalQty = 0;
                    int itemNo = 1;
                    int amountTA020 = 0;
                    string empNo = string.Empty;
                    string itemName = string.Empty;
                    string remark = string.Empty;   //PURTA 取最後一筆
                    string purtbTB200 = string.Empty;
                    string tempCommand = string.Empty;

                    // Insert PURTB
                    foreach (DataRow excelDr in excelDt.Rows)
                    {
                        // Insert PURTB 指令
                        string strDateTime = excelDr["TB011"].ToString();
                        string sourceOrderNo = Convert.IsDBNull(excelDr["TB029"]) ? "" : excelDr["TB029"].ToString();
                        string sourceOrderHead = string.Empty;
                        string sourceOrderBody = string.Empty;
                        int sourceOrderItem = 0;

                        // TB004 要先取出來
                        itemName = excelDr["TB004"].ToString();

                        // TA011 的數量是PURTB的所有TB009的總計
                        totalQty += int.Parse(excelDr["TB009"].ToString());
                        empNo = excelDr["TA012"].ToString();
                        // 判斷是否為急件
                        if (excelDr["TA202"].ToString() == "Y")
                        {
                            isUrgent = "Y";
                        }

                        // 取得來源單號
                        if (sourceOrderNo.Length > 0 && sourceOrderNo.IndexOf("-") > 0)
                        {
                            sourceOrderHead = sourceOrderNo.Split('-')[0];
                            sourceOrderBody = sourceOrderNo.Split('-')[1];
                            sourceOrderItem = 0;
                        }
                        else
                        {
                            // 3104是客服的請購單別, 因為有三區料件的需求, 當開立3104單別是, 才需要特別寫入TB029, TB031, TB031欄位
                            if (noteHead == ErpNoteHead.H_3104)
                            {
                                sourceOrderHead = noteHeadString;
                                sourceOrderBody = noteNumber;
                                sourceOrderItem = itemNo;
                            }
                        }

                        strDateTime = DateTime.Parse(strDateTime).ToString("yyyyMMdd");
                        sb.Append("INSERT INTO @PURTB_Temp ")
                            .Append("(TB001, TB002, TB003, TB004, TB005, TB006, TB007, TB008, TB009, TB010, ")
                            .Append(" TB011, TB012, TB014, TB015, TB016, TB019, TB020, TB021, TB024, TB025, ")
                            .Append(" TB026, TB032, TB029, TB030, TB031, TB043, TB050, TB201, CREATOR) VALUES ( ")
                            .Append(string.Format("'{0}','{1}','{2:0000}','{3}','{4}','{5}','{6}','{7}',{8},'{9}'"
                                    + ",'{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}'"
                                    + ",'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}'",
                                noteHeadString, noteNumber, itemNo, excelDr["TB004"], excelDr["TB005"], excelDr["TB006"], "", excelDr["TB008"], excelDr["TB009"], excelDr["TB010"]
                                , strDateTime, excelDr["TB012"], excelDr["TB009"], "", "", strDateTime, "N", "N", excelDr["TB012"], "N"
                                , "2", "N", sourceOrderHead, sourceOrderBody, sourceOrderItem > 0 ? string.Format("{0:0000}", sourceOrderItem) : "", excelDr["TB043"], "", excelDr["TB201"], adUserName))
                            .AppendLine(");");

                        // TB016 ~ TB018 請購單價
                        // 邏輯如下
                        // 1. 先用核價單, 單價低的先取, 如果沒有再取
                        // 2. 採購單一年內最新交易金額
                        // 如果都沒有, 就直接帶0, 請採購重新議價
                        tempCommand = string.Format("SELECT TOP 1 TL004, TL005, TM004, TM010, TM014, TM015 FROM PURTL, PURTM WHERE TL001=TM001 AND TL002=TM002 AND TM004='{0}' AND TL006='Y' AND ('{1}' < TM015  OR TM015='') AND '{1}' >= TM014 ORDER BY TM010, TM014 DESC, TL004 DESC ", excelDr["TB004"], requestDate.ToString("yyyyMMdd"));
                        tempDt = MvDbConnector.queryDataBySql(conn, tempCommand);
                        if (tempDt.Rows.Count == 0)
                        {
                            tempCommand = string.Format("SELECT TOP 1 TD005, TD004, TD008, TD010, TC004, TC003, TC005, Round(TD010*C.MG004,0) AS AMTNT, TD001+'-'+TD002+'-'+TD003 AS TD0013, C.MG004, MF004  FROM PURTD, PURTC, CMSMG C, CMSMF WHERE TC001=TD001 AND TC002=TD002 AND TC005=C.MG001 AND TD018='Y' AND TD004='{0}' AND TC004<>'1M1468' AND TC003 > '{1}' AND TC005=MF001 AND TD010 <> 0 AND (TD015<> 0 or TD016<>'y') ORDER BY AMTNT, TC003 DESC, TD002 DESC, TD003 DESC ", excelDr["TB004"], requestDate.AddYears(-1).ToString("yyyyMMdd"));
                            tempDt = MvDbConnector.queryDataBySql(conn, tempCommand);
                        }
                        // 如果品號為9開頭的, 不用再串值了
                        if (tempDt.Rows.Count > 0 && itemName.StartsWith("9") == false)
                        {
                            if (tempDt.Columns.Contains("TL004"))
                            {
                                sb.AppendLine(string.Format("UPDATE @PURTB_Temp SET TB010='{3}', TB016='{4}', TB017={5}, TB018=TB009*{5}, TB049={5}, TB050='{4}', TB551=TB009*{5}, TB013='', UDF05='Y' WHERE TB001='{0}' AND TB002='{1}' AND TB003='{2:0000}';", noteHeadString, noteNumber, itemNo, tempDt.Rows[0]["TL004"], tempDt.Rows[0]["TL005"], tempDt.Rows[0]["TM010"]));
                            }
                            else
                            {
                                sb.AppendLine(string.Format("UPDATE @PURTB_Temp SET TB010='{3}', TB016='{4}', TB017={5}, TB018=ROUND(TB009*{5},{6}), TB044={7}, TB049={5}, TB050='{4}' , TB051=ROUND(TB009*{5},{6}), TB551=ROUND(TB009*{5}*{7}, 0), TB013='', UDF05='Y' FROM CMSMF WHERE TB001='{0}' AND TB002='{1}' AND TB003='{2:0000}';", ((int)noteHead).ToString(), noteNumber, itemNo, tempDt.Rows[0]["TC004"], tempDt.Rows[0]["TC005"], tempDt.Rows[0]["TD010"], tempDt.Rows[0]["MF004"], tempDt.Rows[0]["MG004"]));
                            }
                        }
                        else
                        {
                            sb.AppendLine(string.Format("UPDATE @PURTB_Temp SET TB010=MB032, TB016='NTD', TB017=0, TB018=0, TB049=0, TB050='NTD', TB051=0, TB551=0, UDF05='' FROM INVMB WHERE TB004=MB001 AND TB010='' AND TB001='{0}' AND TB002='{1}' AND TB003='{2:0000}';", noteHeadString, noteNumber, itemNo));
                        }

                        // TB200 備註庫存量
                        tempCommand = string.Format("SELECT SUM(MC007) MC007 FROM INVMC WHERE MC001='{0}' ", itemName);
                        tempDt = MvDbConnector.queryDataBySql(conn, tempCommand);
                        if (tempDt == null || tempDt.Rows.Count == 0)
                        {
                            purtbTB200 = "總庫存 0;";
                        }
                        else
                        {
                            purtbTB200 = string.Format("總庫存 {0:0};", tempDt.Rows[0]["MC007"]);
                            tempCommand = string.Format("SELECT C.MC002, A.MC007 FROM INVMC A, CMSMC C WHERE A.MC001='{0}' AND A.MC002=C.MC001 AND A.MC007 <> 0 ORDER BY A.MC007 DESC", itemName);
                            tempDt = MvDbConnector.queryDataBySql(conn, tempCommand);
                            if (tempDt != null && tempDt.Rows.Count > 0)
                            {
                                foreach (DataRow tempDr in tempDt.Rows)
                                {
                                    purtbTB200 += string.Format("{0} {1:0};", tempDr["MC002"].ToString().TrimEnd(), tempDr["MC007"]);
                                }
                            }
                        }
                        sb.AppendLine(string.Format("UPDATE @PURTB_Temp SET TB200='{3}' WHERE TB001='{0}' AND TB002='{1}' AND TB003='{2:0000}' AND TB004='{4}';", noteHeadString, noteNumber, itemNo, purtbTB200, itemName));

                        // 記錄remark 給PURTA匯入使用
                        remark = excelDr["TB012"].ToString();
                        itemNo++;
                    }

                    sb.AppendLine(string.Format("UPDATE @PURTB_Temp SET TB059=MB004 FROM (SELECT MB001, MB004 FROM BOMMB, (SELECT MB001 MB1,MIN(MB009) MB9 FROM BOMMB WHERE LEFT(MB002,1)='*' GROUP BY MB001)B WHERE MB001=MB1 AND MB009=MB9 AND MB001='10204006')A WHERE TB004=MB001 AND CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB053=MM005 FROM (SELECT MM001, SUM(MM005) MM005 FROM INVMM WHERE (MM002 IN ('201','301','305','472','701','30T')) AND MM005 > 0 GROUP BY MM001) A WHERE TB059=MM001 AND TB059<> '' AND CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB059=MB004, TB060='Y' FROM (SELECT MB001, MB004 FROM BOMMB, (SELECT MB001 MB1,MIN(MB009) MB9 FROM BOMMB WHERE LEFT(MB002,1)='*' AND MB010='Y' GROUP BY MB001)B WHERE MB001=MB1 AND MB009=MB9)A WHERE TB004=MB001 AND CREATOR='{0}';", adUserName))
                    // 這條不懂
                        .AppendLine(string.Format("DELETE @PURTB_Temp WHERE TB059 <> '' AND TB053 > TB009 AND CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB004=TB059 WHERE TB059 <> '' AND TB060='Y' AND CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB005=MB002, TB006=MB003, TB007=MB004 FROM INVMB WHERE TB004=MB001 AND TB060='Y' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB005=MB002, TB006=MB003, TB007=MB004, TB015=MB004 FROM INVMB WHERE TB004=MB001 AND LEFT(TB004,1) < '9' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB009=MB038, TB014=MB038, UDF05='Y' FROM INVMB WHERE TB004=MB001 AND TB001='3102' AND MB038 > TB009 AND MB077='批量' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB013='', TB022='', TB023='', TB027='', TB028='', TB044=1 WHERE CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET COMPANY='{3}', USR_GROUP='{1}', CREATE_DATE='{2}', MODIFIER='', MODI_DATE='', FLAG=0 WHERE CREATOR='{0}';", adUserName, userGroup, createDate, company.ToString()))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB007=MB004, TB015=MB004 FROM INVMB WHERE TB004=MB001 AND LEFT(TB004,1)='9' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET [@PURTB_Temp].TB012=MOCTB.TB017+'/'+RTRIM([@PURTB_Temp].TB012), [@PURTB_Temp].TB024=MOCTB.TB017+'/'+RTRIM([@PURTB_Temp].TB024) FROM MOCTB WHERE [@PURTB_Temp].TB004=MOCTB.TB003 AND [@PURTB_Temp].TB201=MOCTB.TB200 AND [@PURTB_Temp].TB021<> '' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB039='N', TB040='1', TB042='2', TB046='N', TB058='1', TB064='N' WHERE CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB057=MA076 FROM PURMA WHERE TB010=MA001 AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB063=NN004, TB026=NN006 FROM CMSNN WHERE TB057=NN001 AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB018=Round(TB014*TB017,Convert(Int,MF004)), TB051=Round(TB014*TB049,Convert(Int,MF004)) FROM CMSMF WHERE TB050=MF001 AND (TB050<> '' OR TB050 IS NULL) AND [@PURTB_Temp].UDF05='Y' AND [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB045=Round(TB018*TB044,0) WHERE [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        // 自己加的
                        .AppendLine(string.Format("UPDATE @PURTB_Temp SET TB033='', TB034=0, TB035=0, TB036='', TB037='', TB038='', TB041='', TB047='', TB048='', TB052=0, TB053=0, TB054='', TB055='', TB056='', TB059='', TB060='', TB061='', TB062='', TB500='', TB501='', TB502='', TB503='', TB550='', UDF01='', UDF02='', UDF03='', UDF04='', UDF06=0, UDF07=0, UDF08=0, UDF09=0, UDF10=0 WHERE [@PURTB_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("INSERT INTO MVTEST.dbo.PURTB SELECT * FROM @PURTB_Temp WHERE CREATOR='{0}';", adUserName))
                        ;

                    sb.AppendLine(string.Format("SELECT * FROM MVTEST.dbo.PURTB WHERE TB001='{0}' AND TB002='{1}';", noteHeadString, noteNumber));
                    resultDt = MvDbConnector.queryDataBySql(conn, sb.ToString());
                    resultDt.TableName = "PURTB";
                    ds.Tables.Add(resultDt.Copy());

                    object sumObject;
                    sumObject = resultDt.Compute("Sum(TB045)", string.Empty);
                    amountTA020 = Convert.ToInt32(sumObject);

                    // Insert PURTA
                    sb.Clear();
                    sb.Append("DECLARE @PURTA_Temp TABLE (COMPANY NVARCHAR(20), CREATOR NCHAR(10), USR_GROUP NCHAR(10), CREATE_DATE NCHAR(8), ")
                        .Append("MODIFIER NCHAR(10), MODI_DATE NCHAR(8), FLAG NUMERIC(3,0), TA001 NCHAR(4), TA002 NCHAR(11), TA003 NVARCHAR(8), ")
                        .Append("TA004 NVARCHAR(6), TA005 NVARCHAR(26), TA006 NVARCHAR(255), TA007 NVARCHAR(1), TA008 NUMERIC(1,0), TA009 NVARCHAR(1), ")
                        .Append("TA010 NVARCHAR(6), TA011 NUMERIC(16,3), TA012 NVARCHAR(10), TA013 NVARCHAR(8), TA014 NVARCHAR(10), TA015 NUMERIC(16,3), ")
                        .Append("TA016 NVARCHAR(1), TA017 NUMERIC(1,0), TA018 NVARCHAR(25), TA019 NVARCHAR(4), TA020 NUMERIC(21,6), TA021 NVARCHAR(4), ")
                        .Append("TA022 NVARCHAR(4), TA023 NUMERIC(21,6), TA024 NUMERIC(15,6), TA025 NVARCHAR(1), TA026 NVARCHAR(30), TA027 NVARCHAR(60), ")
                        .Append("TA028 NVARCHAR(1), TA550 NVARCHAR(1), TA551 NUMERIC(21,6), UDF01 NVARCHAR(255), UDF02 NVARCHAR(255), UDF03 NVARCHAR(255), ")
                        .Append("UDF04 NVARCHAR(255), UDF05 NVARCHAR(255), UDF06 NUMERIC(21,6), UDF07 NUMERIC(21,6), UDF08 NUMERIC(21,6), UDF09 NUMERIC(21,6), ")
                        .Append("UDF10 NUMERIC(21,6), TA200 NVARCHAR(8), TA201 NVARCHAR(10), TA202 NVARCHAR(1), TA203 NVARCHAR(255) ")
                        .AppendLine(" );");

                    // 3104是客服的請購單別, 因為有三區料件的需求, 當開立3104單別是, 才需要特別寫入TA005欄位
                    string sourceNote = string.Empty;
                    if (noteHead == ErpNoteHead.H_3104)
                    {
                        sourceNote = string.Format("{0}-{1}", noteHeadString, noteNumber);
                    }
                    sb.Append("INSERT INTO @PURTA_Temp ")
                        .Append("(TA001, TA002, TA003, TA004, TA005, TA006, TA007, TA008, TA009, TA010, TA011, TA012, TA013, TA014, TA016, TA202, CREATOR) VALUES (")
                        .Append(string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},'{9}'"
                            + ",'{10}','{11}',{12},'{13}','{14}','{15}','{16}'",
                            noteHeadString, noteNumber, createDate, "", sourceNote, remark, "N", "0", "9", "001"
                            , totalQty, empNo, createDate, "", "N", "", adUserName))
                        .AppendLine(");");

                    sb.AppendLine(string.Format("UPDATE @PURTA_Temp SET TA004=MV004 FROM CMSMV WHERE TA012=MV001 AND [@PURTA_Temp].CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("UPDATE @PURTA_Temp SET COMPANY='{3}', CREATOR='{0}', USR_GROUP='{1}', CREATE_DATE='{2}', MODIFIER='', MODI_DATE='', FLAG=0 WHERE CREATOR='{0}';", adUserName, userGroup, createDate, company.ToString()))
                        .AppendLine(string.Format("UPDATE @PURTA_Temp SET TA021='000' WHERE CREATOR='{0}';", adUserName));

                    // 急件的判斷, 加判是否為3105
                    if (noteHead == ErpNoteHead.H_3105)
                    {
                        isUrgent = "Y";
                    }
                    sb.AppendLine(string.Format("UPDATE @PURTA_Temp SET TA202='{1}', TA203='{2}' WHERE CREATOR='{0}';", adUserName, isUrgent, isUrgent.Equals("Y") ? "急件" : ""))
                        // TA020 要另外算, 才能匯入
                        .AppendLine(string.Format("UPDATE @PURTA_Temp SET TA020={1} WHERE CREATOR='{0}';", adUserName, amountTA020))
                        // 自己加的
                        .AppendLine(string.Format("UPDATE @PURTA_Temp SET TA017=0, TA015=0, TA018='', TA019='', TA022='', TA023=0, TA024=0, TA025='', TA026='', TA027='', TA028='', TA550='', TA551=0, UDF01='', UDF02='', UDF03='', UDF04='', UDF05='', UDF06=0, UDF07=0, UDF08=0, UDF09=0, UDF10=0, TA200='', TA201='', TA203='' WHERE CREATOR='{0}';", adUserName))
                        .AppendLine(string.Format("INSERT INTO MVTEST.dbo.PURTA SELECT * FROM @PURTA_Temp WHERE CREATOR='{0}';", adUserName))
                        ;

                    sb.AppendLine(string.Format("SELECT * FROM MVTEST.dbo.PURTA WHERE TA001='{0}' AND TA002='{1}';", noteHeadString, noteNumber));
                    resultDt = MvDbConnector.queryDataBySql(conn, sb.ToString());
                    resultDt.TableName = "PURTA";
                    ds.Tables.Add(resultDt.Copy());
                    //做完以後
                    scope.Complete();
                }
                catch (SqlException se)
                {
                    //發生例外時，會自動rollback
                    throw se;
                }
                finally
                {
                    command.Dispose();
                    command = null;

                    resultDt.Dispose();
                    resultDt = null;
                }
            }
            return ds;
        }


        public static DataSet createErpNote_MoveOrder(SqlConnection conn, ErpNoteHead noteHead, DataTable excelDt, DateTime requestDate, string adUserName, string userGroup)
        {

            if(noteHead != ErpNoteHead.H_A121 || noteHead != ErpNoteHead.H_A12E)
            {
                return null;
            }

            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();
            DataRow dr = null;


            // 使用之前的流程串出開立調撥單的流程

            //INVTB
            sb.Clear();
            sb.Append("DECLARE @INVTB_Temp TABLE (COMPANY NCHAR(20), CREATOR NCHAR(10), USR_GROUP NCHAR(10), CREATE_DATE NCHAR(8), ")
                .Append("MODIFIER NCHAR(10), MODI_DATE NCHAR(8), FLAG NUMERIC(3,0), TB001 NCHAR(4), TB002 NCHAR(11), TB003 NCHAR(4), ")
                .Append("TB004 NVARCHAR(40), TB005 NVARCHAR(120), TB006 NVARCHAR(120), TB007 NUMERIC(16,3), TB008 NVARCHAR(6), TB009 NUMERIC(16,3), ")
                .Append("TB010 NUMERIC(21,6), TB011 NUMERIC(21,6), TB012 NVARCHAR(10), TB013 NVARCHAR(10), TB014 NVARCHAR(20), TB015 NVARCHAR(8), ")
                .Append("TB016 NVARCHAR(8), TB017 NVARCHAR(255), TB018 NVARCHAR(1),6), TB019 NVARCHAR(8), TB020 NVARCHAR(6), TB021 NVARCHAR(20), ")
                .Append("TB022 NUMERIC(16,3), TB023 NVARCHAR(6), TB024 NVARCHAR(1), TB025 NUMERIC(16,3), TB026 NVARCHAR(10), TB027 NVARCHAR(10), ")
                .Append("TB028 NUMERIC(21,6), TB029 NUMERIC(15,6), TB030 NVARCHAR(1), TB031 NVARCHAR(30), TB032 NVARCHAR(60), TB500 NVARCHAR(1250), ")
                .Append("TB501 NVARCHAR(1), TB502 NVARCHAR(20), UDF01 NVARCHAR(255), UDF02 NVARCHAR(255), UDF03 NVARCHAR(255), UDF04 NVARCHAR(255), ")
                .Append("UDF05 NVARCHAR(255), UDF06 NUMERIC(21,6), UDF07 NUMERIC(21,6), UDF08 NUMERIC(21,6), UDF09 NUMERIC(21,6), UDF10 NUMERIC(21,6) ")
                .AppendLine(" );");














            //INVTA
            sb.Clear();
            sb.Append("DECLARE @INVTA_Temp TABLE (COMPANY NVARCHAR(20), CREATOR NVARCHAR(10), USR_GROUP NCHAR(10), CREATE_DATE NVARCHAR(8), ")
                .Append("MODIFIER NVARCHAR(10), MODI_DATE NVARCHAR(8), FLAG NUMERIC(3,0), TA001 NCHAR(4), TA002 NCHAR(11), TA003 NVARCHAR(8), ")
                .Append("TA004 NVARCHAR(6), TA005 NVARCHAR(255), TA006 NVARCHAR(1), TA007 NUMERIC(1,0), TA008 NVARCHAR(6), TA009 NVARCHAR(2), ")
                .Append("TA010 NUMERIC(5,0), TA011 NUMERIC(16,3), TA012 NUMERIC(21,6), TA013 NVARCHAR(1), TA014 NVARCHAR(8), TA015 NVARCHAR(10), ")
                .Append("TA016 NUMERIC(16,3), TA017 NVARCHAR(1), TA018 NVARCHAR(1), TA019 NUMERIC(1,0), TA020 NVARCHAR(1), TA021 NVARCHAR(4), ")
                .Append("TA022 NVARCHAR(11), TA023 NUMERIC(21,6), TA024 NUMERIC(15,6), TA025 NVARCHAR(1), TA026 NVARCHAR(30), TA027 NVARCHAR(60), ")
                .Append("TA028 NVARCHAR(15), UDF01 NVARCHAR(255), UDF02 NVARCHAR(255), UDF03 NVARCHAR(255), UDF04 NVARCHAR(255), UDF05 NVARCHAR(255), ")
                .Append("UDF06 NUMERIC(21,6), UDF07 NUMERIC(21,6), UDF08 NUMERIC(21,6), UDF09 NUMERIC(21,6), UDF10 NUMERIC(21,6), TA200 NVARCHAR(20) ")
                .Append("TA201 NVARCHAR(20) ")
                .AppendLine(" );");





            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    conn.Open();

                    // Get Data schema INVTA
                    sb.AppendLine(string.Format(@"SELECT TOP 0 * from MVTEST.dbo.INVTA"));
                    majorData = MvDbConnector.queryDataBySql(conn, sb.ToString());

                    // add simulate data
                    dr = MvDbDao.simulateData_INVTA(majorData);
                    majorData.Rows.Add(dr);

                    // use BulkCopy insert to DB
                    using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
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
                    majorData = MvDbConnector.queryDataBySql(conn, sb.ToString());

                    // add simulate data
                    dr = MvDbDao.simulateData_INVTB(conn, majorData, "10101001");
                    majorData.Rows.Add(dr);

                    // use BulkCopy insert to DB
                    using (SqlBulkCopy sbc = new SqlBulkCopy(conn))
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
                    conn.Close();
                    conn.Dispose();
                }
            }

            sb = null;

            return null;
        }
    }
}
