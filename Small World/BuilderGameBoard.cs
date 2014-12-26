using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface GameBoardBuilderInterface
    {
        void addMap();

        void addPlayer(string player, string people);

        Game createGame(string player1, string people1, string player2, string people2);

        void placeUnits();

        void initNbTours();

    }

    public abstract class BuilderGameBoard : GameBoardBuilderInterface 
    {
        protected int nbTiles;

        protected int nbTours;

        protected int nbUnits;

        private PeopleFactory peopleFactory;

        private Game game;

        private GameBoard gameBoard;


    }
}
