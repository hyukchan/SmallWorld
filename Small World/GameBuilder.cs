using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{
    public interface GameBuilderInterface
    {
        void addMap();

        void addPlayer(string player, string people);

        Game CreateGame(string player1, string people1, string player2, string people2);

        void placeUnits();

        void initNbTurns();

    }


    public abstract class GameBuilder : GameBuilderInterface
    {
        protected int nbTiles;

        protected int nbTurns;

        protected int nbUnits;

        private PeopleFactory peopleFactory;

        private Game game;

        private GameBoard gameBoard;

        private BuilderGameBoard strategy;

        public int NbTiles
        {
            get
            {
                return nbTiles;
            }
        }

        public int NbTurns
        {
            get
            {
                return nbTurns;
            }
        }

        public int NbUnits
        {
            get
            {
                return nbUnits;
            }
        }

        public PeopleFactory PeopleFactory
        {
            get
            {
                return peopleFactory;
            }
            set
            {
                peopleFactory = value;
            }
        }

        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
            }
        }

        public GameBoard GameBoard
        {
            get
            {
                return gameBoard;
            }
            set
            {
                gameBoard = value;
            }
        }

        public BuilderGameBoard Strategy
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        public void addMap()
        {
            List<Tile> listTiles;
            listTiles = Strategy.build();

            Game.Map.ListTiles = listTiles;
        }

        public void addPlayer(string playername, string people)
        {
            People p = PeopleFactory.FactoryInstance.CreatePeople(people);
            Player player = new Player(playername, p);
            Game.PlayerList.Add(player);

            Random r = new Random();
            int first = r.Next(Game.PlayerList.Count);
            Game.FirstPlayer = first;
            Game.CurrentPlayer = Game.FirstPlayer;
            
        }

        public Game CreateGame(string player1, string people1, string player2, string people2)
        {
            Game = new Game();
            this.addPlayer(player1, people1);
            this.addPlayer(player2, people2);
            this.addMap();
            this.placeUnits();
            this.initNbTurns();
            return Game;
        }

        public void initNbTurns()
        {
            Game.NbRemainingTurns = nbTurns;
        }

        public unsafe void placeUnits()
        {
            int i, j;

            for (i = 0; i < NbUnits; i++)
            {
                Game.PlayerList[0].Units.Add(game.PlayerList[0].People.CreateUnit());
                Game.PlayerList[1].Units.Add(game.PlayerList[1].People.CreateUnit());
            }

            WrapperAlgo wrapper = new WrapperAlgo();
            int* position;
            int* map = wrapper.createGameBoard(NbTiles);

            for (i = 0; i < NbTiles; i++)
            {
                for (j = 0; j < NbTiles; j++)
                {
                    if (Game.Map.ListTiles[i * NbTiles + j].GetType() == new Desert().GetType())
                    {
                        map[i * NbTiles + j] = Tile.DESERT;
                    }
                    if (Game.Map.ListTiles[i * nbTiles + j].GetType() == new Plain().GetType())
                    {
                        map[i * NbTiles + j] = Tile.PLAIN;
                    }
                    if (Game.Map.ListTiles[i * nbTiles + j].GetType() == new Mountain().GetType())
                    {
                        map[i * NbTiles + j] = Tile.MOUNTAIN;
                    }
                    if (Game.Map.ListTiles[i * nbTiles + j].GetType() == new Forest().GetType())
                    {
                        map[i * NbTiles + j] = Tile.FOREST;
                    }
                }
            }

            position = wrapper.startingPositions(map, NbTiles);

            Game.TabMap = map;

            Position p1 = new Position { X = position[0], Y = position[1] };
            Position p2 = new Position { X = position[2], Y = position[3] };

            List<Position> pos = new List<Position>();
            pos.Add(p1);
            pos.Add(p2);

            for (i = 0; i < Game.PlayerList.Count; i++)
            {
                for (j = 0; j < Game.PlayerList[i].Units.Count; j++)
                {
                    Game.PlayerList[i].Units[j].Position = new Position { X = pos[i].X, Y = pos[i].Y };
                    Game.PlayerList[i].Units[j].TabMap = map;
                    Game.PlayerList[i].Units[j].Costs = wrapper.costTab(NbTiles);
                    Game.PlayerList[i].Units[j].Moves = wrapper.createGameBoard(NbTiles);
                    Game.PlayerList[i].Units[j].SizeMap = NbTiles;
                    Game.PlayerList[i].Units[j].CalculateMoves();
                    Game.PlayerList[i].Units[j].endTurn();
                }

                Game.PlayerList[i].GetGamePoints();
            }

            foreach (Unit u in Game.PlayerList[Game.CurrentPlayer].Units)
            {
                u.newTurn();
            }

        }
    }

}
