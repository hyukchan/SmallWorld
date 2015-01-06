using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{
    public abstract class Unit
    {
        const int ATTACK_PT = 2;
        const int DEFENSE_PT = 1;
        const int HIT_PT = 5;
        const int MOVE_PT = 1;

       
        private Position position;
        protected int* tabMap;
        private int* moves;
        private double* costs;
        private int sizeMap;

        private bool turnEnded;

        private int attackPt;
        private int defensePt;
        private int hitPt;
        private double movePt;
        private int gamePt;

        protected WrapperAlgo wrapperAlgo;

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

        public unsafe int* TabMap
        {
            get
            {
                return tabMap;
            }
            set
            {
                tabMap = value;
            }
        }

        public unsafe int* Moves
        {
            get
            {
                return moves;
            }
            set
            {
                moves = value;
            }
        }

        public unsafe double* Costs
        {
            get
            {
                return costs;
            }
            set
            {
                costs = value;
            }
        }

        public int SizeMap
        {
            get
            {
                return sizeMap;
            }
            set
            {
                sizeMap = value;
            }
        }

        public bool TurnEnded
        {
            get
            {
                return turnEnded;
            }
            set
            {
                turnEnded = value;
            }
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
            get
            {
                return hitPt;
            }
            set
            {
                hitPt = value;
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

        public double MovePt
        {
            get
            {
                return movePt;
            }
            set
            {
                movePt = value;
            }
        }

        public Unit()
        {
            HitPt = HIT_PT;
            MovePt = MOVE_PT;

            Position p = new Position { X = 2, Y = 3 };
            var p2 = p;

            Position a = new Position { X = 2, Y = 3 };
            Position b = a;
            
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

        public void endTurn()
        {
            throw new System.NotImplementedException();
        }

        public void newTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}
