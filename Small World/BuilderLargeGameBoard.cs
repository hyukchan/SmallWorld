using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface StrategyLargeInterface : StrategyInterface
    {

        List<Tile> build();
    }

    [Serializable]
    public class BuilderLargeGameBoard : BuilderGameBoard, StrategyLargeInterface
    {
        public const int NB_TILES_LARGE = 14;
        public const int NB_UNITS_LARGE = 8;
        public const int NB_TURNS_LARGE = 30;

        /// <summary>
        /// Monteur d'un grand plateau
        /// </summary>
        public BuilderLargeGameBoard()
        {
            size = NB_TILES_LARGE;
        }
    }
   
}
