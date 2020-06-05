using System;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Transactions;
using MvLocalProject.Model;
using MvSharedLib.Model;
using MvSharedLib.Controller;

namespace MvLocalProject.Controller
{

    internal sealed class MvDbDao : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~MvDbDao() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion

        public static DataSet collectData_MocP10Auto_VB6(DateTime dateTime)
        {

            DataTable majorData = null;
            DataTable tmpDt = null;
            StringBuilder sb = new StringBuilder();
            DataRowCollection tmpDrc = null;
            DataSet sheetsData = new DataSet("SheetsData");

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {

                connection.Open();

                sb.Clear();
                sb.Append("SELECT * FROM MACHVISION.dbo.MOCTA WHERE (TA011 = '1' OR TA011 = '2' OR TA011 = '3') and TA013 = 'Y' and TA001 = TA024 and TA002 = TA025 AND TA201<> '' AND (TA024='A511' OR (TA024='A513' AND (TA025='20150500001' OR TA025='20130700018'))) ORDER BY TA201, TA200, TA024, TA025");

                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                if (majorData == null)
                {
                    return null;
                }

                // 宣告合計資訊
                string wkT200 = "";
                string wkT201 = "";
                string wkTA024 = "";
                string wkTA025 = "";

                string wkTB004 = "";
                string wkTB005 = "";
                string wkFUNQTY = "";
                string wkQTYUN = "";
                string wkNoDelv = "";

                // 將取得的rows先暫存
                tmpDrc = majorData.Rows;

                majorData = new DataTable("Detail");
                Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start collect data");

                SqlCommand command = null;
                SqlTransaction transaction = null;

                command = connection.CreateCommand();
                // 在執行transaction之前先 truncate 有關summary的table
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10_RV";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10_AS";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10LOG";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10P";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10PR";
                command.ExecuteNonQuery();
                command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10PRSUM";
                command.ExecuteNonQuery();

                try
                {
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start collect data Detail");
                    // 設定transaction
                    transaction = connection.BeginTransaction("ERPDB2.TEMP");
                    command.Connection = connection;
                    command.Transaction = transaction;

                    // insert data for sheet Detail used
                    foreach (DataRow dr in tmpDrc)
                    {
                        // truncate detail 的資料
                        command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10";
                        command.ExecuteNonQuery();
                        command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10_RV";
                        command.ExecuteNonQuery();
                        command.CommandText = "Truncate Table TEMP.dbo.TMP_MOCP10_AS";
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("SELECT TA024 T24, TA025 T25, TA034 T34, TA200 T200, TA201 T201 FROM MACHVISION.dbo.MOCTA WHERE TA201 ='" + dr["TA201"] + "' AND TA200 ='" + dr["TA200"] + "' AND TA013='Y' AND TA001=TA024 AND TA002=TA025 ");
                        command.CommandText = sb.ToString();
                        tmpDt = MvDbConnector.queryDataBySql(command);

                        wkT200 = (string)tmpDt.Rows[0]["T200"];
                        wkT201 = (string)tmpDt.Rows[0]["T201"];
                        wkTA024 = (string)tmpDt.Rows[0]["T24"];
                        wkTA025 = (string)tmpDt.Rows[0]["T25"];

                        sb.Clear();
                        sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10(TA034, TA024, TA025, TB003, TB012, TB013, TB004, TB005, TA200, TA201, TB009, TA003, TA009) ")
                            .Append("SELECT T34, TA024, TA025, TB003, TB012, TB013, SUM(TB004) TB004, SUM(TB005) TB005, TB200, T201, TB009, TA003, TA009 ")
                            .Append("FROM MACHVISION.dbo.MOCTA, MACHVISION.dbo.MOCTB, ")
                            .Append("(SELECT TA024 T24, TA025 T25, TA034 T34, TA200 T200, TA201 T201 FROM MACHVISION.dbo.MOCTA WHERE TA201 = '" + dr["TA201"] + "' AND TA200 = '" + dr["TA200"] + "' AND TA013 = 'Y' AND TA001 = TA024 AND TA002 = TA025) C ")
                            .Append("WHERE TA013='Y' and TB018='Y' and TA001=TB001 and TA002=TB002 and  TA024=C.T24 and TA025=C.T25 GROUP By TA024, TA025, TB003, TB012, TB013, TB003, T34, T200, T201, TB009, TA003, TA009, TB200");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("UPDATE TEMP.dbo.TMP_MOCP10 SET FUNQTY = QTYIN FROM(SELECT MM001, (MM005)QTYIN FROM MACHVISION.dbo.INVMM WHERE MM002 = '303' AND MM003 = '" + wkT200 + "' AND MM005 <> 0)B  WHERE TB003 = B.MM001 AND TA200 = '" + wkT200 + "' ");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("UPDATE TEMP.dbo.TMP_MOCP10 SET TEMP.dbo.TMP_MOCP10.MB077 = MACHVISION.dbo.INVMB.MB077 FROM MACHVISION.dbo.INVMB WHERE TB003 = MB001");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();
                        //transaction.Commit();

                        sb.Clear();
                        sb.Append("SELECT Sum(TB004) TB004, Sum(TB005) TB005, Sum(FUNQTY) FUNQTY, Sum(TB004-TB005-FUNQTY) QTYUN FROM TEMP.dbo.TMP_MOCP10 Where MB077 <>'耗材'");

                        command.CommandText = sb.ToString();
                        tmpDt = MvDbConnector.queryDataBySql(command);

                        // 取得合計資訊
                        wkTB004 = tmpDt.Rows[0]["TB004"].ToString();
                        wkTB005 = tmpDt.Rows[0]["TB005"].ToString();
                        wkFUNQTY = tmpDt.Rows[0]["FUNQTY"].ToString();
                        wkQTYUN = tmpDt.Rows[0]["QTYUN"].ToString();

                        double tmpResult = double.Parse(wkQTYUN) / double.Parse(wkTB004);
                        wkNoDelv = "未到料比率:" + String.Format("{0:0.0%}", Math.Round(tmpResult, 3));
                        //string wkNoDelv = "未到料比率:" + string.Format( Format(Round(wkQTYUN / wkTB004 * 100, 1)) + "%";
                        //wkNoDelv = "未到料比率:0%";

                        sb.Clear();
                        sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10_RV SELECT MAX(TG003) TG003, TH004, TH009, TH063 ")
                            .Append("FROM MACHVISION.dbo.PURTH, MACHVISION.dbo.PURTG ")
                            .Append("WHERE TH001 = TG001 AND TH002 = TG002 AND TH030 = 'Y' AND TH063 = '" + dr["TA200"] + "' GROUP BY TH004, TH009, TH063");

                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10_AS SELECT MAX(TA003) TA3, TB004, TB013, TB027 ")
                            .Append("FROM MACHVISION.dbo.INVTB, MACHVISION.dbo.INVTA ")
                            .Append("WHERE TB001=TA001 AND TB002=TA002 AND TB018='Y' AND TB027='" + dr["TA200"] + "' GROUP BY TB004, TB013, TB027 ");

                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("UPDATE TEMP.dbo.TMP_MOCP10 SET RVDATE=TG003 FROM TEMP.dbo.TMP_MOCP10_RV A WHERE TB003=A.TH004 AND TB009=A.TH009 AND TA200=A.TH063");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("UPDATE TEMP.dbo.TMP_MOCP10 SET ASDATE=TA3 FROM  TEMP.dbo.TMP_MOCP10_AS A WHERE TB003=A.TB004 AND TB009=A.TB013 AND TA200=A.TB027 ");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        sb.Clear();
                        sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10LOG SELECT * FROM TEMP.dbo.TMP_MOCP10 ");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        // insert data for sheet Summary used
                        sb.Clear();
                        sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10P(TA201, TA200, TA024, TA025, TB012, TB004, TB005, FUNQTY, QTY_NOT, WKNODELV) VALUES('")
                            .Append(wkT201 + "', '" + wkT200 + "', '" + wkTA024 + "', '" + wkTA025 + "','                                 合計:', '" + wkTB004 + "', '" + wkTB005 + "'," + wkFUNQTY + ", " + wkQTYUN + ", '" + wkNoDelv + "') ");
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        // 取得 data table sheet detail 資料
                        sb.Clear();
                        sb.Append("SELECT TA201, TA200, TA024, TA025, TA003, TA009,TA034, RTRIM(TB003) TB003, RTRIM(TB012), RTRIM(TB013), TB004, TB005, FUNQTY, QTY_NOT=CASE MB077 When '耗材' THEN 0 Else TB004-TB005-FUNQTY End, Rtrim(TB009), MB077, RVDATE, ASDATE ")
                            .Append("FROM TEMP.dbo.TMP_MOCP10 WHERE TB003 <> '' ORDER BY TB003 ");
                        command.CommandText = sb.ToString();
                        tmpDt = MvDbConnector.queryDataBySql(command);

                        majorData.Merge(tmpDt);
                    }

                    // insert data for sheet PR_Detail used
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start collect data PR_Detail");
                    DateTime startDate = DateTime.Parse(dateTime.AddMonths(-4).ToShortDateString());

                    sb.Clear();
                    sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10PR ")
                        .Append("SELECT TB001, TB002, TB003, TA003, TA004, TA005, TA006, TB004, TB005, TB006, TB009, TB014, TB016, TB017, TB018, TB022, TB025, '', '', '', 0, '' ")
                        .Append("FROM MACHVISION.dbo.PURTB, MACHVISION.dbo.PURTA WHERE TB001=TA001 AND TB002=TA002 AND TB001='3101' AND TA003>='" + startDate.ToString("yyyyMMdd") + "' AND TB025<>'V' ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE  TEMP.dbo.TMP_MOCP10PR SET MACHNO=TA201, MOCSTATUS=TA011 FROM MACHVISION.dbo.MOCTA WHERE TA001=LEFT(TEMP.dbo.TMP_MOCP10PR.TA005,4) AND TA002=SUBSTRING(TEMP.dbo.TMP_MOCP10PR.TA005,6,11) ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE TEMP.dbo.TMP_MOCP10PR SET LOCATION=MACHVISION.dbo.MOCTB.TB200 FROM MACHVISION.dbo.MOCTB WHERE MACHVISION.dbo.MOCTB.TB001=LEFT(TEMP.dbo.TMP_MOCP10PR.TA005,4) AND MACHVISION.dbo.MOCTB.TB002=SUBSTRING(TEMP.dbo.TMP_MOCP10PR.TA005,6,11) ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Append("UPDATE  TEMP.dbo.TMP_MOCP10PR SET POQTY=TD015, POSURE=TD018 FROM MACHVISION.dbo.PURTD WHERE TD001=LEFT(TB022,4) AND TD002=SUBSTRING(TEMP.dbo.TMP_MOCP10PR.TB022,6,11) ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // insert data for sheet PR_Summary used
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start collect data PR_Summary");
                    sb.Clear();
                    sb.Append("INSERT INTO TEMP.dbo.TMP_MOCP10PRSUM(MACHNO, LOCATION, PRTOTAL) SELECT MACHNO, LOCATION, COUNT(*) FROM TEMP.dbo.TMP_MOCP10PR WHERE MACHNO <> '' AND (MOCSTATUS <> 'Y' OR MOCSTATUS <>'y') GROUP BY MACHNO, LOCATION ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE TEMP.dbo.TMP_MOCP10PRSUM SET PRAPP=WNO FROM (SELECT MACHNO MNO, LOCATION LO, COUNT(*)WNO FROM TEMP.dbo.TMP_MOCP10PR WHERE TB025='Y' GROUP BY MACHNO, LOCATION) A WHERE MACHNO=A.MNO AND LOCATION=A.LO ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE TEMP.dbo.TMP_MOCP10PRSUM SET POTOTAL=WNO FROM (SELECT MACHNO MNO, LOCATION LO, COUNT(*)WNO FROM TEMP.dbo.TMP_MOCP10PR WHERE TB022 <> '' GROUP BY MACHNO, LOCATION) A WHERE MACHNO=A.MNO AND LOCATION=A.LO ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE TEMP.dbo.TMP_MOCP10PRSUM SET POAPP=WNO FROM (SELECT MACHNO MNO, LOCATION LO, COUNT(*)WNO FROM TEMP.dbo.TMP_MOCP10PR WHERE TB022<> '' AND POSURE='Y' GROUP BY MACHNO, LOCATION) A WHERE MACHNO=A.MNO AND LOCATION=A.LO ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.Append("UPDATE TEMP.dbo.TMP_MOCP10PRSUM SET MOCNO=TA005 FROM TEMP.dbo.TMP_MOCP10PR WHERE TEMP.dbo.TMP_MOCP10PRSUM.MACHNO=TEMP.dbo.TMP_MOCP10PR.MACHNO AND TEMP.dbo.TMP_MOCP10PRSUM.LOCATION=TEMP..TMP_MOCP10PR.LOCATION ");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // commit data
                    transaction.Commit();

                    int tmpCount = majorData.Rows.Count;
                    // Put sheet Detail data
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start put data Detail");
                    sheetsData.Tables.Add(majorData);

                    // Put sheet Summary data
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start put data Summary");
                    majorData = new DataTable("Summary");
                    sb.Clear();
                    sb.Append("SELECT TA201, TA200, TA024, TA025, '', '', TB012,'', TB004, TB005, FUNQTY, QTY_NOT, WKNODELV ")
                        .Append("FROM TEMP.dbo.TMP_MOCP10P ORDER BY TB003");
                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    majorData.Merge(tmpDt);
                    sheetsData.Tables.Add(majorData);

                    // Put sheet PR_Detail data
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start put data PR_Detail");
                    majorData = new DataTable("PR_Detail");
                    sb.Clear();
                    sb.Append("SELECT MACHNO, LOCATION, TB001, TB002, TB003, TA003, TA004, TA005, TA006, TB004, TB005, TB006, TB009, TB014, TB016, TB022, TB025, MOCSTATUS, POQTY, POSURE ")
                        .Append(" From TEMP.dbo.TMP_MOCP10PR  WHERE MOCSTATUS <> 'Y' OR MOCSTATUS <>'y' ORDER BY MACHNO, TB004 ");
                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    majorData.Merge(tmpDt);
                    sheetsData.Tables.Add(majorData);

                    // Put sheet PR_Summary data
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " start put data PR_Summary");
                    majorData = new DataTable("PR_Summary");
                    sb.Clear();
                    sb.Append("SELECT MACHNO, LOCATION, MOCNO, PRTOTAL, PRAPP, Round(PRAPP/PRTOTAL,3)*100, POTOTAL, POAPP, POP=CASE POTOTAL WHEN 0 THEN 0 ELSE Round(POAPP/POTOTAL,3)*100 END ")
                        .Append("From TEMP.dbo.TMP_MOCP10PRSUM ORDER BY MACHNO  ");

                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    majorData.Merge(tmpDt);
                    sheetsData.Tables.Add(majorData);
                    Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " end collect data");
                }
                catch (SqlException)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                    }
                }

            }

