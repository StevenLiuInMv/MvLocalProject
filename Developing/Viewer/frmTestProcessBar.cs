﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvLocalProject.Viewer
{
    public partial class frmTestProcessBar : Form
    {
        public frmTestProcessBar()
        {
            InitializeComponent();
        }


        private void frmTestProcessBar_Load(object sender, EventArgs e)
        {
            this.button1.Text = "開始";
            this.button2.Text = "停止";
            this.textBox1.Multiline = true; //多行
            this.textBox1.ScrollBars = ScrollBars.Vertical; //顯示垂直捲軸
            this.backgroundWorker1.WorkerReportsProgress = true; //回報進度
            this.backgroundWorker1.WorkerSupportsCancellation = true; //允許中斷
            this.timer1.Interval = 1000;
        }
        //--------------------------------
        string msg; //存放回報訊息
        DateTime TimerTick; //計時器時間

        private void button1_Click(object sender, EventArgs e)
        {
            this.TimerTick = DateTime.Parse("2018/1/1 00:00:00"); //初始時間點
            this.timer1.Start(); //啟動計時器
            this.progressBar1.Visible = true; //顯示進度條
            this.backgroundWorker1.RunWorkerAsync(); //呼叫背景程式
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync(); //中斷背景程式
        }

        private void todo(BackgroundWorker worker, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 1000; i++)
            {
                if (worker.CancellationPending) //如果被中斷...
                {
                    e.Cancel = true;
                    break;
                }
                System.Threading.Thread.Sleep(300); //延遲300毫秒
                this.msg = "第 " + i + " 圈 ... \r\n"; //設定訊息
                worker.ReportProgress(i / 10); //回報進度
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending) //如果被中斷...
                e.Cancel = true;

            BackgroundWorker worker = (BackgroundWorker)sender;
            this.todo(worker, e); //欲背景執行的function
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox1.Text += msg;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.ScrollToCaret();
            this.textBox1.Refresh();
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Error != null))
                MessageBox.Show(e.Error.Message);
            else if (e.Cancelled)
                MessageBox.Show("使用者中斷程式");
            else
                MessageBox.Show("完成");

            this.progressBar1.Visible = false; //隱藏進度條
            this.timer1.Stop(); //停止計時器
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.TimerTick = this.TimerTick.AddSeconds(1); //計時器 每秒+1
            this.label1.Text = this.TimerTick.ToString("HH:mm:ss"); //設定顯示格式
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {

        }

        private void frmTestProcessBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
