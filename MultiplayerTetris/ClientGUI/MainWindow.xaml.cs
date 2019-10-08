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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StoneButton_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Steen.png", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Papier.png", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Yourchoice.Source = new BitmapImage(new Uri(@"Resources\Schaar.png", UriKind.Relative));
        }
    }
}
