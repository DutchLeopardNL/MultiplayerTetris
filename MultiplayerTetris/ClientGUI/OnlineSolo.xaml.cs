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
using ServerProject.Communication;
using ServerProject.Data;

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

			Dictionary<string, int> scores = FileIO.Read(Server.path, "scores.txt");

			var ordered = scores.OrderBy(x => x.Value);

			int counter = 0;
			string result = "Top 5 wins:\n";
			for (int i = ordered.Count() - 1; i >= 0; i--)
			{
				if (counter > 5) break;

				var pair = ordered.ElementAt(i);
				result += $"{pair.Key}:\t{pair.Value} wins\n";

				counter++;
			}

			LeaderboardLabel.Content = result;
        }

        private void Onlinebtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this.client);
            mainWindow.Show();

            this.Close();
        }
	}
}
