using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface People
    {
        int attackPt
        {
            get
            {
                return attackPt;
            }
        }

        int defensePt
        {
            get
            {
                return defensePt;
            }
        }

        int hp
        {
            get
            {
                return hp;
            }
            set
            {
                if (value > 0 && value < 5)
                {
                    hp = value;
                }
            }
        }

        double mvtPt
        {
            get
            {
                return mvtPt;
            }
            set
            {
                if(value >= 0)
                {
                    mvtPt = value;
                }
            }
        }
    }
}
