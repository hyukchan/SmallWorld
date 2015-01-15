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

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        public void OnClickNewGame(object sender, RoutedEventArgs e)
        {
            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.Show();
            this.Close();
        }

        public void OnClickLoadGame(object sender, RoutedEventArgs e)
        {
            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.Show();
            this.Close();
        }
    }
}
