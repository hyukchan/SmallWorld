using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable]
    public class ElfUnit : Unit
    {
        /// <summary>
        /// Crée une unité elf
        /// </summary>
        public ElfUnit()
        {
        }

        /// <summary>
        /// Indique si la case visée est accessible pour un elf
        /// </summary>
        /// <param name="x"> l'abscisse de l'unité</param>
        /// <param name="y">l'ordonnée de l'unité</param>
        /// <returns>True si la case est accessible, false sinon</returns>
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
                    return ((x == _x + 1 && y == _y) || ((x == _x - 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT / 2;
                }
                else
                {
                    return ((x == _x - 1 && y == _y) || ((x == _x + 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT / 2;
                }
            }
            else
            {
                if (_y % 2 == 0)
                {
                    return ((x == _x + 1 && y == _y) || ((x == _x - 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
                else
                {
                    return ((x == _x - 1 && y == _y) || ((x == _x + 1 || x == _x) && Math.Abs(y - _y) <= 1)) && this.MovePt >= Unit.MOVE_PT;
                }
            }
        }


        public override unsafe void PossibleMoves(double movePt, int* moves, int x, int y, int size, int* map)
        {
            Moves = wrapperAlgo.possibleMoves(movePt, moves, x, y, size, map, People.ELF);
        }


        /// <summary>
        /// Calcule les points de l'unité courante selon sa position
        /// </summary>
        public override void UpdateGamePoints()
        {
            GamePt = 1;
        }

        /// <summary>
        /// Donne l'adresse de l'image d'une unité dont le tour n'est pas fini
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/elf.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'adresse de l'image d'une unité dont le tour est fini
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri GetUnactiveUnitIcon()
        {
            return new Uri("./textures/unactive_elf.png", UriKind.Relative);
        }
    }
}
