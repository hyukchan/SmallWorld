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

        
        public override unsafe bool CanMove(int x, int y)
        {
            int _x = Position.X;
            int _y = Position.Y;
            if (tabMap[y * SizeMap + x] == Tile.PLAIN)
            {
                if (_y % 2 == 0)
                {
                    //this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x + 1 && y == _y) || ((x == _x - 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT / 2;
                }
                else
                {
                    //this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x - 1 && y == _y) || ((x == _x + 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT / 2;
                }
            }
            else if (tabMap[_y * SizeMap + _x] == Tile.MOUNTAIN && tabMap[y * SizeMap + x] == Tile.MOUNTAIN && this.MovePt >= Unit.MOVE_PT)
            {
               // this.MovePt = this.MovePt - Unit.MOVE_PT;
                return true;
            }
            else
            {
                if (_y % 2 == 0)
                {
                   // this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x + 1 && y == _y) || ((x == _x - 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
                else
                {
                   // this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x - 1 && y == _y) || ((x == _x + 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
            }
        }
        
        

        public override unsafe void CalculateMoves()
        {
            wrapperAlgo.initializeDwarfMvt(TabMap, SizeMap, Position.X, Position.Y, Costs, Moves, MovePt);
        }

        public override unsafe void UpdateGamePoints()
        {
            if (TabMap[Position.Y * SizeMap + Position.X] == Tile.PLAIN)
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
