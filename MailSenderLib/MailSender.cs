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
using CodePassword;

namespace MailSenderLib
{
    public class MailSender
    {
        private readonly SmtpClient smtp = new SmtpClient();

        private readonly MailMessage message = new MailMessage();

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
            message = new MailMessage(new MailAddress(credential.From, credential.MailFromName), mailto)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

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
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(credential.From, credential.PW);
            smtp.EnableSsl = credential.SSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        /// <summary>
        /// Посылает письмо
        /// </summary>
        /// <param name="mailfrom">От кого</param>
        /// <param name="smtps">SMTP сервер</param>
        /// <param name="port">Порт</param>
        /// <param name="pw">Пароль</param>
        /// <param name="ssl">Включить SSL</param>
        /// <param name="mailfromname"></param>
        /// <param name="isbodyhtml"></param>
        /// <param name="mailto">Кому</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Тело письма</param>
        /// <param name="attachFile">путь к приложениям</param>
        public MailSender(string mailfrom, string smtps, int port, string pw, bool ssl, string mailfromname, bool isbodyhtml,
            MailAddress mailto, string subject, string body, string[] attachFile)
        {
            // message
            message = new MailMessage(new MailAddress(mailfrom, mailfromname), mailto)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isbodyhtml
            };

            if (attachFile != null)
            {
                foreach (string s in attachFile)
                {
                    message.Attachments.Add(new Attachment(s));
                }

            }

            // smtp
            smtp.Host = smtps;
            smtp.Port = port;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailfrom, pw);
            smtp.EnableSsl = ssl;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
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
