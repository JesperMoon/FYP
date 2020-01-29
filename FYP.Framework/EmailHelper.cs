using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Framework
{
    public class EmailHelper
    {
        public static void SentMail(string mailFrom, string mailTo, string emailSubject, string emailBody, string userName, string password)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = emailSubject;
                mail.IsBodyHtml = true;
                mail.Body = emailBody;

                SmtpServer.Port = 587; // SSL Required (465) : TLS Requried (587)
                SmtpServer.Credentials = new System.Net.NetworkCredential(userName, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch(Exception err)
            {

            }

        }
    }
}
