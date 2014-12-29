using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface OrcInterface : PeopleInterface
    {

    }

    public class Orc : People, OrcInterface
    {
        public Orc()
        {
            
        }

        public override Unit CreateUnit()
        {
            return new OrcUnit();
        }
    }
}
