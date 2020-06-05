using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MvLocalProject.Controller;
using MvSharedLib.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmReportMocP07 : Form
    {
        DataTable machineTypeMappingTable = new DataTable();

        public frmReportMocP07()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");

            System.Console.Write("11");
        }

        /// <summary>
        /// query the data from DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection sqlConnection = null;
            string machineType = null;
            DataTable dataTable = null;

            // 先將值存起來, 後續顯示判斷使用
            machineType = cboRpt.Text;

            // get machineTypeName from data base
            if (machineTypeMappingTable.Rows.Count == 0)
            {
                // get machineTypeName and Type id
                sb.Clear();
                sb.Append(@"SELECT DISTINCT(MachTypeID), MachTypeName FROM ERPBK.MVPlanSystem2018.dbo.tblMachManages WHERE OrderType is NULL ");
                // create connection
                try
                {
                    sqlConnection = MvDbConnector.Connection_ERPBK_Dot_MVPlanSystem2018;
                    sqlConnection.Open();

                    // read data from db
                    machineTypeMappingTable = queryMachTypeIdAndName(sqlConnection, sb.ToString());
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
                // 
                cboRpt.DataSource = machineTypeMappingTable;
                cboRpt.ValueMember = "MachTypeID";
                cboRpt.DisplayMember = "Value.DisplayName";
                cboRpt.Text = "";
                cboRpt.SelectedValue = "";
            } else
            {
                machineType = cboRpt.SelectedValue == null ? "" : cboRpt.SelectedValue.ToString() ;
            }

            // get the list from data base
            sb.Clear();
            sb.Append(@"SELECT MachID,MachSN,MachTypeID,MachTypeName,OrderNumber,Locate,CustomerName,EndCustomerName,OrderType, ")
                .Append("ShipType,OrderShipDate,ConfirmShipDate,MachNote,MachShip,MachClose,CustomerPONo,ManufactureOrderNo,MDate ")
                .Append("FROM ERPBK.MVPlanSystem2018.dbo.tblMachManages ")
                .Append("WHERE OrderType is NULL ")
                .Append(machineType == "" ? "" : " AND  MachTypeID = '" + machineType + "'");

            // create connection
            try
            {
                sqlConnection = MvDbConnector.Connection_ERPBK_Dot_MVPlanSystem2018;
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

            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;

            // show data table result in console
            //showDataTableInConsole(dataTable);
            sb.Clear();
            sb = null;
            dataTable = null;
        }


        private void buttonQueryTest_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = null;
            DataTable dataTable = null;

            // create sql statement
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"SELECT * FROM ERPBK.DEMO.dbo.ACPMA");

            try
            {
                // create connection
                sqlConnection = MvDbConnector.Connection_ERPBK_DEMO;
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

            // show data table result in console
            showDataTableInConsole(dataTable);

            dataTable = null;
        }


        private DataTable queryMachTypeIdAndName(SqlConnection connection, string command)
        {
            DataTable dt = null;

            dt = MvDbConnector.queryDataBySql(connection, command);
            return dt;
        }

        private string queryMachTypeNameById(string name)
        {
            return null;
        }



        public void showDataTableInConsole(DataTable sourceTable)
        {
            // show table result
            StringBuilder sb = new StringBuilder();
            string columnName = null;
            List<string> columnNameList = new List<string>();

            // show schema
            foreach (DataColumn dataColumn in sourceTable.Columns)
            {
                columnName = dataColumn.ColumnName.ToString();
                columnNameList.Add(columnName);
                sb.Append(columnName);
                sb.Append(", ");
            }
            sb.Length -= 1;
            sb.AppendLine();
            Console.Write(sb.ToString());

            // show table content
            sb.Clear();
            foreach (DataRow dataRow in sourceTable.Rows)
            {
                foreach (string column in columnNameList)
                {
                    sb.Append(dataRow[column].ToString())
                        .Append(", ");
                }
                sb.Length -= 1;
                sb.AppendLine();
            }
            Console.Write(sb.ToString());

            sb.Clear();
            columnNameList.Clear();

            sb = null;
            columnName = null;
            columnNameList = null;
        }

        private void buttonLoadERPData_Click(object sender, EventArgs e)
        {

        }

        private void frmMocP07_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
