using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public abstract class Unit
    {
        const int ATTACK_PT = 2;
        const int DEFENSE_PT = 1;
        const int HIT_PT = 5;
        const int MOVE_PT = 1;

        private int gamePt;
        private Position position;

        public Unit()
        {
            HitPt = HIT_PT;
            MovePt = MOVE_PT;

            Position p = new Position { X = 2, Y = 3 };
            var p2 = p;

            Position a = new Position { X = 2, Y = 3 };
            Position b = a;
            
        }

        public int AttackPt
        {
            get { return ATTACK_PT; }
        }

        public int DefensePt
        {
            get { return DEFENSE_PT; }
        }

        public int HitPt
        {
            get;
            set;
        }

        public int MovePt
        {
            get;
            set;
        }

        public Position Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public int GamePt
        {
            get
            {
                this.UpdateGamePoints();
                return gamePt;
            }
            set
            {
                gamePt = value;
            }
        }

        public void Attack(Unit u)
        {
            double ptDef = u.DefensePt * (u.HitPt / HIT_PT);
            double ptAtck = AttackPt * (HitPt / HIT_PT);
            double forceRatio = 1 - (ptAtck / ptDef);
            double dieProbaAtckBasic = ptAtck / ptDef;
            double dieProbaAtckFinal = dieProbaAtckBasic + dieProbaAtckBasic * forceRatio;

        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public int UpdateGamePoints()
        {
            //TODOSMALLWORLD calculer le point du jeu en fonction de la case où l'unité se situe etc...
            return gamePt;
        }
    }
}
