using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface StrategyMediumInterface : StrategyInterface
    {

        new List<List<Tile>> build();
    }

    public class BuilderMediumGameBoard : BuilderGameBoard, StrategyMediumInterface
    {
        public const int NB_TILES_MEDIUM = 10;

        public BuilderMediumGameBoard()
        {
            size = NB_TILES_MEDIUM;
        }
    }
    
}
