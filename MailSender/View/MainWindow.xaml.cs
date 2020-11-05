
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MailSenderLib;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MailCredential m = new MailCredential();
            if (m.ReadFromJson("mailcredential.json"))
            {
                Presets1(m);
            }
            else
            {
                Presets2();
            }

        }

        /// <summary>
        /// Забираем переменные из файла json
        /// </summary>
        private void Presets1(MailCredential m)
        {
            tbServer.Text = m.SmtpServer;
            tbPort.Text = m.Port.ToString();
            tbUserName.Text = m.Login;
            tbPassword.Password = m.PW;
            chBoxSSL.IsChecked = m.SSL;
            cbFrom.Text = m.From;
            cbTo.Text = "79262685273@ya.ru";
            tbSubject.Text = "Тест рассыльщика";
            tbBody.Text = "Текст рассылки";

        }

        /// <summary>
        /// Забираем переменные из статического класса StaticVariables
        /// </summary>
        private void Presets2()
        {
            tbServer.Text = StaticVariables.SmtpServer;
            tbPort.Text = StaticVariables.Port.ToString();
            tbUserName.Text = StaticVariables.Login;
            tbPassword.Password = StaticVariables.PW;
            cbFrom.Text = StaticVariables.From;
            cbTo.Text = StaticVariables.To;
            tbSubject.Text = StaticVariables.Subj;
            tbBody.Text = StaticVariables.Text;
            chBoxSSL.Content = StaticVariables.Ssl;
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMail();
        }

        /// <summary>
        /// Отправка письма на почтовый ящик
        /// </summary>       
        private void SendMail()
        {
            try
            {
                MailAddress mTo = new MailAddress(cbTo.Text);

                MailSenderLib.MailSender ms = new MailSenderLib.MailSender(mTo, tbSubject.Text, tbBody.Text, null);
                ms.Send();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка отправки письма:\n" + e.ToString());
            }

        }




    }
}
