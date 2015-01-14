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
        public override unsafe bool CanMove(int x, int y)
        {
            int _x = Position.X;
            int _y = Position.Y;
            if (tabMap[y * SizeMap + x] == Tile.DESERT)
            {
                return false;
            }
            else if (tabMap[y * SizeMap + x] == Tile.FOREST)
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

        public override unsafe void CalculateMoves()
        {
            wrapperAlgo.initializeElfMvt(TabMap, SizeMap, Position.X, Position.Y, Costs, Moves, MovePt);
        }

        public override void UpdateGamePoints()
        {
            GamePt = 1;
        }

        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/elf.png", UriKind.Relative);
        }

    }
}
