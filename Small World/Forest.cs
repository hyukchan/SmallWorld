using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{   
    public interface ForestInterface : TileInterface
    {

    }
    [Serializable()]
    public class Forest : Tile, ForestInterface
    {
        /// <summary>
        /// Constructeur d'une case "Foret"
        /// </summary>
        public Forest()
        {
            
        }
    }
}
