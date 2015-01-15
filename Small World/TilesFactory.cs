using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable()]
    public class TileFactory
    {
        private static TileFactory tileFactory_instance;

        private Desert desert;
        private Plain plain;
        private Forest forest;
        private Mountain mountain;

        /// <summary>
        /// Constructeur de la fabrique de cases
        /// </summary>
        public TileFactory()
        {
            desert = new Desert();
            plain = new Plain();
            forest = new Forest();
            mountain = new Mountain();
        }

        /// <summary>
        /// Getter de l'unique instance de la fabrique
        /// </summary>
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

        /// <summary>
        /// Accesseur de l'instance de la case Desert
        /// </summary>
        /// <returns>L'instance de la case</returns>
        public Desert GetDesert()
        {
            return desert;
        }

        /// <summary>
        /// Accesseur de l'instance de la case Montagne
        /// </summary>
        /// <returns>L'instance de la case</returns>
        public Mountain GetMountain()
        {
            return mountain;
        }

        /// <summary>
        /// Accesseur de l'instance de la case Plaine
        /// </summary>
        /// <returns>L'instance de la case</returns>
        public Plain GetPlain()
        {
            return plain;  
        }

        /// <summary>
        /// Accesseur de l'instance de la case Foret
        /// </summary>
        /// <returns>L'instance de la case</returns>
        public Forest GetForest()
        {
            return forest;
        }
    }
}
