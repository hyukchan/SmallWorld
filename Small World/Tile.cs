using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface TileInterface
    {
    }

    [Serializable]
    public abstract class Tile : TileInterface
    {
        public const int PLAIN = 0;
        public const int DESERT = 1;
        public const int FOREST = 2;
        public const int MOUNTAIN = 3;
    }
}
