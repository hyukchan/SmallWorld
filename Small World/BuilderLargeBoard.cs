﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface LargeGameBoardBuilderInterface : GameBoardBuilderInterface
    {

    }


    public class LargeGameBoardCreator : GameBoardCreator
    {
        public LargeGameBoardCreator()
        {
            throw new System.NotImplementedException();
        }

        public int NbTiles
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int NbTurns
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int NbUnits
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void CreateGameBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}
