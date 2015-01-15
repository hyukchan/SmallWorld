using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Wrapper;

namespace Small_World
{

    public interface GameInterface
    {
        string winner();

        void ChangePlayer();

        List<Unit> SelectUnit(int x, int y);

        List<Unit> SelectOpponentUnit(int x, int y);

        bool CanMove(Unit unit, int x, int y);

        int AskToMove(Unit unit, int x, int y);

        void checkEndOfGame();

        bool save();

        bool saveAs(string filename);

        Game load(Game g);

    }

    [Serializable()]
    public class Game : GameInterface, INotifyPropertyChanged
    {

        private int firstPlayer;

        private int currentPlayer;

        private List<Player> playerList;

        private int nbRemainingTurns;

        private bool gameEnded;

        private GameBoard map;

        [field:NonSerialized]
        unsafe private int* tabMap;

        private String saveName;

        /// <summary>
        /// Getter/Setter de l'indice du premier joueur (qui commence à jouer) dans la liste
        /// </summary>
        public int FirstPlayer
        {
            get
            {
                return firstPlayer;
            }
            set
            {
                firstPlayer = value;
            }
        }

        /// <summary>
        /// Getter/Setter de l'indice du joueur courant dans la liste
        /// </summary>
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
            }
        }

        /// <summary>
        /// Getter/Setter de la liste des joueurs
        /// </summary>
        public List<Player> PlayerList
        {
            get
            {
                return playerList;
            }
            set
            {
                playerList = value;
            }
        }

        /// <summary>
        /// Getter/Setter du nombre de tours restants dans la partie
        /// </summary>
        public int NbRemainingTurns
        {
            get
            {
                return nbRemainingTurns;
            }
            set
            {
                nbRemainingTurns = value;
                OnPropertyChanged("RemainingTurns");
            }
        }

        /// <summary>
        /// Getter/Setter du booleen de fin de jeu
        /// </summary>
        public bool GameEnded
        {
            get
            {
                return gameEnded;
            }
            set
            {
                gameEnded = value;
            }
        }

        /// <summary>
        /// Getter/Setter du plateau de jeu
        /// </summary>
        public GameBoard Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }

        /// <summary>
        /// Getter/Setter du tableau représentant le plateau de jeu
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
        /// Getter/Setter du nom de sauvegarde de la partie
        /// </summary>
        public String SaveName
        {
            get
            {
                return saveName;
            }
            set
            {
                saveName = value;
            }
        }

        /// <summary>
        /// Constructeur de la partie
        /// </summary>
        public Game()
        {
            PlayerList = new List<Player>();
            Map = new GameBoard();
        }

        /// <summary>
        /// Constructeur d'une partie
        /// </summary>
        /// <param name="list">La liste des joueurs</param>
        /// <param name="g">Le plateau à recopier</param>
        public Game(List<Player> list, GameBoard g)
        {
            PlayerList = list;
            Map = g;
            SaveName = "";

            // selection random du premier joueur
            Random r = new Random();
            int first = r.Next(PlayerList.Count);
            FirstPlayer = first;
            CurrentPlayer = FirstPlayer;
        }

        /// <summary>
        /// Donne le vainqueur de la partie
        /// </summary>
        /// <returns>Le nom du vainqueur, ou egalite</returns>
        public string winner()
        {
            if (PlayerList[0].Points > PlayerList[1].Points)
            {
                return PlayerList[0].Name;
            }
            else if (PlayerList[0].Points < PlayerList[1].Points)
            { 
                return PlayerList[1].Name;
            }
            else
            {
                string s = "egalite";
                return s;
            }
        }

        /// <summary>
        /// Permet le changement de joueur courant
        /// </summary>
        public void ChangePlayer()
        {
            this.PlayerList[CurrentPlayer].GetGamePoints();

            foreach (Unit u in PlayerList[CurrentPlayer].Units)
            {
                u.endTurn();
            }

            CurrentPlayer = (CurrentPlayer + 1) % PlayerList.Count;
            foreach (Unit u in PlayerList[CurrentPlayer].Units)
            {
                u.newTurn();
            }

            if (CurrentPlayer == FirstPlayer)
            {
                NbRemainingTurns--;
            }
            if (NbRemainingTurns == 0)
            {
                GameEnded = true;
            }

            GameMessages.Instance.addMessage(PlayerList[CurrentPlayer].Name + "'s Turn !\n");
        }

        /// <summary>
        /// Sélectionne les unités du joueur courant à une position donnée
        /// </summary>
        /// <param name="x">L'abscisse de la case selectionnée</param>
        /// <param name="y">L'ordonnée de la case selectionnée</param>
        /// <returns>La liste des unités sur la case</returns>
        public List<Unit> SelectUnit(int x, int y)
        {
            int i;
            List<Unit> unitList = new List<Unit>();
            Position pos = new Position();
            pos.X = x;
            pos.Y = y;

            for (i = 0; i < PlayerList[CurrentPlayer].Units.Count; i++)
            {
                if(PlayerList[CurrentPlayer].Units[i].Position.Equals(pos))
                {
                    unitList.Add(PlayerList[CurrentPlayer].Units[i]);
                }
            }

            return unitList;
        }

        /// <summary>
        /// Sélectionne les unités du joueur adverse à une position donnée
        /// </summary>
        /// <param name="x">L'abscisse de la case selectionnée</param>
        /// <param name="y">L'ordonnée de la case selectionnée</param>
        /// <returns>La liste des unités sur la case</returns>
        public List<Unit> SelectOpponentUnit(int x, int y)
        {
            int i;
            List<Unit> unitList = new List<Unit>();

            Position pos = new Position {X = x, Y = y };
            int opponent = (CurrentPlayer + 1) % PlayerList.Count;

            for (i = 0; i < PlayerList[opponent].Units.Count; i++)
            {
                if (PlayerList[opponent].Units[i].Position.Equals(pos))
                {
                    unitList.Add(PlayerList[opponent].Units[i]);
                }
            }
            return unitList;
        }

        /// <summary>
        /// Gere le cas des déplacements des nains de montagne en montagne
        /// </summary>
        /// <param name="u">L'unité à déplacer</param>
        /// <param name="x">L'abscisse de la case selectionnée</param>
        /// <param name="y">L'ordonnée de la case selectionnée</param>
        /// <returns>La liste des unités sur la case</returns>
        public bool CanMove(Unit u, int x, int y)
        {
            if (u.Position.X == x && u.Position.Y == y) 
            {
                return false; 
            }
            if((u.GetType() == new DwarfUnit().GetType()) && (SelectOpponentUnit(x,y).Count != 0))
            {
                if (u.Position.Y % 2 == 0 && (Math.Abs(y - u.Position.Y) > 1 || (x > u.Position.X || x < u.Position.X -1)))
                {
                    return false;
                }
                if (u.Position.Y % 2 == 1 && (Math.Abs(y - u.Position.Y) > 1 || (x < u.Position.X || x > u.Position.X + 1)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Demande un déplacement et gère les attaques si besoin
        /// </summary>
        /// <param name="u">L'unité demandeuse du déplacement</param>
        /// <param name="x">L'abscisse de la case visée</param>
        /// <param name="y">L'ordonée de la case visée</param>
        /// <returns>1 si un déplacement a eu lieu, 2 si un combat a eu lieu</returns>
        public unsafe int AskToMove(Unit u, int x, int y)
        {
            if (CanMove(u, x, y))
            {
                List<Unit> listUnits = SelectOpponentUnit(x,y);
                if (listUnits.Count == 0)
                {
                    u.Move(x, y);
                    u.PossibleMoves(u.MovePt, u.Moves, u.Position.X, u.Position.Y, u.SizeMap, u.TabMap);
                    PlayerList[CurrentPlayer].GetGamePoints();
                    return 1;
                }
                else
                {
                    if (u.CanMove(x, y))
                    {
                        //if ((u.GetType() == new ElfUnit().GetType() && TabMap[y * u.SizeMap + x] == Tile.FOREST) || ((u.GetType() == new OrcUnit().GetType() || u.GetType() == new DwarfUnit().GetType()) && TabMap[y*u.SizeMap + x] == Tile.PLAIN))
                        //{
                        //    u.MovePt -= Unit.MOVE_PT / 2;
                        //}
                        //else
                        //{
                        //    u.MovePt -= Unit.MOVE_PT;
                        //}
                        Unit best = listUnits[0];
                        foreach (Unit unit in listUnits)
                        {
                            if ((unit.DefensePt + unit.HitPt) > (best.DefensePt + best.HitPt))
                            {
                                best = unit;
                            }
                        }

                        int rounds = 0;
                        Random r = new Random();
                        while (rounds < 3)
                        {
                            rounds = r.Next(Math.Max(u.HitPt, best.HitPt) + 2);
                        }
                        int attackUnitHpLost = 0;
                        int defenseUnitHpLost = 0;
                        int attackUnitHpBefore = u.HitPt;
                        int defenseUnitHpBefore = best.HitPt;
                        u.Attack(best, rounds);
                        attackUnitHpLost = attackUnitHpBefore - u.HitPt;
                        defenseUnitHpLost = defenseUnitHpBefore - best.HitPt;

                        if (attackUnitHpLost > 0)
                        {
                            GameMessages.Instance.addMessage(PlayerList[CurrentPlayer].Name + "'s unit has lost "+attackUnitHpLost+" hp\n");
                        }

                        if (defenseUnitHpLost > 0)
                        {
                            GameMessages.Instance.addMessage(PlayerList[(CurrentPlayer + 1)%2].Name + "'s unit has lost " + defenseUnitHpLost + " hp\n");
                        }

                        // unite defensive meurt
                        if (best.HitPt == 0)
                        {
                            GameMessages.Instance.addMessage(PlayerList[CurrentPlayer].Name + " killed an unit !\n");
                            if (u.GetType() == new OrcUnit().GetType())
                            {
                                ((OrcUnit)u).BonusPt++;
                                

                            }
                            PlayerList[(CurrentPlayer + 1) % PlayerList.Count].Units.Remove(best);
                            OnPropertyChanged("Units");
                            PlayerList[(CurrentPlayer + 1) % PlayerList.Count].GetGamePoints();
                            PlayerList[CurrentPlayer].GetGamePoints();

                            if (SelectOpponentUnit(x, y).Count == 0)
                            {
                                u.Move(x, y);
                                u.PossibleMoves(u.MovePt, u.Moves, u.Position.X, u.Position.Y, u.SizeMap, u.TabMap);
                                checkEndOfGame();
                                return 2;
                            }
                        }

                        //unite attaquante meurt
                        if (u.HitPt == 0)
                        {
                            GameMessages.Instance.addMessage(PlayerList[CurrentPlayer].Name + "'s unit died trying to attack !\n");
                            if (best.GetType() == new OrcUnit().GetType())
                            {
                                ((OrcUnit)best).BonusPt++;
                            }
                            PlayerList[CurrentPlayer].Units.Remove(u);
                            OnPropertyChanged("Units");
                            PlayerList[CurrentPlayer].GetGamePoints();
                            PlayerList[(CurrentPlayer + 1) % PlayerList.Count].GetGamePoints();
                            checkEndOfGame();
                        }
                        if (((u.GetType() == new DwarfUnit().GetType() || u.GetType() == new OrcUnit().GetType()) && TabMap[y * u.SizeMap + x] == Tile.PLAIN) || (u.GetType() == new ElfUnit().GetType() && TabMap[y * u.SizeMap + x] == Tile.FOREST))
                        {
                            u.MovePt -= Unit.MOVE_PT / 2;
                            u.PossibleMoves(u.MovePt, u.Moves, u.Position.X, u.Position.Y, u.SizeMap, u.TabMap);
                        }
                        else
                        {
                            u.MovePt -= Unit.MOVE_PT;
                            u.PossibleMoves(u.MovePt, u.Moves, u.Position.X, u.Position.Y, u.SizeMap, u.TabMap);
                        }
                    }
                }
            }
            else
            {
                GameMessages.Instance.addMessage("The unit can't move !\n");
            }
            return 1;
        }

        /// <summary>
        /// Termine le tour des unités du joueur courant
        /// </summary>
        public void EndTurn()
        {
            foreach (Unit u in PlayerList[CurrentPlayer].Units)
            {
                u.endTurn();
            }
            CurrentPlayer = (CurrentPlayer + 1) % PlayerList.Count;
        }

        /// <summary>
        /// Verifie si la partie est finie
        /// </summary>
        public void checkEndOfGame()
        {
            bool end = false;
            foreach (Player p in PlayerList)
            {
                end = end || (p.Units.Count == 0);
            }
            GameEnded = end;
        }

        /// <summary>
        /// Sauvegarde une partie déjà sauvegardée
        /// </summary>
        /// <returns></returns>
        public bool save()
        {
            if (SaveName != "")
            {
                this.saveAs(SaveName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sauvegarde une partie pour la première fois
        /// </summary>
        /// <param name="filename">Le nom de la sauvegarde</param>
        /// <returns></returns>
        public bool saveAs(string filename)
        {
            using (FileStream file = File.Create(filename + ".sw"))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(file, this);
            }

            return true;
        }

        /// <summary>
        /// Charge une partie déjà commencée
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public unsafe Game load(Game g)
        {
            WrapperAlgo w = new WrapperAlgo();
            this.Map = new GameBoard();
            this.FirstPlayer = g.FirstPlayer;
            this.CurrentPlayer = g.CurrentPlayer;
            this.Map = g.Map;
            this.NbRemainingTurns = g.NbRemainingTurns;
            this.PlayerList = g.PlayerList;
            this.SaveName = g.SaveName;
            this.TabMap = w.mapCreation(g.Map.Size);
            this.GameEnded = g.GameEnded;
            this.PlayerList[0].Units = g.PlayerList[0].Units;
            this.PlayerList[1].Units = g.PlayerList[1].Units;



            int i, j, l;
            //for (i = 0; i < g.Map.Size; i++)
            //{
            //    for (j = 0; j < g.Map.Size; j++)
            //    {
            //        this.TabMap[i * g.Map.Size + j] = 0;
            //    }
            //}

            Tile k;
            for (i = 0; i < g.Map.Size; i++)
            {
                for (j = 0; j < g.Map.Size; j++)
                {
                    k = this.Map.ListTiles[i * g.Map.Size + j];
                    if (k.GetType() == new Mountain().GetType())
                    {
                        l = Tile.MOUNTAIN;
                    }
                    else if (k.GetType() == new Forest().GetType())
                    {
                        l = Tile.FOREST;
                    }
                    else if (k.GetType() == new Desert().GetType())
                    {
                        l = Tile.DESERT;
                    }
                    else
                    {
                        l = Tile.PLAIN;
                    }
                    switch (l)
                    {
                        case Tile.DESERT:
                            this.TabMap[i * g.Map.Size + j] = Tile.DESERT;
                            break;
                        case Tile.PLAIN:
                            this.TabMap[i * g.Map.Size + j] = Tile.PLAIN;
                            break;
                        case Tile.FOREST:
                            this.TabMap[i * g.Map.Size + j] = Tile.FOREST;
                            break;
                        case Tile.MOUNTAIN:
                            this.TabMap[i * g.Map.Size + j] = Tile.MOUNTAIN;
                            break;
                        default:
                            break;

                    }
                }
            }

            foreach (Player player in PlayerList)
            {
                foreach (Unit u in player.Units)
                {
                    u.restore(this.TabMap);
                }
            }
            return this;
        }

        #region INotifyPropertyChanged

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
