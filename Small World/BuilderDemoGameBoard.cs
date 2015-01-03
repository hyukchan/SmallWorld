﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface StrategyDemoInterface : StrategyInterface
    {
        
        new List<List<Tile>> build();
    }

    public class BuilderDemoGameBoard : BuilderGameBoard ,StrategyDemoInterface
    {
        public const int NB_TILES_DEMO = 6;
        public const int NB_UNITS_DEMO = 4;
        public const int NB_TURNS_DEMO = 5;

        public BuilderDemoGameBoard()
        {
            size = NB_TILES_DEMO;
        }
    }
}
