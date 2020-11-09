using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailSenderLib
{
    public class EmailSendService
    {
        #region vars

        /// <summary>
        /// Email, с которого будет рассылаться почта
        /// </summary>
        private string strLogin;

        /// <summary>
        /// Пароль к email, с которого будет рассылаться почта
        /// </summary>
        private string strPassword;

        /// <summary>
        /// SMTP-server
        /// </summary>
        private string strSmtp = "smtp.yandex.ru";

        /// <summary>
        /// Порт для SMTP-server
        /// </summary>
        private int iSmtpPort = 25;

        /// <summary>
        /// Текст письма для отправки
        /// </summary>
        private string strBody;

        /// <summary>
        /// Тема письма для отправки
        /// </summary>
        private string strSubject;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sLogin">Логин с которого отправляется сообщение</param>
        /// <param name="sPassword">Пароль для логина</param>
        public EmailSendService(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        /// <summary>
        /// Отправка Email конкретному адресату
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        private void SendMail(string mail, string name)
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString(), "Ошибка отправки письма!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Отсылает спимок на емейлов
        /// </summary>
        /// <param name="emails"></param>
        public void SendMails(IQueryable<Email> emails)
        {

            foreach (Email email in emails)
            {
                SendMail(email.Value, email.Name);
            }
        }
    }
}
