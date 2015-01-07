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
            GameCreator gameCreator = new GameCreator();
            GameBuilder gameBuilder = gameCreator.CreateGame();

            InitializeComponent();
            PopulationCollection populationCollection = new PopulationCollection();
            PeopleBox1.DataContext = populationCollection;
            PeopleBox2.DataContext = populationCollection;

            PeopleBox1.SelectedItem = "Elf";
            PeopleBox2.SelectedItem = "Dwarf";

            MapCollection mapCollection = new MapCollection();
            mapBox.DataContext = mapCollection;
            mapBox.SelectedItem = "Demo";
        }

        private void OnClickStartGame(object sender, RoutedEventArgs e)
        {

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
            Add("Normal");
            Add("Large");
        }
    }
}
