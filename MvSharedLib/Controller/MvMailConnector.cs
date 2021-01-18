using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MvSharedLib.Controller
{
    public sealed class MvMailConnector
    {
        public static string SMTPServer
        {
            get { return ConfigurationManager.AppSettings["SMTPServer"]; }
        }
        public static string SMTPPort
        {
            get { return ConfigurationManager.AppSettings["SMTPPort"]; }
        }

        public static void SendMailBySMPT(MailMessage mailMessage)
        {
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Host = MvMailConnector.SMTPServer;               //設定SMTP Server
            smtpclient.Port = int.Parse(MvMailConnector.SMTPPort);      //設定Port
            smtpclient.Send(mailMessage);
        }
    }
}
