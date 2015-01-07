using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
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
