using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Small_World
{
    [Serializable()]
    public class Player : System.ComponentModel.INotifyPropertyChanged
    {
        private People people;
        private int points;
        private string name;
        private List<Unit> units;

        /// <summary>
        /// Constructeur par défaut d'un joueur
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Constructeur d'un joueur à partir de deux paramètres
        /// </summary>
        /// <param name="name">Le nom du joueur</param>
        /// <param name="people">Le peuple du joueur</param>
        public Player(string name, People people)
        {
            Name = name;
            People = people;
            Units = new List<Unit>();
        }

        /// <summary>
        /// Getter/Setter du peuple du joueur
        /// </summary>
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

        /// <summary>
        /// Getter/Setter des points du joueur
        /// </summary>
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter/Setter du nom du joueur
        /// </summary>
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

        /// <summary>
        /// Getter/Setter de la liste des unités du joueur
        /// </summary>
        public List<Unit> Units
        {
            get
            {
                return units;
            }
            set
            {
                units = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Calcule les points du joueur en fonction de la position de ses unités
        /// </summary>
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

        /// <summary>
        /// Donne l'image du joueur dont le tour est en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public Uri getPlayerAvatar()
        {
            return People.getPeopleImage();
        }

        /// <summary>
        /// Donne l'image du joueur dont le tour n'est pas en cours
        /// </summary>
        /// <returns>L'URI de l'image</returns>
        public Uri getUnactivePlayerAvatar()
        {
            return People.getUnactivePeopleImage();
        }

        #region INotifyPropertyChanged

        [field : NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
}
