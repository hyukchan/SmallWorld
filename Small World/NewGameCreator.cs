using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class NewGameCreator : GameCreator
    {
        /// <summary>
        /// Getter/Setter du monteur de la partie
        /// </summary>
        public GameBuilder GameBuilder
        {
            get
            {
                return gameBuilder;
            }
            set
            {
                gameBuilder = value;
            }
        }

        /// <summary>
        /// Getter/Setter du nom de la stratégie à employer
        /// </summary>
        public string GameType
        {
            get
            {
                return gameType;
            }
            set
            {
                if (value == DEMO)
                {
                    gameType = value;
                    GameBuilder = new DemoGameBuilder();
                }
                if (value == MEDIUM)
                {
                    gameType = value;
                    GameBuilder = new MediumGameBuilder();
                }
                if (value == LARGE)
                {
                    gameType = value;
                    GameBuilder = new LargeGameBuilder();
                }
            }
        }

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
