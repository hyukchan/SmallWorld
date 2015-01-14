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

    public abstract class People : PeopleInterface
    {
        public abstract Unit CreateUnit();

        public abstract Uri getPeopleImage();

        public abstract Uri getUnactivePeopleImage();
    }

}
