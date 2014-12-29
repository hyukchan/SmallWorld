using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface DwarfInterface : PeopleInterface
    {

    }

    public class Dwarf : People, DwarfInterface
    {
        public Dwarf()
        {
            
        }

        public override Unit CreateUnit()
        {
            return new DwarfUnit();
        }
    }
}
