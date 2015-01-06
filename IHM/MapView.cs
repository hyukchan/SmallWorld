using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wrapper;

//TODO

namespace IHM
{
    class MapView:Panel
    {
        protected unsafe override void OnRender(DrawingContext dc){
            WrapperAlgo wrapper = new WrapperAlgo();
            var map = wrapper.createGameBoard(4);
            BitmapImage image = new BitmapImage(new Uri("textures/desert.png", UriKind.RelativeOrAbsolute));
            dc.DrawImage(image, new System.Windows.Rect(0,0,69,79));
        }
    }
}
