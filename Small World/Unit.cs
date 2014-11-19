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
        public Unit()
        {
            HitPt = HIT_PT;
            MovePt = MOVE_PT;

            Position p = new Position(2, 3);
            var p2 = p;

            Position a = new Position(2,3);
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
                throw new System.NotImplementedException();
            }
            set
            {
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
    }
}
