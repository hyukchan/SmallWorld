using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{



    public interface peopleFactoryInterface
    {
        People peopleFactory(string people);
    }
    public class PeopleFactory : peopleFactoryInterface
    {

        public const string DWARF = "Dwarf";
        public const string ELF = "Elf";
        public const string ORC = "Orc";

        private static PeopleFactory peoplefactoryInstance;

        public PeopleFactory()
        {

        }

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

        public People peopleFactory(string people)
        {
            throw new System.NotImplementedException();
        }
    }



}

