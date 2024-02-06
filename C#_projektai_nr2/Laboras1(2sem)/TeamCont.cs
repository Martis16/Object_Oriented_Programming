using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras1_2sem_
{
    /// <summary>
    /// Dynamic container
    /// </summary>
    class TeamCont
    {
        private Player[] Team;
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public TeamCont(int capacity = 16)
        {
            Count = 0;
            Capacity = capacity;
            Team = new Player[capacity];
        }

        public Player GetPlayer(int i) { return Team[i]; }

        /// <summary>
        /// Increases capacity of container
        /// /// <param name="NewCpty">new capacity</param>
        /// </summary>
        public void IncreaseCapacity(int NewCpty)
        {
            if (NewCpty > Capacity)
            {
                Player[] TempT = new Player[NewCpty];
                for (int i = 0; i < Count; i++)
                {
                    TempT[i] = Team[i];
                }
                Team = TempT;
                Capacity = NewCpty;
            }
        }

        /// <summary>
        /// Add's player in a container
        /// /// <param name="plr">the player that will be added</param>
        /// </summary>
        public void SetPlayer(Player plr)
        {
            if(Count == Capacity)
            {
                IncreaseCapacity(Capacity * 2);
            }
            Team[Count++] = plr;
        }

        /// <summary>
        /// Returns team's avarage age
        /// </summary>
        public double AvgAge()
        {
            double a = 0;
            double avg;
            for (int i = 0; i < Count; i++)
            {
                a = a + Team[i].Age;
            }
            return avg = a / Count;
        }

        /// <summary>
        /// Returns team's avarage height
        /// </summary>
        public double AvgHeight()
        {
            double a = 0;
            double avg;
            for (int i = 0; i < Count; i++)
            {
                a = a + Team[i].Height;
            }
            return avg = a / Count;
        }

        /// <summary>
        /// method to sort the conatainer by age
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < Count-1; i++)
            {
                Player pl = Team[i];
                int im = i;
                for (int j = i; j < Count; j++)
                {
                    if (Team[j] < pl)
                    {
                        im = j;
                        pl = Team[j];
                    }
                }
                    Team[im] = Team[i];
                    Team[i] = pl;
            }
        }

        /// <summary>
        /// Removes a player if his is less more than specified
        /// </summary>
        /// <param name="age">the specified age</param>
        public void Remove(int age)
        {
            int m = 0;
            for (int i = 0; i < Count; i++)
            {
                if(Team[i].Age <= age)
                    Team[m++] = Team[i];
            }Count = m;
        }
    }
}
