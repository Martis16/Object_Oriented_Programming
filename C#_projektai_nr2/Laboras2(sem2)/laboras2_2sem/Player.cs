using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboras2_2sem
{
    /// <summary>
    /// Class to describe the data of one player
    /// </summary>
    class Player : IComparable<Player>
    {
        public string NamSur { get; set; } //Player's name and surname
        public int Height { get; set; }    //player's height
        public int Age { get; set; }       //player's age

        public Player(string NamS, int age, int Hght)
        {
            NamSur = NamS;
            Height = Hght;
            Age = age;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        public override string ToString()
        {
            string line;
            line = string.Format("{0, -17}; {1, 2}; {2,6};", NamSur, Age, Height);
            return line;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        public override bool Equals(object objektas)
        {
            Player plr = objektas as Player;
            return plr.NamSur == NamSur;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Student comparison method
        /// </summary>
        /// <param name="p1">player one</param>
        /// <returns> true if player's one age is less than age of other player or if age is
        /// equal, then compares names and surnames of both players in alphabetical order
        /// else returns false
        /// </returns>        public int CompareTo(Player pl)
        {
            int poz = String.Compare(this.NamSur, pl.NamSur, StringComparison.CurrentCulture);
            if ((this.Age > pl.Age) || ((this.Age == pl.Age) && (poz > 0)))
                return 1;
            else return -1;
        }
    }
}
