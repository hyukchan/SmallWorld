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

        public override unsafe void CalculateMoves()
        {
            wrapperAlgo.initializeOrcMvt(TabMap, SizeMap, Position.X, Position.Y, Costs, Moves, MovePt);
        }

        public override unsafe int UpdateGamePoints()
        {
            if (TabMap[Position.X * SizeMap + y] == Tile.PLAIN)
            {
                GamePt = BonusPt; 
            }
            else
            {
                GamePt = BonusPt + 1;
            }
            return GamePt;
        }
    }
}
