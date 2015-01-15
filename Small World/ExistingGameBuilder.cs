using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Small_World
{
    public class ExistingGameBuilder : GameCreator
    {
        public string saveName;

        public ExistingGameBuilder(string name)
        {
            saveName = name;
        }

        /// <summary>
        /// Monteur d'une partie sauvegardée
        /// </summary>
        /// <returns>La partie chargée</returns>
        public Game CreateGame()
        {
            Game g = new Game();
            if(File.Exists(saveName)){
                using(FileStream f = File.OpenRead(saveName)){
                    BinaryFormatter b = new BinaryFormatter();               
                    g.load((Game)b.Deserialize(f));
                }
            }
            else
            {
                return null;
            }
            return g;
        }

    }
}
