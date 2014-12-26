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

    public class CreateDwarf : People, PeopleInterface
    {
        public CreateDwarf() { }

        public override Unit createUnit()
        {
            return new DwarfUnit();
        }
    }

    public class CreateElf : People, PeopleInterface
    {
        public CreateElf() { }

        public override Unit createUnit()
        {
            return new ElfUnit();
        }
    }

    public class CreateOrc : People, PeopleInterface
    {
        public CreateOrc() { }

        public override Unit createUnit()
        {
            return new OrcUnit();
        }
    }


}
