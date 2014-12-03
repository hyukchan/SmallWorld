using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing.BitmapImage;

//TODO

namespace IHM
{
    class MapView:Panel
    {
        protected override void OnRender(DrawingContext dc){
            BitmapImage image = new BitmapImage(new Uri("textures/desert.png", UriKind.Relative));
            dc.DrawImage("textures/desert.png",);
        }
    }
}
