using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{   
    public interface DesertInterface : TileInterface
    {

    }
    public class Desert : Tile, DesertInterface
    {
        public Desert()
        {
            
        }
    }
}
