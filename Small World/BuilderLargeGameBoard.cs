using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface StrategyLargeInterface : StrategyInterface
    {

        new List<List<Tile>> build();
    }

    public class BuilderLargeGameBoard : BuilderGameBoard, StrategyLargeInterface
    {
        public const int NB_TILES_LARGE = 14;

        public BuilderLargeGameBoard()
        {
            size = NB_TILES_LARGE;
        }
    }
   
}
