using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface StrategyInterface
    {
        public const int NB_TILES_DEMO = 6;
        public const int NB_TILES_MEDIUM = 10;
        public const int NB_TILES_LARGE = 14;

        List<List<Tile>> build();
    }

    public unsafe abstract class BuilderGameBoard : StrategyInterface
    {
        protected int size;

        public int Size
        {
            get
            {
                return size;
            }
        }

        public unsafe List<List<Tile>> build()
        {
            //TODOSW strategie avec wrapper
            return null;
        }
    }

}
