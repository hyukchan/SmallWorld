using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class DwarfUnit : Unit
    {
        public DwarfUnit()
        {
            
        }

        //TODOSW to fill
        public void Move(Position p)
        {
            if (MovePt > 0)
            {
                MovePt--;
            }
        }

        public override unsafe void CalculateMoves()
        {
            wrapperAlgo.initializeDwarfMvt(TabMap, SizeMap, Position.X, Position.Y, Costs, Moves, MovePt);
        }

        public override unsafe void UpdateGamePoints()
        {
            if (TabMap[Position.X * SizeMap + Position.Y] == Tile.PLAIN)
            {
                GamePt = 0;
            }
            else
            {
                GamePt = 1;
            }
        }

        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/dwarf.png", UriKind.Relative);
        }

    }
}
