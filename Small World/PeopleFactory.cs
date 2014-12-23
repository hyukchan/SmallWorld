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

        private static PeopleFactory factoryInstance;

        public PeopleFactory()
        {

        }

        public static PeopleFactory FactoryInstance
        {
            get
            {
                if (factoryInstance == null)
                {
                    factoryInstance = new PeopleFactory();
                }
                return factoryInstance;
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

            }
        }
    }

        //TODOSW créer les classes des peuples (ici ou classe People ? )
        public class CreateDwarf
        {
            public CreateDwarf(){}
        }

        public class CreateElf
        {
            public CreateElf(){}
        }

        public class CreateOrc
        {
            public CreateOrc(){}
        }

    }

