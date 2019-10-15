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
    /// Interaction logic for OnlineSolo.xaml
    /// </summary>
    public partial class OnlineSolo : Window
    {
        public OnlineSolo()
        {
            InitializeComponent();
        }

     

        private void Onlinebtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            NameScreen nameScreen = new NameScreen();
            mainWindow.Show();
            mainWindow.ClientId = nameScreen.clientName;
            this.Close();
        }

        private void Solobtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
