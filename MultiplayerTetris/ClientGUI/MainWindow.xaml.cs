using ClientGUI.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        private Client client;
        public string ClientId { set; get; }
        public MainWindow()
        {
            this.client = new Client();
            //string ClientId;
            client.Connect("localhost", 10001);

            InitializeComponent();
        }

        private void StoneButton_Click(object sender, RoutedEventArgs e)
        {
            
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Steen.png", UriKind.Relative));
            /*RotateImage();*/
            StoneButton.Click -= StoneButton_Click;
            PaperButton.Click -= PaperButton_Click;
            ScissorButton.Click -= ScissorButton_Click;
            this.client.Write($"{ClientId}::Rock");
        }
        private void PaperButton_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Papier.png", UriKind.Relative));
            StoneButton.Click -= StoneButton_Click;
            PaperButton.Click -= PaperButton_Click;
            ScissorButton.Click -= ScissorButton_Click;
            this.client.Write($"{ClientId}::Paper");
        }

        private void ScissorButton_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Schaar.png", UriKind.Relative));
            StoneButton.Click -= StoneButton_Click;
            PaperButton.Click -= PaperButton_Click;
            ScissorButton.Click -= ScissorButton_Click;
            this.client.Write($"{ClientId}::Scissors");
        }


        private void RotateImage()
        {
            Timer timer = new Timer();
            timer.Interval = 3;
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

                Dispatcher.Invoke(() =>
                {
                    RotateTransform transform = new RotateTransform();
                    transform.CenterX = 150;
                    transform.CenterY = 150;
                    transform.Angle = currentAngle;
                    if (currentAngle == 130)
                    {
                        this.Top += 5;
                    }
                    Yourchoice.RenderTransform = transform;
                });
            };
            timer.Start();
        }
        static class NativeMethods
        {
            [DllImport("Kernel32.dll")]
            public static extern void AllocConsole();

            [DllImport("Kernel32")]
            public static extern void FreeConsole();

            public const int SW_HIDE = 0;
            public const int SW_SHOW = 5;
        }

        private void ShowConsole_Click(object sender, RoutedEventArgs e)
        {
            NativeMethods.AllocConsole();
            Console.WriteLine("Console text");
        }

      

        private void CloseConsole_Click(object sender, RoutedEventArgs e)
        {
            NativeMethods.FreeConsole();
        }
    } }
 