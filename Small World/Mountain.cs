using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface MountainInterface : TileInterface
    {

    }
    public class Mountain : Tile, MountainInterface
    {
        public Mountain()
        {
            
        }
    }
}
