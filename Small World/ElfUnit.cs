using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class ElfUnit : Unit
    {
        public ElfUnit()
        {
        }

        //TODO to fill
        public void Move(Position p)
        {
            if (MovePt > 0)
            {
                MovePt--;
            }
        }

        public override void CalculateMoves()
        {

        }

        public override int UpdateGamePoints()
        {
            GamePt = 1;
            return GamePt;
        }

    }
}
