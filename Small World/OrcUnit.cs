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
            if (tabMap[y * SizeMap + x] == Tile.DESERT)
            {
                return false;
            }
            else if (tabMap[y * SizeMap + x] == Tile.PLAIN)
            {
                if (_y % 2 == 0)
                {
                    return (x - _x) <= 1 && Math.Abs(y - _y) <= 1 && MovePt >= Unit.MOVE_PT / 2;
                }
                else
                {
                    return (x - _x) <= 0 && (x - _x) >= -1 && Math.Abs(y - _y) <= 1 && MovePt >= Unit.MOVE_PT / 2;
                }
            }
            else
            {
                if (_y % 2 == 0)
                {
                    return (x - _x) <= 1 && Math.Abs(y - _y) <= 1 && MovePt >= Unit.MOVE_PT;
                }
                else
                {
                    return (x - _x) <= 0 && (x - _x) >= -1 && Math.Abs(y - _y) <= 1 && MovePt >= Unit.MOVE_PT;
                }
            }
        }

        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/orc.png", UriKind.Relative);
        }

    }
}
