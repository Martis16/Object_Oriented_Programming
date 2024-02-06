using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_sem2
{
    /// <summary>
    /// Data class
    /// </summary>
    public class Tickets
    {
        public string RouteNumber { get; set; }
        public string Day { get; set; }
        public DateTime DepartingTime { get; set; }
        public string NamSur { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="NamSur">Name and surname</param>
        /// <param name="Day">Day</param>
        /// <param name="DTime">Departing time</param>
        /// <param name="Rnmbr">Route number</param>
        public Tickets(string NamSur, string Day, DateTime DTime, string Rnmbr)
        {
            RouteNumber = Rnmbr;
            this.Day = Day;
            DepartingTime = DTime;
            this.NamSur = NamSur;
        }


        /// <summary>
        /// new names
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        void Add(string a, string b, string c, DateTime d)
        {
            RouteNumber = b;
            Day = a;
            NamSur = c;
            DepartingTime = d;
        }


        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns>printing format for tickets</returns>
        public override string ToString()
        {
            string line;
            line = string.Format("|  {0,-18}| {1,-14}| {2,-13}| {3,-13}|",NamSur, Day, DepartingTime.ToString("hh:mm:ss"),RouteNumber);
            return line;
        }


        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Route rt = obj as Route;
            return rt.RouteNumber == RouteNumber;
        }


        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns></returns>        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
