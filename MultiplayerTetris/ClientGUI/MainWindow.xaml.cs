using ClientGUI.Communication;
using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClientGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly Client client;
		public MainWindow(Client client)
		{
			this.client = client;
			this.client.MainWindow = this;

			this.InitializeComponent();
		}

		private void StoneButton_Click(object sender, RoutedEventArgs e)
		{

			this.Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Steen.png", UriKind.Relative));
			this.StoneButton.Click -= this.StoneButton_Click;
			this.PaperButton.Click -= this.PaperButton_Click;
			this.ScissorButton.Click -= this.ScissorButton_Click;
			this.client.Write($"{this.client.PlayerID}::Rock");
		}
		private void PaperButton_Click(object sender, RoutedEventArgs e)
		{
			this.Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Papier.png", UriKind.Relative));
			this.StoneButton.Click -= this.StoneButton_Click;
			this.PaperButton.Click -= this.PaperButton_Click;
			this.ScissorButton.Click -= this.ScissorButton_Click;
			this.client.Write($"{this.client.PlayerID}::Paper");
		}

		private void ScissorButton_Click(object sender, RoutedEventArgs e)
		{
			this.Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Schaar.png", UriKind.Relative));
			this.StoneButton.Click -= this.StoneButton_Click;
			this.PaperButton.Click -= this.PaperButton_Click;
			this.ScissorButton.Click -= this.ScissorButton_Click;

			this.client.Write($"{this.client.PlayerID}::Scissors");
		}
		public void ResetGame()
		{
			this.result.Text = "";
			this.StoneButton.Click += this.StoneButton_Click;
			this.PaperButton.Click += this.PaperButton_Click;
			this.ScissorButton.Click += this.ScissorButton_Click;
			this.Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Blank.png", UriKind.Relative));
		}


		private void RotateImage()
		{
			Timer timer = new Timer
			{
				Interval = 3
			};
			int timesRotated = 0;
			int currentAngle = 0;
			timer.Elapsed += (x, y) =>
			{
				if (timesRotated == 5000)
				{
					timer.Stop();
					return;
				}

				currentAngle += 10;
				if (currentAngle == 360)
				{
					currentAngle = 0;
					timesRotated++;
				}

				this.Dispatcher.Invoke(() =>
				{
					RotateTransform transform = new RotateTransform
					{
						CenterX = 150,
						CenterY = 150,
						Angle = currentAngle
					};
					if (currentAngle == 130)
					{
						this.Top += 5;
					}
					this.Yourchoice.RenderTransform = transform;
				});
			};
			timer.Start();
		}

		private static class NativeMethods
		{
			[DllImport("Kernel32.dll")]
			public static extern void AllocConsole();

			[DllImport("Kernel32")]
			public static extern void FreeConsole();

			public const int SW_HIDE = 0;
			public const int SW_SHOW = 5;
		}

		private void ResetGameButton_Click(object sender, RoutedEventArgs e)
		{
			this.ResetGame();
		}
	}
}
