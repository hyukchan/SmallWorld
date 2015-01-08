using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Wrapper;

namespace Small_World
{

    public interface GameInterface
    {
        Player winner();

        void ChangePlayer();

        List<Unit> SelectUnit(int x, int y);

        List<Unit> SelectOpponentUnit(int x, int y);

        bool CanMove(Unit unit, int x, int y);

        int AskToMove(Unit unit, int x, int y);

        void checkEndOfGame();

        bool save();

        bool saveAs(string filename);

        Game load(string filename);

        //TODOSW restaurer ?
    }

    public class Game : GameInterface
    {

        private int firstPlayer;

        private int currentPlayer;

        private List<Player> playerList;

        private int nbRemainingTurns;

        private bool gameEnded;

        private GameBoard map;

        unsafe private int* tabMap;

        private String saveName;

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

        public int NbRemainingTurns
        {
            get
            {
                return nbRemainingTurns;
            }
            set
            {
                nbRemainingTurns = value;
            }
        }

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

        public Game()
        {
            PlayerList = new List<Player>();
            Map = new GameBoard();
        }

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

        public Player winner()
        {
            if (PlayerList[0].Points > PlayerList[1].Points)
            {
                return PlayerList[0];
            }
            else 
            { 
                return PlayerList[1];
            }
        }

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
        }

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

        public bool CanMove(Unit u, int x, int y)
        {
            //nains peuvent se déplacer de montagne en montagne
            if((u.GetType() == new DwarfUnit().GetType()) && (SelectOpponentUnit(x,y).Count != 0) && (Math.Abs(x - u.Position.X) > 1) && (Math.Abs(y-u.Position.Y) > 1 ))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public int AskToMove(Unit u, int x, int y)
        {
            if (CanMove(u, x, y))
            {
                List<Unit> listUnits = SelectOpponentUnit(x,y);
                if (listUnits.Count == 0)
                {
                    u.Move(x, y);
                    return 1;
                }
                else
                {
                    Unit best = listUnits[0];
                    foreach (Unit unit in listUnits)
                    {
                        if ((unit.DefensePt + unit.HitPt) > (best.DefensePt + best.HitPt))
                        {
                            best = u;
                        }
                    }

                    int rounds = 0;
                    Random r = new Random();
                    while (rounds < 3)
                    {
                        rounds = r.Next(Math.Max(u.HitPt, best.HitPt) + 2);
                    }
                    u.Attack(best, rounds);

                    // unite defensive meurt
                    if (best.HitPt == 0)
                    {
                        if (u.GetType() == new OrcUnit().GetType())
                        {
                            ((OrcUnit)u).BonusPt++;
                        }
                        PlayerList[(CurrentPlayer + 1) % PlayerList.Count].Units.Remove(best);
                        PlayerList[(CurrentPlayer + 1) % PlayerList.Count].GetGamePoints();
                        PlayerList[CurrentPlayer].GetGamePoints();

                        if (SelectOpponentUnit(x, y).Count == 0)
                        {
                            u.Move(x, y);
                            checkEndOfGame();
                            return 2;
                        }
                    }

                    //unite attaquante meurt
                    if (u.HitPt == 0)
                    {
                        if (best.GetType() == new OrcUnit().GetType())
                        {
                            ((OrcUnit)best).BonusPt++;
                        }
                        PlayerList[CurrentPlayer].Units.Remove(u);
                        PlayerList[CurrentPlayer].GetGamePoints();
                        PlayerList[(CurrentPlayer + 1) % PlayerList.Count].GetGamePoints();
                        checkEndOfGame();
                    }
                }
            }
            else
            {
                
            }
            return 1;
        }

        public void EndTurn()
        {
            foreach (Unit u in PlayerList[CurrentPlayer].Units)
            {
                u.endTurn();
            }
            CurrentPlayer = (CurrentPlayer + 1) % PlayerList.Count;
        }

        public void checkEndOfGame()
        {
            bool end = false;
            foreach (Player p in PlayerList)
            {
                end = end || (p.Units.Count == 0);
            }
            GameEnded = end;
        }

        public unsafe void restore()
        {
            WrapperAlgo wrapper = new WrapperAlgo();

            int mapSize = this.Map.Size;
            int* tiles = wrapper.createGameBoard(mapSize);
            int i, j;

            for (i = 0; i < mapSize; i++)
            {
                for (j = 0; j < mapSize; j++)
                {
                    if (Map.ListTiles[i * mapSize + j].GetType() == new Desert().GetType())
                    {
                        tiles[i * mapSize + j] = Tile.DESERT;
                    }
                    if (Map.ListTiles[i * mapSize + j].GetType() == new Mountain().GetType())
                    {
                        tiles[i * mapSize + j] = Tile.MOUNTAIN;
                    }
                    if (Map.ListTiles[i * mapSize + j].GetType() == new Plain().GetType())
                    {
                        tiles[i * mapSize + j] = Tile.PLAIN;
                    }
                    if (Map.ListTiles[i * mapSize + j].GetType() == new Forest().GetType())
                    {
                        tiles[i * mapSize + j] = Tile.FOREST;
                    }
                }
            }

            foreach (Player player in PlayerList)
            {
                foreach (Unit u in player.Units)
                {
                    u.restore(tiles);
                }
            }
        }

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

        public bool saveAs(string filename)
        {
            FileStream file = File.Create(filename);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(file, this);
            file.Close();

            return true;
        }

        public Game load(string filename)
        {
            FileStream file = File.OpenRead(filename);
            BinaryFormatter b = new BinaryFormatter();
            Game g = (Game)b.Deserialize(file);
            file.Close();
            g.restore();

            return g;
        }
    }
}
