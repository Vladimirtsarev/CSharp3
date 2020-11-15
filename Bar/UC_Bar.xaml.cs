using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Bar
{
    /// <summary>
    /// Логика взаимодействия
    /// </summary>
    public partial class UC_Bar : UserControl
    {
        
        public string Text
        { 
            //get 
            //{
            //    return label.Content; 
            //}
            set
            {                
                label.Content = value;
            }
        }

        public ComboBox ComboBox 
        {
            get 
            { 
                return cbox; 
            }
            set
            {                
                cbox = value;
            }
        }

        public UC_Bar()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler btnAddClick;
        public event RoutedEventHandler btnEditClick;
        public event RoutedEventHandler btnDeleteClick;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnAddClick?.Invoke(sender, e);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }
    }
}
