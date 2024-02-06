using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3_sem2_
{
    /// <summary>
    /// Base class of car
    /// </summary>
    abstract class Car : Object
    {
        public string Number { get; set; }
        public string Manufacturer { get; set; }
        public string Make { get; set; }
        public DateTime Year { get; set; }
        public DateTime TAdate { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Car()
        {

        }

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="Nmbr">Number</param>
        /// <param name="Manuf">Manufacturer</param>
        /// <param name="Make">Make</param>
        /// <param name="Year">Year</param>
        /// <param name="TAd">TA date</param>
        public Car(string Nmbr, string Manuf, string Make, DateTime Year, DateTime TAd)
        {
            Number = Nmbr;
            Manufacturer = Manuf;
            this.Make = Make;
            this.Year = Year;
            TAdate = TAd;
        }
        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string line;
            line = string.Format(" {0,3}| {1,8}    | {2,9} | {3,2} |",
            Number, Manufacturer, Make, Year.ToString("yyyy-MM"));
            return line;
        }
    }
}
