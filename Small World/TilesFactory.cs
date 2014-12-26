using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class TileFactory
    {
        private static TileFactory tileFactory_instance;

        private Desert desert;
        private Plain plain;
        private Forest forest;
        private Mountain mountain;

        public TileFactory()
        {
            desert = new Desert();
            plain = new Plain();
            forest = new Forest();
            mountain = new Mountain();
        }

        public static TileFactory TileFactory_Instance
        {
            get
            {
                if (tileFactory_instance == null) //Cette classe est un singleton
                {
                    tileFactory_instance = new TileFactory();
                }
                return tileFactory_instance;
            }
        }

        public Tile GetTile()
        {
            // TODOSW utile de le garder ?
            throw new System.NotImplementedException();
        }

        public Desert GetDesert()
        {
            return desert;
        }

        public Mountain GetMountain()
        {
            return mountain;
        }

        public Plain GetPlain()
        {
            return plain;  
        }

        public Forest GetForest()
        {
            return forest;
        }
    }
}
