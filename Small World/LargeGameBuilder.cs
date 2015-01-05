﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface LargeGameBuilderInterface : GameBuilderInterface
    {

    }

    public class LargeGameBuilder : GameBuilder, LargeGameBuilderInterface
    {
        public LargeGameBuilder()
        {
            nbTiles = BuilderLargeGameBoard.NB_TILES_LARGE;
            nbTurns = BuilderLargeGameBoard.NB_TOUR_LARGE;
            nbUnits = BuilderLargeGameBoard.NB_UNITS_LARGE;

            PeopleFactory = new PeopleFactory();
            GameBoard = new GameBoard();
            Game = new Game();
            Strategy = new BuilderDemoGameBoard();
        }

        public Game createGame(string player1, string people1, string player2, string people2)
        {
            //TODOSW
            return null;
        }
    }

   
}
