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
            List<Position> posOwned = new List<Position>();
            List<Unit> unitsCounted = new List<Unit>();

            int i;
            int pointsCount = 0;
            int max;
            foreach (Unit unit in units)
            {
                unit.UpdateGamePoints();
                max = unit.GamePt;

                if (posOwned.Contains(unit.Position))
                {
                    for (i = 0; i < unitsCounted.Count; i++)
                    {
                        if (unitsCounted[i].Position.Equals(unit.Position))
                        {
                            if (unitsCounted[i].GamePt < max)
                            {
                                max = unitsCounted[i].GamePt;
                                unitsCounted.Remove(unitsCounted[i]);
                                unitsCounted.Add(unit);
                            }
                            else
                            {
                                max = 0;
                            }
                        }
                    }
                }
                else
                {
                    posOwned.Add(unit.Position);
                    unitsCounted.Add(unit);
                }
                
                pointsCount += max;
            }
            Points = pointsCount;
        }

        public Uri getPlayerAvatar()
        {
            return People.getPeopleImage();
        }

        public Uri getUnactivePlayerAvatar()
        {
            return People.getUnactivePeopleImage();
        }
    }
}
