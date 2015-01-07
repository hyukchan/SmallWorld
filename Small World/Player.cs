using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Player
    {
        private People people;
        private int points;
        private string name;
        private List<Unit> units;

        public Player()
        {
        }

        public Player(string name, People people)
        {
            Name = name;
            People = people;
            Units = new List<Unit>();
        }

        public People People
        {
            get
            {
                return people;
            }
            set
            {
                people = value;
            }
        }

        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public List<Unit> Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;
            }
        }
    
        public void GetGamePoints()
        {
            int pointsCount = 0;
            foreach (Unit unit in units)
            {
                unit.UpdateGamePoints();
                pointsCount += unit.GamePt;
            }
            Points = pointsCount;
        }
    }
}
