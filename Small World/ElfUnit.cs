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

        public override unsafe void CalculateMoves()
        {
            wrapperAlgo.initializeElfMvt(TabMap, SizeMap, Position.X, Position.Y, Costs, Moves, MovePt);
        }

        public override void UpdateGamePoints()
        {
            GamePt = 1;
        }

    }
}
