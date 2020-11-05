using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace MailSenderLib
{
    public class MailCredential
    {

        #region Prorerty

        [Newtonsoft.Json.JsonProperty("smtp")]
        /// <summary>
        /// Адрес SMTP сервера, например "smtp.gmail.com"
        /// </summary>
        public string SmtpServer { get; set; }

        [Newtonsoft.Json.JsonProperty("prt")]
        /// <summary>
        /// Порт SMTP сервера, например 587
        /// </summary>
        public int Port { get; set; } 

        [Newtonsoft.Json.JsonProperty("lgn")]
        /// <summary>
        /// Логин SMTP сервера, например mail@gmail.com
        /// </summary>
        public string Login { get; set; }

        [Newtonsoft.Json.JsonProperty("pw")]
        /// <summary>
        /// Пароль SMTP сервера
        /// </summary>
        public string PW { get; set; }

        [Newtonsoft.Json.JsonProperty("frm")]
        /// <summary>
        /// Емейл адрес отправителя
        /// </summary>
        public string From { get; set; } 

        //[Newtonsoft.Json.JsonProperty("to")]
        ///// <summary>
        ///// Емейл адрес получателя
        ///// </summary>
        //public string To { get; set; } = "79262685273@ya.ru";

        [Newtonsoft.Json.JsonProperty("ssl")]
        /// <summary>
        /// SSL
        /// </summary>
        public bool SSL { get; set; }

        [Newtonsoft.Json.JsonProperty("fromName")]
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string MailFromName { get; set; } 

        #endregion


        /// <summary>
        /// Запись учетных данных в json
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteToJson(string fileName)
        {
            WriteToJson(this,  fileName);
        }

        /// <summary>
        /// Запись учетных данных в json
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="fileName"></param>
        public void WriteToJson(MailCredential mc, string fileName)
        {
            using (StreamWriter swf = new StreamWriter(fileName))
            {
                swf.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(mc));
                swf.Close();
            }

        }

        /// <summary>
        /// Возвращает данные из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        public bool ReadFromJson(string fileName)
        {
            if (File.Exists(fileName))
            {
                MailCredential x = new MailCredential();
                x = Newtonsoft.Json.JsonConvert.DeserializeObject<MailCredential>(File.ReadAllText(fileName));
                SmtpServer = x.SmtpServer;
                Port = x.Port;
                Login = x.Login;
                PW = x.PW;
                From = x.From;
                MailFromName = x.MailFromName;
                SSL = x.SSL;
                return true;
            }
            else
            {
                MessageBox.Show("Не найден файл с учетными данными" + fileName, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;                
            }

        }
    }


}
