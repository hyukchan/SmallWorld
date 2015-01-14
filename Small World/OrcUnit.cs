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

        public override unsafe void UpdateGamePoints()
        {
            if (TabMap[Position.X * SizeMap + Position.Y] == Tile.PLAIN)
            {
                GamePt = BonusPt;
            }
            else
            {
                GamePt = BonusPt + 1;
            }
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
            else
            {
                if (_y % 2 == 0)
                {
                    //this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x + 1 && y == _y) || ((x == _x - 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
                else
                {
                    //this.MovePt = this.MovePt - Unit.MOVE_PT / 2;
                    return ((x == _x - 1 && y == _y) || ((x == _x + 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
            }
        }

        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/orc.png", UriKind.Relative);
        }

        public override Uri GetUnactiveUnitIcon()
        {
            return new Uri("./textures/unactive_orc.png", UriKind.Relative);
        }
    }
}
