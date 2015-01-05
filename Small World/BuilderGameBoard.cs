using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface StrategyInterface
    {
        List<List<Tile>> build();
    }

    public abstract class BuilderGameBoard : StrategyInterface
    {
        protected int size;

        public int Size
        {
            get
            {
                return size;
            }
        }

        public List<List<Tile>> build()
        {
            //TODOSW strategie avec wrapper
            return null;
        }
    }

}
