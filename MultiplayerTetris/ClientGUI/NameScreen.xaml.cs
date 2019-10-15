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
using System.Windows.Shapes;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for NameScreen.xaml
    /// </summary>
    public partial class NameScreen : Window
    {
       public string clientName { get; set; }
        public NameScreen()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            OnlineSolo onlineSolo = new OnlineSolo();
            /*clientName = clientNameChoice.Text;*/
            onlineSolo.Show();
            this.Close();
        }
    }
}
