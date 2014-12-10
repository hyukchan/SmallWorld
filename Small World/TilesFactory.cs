using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class TileFactory
    {

        private Desert desert;
        private Plain plain;
        private Forest forest;
        private Mountain mountain;
        public TileFactory()
        {
            //TODOSW instancier chaque attribut pour poids mouche
        }
    
        public Desert Desert
        {
            get
            {
                //TODOSW return desert
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Mountain Mountain
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Plain Plain
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Forest Forest
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Tile GetTile()
        {
            throw new System.NotImplementedException();
        }

        public Desert GetDesert()
        {
            throw new System.NotImplementedException();
        }

        public Mountain GetMountain()
        {
            throw new System.NotImplementedException();
        }

        public Plain GetPlain()
        {
            throw new System.NotImplementedException();
        }

        public Forest GetForest()
        {
            throw new System.NotImplementedException();
        }
    }
}