            return sheetsData;
        }

        /// <summary>
        /// 取得BomP09 要顯示的產品編號
        /// </summary>
        /// <returns></returns>
        public static DataTable collectData_BomList()
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {

                sb.Clear();
                sb.Append(@"Select RTRIM(MC001) MC001, (RTRIM(MC001)+'_'+RTRIM(MB002)) as MB0012 From MACHVISION.dbo.BOMMC, MACHVISION.dbo.INVMB Where MC001=MB001 Order by MC001");

                try
                {
                    connection.Open();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                    if (majorData == null)
                    {
                        return null;
                    }
                }
                catch (SqlException)
                {
                    // do nothing
                }
            }

            sb.Clear();
            sb = null;

            return majorData;
        }

        public static bool checkData_BomInInvmb(string pattern)
        {
            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {

                sb.Clear();
                sb.Append(string.Format("SELECT * FROM MACHVISION.dbo.INVMB WHERE MB001='{0}'", pattern));

                try
                {
                    connection.Open();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                    if (majorData == null)
                    {
                        return false;
                    }
                    return majorData.Rows.Count > 0 ? true : false;

                }
                catch (SqlException)
                {
                    // do nothing
                    return false;
                }
            }
        }

        /// <summary>
        /// 取得Bom資料by BomID
        /// </summary>
        /// <param name="bomId"></param>
        /// <returns></returns>
        public static DataTable collectData_BomP09_VB6(string bomId, bool trimLv, bool includePrice)
        {

            DataTable majorData = null;
            DataTable tmpDt = null;
            StringBuilder sb = new StringBuilder();

            // 報表所需的資料
            decimal totalAmount = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
                SqlCommand command = connection.CreateCommand();
                try
                {
                    connection.Open();

                    // 取得執行時間的字串
                    // 為什麼是取3年前, 目前還不知道
                    DateTime endDate = System.DateTime.Now;
                    DateTime startDate = DateTime.Parse(endDate.AddMonths(-36).ToShortDateString());
                    string executeDate = DateTime.Now.ToString("yyyyMMdd");

                    // truncate tables
                    sb.AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09UP;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP11_VENDOR;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_A512;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP11UP;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_FOREIGN;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_ONLY_ONE;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    //執行sql statement 抄寫到TEMP資料庫的Table
                    sb.Clear();
                    sb.AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP11_VENDOR SELECT TC004, TD010, TC005, TD004, LEFT(A.CDATE,8) FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD,(SELECT MAX(PURTD.CREATE_DATE+'_'+TC001+'_'+TC002+'-'+TD003) CDATE, TD004 TD4 FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD WHERE TC001=TD001 AND TC002=TD002 AND TC014='Y' and TD010<> 0 GROUP BY TD004)A WHERE TC001=TD001 AND TC002=TD002 AND TC014='Y' AND PURTD.CREATE_DATE=LEFT(A.CDATE,8) AND A.TD4=PURTD.TD004 AND TD001=SUBSTRING(CDATE,10,4) AND TD002=SUBSTRING(CDATE,15,11) AND TD003=RIGHT(CDATE,4);")
                        .AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_FOREIGN(TD004, TC004) SELECT TD004, TC004 FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD, MACHVISION.dbo.PURMA WHERE TC001=TD001 AND TC002=TD002 AND TC004=MA001 AND MA057='2' AND TC014='Y' and TD018='Y' GROUP BY TC004, TD004;")
                        .AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_ONLY_ONE(TD004, TC004) SELECT TD004, TC004 FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD, MACHVISION.dbo.PURMA WHERE TC001=TD001 AND TC002=TD002 AND TC004=MA001 AND TC014='Y' and TD018='Y' GROUP BY TC004, TD004;")
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X, MD016X,  MO1) ")
                        .AppendLine(string.Format("Select MD001, MD003, '1', MD006, MB025, MB002, MB077, MD006, MD200, MD201, MD016, MD002 From MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD003=MB001 and MD001='{0}' and MD017<>'4' and (MD012='' or MD012>= '{1}') AND MB017<>'Y';", bomId, executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, A3, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1, MO2) ")
                        .AppendLine(string.Format("Select '{0}', MD001, MD003, '2', BOMMD.MD006*TEMP.dbo.TMP_BOMP09.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016X, MO1, MD002 From TEMP.dbo.TMP_BOMP09, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A2=BOMMD.MD001 and LV='1' and MD017<>'4' and A2<>'11502001' and (MD012='' or MD012>= '{1}') AND MB017<>'Y' AND MB025 <> 'Y';", bomId, executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, A3, A4, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X,  MD201X,MD016X,MO1,MO2,MO3) ")
                        .AppendLine(string.Format("Select A1, A2, MD001, MD003, '3', BOMMD.MD006*TEMP.dbo.TMP_BOMP09.MD006 , MB025, MB002, MB077, A9, MD200, MD201,MD016X, MO1,MO2,MD002 From TEMP.dbo.TMP_BOMP09, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A3=BOMMD.MD001 and LV='2' and MD017<>'4' and A3<>'11502001' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, A3, A4, A5,LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X, MD016X,MO1,MO2,MO3,MO4) ")
                        .AppendLine(string.Format("Select A1, A2, A3, MD001, MD003, '4', BOMMD.MD006*TEMP.dbo.TMP_BOMP09.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MD002 From TEMP.dbo.TMP_BOMP09, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A4=BOMMD.MD001 AND LV='3' and MD017<>'4' and A4<>'11502001' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, A3, A4, A5, A6, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5) ")
                        .AppendLine(string.Format("Select A1, A2, A3, A4, MD001, MD003, '5', BOMMD.MD006*TEMP.dbo.TMP_BOMP09.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4, MD002 From TEMP.dbo.TMP_BOMP09, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A5=BOMMD.MD001 AND LV='4' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, A2, A3, A4, A5, A6, A7, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5, MO6) ")
                        .AppendLine(string.Format("Select A1, A2, A3, A4, A5, MD001, MD003, '6', BOMMD.MD006*TEMP.dbo.TMP_BOMP09.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4,MO5, MD002 From TEMP.dbo.TMP_BOMP09, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A6=BOMMD.MD001 AND LV='5' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP09(A1, LV, MO1, MD006) ").AppendLine(string.Format("VALUES('{0}' , 0, '', 0 )", bomId));
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A2 WHERE A2<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A3 WHERE A3<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A4 WHERE A4<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A5 WHERE A5<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A6 WHERE A6<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET A8=A7 WHERE A7<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MO6=MO1 WHERE MO1<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MO6=MO2 WHERE MO2<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MO6=MO3 WHERE MO3<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MO6=MO4 WHERE MO4<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MO6=MO5 WHERE MO5<>'' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='.1' WHERE  LV='1' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='..2' WHERE  LV='2' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='...3' WHERE  LV='3' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='....4' WHERE  LV='4' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='.....5' WHERE  LV='5' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET LV='......6' WHERE  LV='6' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X=MB025 FROM MACHVISION.dbo.INVMB WHERE  A8=MB001 ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X='市購' WHERE  MB025X='P' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X='模組' WHERE  MB025X='M' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X='再製' WHERE  MB025X='S' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X='虛設' WHERE  MB025X='Y' OR MB025X='F' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MB025X='選配' WHERE  MB025X='O' ;");

                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP11UP SELECT Rtrim(X4) X4, TD010 AS UPRICE, TC005, LEFT(CDATE,8) FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD, (SELECT TD004 X4,MAX(CREATE_DATE+TD002+TD003)CDATE FROM MACHVISION.dbo.PURTD WHERE TD018<>'V' AND TD010<> 0 GROUP BY TD004)C WHERE TC001=TD001 AND TC002=TD002 AND TD004=C.X4 AND PURTD.CREATE_DATE=LEFT(CDATE,8) AND PURTD.TD002+PURTD.TD003=SUBSTRING(CDATE,9,15) AND TD010 <> 0 ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET SUBPN=MB004 FROM MACHVISION.dbo.BOMMB WHERE A8=MB001 AND MB002='****************************************' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET SUBPN=MB004 FROM MACHVISION.dbo.BOMMB WHERE A8=MB001 AND (MB002=A3 OR MB002=A4 OR MB002=A5) ;")
                        .AppendLine(string.Format("INSERT INTO TEMP.dbo.TMP_BOMP09UP SELECT Rtrim(X4), TD010*TC006 AS UPRICE, CDATE FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD,(SELECT TD004 X4,MAX(CREATE_DATE)CDATE FROM MACHVISION.dbo.PURTD WHERE TD018='Y' AND TD010<> 0 AND CREATE_DATE >'{0}' GROUP BY TD004)C WHERE TC001=TD001 AND TC002=TD002 AND TD004=C.X4 AND PURTD.CREATE_DATE=CDATE AND TD010 <> 0", startDate.ToString("yyyyMMdd")))
                        .AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_A512 SELECT TA006, TA022, TA032, CDATE FROM MACHVISION.dbo.MOCTA,(SELECT TA006 TA6, MAX(TA003)CDATE FROM MACHVISION.dbo.MOCTA WHERE TA013='Y' AND TA001='A512' AND TA022<> 0 GROUP BY TA006)B WHERE TA006=TA6 AND TA003=CDATE AND TA022 <> 0 ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET UP=UPRICE, UP_DATE=UPRICE_DATE FROM TEMP.dbo.TMP_BOMP09UP WHERE RTRIM(A8)=RTRIM(X4) ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET UP=TA022, MD016X=TA032, UP_DATE=TA022_DATE FROM TEMP.dbo.TMP_BOMP09_A512 WHERE RTRIM(A8)=RTRIM(TA006) AND UP=0 ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET UP=UPRICE, YN='Y', UP_DATE=UPRICE_DATE FROM TEMP.dbo.TMP_BOMP11UP, (SELECT MAX(LEFT(X4,11) ) AS X4MAX FROM TEMP.dbo.TMP_BOMP11UP WHERE SUBSTRING(X4,11,1)<>'' AND UPRICE <> 0 GROUP BY LEFT(X4,10))B  WHERE X4=X4MAX AND LEFT(RTRIM(A8),10)=LEFT(RTRIM(X4),10) AND UP=0 ;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_A512 ;")
                        .AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_A512 SELECT TA006, TA022, TA032, CDATE FROM MACHVISION.dbo.MOCTA,(SELECT TA006 TA6, MAX(TA003)CDATE FROM MACHVISION.dbo.MOCTA WHERE TA013='Y' AND TA001='A512' AND TA032='1M1468' GROUP BY TA006)B WHERE TA006=TA6 AND TA003=CDATE ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET CNC='Y' FROM TEMP.dbo.TMP_BOMP09_A512 WHERE RTRIM(A8)=RTRIM(TA006) ;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09UP ;")
                        .AppendLine(string.Format("INSERT INTO TEMP.dbo.TMP_BOMP09UP SELECT Rtrim(X4), TD010*TC006 AS UPRICE, CDATE FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD,(SELECT TD004 X4,MAX(CREATE_DATE)CDATE FROM MACHVISION.dbo.PURTD WHERE TD018='Y' AND TD010<> 0 AND CREATE_DATE <='{0}' GROUP BY TD004)C WHERE TC001=TD001 AND TC002=TD002 AND TD004=C.X4 AND PURTD.CREATE_DATE=CDATE AND TD010 <> 0", startDate.ToString("yyyyMMdd")))
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET UP=UPRICE, YEAR3='Y' FROM TEMP.dbo.TMP_BOMP09UP WHERE RTRIM(A8)=RTRIM(X4) AND UP=0  ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET FOREIGN_YN='Y' FROM TEMP.dbo.TMP_BOMP09_FOREIGN WHERE RTRIM(A8)=Rtrim(TD004) ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET ONLY_ONE='Y' FROM  (SELECT TD004 TD4 FROM TEMP.dbo.TMP_BOMP09_ONLY_ONE GROUP BY TD004 HAVING(COUNT(*))=1 )A WHERE RTRIM(A8)=Rtrim(A.TD4) ")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET AMT=ROUND(UP*MD006,0) WHERE MB025X <>'虛設' AND MB025X <>'F' ;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // 取得估價總量
                    sb.Clear();
                    sb.AppendLine("SELECT SUM(AMT) TOTAMT FROM TEMP.dbo.TMP_BOMP09");
                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    totalAmount = (decimal)tmpDt.Rows[0]["TOTAMT"];

                    // update 估價單資料
                    // 先取出A8, 再逐行執行update
                    sb.Clear();
                    sb.AppendLine("SELECT * FROM TEMP.dbo.TMP_BOMP09 WHERE A8 <> ''  ORDER BY MO1,MO2,MO3,MO4,MO5, MO6, LV");
                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    DataRowCollection drc = tmpDt.Rows;

                    foreach (DataRow dr in drc)
                    {
                        sb.Clear();
                        sb.AppendLine("SELECT TL004, TL005, TM004, TM010, TM014, TM015 FROM PURTL, PURTM, (SELECT MG001,MG004 FROM CMSMG, (SELECT MG001 G1, MAX(MG002) G2 FROM CMSMG GROUP BY MG001)B WHERE MG001=G1 AND MG002=G2) C, CMSMF WHERE TL001=TM001 AND TL002=TM002 and TL005=MG001 and TL005=MF001 ")
                            .AppendLine(string.Format(" and TM004='{0}' and TL006='Y' and ('{1}' < TM015  OR TM015='') AND '{2}' >= TM014 ", dr["A8"], executeDate, executeDate))
                            .AppendLine(" ORDER BY TM010, TM014 Desc, TL004 DESC ");
                        command.CommandText = sb.ToString();
                        tmpDt = MvDbConnector.queryDataBySql(command);

                        if (tmpDt.Rows.Count > 0)
                        {
                            DataRow tmpDr = tmpDt.Rows[0];
                            sb.Clear();
                            sb.Append(string.Format("UPDATE TEMP.dbo.TMP_BOMP09 SET APP_VENDOR='{0}', APP_CURR='{1}', APP_UP={2}, APP_DATE='{3}' WHERE A8='{4}' AND A1='{5}' AND A2='{6}' AND A3='{7}' AND A4='{8}'AND A5='{9}' AND A6='{10}'",
                                (string)tmpDr["TL004"], (string)tmpDr["TL005"], (decimal)tmpDr["TM010"], (string)tmpDr["TM014"], (string)dr["A8"], (string)dr["A1"], (string)dr["A2"], (string)dr["A3"], (string)dr["A4"], (string)dr["A5"], (string)dr["A6"]));
                            command.CommandText = sb.ToString();
                            command.ExecuteNonQuery();
                        }
                    }


                    // final summary
                    sb.Clear();
                    sb.AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET APP_UP=APP_UP * B.MG004 FROM MACHVISION.dbo.CMSMG, (SELECT MG001, MG004 FROM MACHVISION.dbo.CMSMG,(SELECT MG001 MG1 ,MAX(MG002)MG2 FROM MACHVISION.dbo.CMSMG GROUP BY MG001)B WHERE MG001=MG1 AND MG002=MG2)B  WHERE APP_CURR=B.MG001 ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET UP=APP_UP, MD016X=APP_VENDOR, APP_CODE='Y'  WHERE UP_DATE < APP_DATE AND APP_UP <> 0 ;")
                        .AppendLine("UPDATE  TEMP.dbo.TMP_BOMP09 SET AMT=MD006*UP  ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MD016X=TC004 FROM TEMP.dbo.TMP_BOMP11_VENDOR WHERE A8=TD004 AND APP_CODE <>'Y' ;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP09 SET MD016X=MA002 FROM MACHVISION.dbo.PURMA WHERE MD016X=MA001 ;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // get result to make excel report
                    sb.Clear();
                    sb.Append(string.Format("SELECT LV,SUBPN, A8, MB025X, Rtrim(MB201), Rtrim(MB202), Rtrim(MB203), Rtrim(MB002), Rtrim(MB206), Rtrim(MB205), Rtrim(MB204), Rtrim(MB207), MB003, MB004, MD006, RTRIM(MD200X), RTRIM(MD201X), RTRIM(MD016X), {0} MD016X, YN, CNC, YEAR3, FOREIGN_YN, ONLY_ONE, APP_CODE ", includePrice == true ? "UP, AMT," : ""))
                        .AppendLine("FROM TEMP.dbo.TMP_BOMP09 LEFT JOIN MACHVISION.dbo.INVMB ON A8=MB001 WHERE LV<> '0' ORDER BY MO1,MO2,MO3,MO4,MO5,MO6,LV desc");
                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = bomId;

                    // 取得資料後清除暫存table
                    sb.Clear();
                    sb.AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09UP;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP11_VENDOR;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_A512;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP11UP;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_FOREIGN;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_ONLY_ONE;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

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
                    connection.Close();
                    connection.Dispose();
                }
            }
            // 20180419 Steven
            // 因為LV 的資料在第6階會用 ....._6 因為在ORBER BY的時候, 如果用 ......6 會有排序的問題
            // 所以才會在第6碼換成_ , 避免排序的問題
            // 在新程式還是還原回來, 讓後端程式好輸出
            //foreach (DataRow dr in majorData.Rows)
            //{
            //    if(dr["LV"].ToString().Trim().Equals("....._6"))
            //    {
            //        dr["LV"] = dr["LV"].ToString().Replace('_', '.');
            //    }
            //}
            // 20180420 不用了, 直接在LV後面用DESC排序即可
            // 20180424 , 加入一開始取值時就把LV給Trim空白的功能
            if (trimLv == true)
            {
                foreach (DataRow dr in majorData.Rows)
                {
                    dr["LV"] = dr["LV"].ToString().Trim();
                }
            }


            // release parameter
            tmpDt.Dispose();
            tmpDt = null;
            sb = null;

            return majorData;
        }

        /// <summary>
        /// 取得多張bom list
        /// </summary>
        /// <param name="bomIdList"></param>
        /// <returns></returns>
        public static DataSet collectData_BomP09_VB6(string[] bomIdList)
        {

            DataSet ds = new DataSet("BomTables");

            DataTable dt = null;
            foreach (string bomId in bomIdList)
            {
                dt = null;
                bool isExist = checkData_BomInInvmb(bomId);
                if (isExist == false)
                {
                    continue;
                }
                dt = collectData_BomP09_VB6(bomId, true, false);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        public static bool isInItPcList(string pcName)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            try
            {
                using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    sb.AppendLine(string.Format("SELECT * FROM itReportLog.dbo.PCList WHERE upper(PCName) = {0}", pcName));
                    command.CommandText = sb.ToString();
                    dt = MvDbConnector.queryDataBySql(command);
                    if (dt == null) { return false; }
                    return dt.Rows.Count > 0 ? true : false;
                }
            }
            catch (SqlException)
            { return false; }
        }

        public static DataTable queryData_ItReportLog()
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    sb.Clear();
                    sb.AppendLine("SELECT * FROM itReportLog.dbo.PCList");
                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = "PCList";
                }
            }
            catch (SqlException)
            { }

            sb.Clear();
            sb = null;

            return majorData;
        }

        public static DataTable collectData_BomP09_Thin(string bomId)
        {

            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
                SqlCommand command = connection.CreateCommand();
                try
                {
                    connection.Open();

                    sb.AppendLine(string.Format(@"SELECT LV, MD003 as A8, MB025 as MB025X, MB002 as Column4, MB004, MD006, RTRIM(MD200) as Column9, MD013 FROM MACHVISION.dbo.GetBomPartList('{0}')", bomId));
                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = bomId;

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
                    connection.Close();
                    connection.Dispose();
                }
            }

            // release parameter
            sb = null;

            return majorData;
        }

        public static DataSet collectData_BomP09_Thin(string[] bomIdList)
        {

            DataSet ds = new DataSet("BomTables");

            DataTable dt = null;
            foreach (string bomId in bomIdList)
            {
                dt = null;
                bool isExist = checkData_BomInInvmb(bomId);
                if (isExist == false)
                {
                    continue;
                }
                dt = collectData_BomP09_Thin(bomId);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        public static DataTable collectData_PurP17_VB6(string dateTime)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
                SqlCommand command = connection.CreateCommand();
                try
                {
                    connection.Open();

                    sb.AppendLine("Truncate Table TEMP.dbo.TMP_PURP17RV ")
                        .AppendLine("INSERT INTO  TEMP.dbo.TMP_PURP17RV  SELECT TD012, TC004, MA002, TD004,TD005,TD006, TC200, TD007, TD008, TD015, TD008-TD015, TC003,TD201, MV002, TD001+'-'+TD002+'-'+TD003, TD024, TD014 ")
                        .AppendLine("  FROM MACHVISION.dbo.PURTC, MACHVISION.dbo.PURTD, MACHVISION.dbo.PURTA LEFT JOIN MACHVISION.dbo.CMSMV ON TA012=MV001, MACHVISION.dbo.PURMA ")
                        .AppendLine(" WHERE TC001=TD001 and TC002=TD002 and TC004=MA001 and TD026=TA001 and TD027=TA002 ")
                        .AppendLine(string.Format("  and TC004=MA001 and TD018='Y' and TD016='N' and TD012='{0}' ", dateTime))
                        .AppendLine(" Order by TD012, TC004, TD001, TD002, TD003")
                        .AppendLine("UPDATE TEMP.dbo.TMP_PURP17RV SET TD015=TH007 FROM MACHVISION.dbo.PURTH WHERE LEFT(PONO,4)=TH011 AND SUBSTRING(PONO,6,11)=TH012 AND SUBSTRING(PONO,18,4)=TH013 AND TH030 <> 'V' ")
                        .AppendLine("SELECT TD012, TC004, MA002, TD004, TD005, TD006, TC200, TD007, TD008, TD015, TD008 - TD015, TC003, TD201, MV002, PONO, TD024, TD014  ")
                        .AppendLine("  FROM TEMP.dbo.TMP_PURP17RV Order by TD012, TC004,  PONO ");

                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = dateTime;

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
                    connection.Close();
                    connection.Dispose();
                }
            }

            // release parameter
            sb = null;

            return majorData;

        }


        public static DataTable collectData_Moc(SqlConnection connection, string parentMocNo)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            if (parentMocNo == null || parentMocNo.Length == 0 || parentMocNo.IndexOf("-") < 0) { return null; }
            string[] tmpStr = parentMocNo.Split('-');
            string mocType = tmpStr[0];
            string mocNumber = tmpStr[1];

            try
            {
                connection.Open();

                //sb.AppendLine("SELECT TA024+'-'+TA025 '母製令', TB001+'-'+TB002 '子製令',TA006 '模組', TB003 '品號', TB012 '品名', TB013'規格', Convert(int,TB004) '需領用量', Convert(int,TB005) '已領用量', MB077 '類別' ")
                // 先用來源單號串出所有母及子製令內容
                sb.AppendLine("SELECT TA024+'-'+TA025 'ParentMoc', TB001+'-'+TB002 'ChildMoc', TA201, RTRIM(TA006) TA006, RTRIM(TB003) TB003, TB012, TB013, Convert(int,TB004) 'TB004', Convert(int,TB005) 'TB005', MB077, TA200, TB201, TB017 ")
                    .AppendLine("FROM ERPDB2.MACHVISION.dbo.MOCTA A, ERPDB2.MACHVISION.dbo.MOCTB B, ERPDB2.MACHVISION.dbo.INVMB MB ")
                    .AppendLine(string.Format("WHERE TA024 = '{0}' AND TA025 = '{1}' AND A.TA001 = B.TB001 AND A.TA002 = B.TB002 AND TB003 = MB001 AND TB018 <> 'V' ORDER BY B.TB001, B.TB002", mocType.ToUpper(), mocNumber));

                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());

                // 如果輸入為子製令, 串出子製令內容
                sb.Clear();
                if (majorData.Rows.Count == 0)
                {
                    sb.AppendLine("SELECT TA024+'-'+TA025 'ParentMoc', TB001+'-'+TB002 'ChildMoc', TA201, RTRIM(TA006) TA006, RTRIM(TB003) TB003, TB012, TB013, Convert(int,TB004) 'TB004', Convert(int,TB005) 'TB005', MB077, TA200, TB201, TB017 ")
                        .AppendLine("FROM ERPDB2.MACHVISION.dbo.MOCTA A, ERPDB2.MACHVISION.dbo.MOCTB B, ERPDB2.MACHVISION.dbo.INVMB MB ")
                        .AppendLine(string.Format("WHERE TA001 = '{0}' AND TA002 = '{1}' AND A.TA001 = B.TB001 AND A.TA002 = B.TB002 AND TB003 = MB001 AND TB018 <> 'V' ORDER BY B.TB001, B.TB002", mocType.ToUpper(), mocNumber));
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                }

                majorData.TableName = parentMocNo;

            }
            catch (SqlException se)
            {
                //發生例外時，會自動rollback
                throw se;
            }

            // release parameter
            sb = null;

            return majorData;
        }

        public static DataTable collectData_Moc(string parentMocNo)
        {
            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                return collectData_Moc(connection, parentMocNo);
            }
        }

        /// <summary>
        /// 取得MxMail的產生資料
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable collectData_ItMxMail(string startDate, string endDate)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
            SqlCommand command = connection.CreateCommand();
            try
            {
                connection.Open();

                sb.Append("SELECT Convert(nvarchar,Convert(datetime,datetime2),111)'建立日期', ")
                    .Append("Convert(nvarchar,Convert(datetime,datetime2),111)'到職日', ")
                    .Append("text1 '工號',text2 '姓名',text4'使用者帳號',Left(text4,1)+Right(replace(text5,'-',''),5) AS '使用者密碼', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%' THEN replace(text3,' ','')+text2+'_CE' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN replace(text3,' ','')+text2+'_CS' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN replace(text3,' ','')+text2+'_CC' ")
                    .Append("   ELSE replace(text3,' ','')+text2+'_'+select2 ")
                    .Append("END'使用者姓名' ")
                    .Append(",'500' AS'QUOTA','1' AS'WM','' AS'WH','' AS'SMB','' AS'FTP','' AS'WEB','' AS'SQL','0' AS'EXP', ")
                    .Append("replace(text3,' ','')'LDAP姓氏', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%' THEN text2+'_CE' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN text2+'_CS' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN text2+'_CC' ")
                    .Append("   ELSE text2+'_'+select2 ")
                    .Append("END'LDAP名字' ")
                    .Append(",text6'電子郵件', ")
                    .Append("'' AS'電話','' AS'傳真','' AS'行動電話','' AS'地址','' AS'網頁', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%'  THEN 'CE中國華東' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN 'CS中國華南' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN 'CC中國華中' ")
                    .Append("   ELSE select2+dep.DepartmentName ")
                    .Append("END'公司' ")
                    .Append(",emp.TitleName'職稱', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%'  THEN 'CE中國華東' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN 'CS中國華南' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN 'CC中國華中' ")
                    .Append("   ELSE select2+dep.DepartmentName ")
                    .Append("END'部門' ")
                    .Append(",'MACHVISION牧德科技' AS'辦公室', ")
                    .Append("'' AS'辦公室電話','' AS'辦公室傳真','' AS'IP電話','' AS'呼叫器','' AS'國家','' AS'郵遞區號','' AS'縣/市','' AS'鄉/鎮/市/區','' AS'街名','' AS'其它' ")
                    .Append("FROM [EFNETDB].[dbo].[mvhr08] hr , ERPBK.mvWorkFlow.dbo.Department dep , ERPBK.mvWorkFlow.dbo.EmployeeTitle emp , [EFNETDB].[dbo].[resda] da ")
                    .Append("WHERE hr.select2 = dep.DepartmentID and hr.select3 = emp.TitleCode ")
                    .Append(string.Format("AND (Convert(nvarchar,Convert(datetime,hr.datetime2),111) >= '{0}' and Convert(nvarchar,Convert(datetime,hr.datetime2),111) <= '{1}') ", startDate, endDate))
                    .Append("AND emp.Disable = 0 AND (da.resda021 IN ('1', '2')) AND hr.mvhr08001 = da.resda001 AND hr.mvhr08002 = da.resda002 ")
                    .Append("ORDER BY datetime2 DESC ");

                command.CommandText = sb.ToString();
                majorData = MvDbConnector.queryDataBySql(command);
                majorData.TableName = "ItMxMail";

            }
            catch (SqlException se)
            {
                //發生例外時，會自動rollback
                throw se;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            // release parameter
            sb = null;

            return majorData;
        }


        /// <summary>
        /// 取得MxMail的產生資料, 此版本有更改password規則
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable collectData_ItMxMail_20190904(string startDate, string endDate)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
            SqlCommand command = connection.CreateCommand();
            try
            {
                connection.Open();

                sb.Append("SELECT Convert(nvarchar,Convert(datetime,datetime2),111)'建立日期', ")
                    .Append("Convert(nvarchar,Convert(datetime,datetime2),111)'到職日', ")
                    .Append("text1 '工號',text2 '姓名',text4'使用者帳號', ")
                    .Append("Left(text4,1)+Left(Right(replace(datetime2,'/',''),2),1)+Left(Right(replace(text5,'-',''),4),3)+Right(replace(datetime2,'/',''),1) AS '使用者密碼', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%' THEN replace(text3,' ','')+text2+'_CE' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN replace(text3,' ','')+text2+'_CS' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN replace(text3,' ','')+text2+'_CC' ")
                    .Append("   ELSE replace(text3,' ','')+text2+'_'+select2 ")
                    .Append("END'使用者姓名', ")
                    .Append("'500' AS'QUOTA','1' AS'WM','' AS'WH','' AS'SMB','' AS'FTP','' AS'WEB','' AS'SQL','0' AS'EXP', ")
                    .Append("replace(text3,' ','')'LDAP姓氏', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%' THEN text2+'_CE' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN text2+'_CS' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN text2+'_CC' ")
                    .Append("   ELSE text2+'_'+select2 ")
                    .Append("END'LDAP名字' ")
                    .Append(",text6'電子郵件', ")
                    .Append("'' AS'電話','' AS'傳真','' AS'行動電話','' AS'地址','' AS'網頁', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%'  THEN 'CE中國華東' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN 'CS中國華南' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN 'CC中國華中' ")
                    .Append("   ELSE select2+dep.DepartmentName ")
                    .Append("END'公司' ")
                    .Append(",emp.TitleName'職稱', ")
                    .Append("CASE ")
                    .Append("   WHEN dep.DepartmentName like '%華東%'  THEN 'CE中國華東' ")
                    .Append("   WHEN dep.DepartmentName like '%華南%' THEN 'CS中國華南' ")
                    .Append("   WHEN dep.DepartmentName like '%華中%' THEN 'CC中國華中' ")
                    .Append("   ELSE select2+dep.DepartmentName ")
                    .Append("END'部門' ")
                    .Append(",'MACHVISION牧德科技' AS'辦公室', ")
                    .Append("'' AS'辦公室電話','' AS'辦公室傳真','' AS'IP電話','' AS'呼叫器','' AS'國家','' AS'郵遞區號','' AS'縣/市','' AS'鄉/鎮/市/區','' AS'街名','' AS'其它' ")
                    .Append("FROM [EFNETDB].[dbo].[mvhr08] hr , ERPBK.mvWorkFlow.dbo.Department dep , ERPBK.mvWorkFlow.dbo.EmployeeTitle emp , [EFNETDB].[dbo].[resda] da ")
                    .Append("WHERE hr.select2 = dep.DepartmentID and hr.select3 = emp.TitleCode ")
                    .Append(string.Format("AND (Convert(nvarchar,Convert(datetime,hr.datetime2),111) >= '{0}' and Convert(nvarchar,Convert(datetime,hr.datetime2),111) <= '{1}') ", startDate, endDate))
                    .Append("AND emp.Disable = 0 AND (da.resda021 IN ('1', '2')) AND hr.mvhr08001 = da.resda001 AND hr.mvhr08002 = da.resda002 ")
                    .Append("ORDER BY datetime2 DESC ");

                command.CommandText = sb.ToString();
                majorData = MvDbConnector.queryDataBySql(command);
                majorData.TableName = "ItMxMail";

            }
            catch (SqlException se)
            {
                //發生例外時，會自動rollback
                throw se;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            // release parameter
            sb = null;

            return majorData;
        }

        /// <summary>
        /// 取得多張bom list
        /// </summary>
        /// <param name="bomIdList"></param>
        /// <returns></returns>
        public static DataSet collectData_BomP07_VB6(string[] bomIdList)
        {

            DataSet ds = new DataSet("BomTables");

            DataTable dt = null;
            foreach (string bomId in bomIdList)
            {
                dt = null;
                // BomP09跟BomP07的檢查邏輯相同, 所以直接引用
                bool isExist = checkData_BomInInvmb(bomId);
                if (isExist == false)
                {
                    continue;
                }
                dt = collectData_BomP07_VB6(bomId, false);

                ds.Tables.Add(dt);
            }
            return ds;
        }


        /// <summary>
        /// 取得Bom資料by BomID for PD
        /// </summary>
        /// <param name="bomId"></param>
        /// <returns></returns>
        public static DataTable collectData_BomP07_VB6(string bomId, bool trimLv)
        {

            DataTable majorData = null;
            DataTable tmpDt = null;
            StringBuilder sb = new StringBuilder();

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
                SqlCommand command = connection.CreateCommand();
                try
                {
                    connection.Open();

                    // 取得執行時間的字串
                    // 為什麼是取3年前, 目前還不知道
                    DateTime endDate = System.DateTime.Now;
                    DateTime startDate = DateTime.Parse(endDate.AddMonths(-36).ToShortDateString());
                    string executeDate = DateTime.Now.ToString("yyyyMMdd");


                    // truncate tables
                    sb.AppendLine("Truncate Table TEMP.dbo.TMP_BOMP07;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_FOREIGN;")
                        .AppendLine("Truncate Table TEMP.dbo.TMP_BOMP09_ONLY_ONE;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    //執行sql statement 抄寫到TEMP資料庫的Table
                    sb.Clear();
                    sb.AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_FOREIGN(TD004, TC004) SELECT TD004, TC004 FROM PURTC, PURTD, PURMA WHERE TC001=TD001 AND TC002=TD002 AND TC004=MA001 AND MA057='2' AND TC014='Y' and TD018='Y' GROUP BY TC004, TD004;")
                        .AppendLine("INSERT INTO TEMP.dbo.TMP_BOMP09_ONLY_ONE(TD004, TC004) SELECT TD004, TC004 FROM PURTC, PURTD, PURMA WHERE TC001=TD001 AND TC002=TD002 AND TC004=MA001 AND TC014='Y' and TD018='Y' GROUP BY TC004, TD004;")
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X, MD016X,  MO1, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("SELECT MD001, MD003, '1', MD006, MB025, MB002, MB077, MD006, MD200,MD201,MD016, MD002, MD032, MD011, MD013 From MACHVISION..BOMMD, MACHVISION..INVMB WHERE MD003=MB001 and MD001='{0}' and MD017<>'4' and (MD012='' or MD012>= '{1}') AND MB017<>'Y';", bomId, executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1, MO2, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select '{0}', MD001, MD003, '2', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016X, MO1, MD002, MD032, MD011, MD013  From TEMP..TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A2=MACHVISION.dbo.BOMMD.MD001 and LV='1' and MD017<>'4' and (MD012='' or MD012>= '{1}') AND MB017<>'Y' AND MB025 <> 'Y';", bomId, executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X,  MD201X,MD016X,MO1,MO2,MO3, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select A1, A2, MD001, MD003, '3', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016X, MO1,MO2,MD002, MD032, MD011, MD013  From TEMP.dbo.TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A3=MACHVISION.dbo.BOMMD.MD001 and LV='2' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, A5,LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X, MD016X,MO1,MO2,MO3,MO4, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select A1, A2, A3, MD001, MD003, '4', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MD002, MD032, MD011, MD013  From TEMP.dbo.TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A4=MACHVISION.dbo.BOMMD.MD001 AND LV='3' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, A5,LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select A1, A2, A3, MD001, MD003, '5', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4, MD002, MD032, MD011, MD013  From TEMP..TMP_BOMP07,MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A5=MACHVISION.dbo.BOMMD.MD001 AND LV='4' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, A5, A6, LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5,MO6, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select A1, A2, A3, A4, MD001, MD003, '6', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4, MO5, MD002, MD032, MD011, MD013  From TEMP..TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A5=MACHVISION.dbo.BOMMD.MD001 AND LV='5' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, A5, A6,A7,LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5,MO6,MO7, YN, MD011X,MD013X) ")
                        .AppendLine(string.Format("Select A1, A2, A3, A4, A5, MD001, MD003, '7', MACHVISION.dbo.BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4, MO5, MO6,MD002, MD032, MD011, MD013  From TEMP.dbo.TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A6=MACHVISION.dbo.BOMMD.MD001 AND LV='6' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate))
                        .Append("INSERT INTO TEMP.dbo.TMP_BOMP07(A1, A2, A3, A4, A5, A6,A7,A8,LV, MD006, MB025X, A2NAME, MB077X, A9, MD200X, MD201X,MD016X, MO1,MO2,MO3,MO4, MO5,MO6, YN) ")
                        .AppendLine(string.Format("Select A1, A2, A3, A4, A5, A6,MD001, MD003, '7', BOMMD.MD006, MB025, MB002, MB077, A9, MD200, MD201,MD016, MO1,MO2,MO3,MO4, MO5, MD002, MD032 From TEMP.dbo.TMP_BOMP07, MACHVISION.dbo.BOMMD, MACHVISION.dbo.INVMB WHERE MD001=MB001 and A7=MACHVISION.dbo.BOMMD.MD001 AND LV='7' and MD017<>'4' and (MD012='' or MD012>= '{0}') AND MB017<>'Y' AND MB025 <> 'Y';", executeDate));
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.AppendLine(string.Format("INSERT INTO TEMP..TMP_BOMP07(A1, LV,  MO1, MD006) VALUES('{0}', 0, '', 0 );", bomId))
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A2 WHERE A2<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A3 WHERE A3<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A4 WHERE A4<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A5 WHERE A5<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A6 WHERE A6<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET A8=A7 WHERE A7<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO1 WHERE MO1<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO2 WHERE MO2<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO3 WHERE MO3<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO4 WHERE MO4<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO5 WHERE MO5<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO6 WHERE MO6<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MO9=MO7 WHERE MO7<>'';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='.1' WHERE  LV='1';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='..2' WHERE  LV='2';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='...3' WHERE  LV='3';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='....4' WHERE  LV='4';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='.....5' WHERE  LV='5';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='......6' WHERE  LV='6';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET LV='.......7' WHERE  LV='7';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X=MB025 FROM INVMB WHERE A8=MB001;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='市購' WHERE  MB025X='P';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='模組' WHERE  MB025X='M';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='再製' WHERE  MB025X='S';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='虛設' WHERE  MB025X='Y';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='Feature件' WHERE MB025X='F';")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MB025X='選配' WHERE  MB025X='O' ;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    sb.Clear();
                    sb.AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET MD201X=MD201 FROM TEMP..MV_BOMMD_P WHERE A2=MD001 AND A8=MD003 AND A1=PROD;")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET FOREIGN_YN='Y' FROM TEMP.dbo.TMP_BOMP09_FOREIGN WHERE Rtrim(A8)=Rtrim(TD004);")
                        .AppendLine("UPDATE TEMP.dbo.TMP_BOMP07 SET ONLY_ONE='Y' FROM (SELECT TD004 TD4 FROM TEMP.dbo.TMP_BOMP09_ONLY_ONE Group By TD004 Having(Count(*))=1 )A WHERE Rtrim(A8)=Rtrim(A.TD4);");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // update 取替代料欄位資訊, SUBPN
                    sb.Clear();
                    sb.AppendLine("SELECT * FROM TEMP.dbo.TMP_BOMP07 ORDER BY A8");
                    command.CommandText = sb.ToString();
                    tmpDt = MvDbConnector.queryDataBySql(command);
                    DataRowCollection drc = tmpDt.Rows;

                    foreach (DataRow dr in drc)
                    {
                        sb.Clear();
                        sb.AppendLine(string.Format("SELECT * FROM MACHVISION.dbo.BOMMB WHERE MB001='{0}' AND MB002='****************************************' ", dr["A8"]));
                        command.CommandText = sb.ToString();
                        tmpDt = MvDbConnector.queryDataBySql(command);

                        // 如果沒有取替代料, 則不需再更新SUBPN 及S1~S5的欄位資料
                        int rowCount = tmpDt.Rows.Count;
                        if (rowCount == 0)
                        {
                            continue;
                        }
                        else if (rowCount > 1)
                        {
                            Console.WriteLine("test");
                        }

                        // 目前取替代料的排序最多5個, 所以先預設5個
                        string finalSUBPN = string.Empty;
                        string tmpSUBPN = string.Empty;
                        string[] seqSUBPN = new string[Model.DefinedHeader.BomP07_MaxSUBPN];

                        for (int i = 0; i < rowCount; i++)
                        {
                            //DataRow tmpDr1 = tmpDt.Rows[i];
                            tmpSUBPN = tmpDt.Rows[i]["MB004"].ToString().Trim();
                            seqSUBPN[i] = tmpSUBPN;
                            finalSUBPN += tmpSUBPN + ";";
                        }

                        // 將finalSUBPN去除最尾的分號
                        finalSUBPN = finalSUBPN.Remove(finalSUBPN.Length - 1, 1);
                        // 組合update 取替代料的Sql
                        sb.Clear();
                        sb.Append(string.Format("UPDATE TEMP.dbo.TMP_BOMP07 SET SUBPN='{0}' ", finalSUBPN));
                        int index = 0;
                        for (int i = 0; i < rowCount; i++)
                        {
                            index = i + 1;
                            sb.Append(string.Format(", S{0}='{1}' ", index, seqSUBPN[i]));
                        }
                        sb.Append(string.Format(" WHERE DATANO={0} ", dr["DATANO"]));
                        // 執行取替代料作業
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();
                    }

                    sb.Clear();
                    sb.AppendLine("UPDATE  TEMP.dbo.TMP_BOMP07 SET PN_NO1=MB004 FROM MACHVISION.dbo.BOMMB WHERE A8=MB001 AND MB010='Y';")
                        .AppendLine("UPDATE  TEMP.dbo.TMP_BOMP07 SET PN_NO1=MB004 FROM MACHVISION.dbo.BOMMB WHERE S1=MB001 AND MB010='Y' AND MB004=A8;")
                        .AppendLine("UPDATE  TEMP.dbo.TMP_BOMP07 SET PN_NO1=MB004 FROM MACHVISION.dbo.BOMMB WHERE S2=MB001 AND MB010='Y' AND MB004=A8;")
                        .AppendLine("UPDATE  TEMP.dbo.TMP_BOMP07 SET PN_NO1=MB004 FROM MACHVISION.dbo.BOMMB WHERE S3=MB001 AND MB010='Y' AND MB004=A8;");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();

                    // get result to make excel report
                    sb.Clear();
                    sb.Append("SELECT LV, SUBPN, A8, MB025X, Rtrim(MB201), Rtrim(MB202), Rtrim(MB203), Rtrim(MB002), Rtrim(MB206),Rtrim(MB205), Rtrim(MB204), Rtrim(MB207), MB003, MB004, MD006, ")
                        .Append("Rtrim(MD200X), Rtrim(MD201X), Rtrim(MD016X), YN, MB037, MB209, MB077, MD011X, MD013X, FOREIGN_YN, ONLY_ONE, Rtrim(PN_NO1) ")
                        .Append("FROM TEMP.dbo.TMP_BOMP07 LEFT JOIN MACHVISION.dbo.INVMB ON A8=MB001 WHERE LV<>'0' ORDER BY MO1,MO2, MO3, MO4, MO5, MO6, MO7,LV ");
                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = bomId;

                    // 取得資料後清除暫存table
                    //sb.Clear();
                    //sb.AppendLine("Truncate Table TEMP..TMP_BOMP07;")
                    //    .AppendLine("Truncate Table TEMP..TMP_BOMP09_FOREIGN;")
                    //    .AppendLine("Truncate Table TEMP..TMP_BOMP09_ONLY_ONE;");
                    //command.CommandText = sb.ToString();
                    //command.ExecuteNonQuery();

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
                    connection.Close();
                    connection.Dispose();
                }
            }

            // 20180424 , 加入一開始取值時就把LV給Trim空白的功能
            if (trimLv == true)
            {
                foreach (DataRow dr in majorData.Rows)
                {
                    dr["LV"] = dr["LV"].ToString().Trim();
                }
            }


            // release parameter
            if (tmpDt != null) { tmpDt.Dispose(); }
            tmpDt = null;
            sb = null;

            return majorData;
        }

        /// <summary>
        /// 取得多張bom list
        /// </summary>
        /// <param name="bomIdList"></param>
        /// <returns></returns>
        public static DataSet collectData_BomP07_Thin(string[] bomIdList)
        {

            DataSet ds = new DataSet("BomTables");

            DataTable dt = null;
            foreach (string bomId in bomIdList)
            {
                dt = null;
                // BomP09跟BomP07的檢查邏輯相同, 所以直接引用
                bool isExist = checkData_BomInInvmb(bomId);
                if (isExist == false)
                {
                    continue;
                }
                dt = collectData_BomP07_Thin(bomId);

                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// 取得BomP07 for PD使用, 含取替代料及優先耗用
        /// </summary>
        /// <param name="bomId"></param>
        /// <returns></returns>
        public static DataTable collectData_BomP07_Thin(string bomId)
        {

            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION;
                SqlCommand command = connection.CreateCommand();
                try
                {
                    connection.Open();

                    sb.AppendLine("SELECT LV, LVNo, MD001, MD003, MB002, MB077, MB025, MB004, MD006, MD200, ")
                        .AppendLine("(SELECT SUBSTRING((SELECT ';'+RTRIM(MB004) FROM MACHVISION.dbo.BOMMB AS mb WHERE mb.MB001=bom.MD003 ORDER BY MB009 FOR XML PATH('')),2,4000)) AS MB004_REPLACED,			--取替代料 ")
                        .AppendLine("(SELECT TOP 1 CASE WHEN mb.MB010='N' THEN RTRIM(mb.MB001) ELSE RTRIM(mb.MB004) END FROM MACHVISION..BOMMB AS mb WHERE mb.MB001=bom.MD003 ORDER BY MB009) AS MB004_FIRST		--優先耗用 ")
                        .AppendLine(string.Format(@"FROM MACHVISION.dbo.GetBomPartList('{0}') AS bom ", bomId));

                    command.CommandText = sb.ToString();
                    majorData = MvDbConnector.queryDataBySql(command);
                    majorData.TableName = bomId;

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
                    connection.Close();
                    connection.Dispose();
                }
            }

            // release parameter
            sb = null;

            return majorData;
        }


        public static bool checkData_hasIllegalItemInInvmb(string[] itemList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @dtTempTable TABLE (MB001 NVARCHAR(50));");
            foreach (string item in itemList)
            {
                sb.Append(string.Format("INSERT INTO @dtTempTable (MB001) VALUES ('{0}');", item));
            }
            sb.Append("SELECT DISTINCT(MB001) FROM @dtTempTable temp WHERE NOT EXISTS (SELECT MB001 FROM MACHVISION.dbo.INVMB source WHERE source.MB001 = temp.MB001);");

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                try
                {
                    connection.Open();
                    return MvDbConnector.hasRowsBySq1(connection, sb.ToString());
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
        }


        public static DataTable checkData_hasIllegalItemListInInvmb(string[] itemList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DECLARE @dtTempTable TABLE (MB001 NVARCHAR(50));");
            foreach (string item in itemList)
            {
                sb.Append(string.Format("INSERT INTO @dtTempTable (MB001) VALUES ('{0}');", item));
            }
            sb.Append("SELECT DISTINCT(MB001) FROM @dtTempTable temp WHERE NOT EXISTS (SELECT MB001 FROM MACHVISION.dbo.INVMB source WHERE source.MB001 = temp.MB001);");

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                try
                {
                    connection.Open();
                    return MvDbConnector.queryDataBySql(connection, sb.ToString());
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
        }

        /// <summary>
        /// 取得IT.dbo.NetworkDevice內的所有資料
        /// </summary>
        /// <returns></returns>
        public static DataTable collectData_ItNetworkDevice(string serverIp = "")
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (SqlConnection connection = MvDbConnector.Connection_ERPBK_Dot_IT)
            {
                sb.Clear();
                sb.Append("SELECT * FROM IT.dbo.NetworkDevice ");
                if (serverIp.Length != 0)
                {
                    sb.Append(string.Format("WHERE DeviceIP = '{0}'", serverIp));
                }

                try
                {
                    connection.Open();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
            return majorData;
        }

        public static DataTable collectData_Cmsmq()
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                sb.Clear();
                sb.Append("select * FROM MACHVISION.dbo.CMSMQ ORDER BY MQ001 ");

                try
                {
                    connection.Open();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
            return majorData;
        }

        public static DataTable collectData_GetOrderTypeSummary(string orderType, string startDate, string endDate)
        {
            DataTable majorData = null;
            StringBuilder sb = new StringBuilder();
            string[] inDisabledOrderType = new string[] { "31", "330A", "330S", "33CE", "A339" };
            string[] inTablePURTA = new string[] { "3101", "3102", "3103", "3104", "3105", "3109", "310X", "31X4" };
            string[] inTablePURTC = new string[] { "3301", "3302", "3303", "3304", "3305", "3309" };

            bool isInDisableOrderType = false;
            bool isInTablePURTA = false;
            bool isInTablePURTC = false;

            isInDisableOrderType = Array.Exists(inDisabledOrderType, element => element == orderType);
            isInTablePURTA = Array.Exists(inTablePURTA, element => element == orderType);
            isInTablePURTC = Array.Exists(inTablePURTC, element => element == orderType);

            if (isInDisableOrderType == true)
            {
                return null;
            }
            else if (isInTablePURTA == true)
            {
                sb.AppendLine(@"SELECT TA.CREATE_DATE, TA.TA001 ""TA001"", TA.TA002 ""TA002"", TB.TB039 ""TB039"", COUNT(*) ""SUMMARY"" ")
                    .AppendLine(" FROM MACHVISION.dbo.PURTA TA, MACHVISION.dbo.PURTB TB ")
                    .AppendLine(string.Format("WHERE CONVERT(nvarchar, CONVERT(datetime, TA.CREATE_DATE), 111) >= '{0}' AND CONVERT(nvarchar, CONVERT(datetime, TA.CREATE_DATE), 111) < '{1}' ", startDate, endDate))
                    .AppendLine("  AND TA.TA001 = TB.TB001 ")
                    .AppendLine("  AND TA.TA002 = TB.TB002 ")
                    .AppendLine("  AND TA.TA007 <> 'V' ")
                    .AppendLine(string.Format("  AND TA.TA001 = '{0}' ", orderType))
                    .AppendLine("GROUP BY TA.CREATE_DATE, TA.TA001, TA.TA002, TB.TB039 ");
            }
            else if (isInTablePURTC == true)
            {
                sb.AppendLine(@"SELECT TC.CREATE_DATE, TC.TC001 ""TC001"", TC.TC002 ""TC002"", TD.TD016 ""TD016"", COUNT(*) ""SUMMARY"" ")
                    .AppendLine(" FROM MACHVISION.dbo.PURTC TC, MACHVISION.dbo.PURTD TD ")
                    .AppendLine(string.Format("WHERE  CONVERT(nvarchar, CONVERT(datetime, TC.CREATE_DATE), 111) >= '{0}' AND CONVERT(nvarchar, CONVERT(datetime, TC.CREATE_DATE), 111) < '{1}' ", startDate, endDate))
                    .AppendLine("  AND TC.TC001 = TD.TD001 ")
                    .AppendLine("  AND TC.TC002 = TD.TD002 ")
                    .AppendLine("  AND TC.TC014 <> 'V' ")
                    .AppendLine(string.Format("  AND TC.TC001 = '{0}' ", orderType))
                    .AppendLine("GROUP BY TC.CREATE_DATE, TC.TC001, TC.TC002, TD.TD016 ");
            }
            else
            {
                // un support order type
                return null;
            }

            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                try
                {
                    connection.Open();
                    majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
            return majorData;
        }


     


        public static bool validData_INVTA(DataTable dt)
        {
            if (dt.Columns.Count != ErpDbTableColumsCount.INVTA) return false;
            if (dt.Rows.Count == 0) return true;
            return checkHasNullValue(dt);
        }

        public static bool validData_INVTB(DataTable dt)
        {
            if (dt.Columns.Count != ErpDbTableColumsCount.INVTB) return false;
            if (dt.Rows.Count == 0) return true;
            return checkHasNullValue(dt);
        }



        private static bool checkHasNullValue(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dr.IsNull(dc) == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static DataRow simulateData_INVTA(DataTable dt)
        {
            if (validData_INVTA(dt) == false)
            {
                return null;
            }
            DataRow dr = dt.NewRow();
            string defaultDate = DateTime.Now.ToString("yyyyMMdd");
            dr["COMPANY"] = MvCompanySite.MV_TEST.ToString();
            dr["CREATOR"] = "MIS_TEST";
            dr["USR_GROUP"] = "220";
            dr["CREATE_DATE"] = defaultDate;
            dr["MODIFIER"] = "MIS_TEST";
            dr["MODI_DATE"] = defaultDate;
            dr["FLAG"] = 1;
            dr["TA001"] = "A121";
            dr["TA002"] = DateTime.Now.ToString("yyyyMM") + "99999";
            dr["TA003"] = defaultDate;
            dr["TA004"] = "220";
            dr["TA005"] = "MIS_TEST";
            dr["TA006"] = "N";
            dr["TA007"] = 0;
            dr["TA008"] = "001";
            dr["TA009"] = "11";
            dr["TA010"] = 0;
            dr["TA011"] = 0;
            dr["TA012"] = 0;
            dr["TA013"] = "N";
            dr["TA014"] = defaultDate;
            dr["TA015"] = "";
            dr["TA016"] = 0;
            dr["TA017"] = "0";
            dr["TA018"] = "0";
            dr["TA019"] = 0;
            dr["TA020"] = "6";
            dr["TA021"] = "";
            dr["TA022"] = "";
            dr["TA023"] = 0;
            dr["TA024"] = 0;
            dr["TA025"] = "";
            dr["TA026"] = "";
            dr["TA027"] = "";
            dr["TA028"] = "";
            dr["UDF01"] = "";
            dr["UDF02"] = "";
            dr["UDF03"] = "";
            dr["UDF04"] = "";
            dr["UDF05"] = "";
            dr["UDF06"] = 0;
            dr["UDF07"] = 0;
            dr["UDF08"] = 0;
            dr["UDF09"] = 0;
            dr["UDF10"] = 0;
            dr["TA200"] = "";
            dr["TA201"] = "";

            defaultDate = null;
            return dr;
        }

        public static DataRow simulateData_INVTB(SqlConnection connection, DataTable dt, string item)
        {
            if (validData_INVTB(dt) == false)
            {
                return null;
            }

            DataTable tempDt = collectData_Invmb(connection, item);
            if (tempDt.Rows.Count != 1)
            {
                return null;
            }

            DataRow drINVMB = tempDt.Rows[0];

            DataRow dr = dt.NewRow();
            string defaultDate = DateTime.Now.ToString("yyyyMMdd");
            dr["COMPANY"] = MvCompanySite.MV_TEST.ToString();
            dr["CREATOR"] = "MIS_TEST";
            dr["USR_GROUP"] = "220";
            dr["CREATE_DATE"] = defaultDate;
            dr["MODIFIER"] = "MIS_TEST";
            dr["MODI_DATE"] = defaultDate;
            dr["FLAG"] = 1;

            dr["TB001"] = "A121";
            dr["TB002"] = DateTime.Now.ToString("yyyyMM") + "99999";
            dr["TB003"] = "001";
            dr["TB004"] = drINVMB["MB001"];
            dr["TB005"] = drINVMB["MB002"];
            dr["TB006"] = drINVMB["MB003"];
            dr["TB007"] = 0;
            dr["TB008"] = drINVMB["MB004"];
            dr["TB009"] = 0;
            dr["TB010"] = 0;
            dr["TB011"] = 0;
            dr["TB012"] = "";
            dr["TB013"] = "T02";
            dr["TB014"] = "";
            dr["TB015"] = "";
            dr["TB016"] = "";
            dr["TB017"] = "MIS_TEST";
            dr["TB018"] = "N";
            dr["TB019"] = "";
            dr["TB020"] = "";
            dr["TB021"] = "";
            dr["TB022"] = 0;
            dr["TB023"] = "";
            dr["TB024"] = "N";
            dr["TB025"] = 0;
            dr["TB026"] = "";
            dr["TB027"] = "P03";
            dr["TB028"] = 0;
            dr["TB029"] = 0;
            dr["TB030"] = "";
            dr["TB031"] = "";
            dr["TB032"] = "";
            dr["TB500"] = "";
            dr["TB501"] = "";
            dr["TB502"] = "";
            dr["UDF01"] = "";
            dr["UDF02"] = "";
            dr["UDF03"] = "";
            dr["UDF04"] = "";
            dr["UDF05"] = "";
            dr["UDF06"] = 0;
            dr["UDF07"] = 0;
            dr["UDF08"] = 0;
            dr["UDF09"] = 0;
            dr["UDF10"] = 0;


            drINVMB = null;
            tempDt.Dispose();
            tempDt = null;

            return dr;
        }

        public static DataTable collectData_Invmb(SqlConnection connection, string pattern)
        {

            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            sb.Append(string.Format("SELECT * FROM MACHVISION.dbo.INVMB WHERE RTRIM(MB001)='{0}'", pattern));

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }

            return majorData;
        }

        public static DataTable collectData_InvmbItems(string[] itemList, string columnPattern = "*")
        {
            using (SqlConnection connection = MvDbConnector.Connection_ERPDB2_Dot_MACHVISION)
            {
                try
                {
                    connection.Open();
                    return collectData_InvmbItems(connection, itemList, columnPattern);
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
        }
        public static DataTable collectData_InvmbItems(SqlConnection connection, string[] itemList, string columnPattern = "*")
        {
            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            if (columnPattern.Length == 0) { return null; }

            sb.Append("DECLARE @dtTempTable TABLE (MB001 NVARCHAR(50));");
            foreach (string item in itemList)
            {
                sb.Append(string.Format("INSERT INTO @dtTempTable (MB001) VALUES ('{0}');", item));
            }
            sb.Append(string.Format("SELECT {0} FROM MACHVISION.dbo.INVMB source WHERE EXISTS (select MB001 FROM @dtTempTable temp WHERE source.MB001 = temp.MB001);", columnPattern));

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }

            return majorData;
        }


        public static DataTable collectData_MachineFromMVPlan(string machineNo, bool isIncludeShipped = false)
        {
            using (SqlConnection connection = MvDbConnector.Connection_ERPBK_Dot_MVPlanSystem2018)
            {
                try
                {
                    connection.Open();
                    return collectData_MachineFromMVPlan(connection, machineNo, isIncludeShipped);
                }
                catch (SqlException se)
                {
                    // do nothing
                    throw se;
                }
            }
        }

        public static DataTable collectData_MachineFromMVPlan(SqlConnection connection, string machineNo, bool isIncludeShipped = false)
        {
            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            sb.Append(string.Format(@"SELECT * FROM tblMachManages  WHERE MachSN = '{0}' ", machineNo));
            if (isIncludeShipped == false)
            {
                sb.Append(" AND MachShip IS Null ");
            }
            sb.Append(" AND (OrderType <> '製令作廢' OR OrderType IS NULL) AND (OrderType <> '指定結案' OR OrderType IS NULL)");

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }

            return majorData;
        }

        public static DataTable collectData_InventoryWithStorageLocation(SqlConnection connection, string[] items, string[] warehosues)
        {
            if (items == null || items.Length == 0) { return null; }

            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            sb.Append(" SELECT * ")
                   .Append(" FROM INVMM MM ")
                   .Append("WHERE MM.MM001 IN (");
            foreach (string data in items)
            {
                sb.Append("'").Append(data).Append("',");
            }
            sb.Remove(sb.Length - 1, 1).Append(")").Append("  AND MM.MM005 > 0 ");

            if (warehosues != null && warehosues.Length > 0)
            {
                sb.Append("  AND MM.MM002 IN (");
                foreach (string data in warehosues)
                {
                    sb.Append("'").Append(data).Append("',");
                }
                sb.Remove(sb.Length - 1, 1).Append(") ");
            }

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }
            return majorData;
        }


        public static DataTable collectData_Inventory(SqlConnection connection, string[] items, string[] warehosues)
        {

            if (items == null || items.Length == 0) { return null; }
            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            sb.Append(" SELECT * ")
                .Append(" FROM INVMC MC ")
                .Append("WHERE MC.MC001 IN (");
            foreach(string data in items)
            {
                sb.Append("'").Append(data).Append("',");
            }
            sb.Remove(sb.Length - 1, 1).Append(")")
                .Append("  AND MC.MC007 > 0 ");

            if (warehosues != null && warehosues.Length>0)
            {
                sb.Append("  AND MC.MC002 IN (");
                foreach (string data in warehosues)
                {
                    sb.Append("'").Append(data).Append("',");
                }
                sb.Remove(sb.Length - 1, 1).Append(") ");
            }

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }
            return majorData;
        }

        public static DataTable collectData_HasStorageLocation(SqlConnection conn, string[] warehosues)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT MC001, MC009 from CMSMC WHERE MC001 IN (");
            foreach (string item in warehosues)
            {
                sb.Append(string.Format("'{0}',", item));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");

            DataTable tempDt = MvDbConnector.queryDataBySql(conn, sb.ToString());
            return tempDt;
        }

        public static DataTable CollectData_CustomerOrder(SqlConnection connection, string[] customerOrderTypes, string startDate, string endDate)
        {

            StringBuilder sb = new StringBuilder();
            DataTable majorData = null;

            sb.Append("SELECT TC001 單別, TC002 單號, TC003 訂單日期, TC004 客戶代號, MA002 客戶簡稱, TC012 客戶單號, TD003 序號, TD004 品號, TD005 品名, TD008 訂單數量, TD009 已交數量, TD013 預交日 ")
                .Append("FROM MACHVISION.dbo.COPTC ")
                .Append("LEFT JOIN MACHVISION.dbo.COPTD ")
                .Append("  ON TC001 = TD001 ")
                .Append(" AND TC002 = TD002 ")
                .Append("LEFT JOIN MACHVISION.dbo.COPMA ")
                .Append("  ON TC004 = MA001 ");
            sb.Append(string.Format("WHERE(TC003 >= '{0}' AND TC003 <= '{1}') ", startDate, endDate));
            sb.Append("   AND TC027 <> 'V' ")
                .Append(" AND TD008 > TD009 ")
                .Append(" AND TC003 <> TD013 ")
                .Append(" AND TC001 IN (");
            foreach (string customerOrderType in customerOrderTypes)
            {
                sb.Append("'").Append(customerOrderType).Append("',");
            }
            sb.Remove(sb.Length - 1, 1).Append(")");

            try
            {
                majorData = MvDbConnector.queryDataBySql(connection, sb.ToString());
            }
            catch (SqlException se)
            {
                throw se;
            }
            return majorData;
        }

        public static bool getUserInfo(ref Model.UserData userData)
        {
            // 確認此帳號是否存在ERP GP
            try
            {
                using (SqlConnection conn = MvDbConnector.getErpDbConnection(userData.CompanySite, MvDBSource.ERPBK_mvWorkFlow))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT dbo.vwEmployee.EmployeeID, dbo.vwEmployee.Name, dbo.vwEmployee.DepartmentID, dbo.Department.DepartmentName ")
                        .Append("FROM dbo.vwEmployee LEFT OUTER JOIN ")
                        .Append(" dbo.Department ON dbo.vwEmployee.DepartmentID = dbo.Department.DepartmentID ")
                        .Append(string.Format(" WHERE dbo.vwEmployee.ADLoginID = '{0}'", userData.AdAccount));

                    //string command = string.Format("select * from DSCSYS.dbo.DSCMA WHERE MA001 = '{0}'", account);
                    conn.Open();
                    // 如果取回的資料不是唯一一筆, 回傳null
                    DataTable dt = MvDbConnector.queryDataBySql(conn, sb.ToString());
                    if (dt?.Rows.Count != 1)
                    {
                        userData = null;
                        return false;
                    }
                    userData.EmployeeID = dt.Rows[0]["EmployeeID"].ToString();
                    userData.EmployeeName = dt.Rows[0]["Name"].ToString();
                    userData.DepartmentID = dt.Rows[0]["DepartmentID"].ToString();
                    userData.DepartmentName = dt.Rows[0]["DepartmentName"].ToString();
                }
                return true;
            }
            catch (SqlException)
            {
                // 如果取不到資料, 就將帶入的userData元件改為null
                return false;
            }
        }
        public static bool validateUserFromErpGP(MvCompanySite company, string account, string password)
        {
            // 確認此帳號是否存在ERP GP
            try
            {
                using (SqlConnection conn = MvDbConnector.getErpDbConnection(company))
                {
                    string command = string.Format("select * from DSCSYS.dbo.DSCMA WHERE MA001 = '{0}'", account);
                    conn.Open();
                    return MvDbConnector.hasRowsBySq1(conn, command);
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }



        public static bool validateUserFromMvWorkFlow(string account, string password)
        {
            // 確認此帳號是否存在mvWorkFlow
            try
            {
                using (SqlConnection conn = MvDbConnector.Connection_ERPBK_Dot_MvWorkFlow)
                {
                    string command = "select * from ERPBK.mvWorkFlow.dbo.vwEmployee";
                    conn.Open();
                    return MvDbConnector.hasRowsBySq1(conn, command);
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

    }
}
