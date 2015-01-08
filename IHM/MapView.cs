using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wrapper;
using Small_World;
//TODO

namespace IHM
{
    class MapView:Panel
    {
        private Game game;

        public MapView(Game g)
        {
            this.game = g;
        }

        unsafe protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            int mapSize = (int) Math.Sqrt(game.Map.Size);
            BitmapImage plainImg = new BitmapImage(new Uri("./textures/plain.png", UriKind.Relative));
            BitmapImage desertImg = new BitmapImage(new Uri("./textures/desert.png", UriKind.Relative));
            BitmapImage mountainImg = new BitmapImage(new Uri("./textures/mountain.png", UriKind.Relative));
            BitmapImage forestImg = new BitmapImage(new Uri("./textures/foret.png", UriKind.Relative));

            BitmapImage[] tilesImg = { plainImg, desertImg, mountainImg, forestImg };

            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    var map = game.TabMap;
                    int tile = map[j * mapSize + i];

                    double d = tilesImg[tile].PixelWidth / 2 * Math.Tan(30 * Math.PI / 180);

                    double posx = i * tilesImg[tile].PixelWidth;
                    double posy = j * (tilesImg[tile].PixelHeight - d);
                    if (j % 2 == 1)
                    {
                        posx += tilesImg[tile].PixelWidth / 2;
                    }

                    dc.DrawImage(tilesImg[tile], new System.Windows.Rect(posx, posy, tilesImg[tile].PixelWidth, tilesImg[tile].PixelHeight));
                }
            }

        }
    }
}
