using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MvLocalProject.Controller
{
    public class Mailer
    {
        public struct MailContent
        {
            /// <summary>
            /// 發送郵件主機，如未指定，則系統自動從 Web.config 取得
            /// </summary>
            public string Host;
            /// <summary>
            /// 寄件者，如未指定，則系統自動從 Web.config 取得
            /// </summary>
            public string From;
            /// <summary>
            /// 收件人，可利用 string array 指定多個收件者
            /// </summary>
            public string[] Receiver;
            /// <summary>
            /// 收件人
            /// </summary>
            public string To;
            /// <summary>
            /// 主旨
            /// </summary>
            public string Subject;
            /// <summary>
            /// 郵件內容
            /// </summary>
            public string Body;
            /// <summary>
            /// 郵件是否為HTML格式
            /// </summary>
            public bool IsBodyHtml;
        }

        public static void sendMail(MailContent mail)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailBody = new MailMessage();

            if (mail.Host == null || mail.Host.Length == 0)
            {
                smtp.Host = ConfigurationManager.AppSettings["MailSender"];
            }
            else
            {
                smtp.Host = mail.Host;
            }

            if (mail.From == null || mail.From.Length == 0)
            {
                mailBody.From = new MailAddress(ConfigurationManager.AppSettings["MailSender"]);
            }
            else
            {
                mailBody.From = new MailAddress(mail.From);
            }

            foreach (string szTo in mail.Receiver)
            {
                mailBody.To.Add(new MailAddress(szTo));
            }
                
            mailBody.Subject = mail.Subject;
            mailBody.SubjectEncoding = System.Text.Encoding.Default;
            mailBody.BodyEncoding = System.Text.Encoding.Default;
            mailBody.Body = mail.Body;
            mailBody.IsBodyHtml = mail.IsBodyHtml;
            smtp.Credentials = CredentialCache.DefaultNetworkCredentials;

            smtp.Send(mailBody);
        }
    }
}
