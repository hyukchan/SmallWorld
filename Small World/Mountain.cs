using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface MountainInterface : TileInterface
    {

    }

    [Serializable()]
    public class Mountain : Tile, MountainInterface
    {
        /// <summary>
        /// Constructeur d'une case "Montagne"
        /// </summary>
        public Mountain()
        {
            
        }
    }
}
