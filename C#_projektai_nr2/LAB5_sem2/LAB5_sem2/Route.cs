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
    public class Route
    {
        public string RouteNumber { get; set; }
        public string Day { get; set; }
        public DateTime DepartingTime { get; set; }
        public int Price { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Rnmbr">Route number</param>
        /// <param name="Day">Day</param>
        /// <param name="DTime">Departing time</param>
        /// <param name="Price">Price</param>
        public Route(string Rnmbr, string Day, DateTime DTime, int Price)
        {
            RouteNumber = Rnmbr;
            this.Day = Day;
            DepartingTime = DTime;
            this.Price = Price;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public Route() { }


        /// <summary>
        /// new names
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        void Add(string a, string b, int c, DateTime d)
        {
            RouteNumber = b;
            Day = a;
            Price = c;
            DepartingTime = d;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns>printing format for routes</returns>
        public override string ToString()
        {
            string line;
            line = string.Format("|  {0,-11}| {1,-14}| {2,-13}| {3,-8}|", RouteNumber, Day, DepartingTime.ToString("hh:mm:ss"), Price);
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
