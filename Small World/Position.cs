using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

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
    }
}
