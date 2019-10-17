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
using ClientGUI.Communication;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for OnlineSolo.xaml
    /// </summary>
    public partial class OnlineSolo : Window
    {
		private Client client;
        public OnlineSolo(Client client)
        {
			this.client = client;

            InitializeComponent();
        }

        private void Onlinebtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this.client);
            mainWindow.Show();

            this.Close();
        }

        private void Solobtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
