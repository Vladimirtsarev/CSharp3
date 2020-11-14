﻿
using System;
using System.CodeDom;
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

            cbSenderSelect.ItemsSource = StaticVariables.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            DB db = new DB();
            dgEmails.ItemsSource = db.Emails;

            MailCredential m = new MailCredential();
            if (m.ReadFromJson("mailcredential.json"))
            {
                //из json
                Presets1(m);
            }
            else
            {
                // из StaticVariables
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

        /// <summary>
        /// Нажатие Отправить письмо 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMail();

            SendEndWindow sew = new SendEndWindow();
            sew.ShowDialog();

            //ErrorMessageWindow emw = new ErrorMessageWindow(new Exception("Типа ошибка"));
            //emw.ShowDialog();
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

                ErrorMessageWindow emw = new ErrorMessageWindow(e);
                emw.ShowDialog();
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Нажатие Перейти в планировщик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }
        
        /// <summary>
        /// Нажатие кнопки Отправить сейчас
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strLogin))
            {
                //MessageBox.Show("Выберите отправителя");
                ErrorMessageWindow emw = new ErrorMessageWindow(new Exception("Выберите отправителя"));
                emw.ShowDialog();
                return;
            }
            if (string.IsNullOrEmpty(strPassword))
            {
                //MessageBox.Show("Укажите пароль отправителя");
                ErrorMessageWindow emw = new ErrorMessageWindow(new Exception("Укажите пароль отправителя"));
                emw.ShowDialog();
                return;
            }

            EmailSendService emailSender = new EmailSendService(strLogin, strPassword);
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
        }

        /// <summary>
        /// Назатие кнопки Отправить запланированно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendP_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if (tsSendTime == new TimeSpan())
            {
                //MessageBox.Show("Некорректный формат даты");
                ErrorMessageWindow emw = new ErrorMessageWindow(new Exception("Некорректный формат даты"));
                emw.ShowDialog();
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                //MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                ErrorMessageWindow emw = new ErrorMessageWindow(new Exception("Дата и время отправки писем не могут быть раньше, чем настоящее время"));
                emw.ShowDialog();
                return;
            }
            EmailSendService emailSender = new EmailSendService(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);

        }

        /// <summary>
        /// Нажатие Следующий таб на табсвичере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabSwitcherControl_btnNextClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex < tabControl.Items.Count - 1)
            {                
                // Показываем все
                tscTabSwicher.IsHideBtnNext = false;
                tscTabSwicher.IsHidebtnPrevious = false; 
                
                tabControl.SelectedIndex++;
            }
            if (tabControl.SelectedIndex == tabControl.Items.Count - 1)
            {
                // скрываем следующую
                tscTabSwicher.IsHideBtnNext = true;                
            }
        }

        /// <summary>
        /// Нажатие Предыдущий таб на табсвичере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabSwitcherControl_btnPreviousClick(object sender, RoutedEventArgs e)
        {            
            if (tabControl.SelectedIndex == 1)
            {
                // скрываем предыдущую
                tscTabSwicher.IsHidebtnPrevious = true;

                tabControl.SelectedIndex--;
            }
            if (tabControl.SelectedIndex > 1)
            {
                //показываем все
                tscTabSwicher.IsHidebtnPrevious = false;
                tscTabSwicher.IsHideBtnNext = false;
                
                tabControl.SelectedIndex--;
            }            
        }
    }
}
