using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class GameBoard
    {
        private List<Tile> listTiles;
        private BuilderGameBoard strategy;
        private TileFactory tilesFactory;
        private int size;

        public GameBoard()
        {
            TilesFactory = TileFactory.TileFactory_Instance;
            ListTiles = new List<Tile>();
        }

        public GameBoard(List<Tile> tiles)
        {
            TilesFactory = TileFactory.TileFactory_Instance;
            ListTiles = tiles;
        }

        public TileFactory TilesFactory
        {
            get
            {
                return tilesFactory;
            }
            set
            {
                tilesFactory = value;
            }
        }

        public List<Tile> ListTiles
        {
            get
            {
                return listTiles;
            }
            set
            {
                listTiles = value;
                size = value.Count;
            }
        }

        public BuilderGameBoard Strategy
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public void Create()
        {
            Strategy.build();
        }

        public void SetStrategy(BuilderGameBoard b)
        {
            Strategy = b;
        }


    }
}
