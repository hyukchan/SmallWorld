using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface DemoGameBuilderInterface : GameBuilderInterface
    {

    }

    public class DemoGameBuilder : GameBuilder, DemoGameBuilderInterface
    {
        public DemoGameBuilder()
        {
            nbTiles = BuilderDemoGameBoard.NB_TILES_DEMO;
            nbTurns = BuilderDemoGameBoard.NB_TURNS_DEMO;
            nbUnits = BuilderDemoGameBoard.NB_UNITS_DEMO;

            PeopleFactory = new PeopleFactory();
            GameBoard = new GameBoard();
            Game = new Game();
            Strategy = new BuilderDemoGameBoard();
        }
        public Game CreateGame(string player1, string people1, string player2, string people2)
        {
            //TODOSW
            return null;
        }
    }

}