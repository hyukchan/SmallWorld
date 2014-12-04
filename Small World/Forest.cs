using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{   
    public interface ForestInterface : TileInterface
    {

    }
    public class Forest : Tile, ForestInterface
    {
        public Forest()
        {
            
        }
    }
}
