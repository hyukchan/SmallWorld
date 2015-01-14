using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    /// <summary>
    /// Structure pour les positions des unités et des cases
    /// </summary>
    [Serializable]
    public struct Position
    {

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// Redéfinition de la fonction Equals pour comparer deux positions
        /// </summary>
        /// <param name="p">La position avec laquelle comparer</param>
        /// <returns></returns>
        public bool Equals(Position p)
        {
            bool res = this.X == p.X;
            if (res)
            {
                res = this.Y == p.Y;
            }
            return res;
        }
    }
}
