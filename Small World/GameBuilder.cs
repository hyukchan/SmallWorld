using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public abstract class GameBuilder
    {
        public Game Game
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public PeopleFactory PeopleFactory
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void AddGameBoard()
        {
            throw new System.NotImplementedException();
        }

        public void AddPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void Build()
        {
            throw new System.NotImplementedException();
        }

        public void PlaceUnits()
        {
            throw new System.NotImplementedException();
        }
    }
}
