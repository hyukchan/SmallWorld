using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface PeopleInterface
    {
        Unit CreateUnit();
        Uri getPeopleImage();
    }

    [Serializable]
    public abstract class People : PeopleInterface
    {
        public const int DWARF = 0;
        public const int ELF = 1;
        public const int ORC = 2;

        public abstract Unit CreateUnit();

        public abstract Uri getPeopleImage();

        public abstract Uri getUnactivePeopleImage();
    }

}
