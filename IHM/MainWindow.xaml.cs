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

        public MainWindow(Game g)
        {
            game = g;
            int mapSize = (int) Math.Sqrt(game.Map.Size);
            double d = Hexagon.w / 2 * Math.Tan(30 * Math.PI / 180);
            double canvasHeight = (Hexagon.h - d) * mapSize + d;
            double canvasWidth = Hexagon.w * (mapSize + 0.5);
            List<Polygon> listHexa = new List<Polygon>();
            List<Polygon> listHexaReachable = new List<Polygon>();

            InitializeComponent();

            myCanvas.Height = canvasHeight;
            myCanvas.Width = canvasWidth;

            MapView mv = new MapView(this.game);
            myCanvas.Children.Add(mv);
        }

        public class Hexagon
        {

            public Polygon polygon;
            public const double h = 80;
            public const double w = 69;

            public Hexagon(double x, double y)
            {
                polygon = new Polygon();
                polygon.Stroke = Brushes.Black;
                polygon.Fill = Brushes.Transparent;
                polygon.StrokeThickness = 2;

                // d + side + d = h
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
