﻿using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MvLocalProject.Model;
using System.Text;

namespace MvLocalProject.Controller
{
    public sealed class MvDbConnector
    {
        private static string ConnectionString_ERPBK_DEMO
        {
            get { return ConfigurationManager.ConnectionStrings["ERPBK.DEMO"].ConnectionString; }
        }

        private static string ConnectionString_ERPBK_Dot_MVPlanSystem2018
        {
            get { return ConfigurationManager.ConnectionStrings["ERPBK.MVPlanSystem2018"].ConnectionString; }
        }

        private static string ConnectionString_ERPBK_Dot_MvWorkFlow
        {
            get { return ConfigurationManager.ConnectionStrings["ERPBK.mvWorkFlow"].ConnectionString; }
        }

        private static string ConnectionString_ERPBK_Dot_IT
        {
            get { return ConfigurationManager.ConnectionStrings["ERPBK.IT"].ConnectionString; }
        }

        private static string ConnectionString_ERPDB2_Dot_TEMP
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.TEMP"].ConnectionString; }
        }

        private static string ConnectionString_ERPDB2_Dot_MACHVISION
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MACHVISION"].ConnectionString; }
        }
        public static string ConnectionString_ERPDB2_Dot_MVTEST
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MVTEST"].ConnectionString; }
        }

        public static string ConnectionString_ERPDB2_Dot_MV_CE
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MV_CE"].ConnectionString; }
        }
        public static string ConnectionString_ERPDB2_Dot_MV_CS
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MV_CS"].ConnectionString; }
        }
        public static string ConnectionString_ERPDB2_Dot_SIGOLD
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.SIGOLD"].ConnectionString; }
        }
        public static string ConnectionString_MV_SOP
        {
            get { return ConfigurationManager.ConnectionStrings["MV_SOP"].ConnectionString; }
        }

        public static SqlConnection Connection_ERPBK_DEMO
        {
            get { return new SqlConnection(ConnectionString_ERPBK_DEMO); }
        }

        public static SqlConnection Connection_ERPBK_Dot_MVPlanSystem2018
        {
            get { return new SqlConnection(ConnectionString_ERPBK_Dot_MVPlanSystem2018); }
        }

        public static SqlConnection Connection_ERPBK_Dot_MvWorkFlow
        {
            get { return new SqlConnection(ConnectionString_ERPBK_Dot_MvWorkFlow); }
        }

        public static SqlConnection Connection_ERPBK_Dot_IT
        {
            get { return new SqlConnection(ConnectionString_ERPBK_Dot_IT); }
        }

        public static SqlConnection Connection_ERPDB2_Dot_TEMP
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_TEMP); }
        }

        public static SqlConnection Connection_ERPDB2_Dot_MACHVISION
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MACHVISION); }
        }

        public static SqlConnection Connection_ERPDB2_Dot_MVTEST
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MVTEST); }
        }
        public static SqlConnection Connection_ERPDB2_Dot_MV_CE
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MV_CE); }
        }
        public static SqlConnection Connection_ERPDB2_Dot_MV_CS
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MV_CS); }
        }
        public static SqlConnection Connection_ERPDB2_Dot_SIGOLD
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_SIGOLD); }
        }
        public static SqlConnection Connection_MV_SOP
        {
            get { return new SqlConnection(ConnectionString_MV_SOP); }
        }


        public static SqlConnection getErpDbConnection(MvCompanySite company, MvDBSource dbSource = MvDBSource.ERPDB2_MACHVISION)
        {
            switch (company)
            {
                case MvCompanySite.MACHVISION:
                    if (dbSource == MvDBSource.ERPDB2_MACHVISION)
                    {
                        return Connection_ERPDB2_Dot_MACHVISION;
                    }
                    else if (dbSource == MvDBSource.ERPBK_mvWorkFlow)
                    {
                        return Connection_ERPBK_Dot_MvWorkFlow;
                    }
                    else
                    {
                        throw new System.Exception("illegal database source");
                    }
                case MvCompanySite.MVTEST:
                    return Connection_ERPDB2_Dot_MVTEST;
                case MvCompanySite.MV_CE:
                    return Connection_ERPDB2_Dot_MV_CE;
                case MvCompanySite.MV_CS:
                    return Connection_ERPDB2_Dot_MV_CS;
                case MvCompanySite.SIGOLD:
                    return Connection_ERPDB2_Dot_SIGOLD;
                default:
                    throw new System.Exception("illegal company");
            }
        }

        public static DataTable queryDataBySql(SqlCommand sqlCommand)
        {

            DataTable dataTable = null;
            try
            {
                // read data from db
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                dataTable = new DataTable();
                dataTable.BeginLoadData();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    dataTable.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
                }

                while (dataReader.Read())
                {
                    object[] items = new object[dataReader.FieldCount];
                    dataReader.GetValues(items);
                    dataTable.LoadDataRow(items, true);
                }

                dataTable.EndLoadData();
                dataReader.Close();
            }
            catch (SqlException se)
            {
                dataTable = null;
                throw se;
            }

            return dataTable;
        }

        public static DataTable queryDataBySql(SqlConnection connection, string command)
        {
            if (connection == null) return null;
            using (SqlCommand sqlCommand = new SqlCommand(command, connection))
            {
                return queryDataBySql(sqlCommand);
            }
        }

        public static bool hasRowsBySq1(SqlCommand sqlCommand)
        {
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                return dataReader.HasRows;
            }
        }

        public static bool hasRowsBySq1(SqlConnection connection, string command)
        {
            if (connection == null) return false;
            using (SqlCommand sqlCommand = new SqlCommand(command, connection))
            {
                return hasRowsBySq1(sqlCommand);
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
                    return hasRowsBySq1(conn, command);
                }
            }
            catch (SqlException)
            {
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
                    return hasRowsBySq1(conn, command);
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static void closeSqlConnection(ref SqlConnection connection)
        {
            if (connection == null) { return; }
            try
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            catch (SqlException se)
            {
                throw se;
            }
        }

        public static void disposeSqlCommand(ref SqlCommand command)
        {
            if (command == null) { return; }
        }

        public static bool getUserInfo(ref UserData userData)
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
    }
}
