using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, chkAll.Checked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string waferList = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                waferList += checkedListBox1.GetItemChecked(i) ? "1" : "0";
            }
            MessageBox.Show("Get wafer list" + Environment.NewLine + waferList);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;
            checkedListBox2.CheckOnClick = true;
        }

        private void chkAllByGroup_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, chkAllByGroup.Checked);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string waferList = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                waferList += checkedListBox2.GetItemChecked(i) ? "1" : "0";
            }
            MessageBox.Show("Get wafer list" + Environment.NewLine + waferList);

        }
    }
}
