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
        protected unsafe int* tabMap;
        private unsafe int* moves;
        private unsafe double* costs;
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
                if (turnEnded)
                {
                    MovePt = 0;
                }
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
            TurnEnded = false;
            wrapperAlgo = new WrapperAlgo();
        }

        public void Attack(Unit u, int rounds)
        {
            double ptDef = u.DefensePt * (u.HitPt / HIT_PT);
            double ptAtck = AttackPt * (HitPt / HIT_PT);
            double dieProbaAtck = 0.5;

            double forceRatio = ((double)Math.Abs(ptAtck - ptDef) / Math.Max(ptAtck, ptDef));
            if (ptAtck > ptDef)
            {
                dieProbaAtck = dieProbaAtck - forceRatio;
            }
            else
            {
                dieProbaAtck = dieProbaAtck + forceRatio;
            }

            Random random = new Random();
            while (rounds > 0 && HitPt > 0 && u.HitPt > 0)
            {
                double r = random.Next(100);
                if (r < dieProbaAtck * 100)
                {
                    this.HitPt--;
                }
                else
                {
                    u.HitPt--;
                }
                rounds--;
            }
        }

        public unsafe bool CanMove(int x, int y)
        {
            return Moves[x * SizeMap + y] > 1;
        }

        public unsafe bool Move(int x, int y)
        {
            if (CanMove(x, y))
            {
                MovePt = Costs[x * SizeMap + y];
                Position = new Position { X = x, Y = y };
                this.CalculateMoves();
                if (MovePt == 0)
                {
                    this.endTurn();
                }
                return true;
            }
            else
            {
                return false;
            }
                   
        }

        public unsafe void restore(int* tiles)
        {
            WrapperAlgo wrapper = new WrapperAlgo();
            TabMap = tiles;
            Costs = wrapper.costTab(SizeMap);
            Moves = wrapper.mapCreation(SizeMap);
            CalculateMoves();


                
        }

        public abstract void UpdateGamePoints();
            // calcule le point du jeu en fonction de la case où l'unité se situe etc...
            // défini dans les unités de chaque classe

        public abstract void CalculateMoves();

        public void endTurn()
        {
            TurnEnded = true;
        }

        public void newTurn()
        {
            TurnEnded = false;
            MovePt = MOVE_PT;
            CalculateMoves();
        }
    }
}
