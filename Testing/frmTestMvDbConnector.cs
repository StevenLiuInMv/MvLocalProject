using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvSharedLib.Controller;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Testing
{
    public partial class frmTestMvDbConnector : Form
    {
        public frmTestMvDbConnector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string newString = MvDbConnector.ConnectionString_ERPDB2_Dot_MVTEST;
            string newString = "remove";
            MessageBox.Show(newString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newString = "server=192.168.222.102;port=3306;user=system;password=11111111; database=infoguard";
            MessageBox.Show(newString);

            DataTable dt = new DataTable();
            using(MySqlConnection connection = new MySqlConnection(newString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("已經建立連線");
                }
                catch (SqlException se)
                {
                    //發生例外時，會自動rollback
                    throw se;
                }
                finally
                {
                    connection.Close();
                }
            }
            MessageBox.Show("Done");
        }
    }
}
