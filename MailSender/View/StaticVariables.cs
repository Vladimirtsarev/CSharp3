using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePassword;


namespace MailSender
{
    public static class StaticVariables
    {



        /// <summary>
        /// Адрес SMTP сервера, например "smtp.gmail.com"
        /// </summary>
        public static string SmtpServer { get; set; } = "smtp.gmail.com";

        /// <summary>
        /// Порт SMTP сервера, например 587
        /// </summary>
        public static int Port { get; set; } = 587;

        /// <summary>
        /// Логин SMTP сервера, например mail@gmail.com
        /// </summary>
        public static string Login { get; set; } = "gkbtest2020@gmail.com";

        /// <summary>
        /// Пароль SMTP сервера
        /// </summary>
        public static string PW { get; set; } = "GKBtest2020";

        /// <summary>
        /// Емейл адрес получателя
        /// </summary>
        public static string From { get; set; } = "gkbtest2020@gmail.com";

        /// <summary>
        /// Емейл адрес отправителя
        /// </summary>
        public static string To { get; set; } = "79262685273@ya.ru";

        /// <summary>
        /// Тема письма
        /// </summary>
        public static string Subj { get; set; } = "Тест";

        /// <summary>
        /// Тело письма
        /// </summary>
        public static string Text { get; set; } = "Текст тестового сообщения";

        /// <summary>
        /// SSL
        /// </summary>
        public static bool Ssl { get; set; } = true;
        

        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            {"report@litaform.ru", CodePassword.Password.getPassword("hdbezpyxoqioshmf") }
            //,{"2@mail.ru", Password.getPassword("password2") }

        };

        public static Dictionary<string, int> SMTPServers
        {
            get { return dicSMTPServers; }
        }
        private static Dictionary<string, int> dicSMTPServers = new Dictionary<string, int>()
        {
            {"smtp.gmail.com", 587 }
            ,{"smtp.yandex.com", 587 }
        };

    }
}
