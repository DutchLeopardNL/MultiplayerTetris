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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string chosenAttack;
        private Client client;
        public MainWindow()
        {
            this.client = new Client();

            client.Connect("localhost", 10001);

			InitializeComponent();
        }

        private void StoneButton_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Steen.png", UriKind.Relative));
            chosenAttack = "Stone";
            RotateImage();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Papier.png", UriKind.Relative));
            chosenAttack = "Paper";
            RotateImage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Schaar.png", UriKind.Relative));
            chosenAttack = "Scissor";
            RotateImage();
        }

        private void RotateImage()
        {
            Timer timer = new Timer();
            timer.Interval = 3;
            int timesRotated = 0;
            int currentAngle = 0;
            timer.Elapsed += (x,y) =>
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
    }
}
