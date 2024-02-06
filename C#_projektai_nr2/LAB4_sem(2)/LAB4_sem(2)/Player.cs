using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_sem_2_
{
    /// <summary>
    /// class to describe player
    /// </summary>
    public class Player
    {
        public string NamSur { get; set; } //Player's name and surname
        public int Height { get; set; }    //player's height
        public int Age { get; set; }       //player's age

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="NamS"></param>
        /// <param name="age"></param>
        /// <param name="Hght"></param>
        public Player(string NamS, int age, int Hght)
        {
            NamSur = NamS;
            Height = Hght;
            Age = age;
        }

        /// <summary>
        /// new names
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        void Add(string a, int b, int c)
        {
            NamSur = a;
            Height = b;
            Age = c;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns>printing format for player</returns>
        public override string ToString()
        {
            string line;
            line = string.Format("{0, -17} {1, 2} {2,6}", NamSur, Age, Height);
            return line;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>player with same name</returns>
        public override bool Equals(object obj)
        {
            Player plr = obj as Player;
            return plr.NamSur == NamSur;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns>hash code</returns>        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// compares two players by age and returns older one, if ages are equal compares names alphabetically
        /// </summary>
        /// <param name="pl1"></param>
        /// <param name="pl2"></param>
        /// <returns></returns>
        public static bool operator >=(Player pl1, Player pl2)
        {
            int p = String.Compare(pl1.NamSur, pl2.NamSur, StringComparison.CurrentCulture);
            return ((pl1.Age > pl2.Age) || (pl1.Age == pl2.Age) && (p > 0));
                
        }


        /// <summary>
        /// compares two players by age and returns younger one, if ages are equal compares names alphabetically
        /// </summary>
        /// <param name="pl1"></param>
        /// <param name="pl2"></param>
        /// <returns></returns>
        public static bool operator <=(Player pl1, Player pl2)
        {
            int p = String.Compare(pl1.NamSur, pl2.NamSur, StringComparison.CurrentCulture);
            return ((pl1.Age < pl2.Age) || (pl1.Age == pl2.Age) && (p < 0));
        }
    }
}
