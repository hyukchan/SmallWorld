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
        /// <summary>
        /// Constructeur d'une case "Desert"
        /// </summary>
        public Desert()
        {
            
        }
    }
}
