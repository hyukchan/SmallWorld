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
using Small_World;

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
            
        public MainWindow(Game g)
        {
            game = g;
            selectedPolygon = null;
            int mapSize = (int) Math.Sqrt(game.Map.Size);
            double d = Hexagon.w / 2 * Math.Tan(30 * Math.PI / 180);
            double canvasHeight = (Hexagon.h - d) * mapSize + d;
            double canvasWidth = Hexagon.w * (mapSize + 0.5);
            listHexa = new List<Polygon>();
            listHexaReachable = new List<Polygon>();

            InitializeComponent();

            //initialize player's informations
            playerOneName.Text = game.PlayerList[0].Name;
            playerOnePoints.Text = game.PlayerList[0].Points.ToString();
            playerOneUnitNumbers.Text = game.PlayerList[0].Units.Count.ToString();

            playerTwoName.Text = game.PlayerList[1].Name;
            playerTwoPoints.Text = game.PlayerList[1].Points.ToString();
            playerTwoUnitNumbers.Text = game.PlayerList[1].Units.Count.ToString();

            myCanvas.Height = canvasHeight;
            myCanvas.Width = canvasWidth;

            MapView mv = new MapView(this.game);
            myCanvas.Children.Add(mv);

            game.PlayerList[0].Units[0].Move(game.PlayerList[0].Units[0].Position.X, game.PlayerList[0].Units[0].Position.Y + 1);

            showUnits();
            showUnitsOnMap();

            

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
                    listHexa.Add(h.polygon);
                    myCanvas.Children.Add(h.polygon);
                }
            }
        }

        public void showUnits()
        {
            int pos = this.listHexa.IndexOf(selectedPolygon);
            int x = pos % (int) Math.Sqrt(this.game.Map.Size);
            int y = pos / (int) Math.Sqrt(this.game.Map.Size);

            playerOneUnitList.Children.Clear();
            foreach (Unit u in game.PlayerList[0].Units)
            {
                UnitBox unitBox = new UnitBox(u);
                unitBox.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickUnitboxHandler);
                if (!(u.Position.X == x && u.Position.Y == y))
                {
                    unitBox.Opacity = 0.5;
                }
                playerOneUnitList.Children.Add(unitBox);
            }

            playerTwoUnitList.Children.Clear();
            foreach (Unit u in game.PlayerList[1].Units)
            {
                UnitBox unitBox = new UnitBox(u);
                unitBox.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftClickUnitboxHandler);
                if (!(u.Position.X == x && u.Position.Y == y))
                {
                    unitBox.Opacity = 0.5;
                }
                playerTwoUnitList.Children.Add(unitBox);
            }
        }



        private void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 4;
                polygon.Stroke = Brushes.White;
                polygon.SetValue(Canvas.ZIndexProperty, 50);
            }
        }

        private void mouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var polygon = sender as Polygon;
            if (polygon != this.selectedPolygon)
            {
                polygon.StrokeThickness = 2;
                polygon.Stroke = Brushes.Black;
                polygon.SetValue(Canvas.ZIndexProperty, 10);
            }
            if (this.listHexaReachable.Contains(polygon))
            {
                polygon.StrokeThickness = 3;
                polygon.Stroke = Brushes.GreenYellow;
                polygon.SetValue(Canvas.ZIndexProperty, 25);
            }
        }

        private void showUnitsOnMap()
        {
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

        private void EndTurnOnClick(object sender, RoutedEventArgs e)
        {
            game.ChangePlayer();
        }



        private void mouseLeftClickHexaHandler(object sender, MouseButtonEventArgs e)
        {
            foreach (Polygon p in this.listHexa)
            {
                if (p == selectedPolygon)
                {
                    if (this.listHexaReachable.Contains(selectedPolygon))
                    {
                        p.StrokeThickness = 3;
                        p.Stroke = Brushes.GreenYellow;
                        p.SetValue(Canvas.ZIndexProperty, 25);
                    }
                    else
                    {
                        p.StrokeThickness = 2;
                        p.Stroke = Brushes.Black;
                        p.SetValue(Canvas.ZIndexProperty, 10);
                    }
                }
            }

            var polygon = sender as Polygon;
            this.selectedPolygon = polygon;
            polygon.StrokeThickness = 4;
            polygon.Stroke = Brushes.White;
            polygon.SetValue(Canvas.ZIndexProperty, 60);

            showUnits();
        }

        private void mouseLeftClickUnitboxHandler(object sender, MouseButtonEventArgs e)
        {
            UnitBox u = (UnitBox) sender;
            int posX = (int) u.unitPosX.Content;
            int posY = (int) u.unitPosY.Content;
            Polygon unitPolygon = listHexa[posY * ((int)Math.Sqrt(game.Map.Size)) + posX];

            foreach (Polygon p in this.listHexa)
            {
                if (p == selectedPolygon)
                {
                    if (this.listHexaReachable.Contains(selectedPolygon))
                    {
                        p.StrokeThickness = 3;
                        p.Stroke = Brushes.GreenYellow;
                        p.SetValue(Canvas.ZIndexProperty, 25);
                    }
                    else
                    {
                        p.StrokeThickness = 2;
                        p.Stroke = Brushes.Black;
                        p.SetValue(Canvas.ZIndexProperty, 10);
                    }
                }
            }

            var polygon = unitPolygon;
            this.selectedPolygon = polygon;
            polygon.StrokeThickness = 4;
            polygon.Stroke = Brushes.White;
            polygon.SetValue(Canvas.ZIndexProperty, 60);

            showUnits();
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