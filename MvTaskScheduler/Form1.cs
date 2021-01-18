using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace MvTaskScheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TaskScheduler
            sendMail();
        }

        private void sendMail()
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("mvmis@machvision.com.tw"); //設定寄件者
            mail.Subject = "AutoMail";
            mail.To.Add("stevenliu@machvision.com.tw");
            mail.Body = "內文";
            mail.Bcc.Add("stevenliu@machvision.com.tw"); //這是密件副本收件者
            mail.CC.Add("stevenliu@machvision.com.tw");//這是副本收件者
            //加入附件
            Attachment attch = new Attachment(@"\\mv-nas03\PIC\BirthDay\生日賀卡_同仁.jpg");
            mail.Attachments.Add(attch);

            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = "mail.machvision.com.tw"; //設定SMTP Server
            smtpclient.Port = 25; //設定Port
            smtpclient.Send(mail);
        }
    }
}
