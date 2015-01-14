using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface ElfInterface : PeopleInterface
    {

    }

    public class Elf : People, ElfInterface
    {
        public Elf()
        {
           
        }

        public override Unit CreateUnit()
        {
            return new ElfUnit();
        }

        public override Uri getPeopleImage()
        {
            return new Uri("./textures/player_elf.png", UriKind.Relative);
        }

        public override Uri getUnactivePeopleImage()
        {
            return new Uri("./textures/unactive_player_elf.png", UriKind.Relative);
        }
    }
}
