using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_sem2
{
    /// <summary>
    /// General knot class
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public sealed class Knot<Type>
    {
        public Type data { get; set; }
        public Knot<Type> Next { get; set; }


        /// <summary>
        /// constructor
        /// </summary>
        public Knot() { }


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        public Knot(Type data, Knot<Type> address)
        {
            this.data = data;
            Next = address;
        }
    }
}
