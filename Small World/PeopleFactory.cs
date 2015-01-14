using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface peopleFactoryInterface
    {
        People CreatePeople(string people);
    }
    public class PeopleFactory : peopleFactoryInterface
    {
        public const string DWARF = "Dwarf";
        public const string ELF = "Elf";
        public const string ORC = "Orc";

        private static PeopleFactory peoplefactoryInstance;

        /// <summary>
        /// Constructeur par défaut de la fabrique de peuples
        /// </summary>
        public PeopleFactory()
        {

        }

        /// <summary>
        /// Getter de l'unique instance de la fabrique
        /// </summary>
        public static PeopleFactory FactoryInstance
        {
            get
            {
                if (peoplefactoryInstance == null)
                {
                    peoplefactoryInstance = new PeopleFactory();
                }
                return peoplefactoryInstance;
            }
        }

        /// <summary>
        /// Crée un peuple
        /// </summary>
        /// <param name="people">Le peuple à créer</param>
        /// <returns>Le peuple crée</returns>
        public People CreatePeople(string people)
        {
            switch (people)
            {
                case DWARF:
                    return new Dwarf();
                case ELF:
                    return new Elf();
                case ORC:
                    return new Orc();
                default: 
                    return null;

            }
        }        
    }
}

