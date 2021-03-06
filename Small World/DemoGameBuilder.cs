﻿using System;
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
        /// <summary>
        /// Constructeur d'une partie de démonstration
        /// </summary>
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
    }

}