using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface GameInterface
    {
        Player winner();

        void changePlayer();

        List<Unit> selectUnit(int x, int y);

        List<Unit> selectOpponentUnit(int x, int y);

        bool canMove(Unit unit, int x, int y);

        int askToMove(Unit unit, int x, int y);

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
            FirstPlayer =first;
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
            if((u.GetType() == new DwarfUnit().GetType()) && (selectOpponentUnit(x,y).Count != 0) && (Math.Abs(x - u.Position.X) <= 1) && (Math.Abs(y-u.Position.Y) <= 1 ))
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
                List<Unit> listUnits = selectOpponentUnit(x,y);
                if (listUnits.Count == 0)
                {
                    u.Move(x, y);
                    return 1;
                }
            }
            return 1;
        }

        public void EndTurn()
        {
            throw new System.NotImplementedException();
        }

        public void MoveUnit()
        {
            throw new System.NotImplementedException();
        }

        public void changePlayer()
        {
            throw new System.NotImplementedException();
        }

        public List<Unit> selectUnit(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public List<Unit> selectOpponentUnit(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public bool canMove(Unit unit, int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int askToMove(Unit unit, int x, int y)
        {
            throw new System.NotImplementedException();
        }


        public void checkEndOfGame()
        {
            throw new System.NotImplementedException();
        }

        public bool save()
        {
            throw new System.NotImplementedException();
        }

        public bool saveAs(string filename)
        {
            throw new System.NotImplementedException();
        }

        public Game load(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}
