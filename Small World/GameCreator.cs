using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class GameCreator
    {
        public const string DEMO = "Demo";
        public const string MEDIUM = "Medium";
        public const string LARGE = "Large";

        public string gameType;
        public GameBuilder gameBuilder;
        public List<string> playerList;

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

        public GameCreator()
        {
            playerList = new List<string>();
        }

        public void AddPlayer(string name, string people)
        {
            playerList.Add(name);
            playerList.Add(people);
        }

        public Game CreateGame()
        {
            return GameBuilder.CreateGame(playerList[0], playerList[1], playerList[2], playerList[3]);
        }
    }
}
