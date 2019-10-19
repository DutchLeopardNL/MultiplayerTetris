using ClientGUI.Communication;
using System.Windows;

namespace ClientGUI
{
	/// <summary>
	/// Interaction logic for NameScreen.xaml
	/// </summary>
	public partial class NameScreen : Window
	{
		private readonly Client client;

		public NameScreen()
		{
			this.client = new Client();
			this.client.Connect("localhost", 10001);

			this.InitializeComponent();
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			OnlineSolo onlineSolo = new OnlineSolo(this.client);
			string name = this.clientNameChoice.Text;
			this.client.Write($"name::{name}");

			onlineSolo.Show();
			this.Close();
		}
	}
}
