using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable]
    public class GameBoard
    {
        private List<Tile> listTiles;
        private BuilderGameBoard strategy;
        private TileFactory tilesFactory;
        private int size;

        /// <summary>
        /// Constructeur du plateau de jeu
        /// </summary>
        public GameBoard()
        {
            TilesFactory = TileFactory.TileFactory_Instance;
            ListTiles = new List<Tile>();
        }

        /// <summary>
        /// Constructeur du plateau à partir d'une liste de cases
        /// </summary>
        /// <param name="tiles">La liste à recopier</param>
        public GameBoard(List<Tile> tiles)
        {
            TilesFactory = TileFactory.TileFactory_Instance;
            ListTiles = tiles;
        }

        /// <summary>
        /// Getter/Setter de la fabrique de cases
        /// </summary>
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

        /// <summary>
        /// Getter/Setter de la liste de cases
        /// </summary>
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

        /// <summary>
        /// Getter/Setter de la stratégie à employer
        /// </summary>
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

        /// <summary>
        /// Getter/Setter de la taille du plateau
        /// </summary>
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

        /// <summary>
        /// Crée le plateau en fonction de la stratégie
        /// </summary>
        public void Create()
        {
            Strategy.build();
        }

        /// <summary>
        /// Modifie la stratégie à employer
        /// </summary>
        /// <param name="b">Le monteur du plateau de taille voulue</param>
        public void SetStrategy(BuilderGameBoard b)
        {
            Strategy = b;
        }


    }
}
