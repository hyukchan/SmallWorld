using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface PeopleInterface
    {
        Unit CreateUnit();
    }

    public abstract class People : PeopleInterface
    {
        public abstract Unit CreateUnit();
    }

}
