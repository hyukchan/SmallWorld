using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface PeopleInterface
    {
        Unit createUnit();
    }

    public abstract class People : PeopleInterface
    {
        public abstract Unit createUnit();
    }

    public class CreateDwarf
    {
        public CreateDwarf() { }
    }

    public class CreateElf
    {
        public CreateElf() { }
    }

    public class CreateOrc
    {
        public CreateOrc() { }
    }


}
