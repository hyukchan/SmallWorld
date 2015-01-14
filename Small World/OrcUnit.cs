using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    
    public class OrcUnit : Unit
    {
        /// <summary>
        /// Constructeur d'une unité orc
        /// </summary>
        public OrcUnit()
        {
        }

        /// <summary>
        /// Getter/Setter du Bonus des orcs
        /// </summary>
        public int BonusPt
        {
            get;
            set;
        }

        /// <summary>
        /// Calcule les points de l'unité courante
        /// </summary>
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

        /// <summary>
        /// Indique si la case visée est accessible pour un orc
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
        /// Donne l'adresse de l'image d'une unité dont le tour n'est pas fini
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri GetUnitIcon()
        {
            return new Uri("./textures/orc.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'adresse de l'image d'une unité dont le tour est fini
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri GetUnactiveUnitIcon()
        {
            return new Uri("./textures/unactive_orc.png", UriKind.Relative);
        }
    }
}
