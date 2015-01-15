﻿using System;
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
using Small_World;
using System.ComponentModel;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;
        private List<Polygon> listHexa;
        private List<Polygon> listHexaReachable;
        public Polygon selectedPolygon;
        private Polygon lastMouseEnteredPolygon;
        private Unit selectedUnit;
        private List<Unit> selectedTileUnits;
        private Brush possibleMovesColor;

        public MainWindow(Game g)
        {
            game = g;
            g.PropertyChanged += new PropertyChangedEventHandler(update);
            g.PlayerList[1].PropertyChanged += new PropertyChangedEventHandler(update);
            g.PlayerList[0].PropertyChanged += new PropertyChangedEventHandler(update);
            GameMessages.Instance.PropertyChanged += new PropertyChangedEventHandler(update);

            selectedPolygon = null;
            selectedTileUnits = new List<Unit>();
            int mapSize = game.Map.Size;
            double d = Hexagon.w / 2 * Math.Tan(30 * Math.PI / 180);
            double canvasHeight = (Hexagon.h - d) * mapSize + d;
            double canvasWidth = Hexagon.w * (mapSize + 0.5);
            listHexa = new List<Polygon>();
            listHexaReachable = new List<Polygon>();

            InitializeComponent();

            var converter = new System.Windows.Media.BrushConverter();
            possibleMovesColor = (Brush)converter.ConvertFromString("#fffd50");

            //initialize player's informations
            playerOneName.Text = game.PlayerList[0].Name;
            playerOnePoints.DataContext = game.PlayerList[0].Points;
            playerOnePoints.Text = game.PlayerList[0].Points.ToString();
            playerOneUnitNumbers.Text = game.PlayerList[0].Units.Count.ToString();

            playerTwoName.Text = game.PlayerList[1].Name;
            playerTwoPoints.Text = game.PlayerList[1].Points.ToString();
            playerTwoUnitNumbers.Text = game.PlayerList[1].Units.Count.ToString();

            myCanvas.Height = canvasHeight;
            myCanvas.Width = canvasWidth;

            showCurrentPlayer();

            MapView mv = new MapView(this.game);
            myCanvas.Children.Add(mv);

            showUnits();
            showUnitsOnMap();

            remainingTurns.Text = "Remaining turns : " + game.NbRemainingTurns.ToString();
            GameMessages.Instance.addMessage("Game Start !\n");
            GameMessages.Instance.addMessage(game.PlayerList[game.CurrentPlayer].Name + "'s Turn !\n");

            menuImage.MouseEnter += new MouseEventHandler(onMouseEnterMenu);
            menuImage.MouseLeave += new MouseEventHandler(onMouseLeaveMenu);

            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    double posx = i * Hexagon.w;
                    double posy = j * (Hexagon.h - d);
                    if (j % 2 == 1)
                    {
                        posx += Hexagon.w / 2;
                    }
                    Hexagon h = new Hexagon(posx, posy, i, j);
                    h.polygon.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                    h.polygon.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                    h.polygon.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickHexaHandler);
                    h.polygon.MouseRightButtonDown += new MouseButtonEventHandler(mouseRightClickHexaHandler);
                    listHexa.Add(h.polygon);
                    myCanvas.Children.Add(h.polygon);
                }
            }
        }

        /// <summary>
        /// Donne les indications visuelles pour montrer à qui est le tour
        /// </summary>
        public void showCurrentPlayer()
        {
            var converter = new System.Windows.Media.BrushConverter();

            Brush unactiveColor = (Brush)converter.ConvertFromString("#555555");
            Brush activeColor = (Brush)converter.ConvertFromString("#333333");
            
            BitmapImage playerOneActiveAvatar = new BitmapImage(game.PlayerList[0].getPlayerAvatar());
            BitmapImage playerTwoActiveAvatar = new BitmapImage(game.PlayerList[1].getPlayerAvatar());
            BitmapImage playerOneUnactiveAvatar = new BitmapImage(game.PlayerList[0].getUnactivePlayerAvatar());
            BitmapImage playerTwoUnactiveAvatar = new BitmapImage(game.PlayerList[1].getUnactivePlayerAvatar());

            if (game.CurrentPlayer == 0)
            {
                playerOnePanel.Background = activeColor;
                playerTwoPanel.Background = unactiveColor;

                playerOneAvatar.Source = playerOneActiveAvatar;
                playerTwoAvatar.Source = playerTwoUnactiveAvatar;
            }
            if (game.CurrentPlayer == 1)
            {
                playerOnePanel.Background = unactiveColor;
                playerTwoPanel.Background = activeColor;

                playerOneAvatar.Source = playerOneUnactiveAvatar;
                playerTwoAvatar.Source = playerTwoActiveAvatar;
            }
            playerOnePanel.Background.Opacity = 0.7;
            playerTwoPanel.Background.Opacity = 0.7;

            showUnits();
        }

        /// <summary>
        /// Ajoute les unités dans la liste des unités de chaque joueur
        /// </summary>
        public void showUnits()
        {
            var converter = new System.Windows.Media.BrushConverter();
            Brush activeColor = (Brush)converter.ConvertFromString("#FFFFFF");
            Brush unactiveColor = (Brush)converter.ConvertFromString("#999999");

            int pos = this.listHexa.IndexOf(selectedPolygon);
            int x = pos % this.game.Map.Size;
            int y = pos / this.game.Map.Size;

            playerOneUnitList.Children.Clear();
            playerTwoUnitList.Children.Clear();

            for (int i = 0; i < 2; i++)
            {
                int unitNumber = 0;
                foreach (Unit u in game.PlayerList[i].Units)
                {
                    UnitBox unitBox = new UnitBox(u, unitNumber, i);
                    unitBox.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickUnitboxHandler);

                    if (!(u.Position.X == x && u.Position.Y == y))
                    {
                        unactiveColor.Opacity = 0.1;
                        unitBox.unitBoxGrid.Background = unactiveColor;
                    }


                    if (selectedUnit == u)
                    {
                        activeColor.Opacity = 0.7;
                        unitBox.unitBoxGrid.Background = activeColor;
                    }

                    if (i == 0)
                    {
                        playerOneUnitList.Children.Add(unitBox);
                    }

                    else
                    {
                        playerTwoUnitList.Children.Add(unitBox);
                    }

                    unitNumber++;
                }
            }
        }


        /// <summary>
        /// Colorie la case sur laquelle la souris vient d'entrer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            lastMouseEnteredPolygon = new Polygon();
            lastMouseEnteredPolygon.Stroke = polygon.Stroke;
            lastMouseEnteredPolygon.StrokeThickness = polygon.StrokeThickness;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 4;
                polygon.Stroke = Brushes.White;
                polygon.SetValue(Canvas.ZIndexProperty, 50);
            }
        }

        /// <summary>
        /// Décolorie la case qu'on vient de sortir
        /// </summary>
        /// <param name="sender">Polygon</param>
        /// <param name="e"></param>
        private void mouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = lastMouseEnteredPolygon.StrokeThickness;
                polygon.Stroke = lastMouseEnteredPolygon.Stroke;
                if (polygon.Stroke == possibleMovesColor)
                {
                    polygon.SetValue(Canvas.ZIndexProperty, 50);
                }
                else
                {
                    polygon.SetValue(Canvas.ZIndexProperty, 10);
                }
                
            }
        }

        /// <summary>
        /// Permet d'afficher les unités sur la carte
        /// </summary>
        private void showUnitsOnMap()
        {
            List<Image> imgToRemove = new List<Image>();
            foreach (UIElement e in myCanvas.Children)
            {
                if (e.GetType() == typeof(Image))
                {
                    imgToRemove.Add((Image)e);
                }
            }

            foreach (Image i in imgToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            double w = Hexagon.w;
            double h = Hexagon.h;

            List<Position> positionList = new List<Position>();
            foreach (Player p in game.PlayerList)
            {
                foreach (Unit u in p.Units)
                {
                    if (!positionList.Contains(u.Position))
                    {
                        positionList.Add(u.Position);

                        double d = w / 2 * Math.Tan(30 * Math.PI / 180);

                        double posx = u.Position.X * w;
                        double posy = u.Position.Y * (h - d);
                        if (u.Position.Y % 2 == 1)
                        {
                            posx += w / 2;
                        }

                        Image imgP1 = new Image();
                        imgP1.Source = new BitmapImage(u.GetUnitIcon());
                        imgP1.Width = w;
                        imgP1.Height = h;
                        imgP1.SetValue(Canvas.ZIndexProperty, 9);
                        imgP1.SetValue(Canvas.LeftProperty, posx);
                        imgP1.SetValue(Canvas.TopProperty, posy);
                        myCanvas.Children.Add(imgP1);
                    }
                }
            }
        }

        public unsafe void showPossibleMoves()
        {
            if (game.PlayerList[game.CurrentPlayer].Units.Contains(selectedUnit))
            {
                for (int i = 0; i < game.Map.Size; i++)
                {
                    for (int j = 0; j < game.Map.Size; j++)
                    {
                        if (selectedUnit.Moves[i * selectedUnit.SizeMap + j] == 1)
                        {
                            Polygon hexa = listHexa[i * selectedUnit.SizeMap + j];
                            hexa.Stroke = possibleMovesColor;
                            hexa.StrokeThickness = 3;
                            hexa.SetValue(Canvas.ZIndexProperty, 25);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet de passer le tour lors du clique sur le bouton endTurnButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndTurnOnClick(object sender, RoutedEventArgs e)
        {
            game.ChangePlayer();

            resetPolygonStroke();

            showCurrentPlayer();

            selectedTileUnits = new List<Unit>();
            selectedUnit = null;
            selectedPolygon = null;
            showUnits();

            checkGameEnded();
        }

        /// <summary>
        /// Permet de mettre les bordures de tous les polygones à noir
        /// </summary>
        private void resetPolygonStroke()
        {
            foreach (Polygon p in this.listHexa)
            {
                if (selectedPolygon != p)
                {
                    p.StrokeThickness = 2;
                    p.Stroke = Brushes.Black;
                    p.SetValue(Canvas.ZIndexProperty, 10);
                }
            }
        }

        /// <summary>
        /// Permet de sélectionner une case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeftClickHexaHandler(object sender, MouseButtonEventArgs e)
        {
            selectedUnit = null;

            var polygon = sender as Polygon;
            this.selectedPolygon = polygon;
            resetPolygonStroke();
            polygon.StrokeThickness = 4;
            polygon.Stroke = Brushes.White;
            polygon.SetValue(Canvas.ZIndexProperty, 60);

            int pos = this.listHexa.IndexOf(polygon);
            int x = pos % this.game.Map.Size;
            int y = pos / this.game.Map.Size;

            getSelectedTileUnits(x, y);

            getNextSelectedUnit();

            showUnits();
        }

        private void getNextSelectedUnit()
        {
            selectedUnit = null;
            if (selectedTileUnits.Count() > 0)
            {
                int i = 0;
                while (selectedUnit == null && i < selectedTileUnits.Count())
                {
                    if (selectedTileUnits[i].MovePt > 0)
                    {
                        selectedUnit = selectedTileUnits[i];
                        showPossibleMoves();
                    }
                    i++;
                }
            }
        }

        private void getSelectedTileUnits(int posX, int posY)
        {
            selectedTileUnits = new List<Unit>();
            foreach (Unit u in game.PlayerList[game.CurrentPlayer].Units)
            {
                if (u.Position.X == posX && u.Position.Y == posY)
                {
                    selectedTileUnits.Add(u);
                }
            }
        }

        /// <summary>
        /// Permet de bouger une unité si il y a eu précédemment une sélection d'unité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseRightClickHexaHandler(object sender, MouseButtonEventArgs e)
        {
            if (selectedTileUnits.Count() > 0 && selectedUnit != null)
            {
                if (selectedTileUnits.Count() == 1)
                {
                    resetPolygonStroke();
                }
                int pos = this.listHexa.IndexOf((Polygon)sender);
                int x = pos % this.game.Map.Size;
                int y = pos / this.game.Map.Size;

                bool moved = false;
                if (selectedUnit.CanMove(x, y))
                {
                    moved = true;
                    game.AskToMove(selectedUnit, x, y);
                }
                else
                {
                    GameMessages.Instance.addMessage("[Error] " + game.PlayerList[game.CurrentPlayer].Name + ", you can't move that unit there !\n");
                }

                if (moved)
                {
                    selectedTileUnits.Remove(selectedUnit);
                }

                if (selectedTileUnits.Count() == 0)
                {
                    selectedUnit = null;
                }
                else
                {
                    getNextSelectedUnit();
                }

                showUnits();
                showUnitsOnMap();
                checkGameEnded();
            }
            else
            {
                GameMessages.Instance.addMessage("[Error] " + game.PlayerList[game.CurrentPlayer].Name + ", you need to select an unit first !\n");
            }
        }

        /// <summary>
        /// vérifie si le jeu est terminé
        /// </summary>
        public void checkGameEnded()
        {
            if (game.GameEnded) {
                EndWindow endWindow;
                if (game.winner() == "egalite")
                {
                    GameMessages.Instance.addMessage("You did a draw !\n");
                    endWindow = new EndWindow(this);
                }
                else
                {
                    GameMessages.Instance.addMessage(game.winner() + " has won !\n");
                    endWindow = new EndWindow(this, game.winner());
                }
                this.IsEnabled = false;
                endWindow.Show();
            }
        }

        /// <summary>
        /// Permet de sélectionner une unitBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseLeftClickUnitboxHandler(object sender, MouseButtonEventArgs e)
        {
            UnitBox u = (UnitBox)sender;
            int posX = (int)u.unitPosX.Content;
            int posY = (int)u.unitPosY.Content;
            int unitNumber = (int)u.unitNumber.Content;
            Polygon unitPolygon = listHexa[posY * game.Map.Size + posX];

            var polygon = unitPolygon;
            this.selectedPolygon = polygon;
            polygon.StrokeThickness = 4;
            polygon.Stroke = Brushes.White;
            polygon.SetValue(Canvas.ZIndexProperty, 60);

            selectedUnit = game.PlayerList[(int)u.playerNumber.Content].Units[(int)u.unitNumber.Content];
            selectedTileUnits = new List<Unit>();
            if ((int)u.playerNumber.Content == game.CurrentPlayer)
            {
                selectedTileUnits.Add(selectedUnit);
            }

            resetPolygonStroke();
            showPossibleMoves();

            showUnits();
        }

        private void onMouseEnterMenu(object sender, MouseEventArgs e)
        {
            menuImage.Source = new BitmapImage(new Uri("./textures/active_menu.png", UriKind.Relative));
        }

        private void onMouseLeaveMenu(object sender, MouseEventArgs e)
        {
            menuImage.Source = new BitmapImage(new Uri("./textures/menu.png", UriKind.Relative));
        }

        private void onMouseDownMenu(object sender, MouseEventArgs e)
        {
            menuPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void onClickResume(object sender, RoutedEventArgs e)
        {
            menuPanel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void onClickExit(object sender, RoutedEventArgs e)
        {
            StartWindow s = new StartWindow();
            s.Show();
            this.Close();
        }

        private void onClickSave(object sender, RoutedEventArgs e)
        {
            SaveWindow s = new SaveWindow(game);
            s.Show();
        }

        public void update(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Points":
                    playerOnePoints.Text = game.PlayerList[0].Points.ToString();
                    playerTwoPoints.Text = game.PlayerList[1].Points.ToString();
                    break;
                case "Units":
                    playerOneUnitNumbers.Text = game.PlayerList[0].Units.Count().ToString();
                    playerTwoUnitNumbers.Text = game.PlayerList[1].Units.Count().ToString();
                    break;
                case "GameMessages":
                    gameMessages.Content = gameMessages.Content + GameMessages.Instance.getLastMessage();
                    gameMessages.ScrollToBottom();
                    break;
                case "RemainingTurns":
                    remainingTurns.Text = "Remaining turns : " + game.NbRemainingTurns.ToString();
                    break;
            }
        }

        public class Hexagon
        {

            public Polygon polygon;
            public const double h = 80;
            public const double w = 69;

            public int posX;
            public int posY;
            public Position pos;

            public Hexagon(double x, double y, int posX, int posY)
            {
                pos.X = posX;
                pos.Y = posY;

                polygon = new Polygon();
                polygon.Stroke = Brushes.Black;
                polygon.Fill = Brushes.Transparent;
                polygon.StrokeThickness = 2;

                double d = w / 2 * Math.Tan(30 * Math.PI / 180);

                PointCollection pCollect = new PointCollection();
                Point p1 = new Point(w / 2, 0);
                Point p2 = new Point(w, d);
                Point p3 = new Point(w, h - d);
                Point p4 = new Point(w / 2, h);
                Point p5 = new Point(0, h - d);
                Point p6 = new Point(0, d);
                pCollect.Add(p1);
                pCollect.Add(p2);
                pCollect.Add(p3);
                pCollect.Add(p4);
                pCollect.Add(p5);
                pCollect.Add(p6);
                polygon.Points = pCollect;

                polygon.SetValue(Canvas.LeftProperty, x);
                polygon.SetValue(Canvas.TopProperty, y);
                polygon.SetValue(Canvas.ZIndexProperty, 10);

            }
        }
    }
}