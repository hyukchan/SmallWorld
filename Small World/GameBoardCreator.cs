using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface GameBoardCreator
    {
        int NbUnits
        {
            get;
            set;
        }

        int NbTurns
        {
            get;
            set;
        }

        int NbTiles
        {
            get;
            set;
        }

        void CreateGameBoard();
    }
}
