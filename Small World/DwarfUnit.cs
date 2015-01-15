using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    [Serializable]
    public class DwarfUnit : Unit
    {
        /// <summary>
        /// Constructeur d'une unité naine
        /// </summary>
        public DwarfUnit()
        {
            
        }

        /// <summary>
        /// Indique si la case visée est accessible pour un nain
        /// </summary>
        /// <param name="x"> l'abscisse de l'unité</param>
        /// <param name="y">l'ordonnée de l'unité</param>
        /// <returns>True si la case est accessible, false sinon</returns>
        public override unsafe bool CanMove(int x, int y)
        {
            int _x = Position.X;
            int _y = Position.Y;
            if (tabMap[y * SizeMap + x] == Tile.PLAIN)
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
            else if (tabMap[_y * SizeMap + _x] == Tile.MOUNTAIN && tabMap[y * SizeMap + x] == Tile.MOUNTAIN && this.MovePt >= Unit.MOVE_PT)
            {
                return true;
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

        /// <summary>
        /// Indique le nombre de points de l'unité selon la case
        /// </summary>
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

        /// <summary>
        /// Calcule les déplacements possibles de l'unité naine
        /// </summary>
        /// <param name="movePt">points de mouvements disponibles</param>
        /// <param name="moves">tableau des déplacements</param>
        /// <param name="x">Abscisse de l'unité</param>
        /// <param name="y">Ordonnée de l'unité</param>
        /// <param name="size">Taille du plateai</param>
        /// <param name="map">plateau</param>
        public override unsafe void PossibleMoves(double movePt, int* moves, int x, int y, int size, int* map)
        {
            Moves = wrapperAlgo.possibleMoves(movePt, moves, x, y, size, map, People.DWARF);
        }

        /// <summary>
        /// Donne l'image d'une unité dont le tour n'est pas fini
        /// </summary>
        /// <returns>L'uri de l'image</returns>
        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/dwarf.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'image d'une unité dont le tour est fini
        /// </summary>
        /// <returns>L'uri de l'image</returns>
        public override Uri GetUnactiveUnitIcon()
        {
            return new Uri("./textures/unactive_dwarf.png", UriKind.Relative);
        }
    }
}
