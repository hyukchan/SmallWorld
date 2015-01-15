using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    public interface StrategyInterface
    {
        List<Tile> build();
    }

    [Serializable()]
    public abstract class BuilderGameBoard : StrategyInterface
    {
        protected int size;

        /// <summary>
        /// Getter/Setter de la taille du plateau
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Contruit la liste des cases du plateau en fonction de la création du wrapper
        /// </summary>
        /// <returns>La liste des cases</returns>
        public unsafe List<Tile> build()
        {

            int i, j, k;

            int* map;
            WrapperAlgo wrapperAlgo = new WrapperAlgo();
            map = wrapperAlgo.createGameBoard(Size);

            List<Tile> finalMap = new List<Tile>();


            for (i = 0; i < Size; i++)
            {
                for (j = 0; j < Size; j++)
                {
                    k = map[i * Size + j];

                    switch (k)
                    {
                        case Tile.DESERT:
                            finalMap.Add(TileFactory.TileFactory_Instance.GetDesert());
                            break;
                        case Tile.PLAIN:
                            finalMap.Add(TileFactory.TileFactory_Instance.GetPlain());
                            break;
                        case Tile.FOREST:
                            finalMap.Add(TileFactory.TileFactory_Instance.GetForest());
                            break;
                        case Tile.MOUNTAIN:
                            finalMap.Add(TileFactory.TileFactory_Instance.GetMountain());
                            break;
                        default: 
                            break;

                    }
                }
            }
                
                return finalMap;
        }
    }

}
