using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MvSharedLib.Model;

namespace MvSharedLib.Controller
{
    public sealed class MvDbConnector
    {
        public static string ConnectionString_ERPDB2_Dot_TEMP
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.TEMP"].ConnectionString; }
            //get { return "Data Source=192.168.222.17;Initial Catalog=TEMP;User ID=sa;Password=Mv1652$;Connection Timeout=15;Persist Security Info=false"; }
        }

        private static string ConnectionString_ERPDB2_Dot_MACHVISION
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MACHVISION"].ConnectionString; }
            //get { return "Data Source=192.168.222.17;Initial Catalog=MACHVISION;User ID=sa;Password=Mv1652$;Connection Timeout=15;Persist Security Info=false"; }
        }
        private static string ConnectionString_ERPDB2_Dot_MVTEST
        {
            get { return ConfigurationManager.ConnectionStrings["ERPDB2.MVTEST"].ConnectionString; }
            //get { return "Data Source=192.168.222.17;Initial Catalog=MVTEST;User ID=sa;Password=Mv1652$;Connection Timeout=15;Connection Timeout=15;Persist Security Info=False"; }
        }

        private static string ConnectionString_MVDB01_Dot_Attend
        {
            get { return ConfigurationManager.ConnectionStrings["MVDB01.Attend"].ConnectionString; }
            //get { return "Data Source=192.168.222.24;Initial Catalog=Attend;User ID=sa;Password=Mv27581652$;Connection Timeout=15;Persist Security Info=False"; }
        }

        private static string ConnectionString_MVDB01_Dot_MvWorkFlow
        {
            get { return ConfigurationManager.ConnectionStrings["MVDB01.mvWorkFlow"].ConnectionString; }
            //get { return "Data Source=192.168.222.24;Initial Catalog=mvWorkFlow;User ID=sa;Password=Mv27581652$;Connection Timeout=15;Persist Security Info=False"; }
        }

        private static string ConnectionString_MVDB01_Dot_MVPlanSystem2018
        {
            get { return ConfigurationManager.ConnectionStrings["MVDB01.MVPlanSystem2018"].ConnectionString; }
            //get { return "Data Source=192.168.222.24;Initial Catalog=MVPlanSystem2018;User ID=sa;Password=Mv27581652$;Connection Timeout=15;Persist Security Info=False"; }
        }
        private static string ConnectionString_MVDB01_Dot_IT
        {
            get { return ConfigurationManager.ConnectionStrings["MVDB01.IT"].ConnectionString; }
            //get { return "Data Source=192.168.222.24;Initial Catalog=IT;User ID=sa;Password=Mv27581652$;Connection Timeout=15;Persist Security Info=False"; }
        }

        private static string ConnectionString_ERPCN_Dot_MV_CE
        {
            get { return ConfigurationManager.ConnectionStrings["ERPCN.MV_CE"].ConnectionString; }
            //get { return "Data Source=192.168.3.245;Initial Catalog=MV_CE;User ID=sacs;Password=Mv1652$;Connection Timeout=15;Persist Security Info=False"; }
        }
        private static string ConnectionString_ERPCN_Dot_MV_CS
        {
            get { return ConfigurationManager.ConnectionStrings["ERPCN.MV_CS"].ConnectionString; }
            //get { return "Data Source=192.168.3.245;Initial Catalog=MV_CS;User ID=sacs;Password=Mv1652$;Connection Timeout=15;Persist Security Info=False"; }
        }

        public static SqlConnection Connection_ERPDB2_Dot_TEMP
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_TEMP); }
        }

        private static SqlConnection Connection_ERPDB2_Dot_MACHVISION
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MACHVISION); }
        }

        public static SqlConnection Connection_ERPDB2_Dot_MVTEST
        {
            get { return new SqlConnection(ConnectionString_ERPDB2_Dot_MVTEST); }
        }

        private static SqlConnection Connection_MVDB01_Dot_Attend
        {
            get { return new SqlConnection(ConnectionString_MVDB01_Dot_Attend); }
        }

        private static SqlConnection Connection_MVDB01_Dot_MvWorkFlow
        {
            get { return new SqlConnection(ConnectionString_MVDB01_Dot_MvWorkFlow); }
        }

        private static SqlConnection Connection_MVDB01_Dot_MVPlanSystem2018
        {
            get { return new SqlConnection(ConnectionString_MVDB01_Dot_MVPlanSystem2018); }
        }

        public static SqlConnection Connection_MVDB01_Dot_IT
        {
            get { return new SqlConnection(ConnectionString_MVDB01_Dot_IT); }
        }

        private static SqlConnection Connection_ERPCN_Dot_MV_CE
        {
            get { return new SqlConnection(ConnectionString_ERPCN_Dot_MV_CE); }
        }

        private static SqlConnection Connection_ERPCN_Dot_MV_CS
        {
            get { return new SqlConnection(ConnectionString_ERPCN_Dot_MV_CS); }
        }

        public static SqlConnection getSystemDbConnection(MvCompanySite companySite, MvSystem mvSystem)
        {
            if (companySite == MvCompanySite.MACHVISION)
            {
                switch (mvSystem)
                {
                    case MvSystem.TW_ERP:
                        return getErpDbConnection(companySite);
                    case MvSystem.TW_WORKFLOW:
                        return Connection_MVDB01_Dot_MvWorkFlow;
                    case MvSystem.TW_MVPLAN:
                        return Connection_MVDB01_Dot_MVPlanSystem2018;
                    case MvSystem.TW_HR:
                        return Connection_MVDB01_Dot_Attend;
                    default:
                        throw new System.Exception("unsupported system");
                }
            }
            else if (companySite == MvCompanySite.MV_CS || companySite == MvCompanySite.MV_CE)
            {
                switch (mvSystem)
                {
                    case MvSystem.CN_ERP:
                        return getErpDbConnection(companySite);
                    default:
                        throw new System.Exception("unsupported system");
                }
            }
            else
            {
                throw new System.Exception("unsupported company site");
            }
        }

        public static SqlConnection getErpDbConnection(MvCompanySite company)
        {
            switch (company)
            {
                case MvCompanySite.MACHVISION:
                    return Connection_ERPDB2_Dot_MACHVISION;
                case MvCompanySite.MV_CE:
                    return Connection_ERPCN_Dot_MV_CE;
                case MvCompanySite.MV_CS:
                    return Connection_ERPCN_Dot_MV_CS;
                case MvCompanySite.MV_TEST:
                    return Connection_ERPDB2_Dot_MVTEST;
                default:
                    throw new System.Exception("unsupported company site");
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
    }
}
