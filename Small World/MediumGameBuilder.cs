using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface MediumGameBuilderInterface : GameBuilderInterface
    {

    }


    public class MediumGameBuilder : GameBuilder, MediumGameBuilderInterface
    {
        public MediumGameBuilder()
        {
            nbTiles = BuilderMediumGameBoard.NB_TILES_MEDIUM;
            nbTurns = BuilderMediumGameBoard.NB_TURNS_MEDIUM;
            nbUnits = BuilderMediumGameBoard.NB_UNITS_MEDIUM;

            PeopleFactory = new PeopleFactory();
            GameBoard = new GameBoard();
            Game = new Game();
            Strategy = new BuilderDemoGameBoard();
        }

    }
    }

