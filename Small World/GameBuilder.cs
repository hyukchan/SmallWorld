using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            Game.GameBoard.ListTiles = listTiles;
        }

        public void addPlayer(string playername, string people)
        {
            People p = PeopleFactory.FactoryInstance.CreatePeople(people);
            Player player = new Player(playername, p);
            Game.ListPlayers.add(player);

            Random r = new Random();
            int first = r.Next(Game.ListPlayers.Count);
            Game.FirstPlayer = first;
            Game.CurrentPlayer = Game.FirstPlayer;
            //TODOSW implémenter les fonctions de l'interface
        }

        public Game CreateGame(string player1, string people1, string player2, string people2)
        {
            this.addPlayer(player1, people1);
            this.addPlayer(player2, people2);
            this.addMap();
            this.placeUnits();
            this.initNbTurns();
            return Game;
        }

        public void initNbTurns()
        {
            Game.NbTurnsRemaining = nbTurns;
        }

        public void placeUnits()
        {
            //TODOSW
        }
    }

}
