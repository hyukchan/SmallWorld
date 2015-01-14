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

        public override Uri getPeopleImage()
        {
            return new Uri("./textures/player_dwarf.png", UriKind.Relative);
        }

        public override Uri getUnactivePeopleImage()
        {
            return new Uri("./textures/unactive_player_dwarf.png", UriKind.Relative);
        }
    }
}
