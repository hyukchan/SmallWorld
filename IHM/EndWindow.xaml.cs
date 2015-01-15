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
using Small_World;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour EndWindow.xaml
    /// </summary>
    public partial class EndWindow : Window
    {
        private MainWindow main;

        public EndWindow(MainWindow m)
        {
            InitializeComponent();
            main = m;
            endWindowImage.Source = new BitmapImage(new Uri("./textures/draw.png", UriKind.Relative));
        }
        public EndWindow(MainWindow m, String winner)
        {
            InitializeComponent();
            main = m;
            endWindowImage.Source = new BitmapImage(new Uri("./textures/victory.png", UriKind.Relative));
            winnerName.Text = winner.ToUpper();
        }

        public void onClickExit(object sender, RoutedEventArgs e)
        {
            main.Close();
            this.Close();
        }

        public void onClickRestart(object sender, RoutedEventArgs e)
        {
            main.Close();
            StartWindow restart = new StartWindow();
            restart.Show();
            this.Close();
        }
    }
}
