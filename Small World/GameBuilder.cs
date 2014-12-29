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

        Game createGame(string player1, string people1, string player2, string people2);

        void placeUnits();

        void initNbTours();

    }

    public interface DemoGameBuilderInterface : GameBuilderInterface
    {

    }

    public interface MediumGameBuilderInterface : GameBuilderInterface
    {

    }

    public interface LargeGameBuilderInterface : GameBuilderInterface
    {

    }



    public abstract class GameBuilder : GameBuilderInterface
    {
        protected int nbTiles;

        protected int nbTurns;

        protected int nbUnits;

        private PeopleFactory peopleFactory;

        private Game game;

        private GameBoard gameBoard;

        private GameBuilder strategy;

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



    }
}
