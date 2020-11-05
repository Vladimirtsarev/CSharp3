using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace MailSenderLib
{
    public class MailSender
    {
        private SmtpClient smtp=new SmtpClient();

        private MailMessage message =new MailMessage();

        /// <summary>
        /// Посылает письмо
        /// </summary>
        /// <param name="mailto">Кому</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Тело письма</param>
        /// <param name="attachFile">путь к приложению</param>
        public MailSender(MailAddress mailto, string subject, string body, string[] attachFile)
        {

            MailCredential credential = new MailCredential();
            credential.ReadFromJson("mailcredential.json");
               
            // message
            message = new MailMessage(new MailAddress(credential.From, credential.MailFromName), mailto);
            message.Subject = subject;
            message.Body = body;
              
            if (attachFile != null)
            {
                foreach (string s in attachFile)
                {                   
                    message.Attachments.Add(new Attachment(s));
                }
                
            }            

            // smtp
            smtp.Host = credential.SmtpServer;
            smtp.Port = credential.Port;
            smtp.Credentials = new NetworkCredential(credential.From, credential.PW);
            smtp.EnableSsl = credential.SSL;
        }

        /// <summary>
        /// Отправить
        /// </summary>
        public void Send()
        {
            try
            {                
                smtp.Send(message);
                MessageBox.Show("Сообщение отправлено");                
            }
            catch (Exception ex)
            {              
                MessageBox.Show(ex.ToString());
            }


        }

       

        
    }
}
