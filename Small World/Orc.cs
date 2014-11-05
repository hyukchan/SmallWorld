using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Orc : People
    {
        public int bonusPt
        {
            get
            {
                return bonusPt;
            }
            set
            {
                if(value >= 0)
                {
                    bonusPt = value;
                }
            }
        }
    }
}
