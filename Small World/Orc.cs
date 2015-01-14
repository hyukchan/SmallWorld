using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface OrcInterface : PeopleInterface
    {

    }

    [Serializable]
    public class Orc : People, OrcInterface
    {
        /// <summary>
        /// Constructeur du peuple orc
        /// </summary>
        public Orc()
        {
            
        }

        /// <summary>
        /// Constructeur d'une unité orc
        /// </summary>
        public override Unit CreateUnit()
        {
            return new OrcUnit();
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour est en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getPeopleImage()
        {
            return new Uri("./textures/player_orc.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour n'est pas en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getUnactivePeopleImage()
        {
            return new Uri("./textures/unactive_player_orc.png", UriKind.Relative);
        }
    }
}
