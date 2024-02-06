using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5_sem2
{
    /// <summary>
    /// General linked list class
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public sealed class Routes<Type>
    {
        Knot<Type> pr;
        Knot<Type> d;
        Knot<Type> pb;

        /// <summary>
        /// constructor
        /// </summary>
        public Routes()
        {
            pr = null;
            d = null;
            pb = null;
        }


        /// <summary>
        /// returns start
        /// </summary>
        public void Start()
        {
            d = pr;
        }

        /// <summary>
        /// returns next
        /// </summary>
        public void Next()
        {
            d = d.Next;
        }

        /// <summary>
        /// returns link not equal to null
        /// </summary>
        /// <returns></returns>
        public bool Is()
        {
            return d != null;
        }

        /// <summary>
        /// returns data
        /// </summary>
        /// <returns></returns>
        public Type GetData() { return d.data; }

        /// <summary>
        /// Adds data in reverse order
        /// </summary>
        /// <param name="inf"></param>
        public void AddDataA(Type inf)
        {
            var d = new Knot<Type>(inf, null);
            d.Next = pr;
            pr = d;
        }

        /// <summary>
        /// Adds data in direct order
        /// </summary>
        /// <param name="inf"></param>
        public void AddDataT(Type inf)
        {
            var d = new Knot<Type>(inf, null);
            if (pr != null)
            {
                pb.Next = d;
                pb = d;
            }
            else
            {
                pr = d;
                pb = d;
            }
        }
    }
}
