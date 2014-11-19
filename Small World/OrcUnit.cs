using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class OrcUnit : Unit
    {
        public OrcUnit()
        {
        }

        public int BonusPt
        {
            get;
            set;
        }

        //TODO to fill
        public void Move(Position p)
        {
            if (MovePt > 0)
            {
                MovePt--;
            }
        }
    }
}
