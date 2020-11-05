﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
        
    }
}
