using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3_sem2_
{
    /// <summary>
    /// derivative class of car
    /// </summary>
    class Cars : Car, IComparable<Cars>, IEquatable<Cars>
    {

        public string FuelType { get; set; }
        public double FuelConsumption { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Cars()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Nmbr">Number</param>
        /// <param name="Manuf">Manufacturer</param>
        /// <param name="Make">Make</param>
        /// <param name="Year">Year</param>
        /// <param name="TAd">TA date</param>
        /// <param name="FuelT">Fuel type</param>
        /// <param name="FuelCon">Fuel consumption</param>
        public Cars(string Nmbr, string Manuf, string Make, DateTime Year, DateTime TAd, string FuelT, double FuelCon):
            base(Nmbr, Manuf, Make, Year, TAd)
        {
            TAdate = TAd;
            FuelType = FuelT;
            FuelConsumption = FuelCon;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string line;
            line = string.Format("{0} {1,8}| {2,8}| {3,4}    |", base.ToString(),
                TAdate.ToString("yyyy-MM-dd"), FuelType, FuelConsumption);
            return line;
        }
        /// <summary>
        /// Cars comparison method
        /// </summary>
        /// <param name="car"></param>
        /// <returns>true if car's Fuel consuption is bigger than other car's or
        /// if Fuel consumption is equal then compares Cars years in a descending order</returns>
        public int CompareTo(Cars car)
        {
            if ((this.FuelConsumption < car.FuelConsumption) ||
                (this.FuelConsumption == car.FuelConsumption) && (this.Year < car.Year))
                return 1;
            else return -1;
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Cars);
        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <param name="kitas"></param>
        /// <returns></returns>
        public bool Equals(Cars kitas)
        {
            return kitas.Number == Number &&
                kitas.Manufacturer == Manufacturer &&
                kitas.Make == Make &&
                kitas.Year == Year &&
                kitas.TAdate == TAdate &&
                kitas.FuelType == FuelType &&
                kitas.FuelConsumption == FuelConsumption;

        }

        /// <summary>
        /// Overriden Object class method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Number.GetHashCode() ^
                Manufacturer.GetHashCode() ^
                Make.GetHashCode() ^
                Year.GetHashCode() ^
                TAdate.GetHashCode() ^
                FuelType.GetHashCode() ^
                FuelConsumption.GetHashCode();
        }
    }
}
