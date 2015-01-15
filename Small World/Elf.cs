using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface ElfInterface : PeopleInterface
    {

    }

    [Serializable()]
    public class Elf : People, ElfInterface
    {
        /// <summary>
        /// Contructeur du peuple elf
        /// </summary>
        public Elf()
        {
           
        }

        /// <summary>
        /// Crée une unité elf
        /// </summary>
        /// <returns></returns>
        public override Unit CreateUnit()
        {
            return new ElfUnit();
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour est en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getPeopleImage()
        {
            return new Uri("./textures/player_elf.png", UriKind.Relative);
        }

        /// <summary>
        /// Donne l'image du peuple dont le tour n'est pas en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public override Uri getUnactivePeopleImage()
        {
            return new Uri("./textures/unactive_player_elf.png", UriKind.Relative);
        }
    }
}
