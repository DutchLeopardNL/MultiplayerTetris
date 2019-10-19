using ClientGUI.Communication;
using ServerProject.Communication;
using ServerProject.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ClientGUI
{
	/// <summary>
	/// Interaction logic for OnlineSolo.xaml
	/// </summary>
	public partial class OnlineSolo : Window
	{
		private readonly Client client;
		public OnlineSolo(Client client)
		{
			this.client = client;

			this.InitializeComponent();

			Dictionary<string, int> scores = FileIO.Read(Server.path, "scores.txt");

			IOrderedEnumerable<KeyValuePair<string, int>> ordered = scores.OrderBy(x => x.Value);

			int counter = 1;
			string result = "Top 5 wins:\n";
			for (int i = ordered.Count() - 1; i >= 0; i--)
			{
				if (counter > 5)
				{
					break;
				}

				KeyValuePair<string, int> pair = ordered.ElementAt(i);
				result += $"{pair.Key}:\t{pair.Value} wins\n";

				counter++;
			}

			this.LeaderboardLabel.Content = result;
		}

		private void Onlinebtn_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow(this.client);
			mainWindow.Show();

			this.Close();
		}
	}
}
