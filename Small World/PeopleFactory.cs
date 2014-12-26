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
                    return new CreateDwarf();
                case ELF:
                    return new CreateElf();
                case ORC:
                    return new CreateOrc();
                default: 
                    return null;

            }
        }

    }



}

