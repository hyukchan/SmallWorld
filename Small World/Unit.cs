using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{
    [Serializable]
    public abstract class Unit
    {
        const int ATTACK_PT = 2;
        const int DEFENSE_PT = 1;
        const int HIT_PT = 5;
        public const double MOVE_PT = 1;

       
        private Position position;
        protected unsafe int* tabMap;
        private unsafe int* moves;
        private int sizeMap;

        private bool turnEnded;

        private int hitPt;
        private double movePt;
        private int gamePt;

        protected WrapperAlgo wrapperAlgo;

        /// <summary>
        /// Getter / Setter de la position de l'unité
        /// </summary>
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

        /// <summary>
        /// Getter / Setter du tableau représentant la carte de l'unité
        /// </summary>
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

        /// <summary>
        /// Getter / Setter des mouvements possibles de l'unité
        /// </summary>
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


        /// <summary>
        /// Getter/Setter de la taille de la carte sur laquelle est l'unité
        /// </summary>
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

        /// <summary>
        /// Getter/Setter de l'état de l'unité courante
        /// </summary>
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
                    this.MovePt = 0;
                }
            }
        }

        /// <summary>
        /// Retourne les points d'attaque de l'unité
        /// </summary>
        public int AttackPt
        {
            get { return ATTACK_PT; }
        }

        /// <summary>
        /// Retourne les points de défense de l'unité
        /// </summary>
        public int DefensePt
        {
            get { return DEFENSE_PT; }
        }

        /// <summary>
        /// Retourne les points de vie de l'unité
        /// </summary>
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

        /// <summary>
        /// Getter/Setter des points de jeu de l'unité
        /// </summary>
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

        /// <summary>
        /// Getter/Setter des points de mouvement de l'unité
        /// </summary>
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

        /// <summary>
        /// Constructeur par défaut d'une unité
        /// </summary>
        public Unit()
        {
            HitPt = HIT_PT;
            MovePt = MOVE_PT;
            TurnEnded = false;
            wrapperAlgo = new WrapperAlgo();
        }

        /// <summary>
        /// Simule un combat entre 2 unités
        /// </summary>
        /// <param name="u"> L'unité à attaquer</param>
        /// <param name="rounds"> Le nombre de tours de combat</param>
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

        /// <summary>
        /// Effectue le déplacement de l'unité
        /// </summary>
        /// <param name="x">L'abscisse à atteindre</param>
        /// <param name="y">L'ordonnée à atteindre</param>
        /// <returns>True si le déplacement a réussi, false sinon</returns>
        public unsafe bool Move(int x, int y)
        {
            if (CanMove(x, y))
            {
                if ((this.GetType() == new DwarfUnit().GetType() || this.GetType() == new OrcUnit().GetType()) && TabMap[y * SizeMap + x] == Tile.PLAIN)
                {
                    this.MovePt = this.MovePt - MOVE_PT / 2;
                }
                else if (this.GetType() == new ElfUnit().GetType() && TabMap[y * SizeMap + x] == Tile.FOREST)
                {
                    this.MovePt = this.MovePt - MOVE_PT / 2;
                }
                else
                {
                    this.MovePt = this.MovePt - MOVE_PT;
                }

                Position = new Position { X = x, Y = y };
                
                if (this.MovePt == 0)
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
         /// <summary>
         /// Restaure l'unité suite à un chargement de partie
         /// </summary>
         /// <param name="tiles">La carte sur laquelle placer les unités</param>
        public unsafe void restore(int* tiles)
        {
            WrapperAlgo wrapper = new WrapperAlgo();
            TabMap = tiles;
            Moves = wrapper.mapCreation(SizeMap);      
        }
        
       /// <summary>
       /// Calcule les déplacements possibles de l'unité
       /// </summary>
       /// <param name="movePt">points de mouvements disponibles</param>
       /// <param name="moves">tableau des déplacements</param>
       /// <param name="x">Abscisse de l'unité</param>
       /// <param name="y">Ordonnée de l'unité</param>
       /// <param name="size">Taille du plateai</param>
       /// <param name="map">plateau</param>
       /// <param name="peuple">peuple courant</param>
       /// <returns></returns>
       public abstract unsafe void PossibleMoves(double movePt, int* moves, int x, int y, int size, int* map);

        /// <summary>
        /// Fonction abstraite pour indiquer si le déplacement demandé est possible
        /// </summary>
        /// <param name="x">L'abscisse à atteindre</param>
        /// <param name="y">L'ordonnée à atteindre</param>
        /// <returns>True si l'opération s'est bien passée, false sinon</returns>
        public abstract bool CanMove(int x, int y);

        /// <summary>
        /// Fonction absraite pour calculer les points de l'unité
        /// </summary>
        public abstract void UpdateGamePoints();

        /// <summary>
        /// Fonction abstraite pour rendre l'URI de l'unité dont le tour est en cours
        /// </summary>
        /// <returns></returns>
        public abstract Uri GetUnitIcon();

        /// <summary>
        /// Fonction abstraite pour rendre l'URI de l'unité dont le tour est en cours
        /// </summary>
        /// <returns></returns>
        public abstract Uri GetUnactiveUnitIcon();

        /// <summary>
        /// Termine le tour de l'unité
        /// </summary>
        public void endTurn()
        {
            TurnEnded = true;
        }

        /// <summary>
        /// Initialise les mouvements de autorise l'unité à jouer
        /// </summary>
        public unsafe void newTurn()
        {
            TurnEnded = false;
            MovePt = MOVE_PT;
            if(this.GetType() == new DwarfUnit().GetType()){
            Moves = wrapperAlgo.possibleMoves(MovePt,Moves,Position.X,Position.Y,SizeMap,TabMap,People.DWARF);
            }
            else if (this.GetType() == new OrcUnit().GetType())
            {
                Moves = wrapperAlgo.possibleMoves(MovePt, Moves, Position.X, Position.Y, SizeMap, TabMap, People.ORC);
            }
            else
            {
                Moves = wrapperAlgo.possibleMoves(MovePt, Moves, Position.X, Position.Y, SizeMap, TabMap, People.ELF);
            }
        }

    }

    
}
