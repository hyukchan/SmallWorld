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

        //protected unsafe override void OnRender(DrawingContext dc){
            
        //    WrapperAlgo wrapper = new WrapperAlgo();
        //    var map = wrapper.createGameBoard(4);
        //    BitmapImage image = new BitmapImage(new Uri("textures/desert.png", UriKind.RelativeOrAbsolute));
        //    dc.DrawImage(image, new System.Windows.Rect(0,0,69,79));
        //}

        unsafe protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            int TAILLE = 4;
            BitmapImage plainImg = new BitmapImage(new Uri("textures/plain.png", UriKind.Relative));
            BitmapImage desertImg = new BitmapImage(new Uri("textures/desert.png", UriKind.Relative));
            BitmapImage mountainImg = new BitmapImage(new Uri("textures/ocean.png", UriKind.Relative));
            BitmapImage forestImg = new BitmapImage(new Uri("textures/foret.png", UriKind.Relative));

            BitmapImage[] tilesImg = { plainImg, desertImg, mountainImg, forestImg };

            for (int j = 0; j < TAILLE; j++)
            {
                for (int i = 0; i < TAILLE; i++)
                {
                    //TODOSW : replace map initialisation using the method createGameBoard(int n)
                    var map = new int[16] { 1, 0, 2 ,1 , 3, 1, 2, 3, 1, 1, 2, 3, 1, 1, 2, 2};
                    int tile = map[j * TAILLE + i];

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
