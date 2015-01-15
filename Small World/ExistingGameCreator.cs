using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Wrapper;

namespace Small_World
{
    public class ExistingGameCreator : GameCreator
    {
        public string saveName;

        public ExistingGameCreator(string name)
        {
            saveName = name;
        }

        /// <summary>
        /// Monteur d'une partie sauvegardée
        /// </summary>
        /// <returns>La partie chargée</returns>
        public override unsafe Game CreateGame()
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
