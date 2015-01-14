using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface DwarfInterface : PeopleInterface
    {

    }

    [Serializable]
    public class Dwarf : People, DwarfInterface
    {
        /// <summary>
        /// Contrusteur du peuple nain
        /// </summary>
        public Dwarf()
        {
            
        }

        /// <summary>
        /// Construit une unité naine
        /// </summary>
        /// <returns></returns>
        public override Unit CreateUnit()
        {
            return new DwarfUnit();
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour est en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getPeopleImage()
        {
            return new Uri("./textures/player_dwarf.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour n'est pas en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getUnactivePeopleImage()
        {
            return new Uri("./textures/unactive_player_dwarf.png", UriKind.Relative);
        }
    }
}
