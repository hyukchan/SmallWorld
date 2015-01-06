using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class DwarfUnit : Unit
    {
        public DwarfUnit()
        {
            
        }

        //TODOSW to fill
        public void Move(Position p)
        {
            if (MovePt > 0)
            {
                MovePt--;
            }
        }
    }
}
