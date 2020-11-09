using System;
using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для ErrorMessageWindow.xaml
    /// </summary>
    public partial class ErrorMessageWindow : Window
    {
        public ErrorMessageWindow(Exception ex)
        {
            InitializeComponent();
            IErr.Content = ex.Message;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
