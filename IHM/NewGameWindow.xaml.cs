using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public NewGameWindow()
        {
            InitializeComponent();
            PopulationCollection populationCollection = new PopulationCollection();
            playerOnePeople.DataContext = populationCollection;
            playerTwoPeople.DataContext = populationCollection;

            playerOnePeople.SelectedItem = "Elf";
            playerTwoPeople.SelectedItem = "Dwarf";

            MapCollection mapCollection = new MapCollection();
            mapType.DataContext = mapCollection;
            mapType.SelectedItem = "Demo";
        }

        private void OnClickStartGame(object sender, RoutedEventArgs e)
        {
            string playerOneNameString = playerOneName.Text;
            string playerTwoNameString = playerTwoName.Text;

            string playerOnePeopleString = (string) playerOnePeople.SelectedItem;
            string playerTwoPeopleString = (string) playerTwoPeople.SelectedItem;

            string mapTypeString = (string) mapType.SelectedItem;

            if (playerOneNameString == "" || playerTwoNameString == "" || playerOnePeopleString == "" || playerTwoPeopleString == "" || mapTypeString == "")
            {
                errorBlock.Text = "Empty fields !";
            }
            else if (playerOnePeopleString == playerTwoPeopleString)
            {
                errorBlock.Text = "You can't play with the same people !";
            }
            else if (playerOneNameString == playerTwoNameString)
            {
                errorBlock.Text = "You want to play alone ?";
            }
            else
            {
                GameCreator gameCreator = new GameCreator();
                gameCreator.AddPlayer(playerOneNameString, playerOnePeopleString);
                gameCreator.AddPlayer(playerTwoNameString, playerTwoPeopleString);

                gameCreator.GameType = mapTypeString;

                Game game = gameCreator.CreateGame();

                MainWindow mainWindow = new MainWindow(game);
                mainWindow.Show();
                this.Close();
            }
        }
    }

    class PopulationCollection : ObservableCollection<string>
    {
        public string selected
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public PopulationCollection()
        {
            Add("Elf");
            Add("Dwarf");
            Add("Orc");
        }
    }

    class MapCollection : ObservableCollection<string>
    {
        public string selected
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MapCollection()
        {
            Add("Demo");
            Add("Medium");
            Add("Large");
        }
    }
}
