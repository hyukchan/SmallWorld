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
        public GameCreator()
        {
            playerList = new List<string>();
        }

        /// <summary>
        /// Ajoute un joueur à la liste
        /// </summary>
        /// <param name="name">Nom du joueur à ajouter</param>
        /// <param name="people">Peuple du joueur à ajouter</param>
        public void AddPlayer(string name, string people)
        {
            playerList.Add(name);
            playerList.Add(people);
        }

        /// <summary>
        /// Crée la partie avec les paramètres courants
        /// </summary>
        /// <returns>La partie créée</returns>
        public Game CreateGame()
        {
            return GameBuilder.CreateGame(playerList[0], playerList[1], playerList[2], playerList[3]);
        }
    }
}
