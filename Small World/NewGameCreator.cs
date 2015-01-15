using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class NewGameCreator : GameCreator
    {

        /// <summary>
        /// Constructeur du créateur de la partie
        /// </summary>
        public NewGameCreator()
        {
            playerList = new List<string>();
        }


        public override Game CreateGame()
        {
            return GameBuilder.CreateGame(playerList[0], playerList[1], playerList[2], playerList[3]);
        }
    }
}
