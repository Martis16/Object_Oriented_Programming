using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_sem_2_
{
    /// <summary>
    /// linked list container class
    /// </summary>
    public sealed class Ballers : IEnumerable
    {
        private knot pr; // start
        private knot pb; // end
        private knot ss; // link


        /// <summary>
        /// constructor
        /// </summary>
        public Ballers()
        {
            pr = null;
            pb = null;
            ss = null;
        }


        /// <summary>
        /// IEnumerator method
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            for (knot ss = pr; ss != null; ss = ss.Next)
            {
                yield return ss.Data;
            }
        }        /// <summary>
        /// Adds data in reverse order
        /// </summary>
        /// <param name="pl"></param>
        public void AddDataA(Player pl)
        {
            var d = new knot(pl, null);
            d.Next = pr;
            pr = d;
        }


        /// <summary>
        /// Adds data in direct order
        /// </summary>
        /// <param name="pl"></param>
        public void AddDataT(Player pl)
        {
            var d = new knot(pl, null);
            if(pr != null)
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
        //public void Papildyti(Player duom)
        //{
        //    Mazgas d1 = new Mazgas();
        //    d1.Data = duom;
        //    d1.Next = pr;
        //    pr = d1;
        //}

        /// <summary>
        /// returns data
        /// </summary>
        /// <returns></returns>
        public Player GetData()
        {
            return ss.Data;
        }


        /// <summary>
        /// returns start
        /// </summary>
        public void Start()
        {
            ss = pr;
        }


        /// <summary>
        /// returns next
        /// </summary>
        public void Next()
        {
            ss = ss.Next;
        }


        /// <summary>
        /// return link not equal to null
        /// </summary>
        /// <returns></returns>
        public bool Is()
        {
            return ss != null;
        }


        /// <summary>
        /// destroys list
        /// </summary>
        public void Destroy()
        {
            while (pr != null)
            {
                ss = pr;
                pr = pr.Next;
                ss.Next = null;
            }
            pb = ss = pr;
        }


        /// <summary>
        /// Sorts list
        /// </summary>
        public void Burbulas()
        {
            if (pr == null) { return; }
            bool kt = true;
            while (kt)
            {
                kt = false;
                knot d = pr;
                while(d.Next != null)
                {
                    if(d.Next.Data <= d.Data)
                    {
                        Player pl = d.Data;
                        d.Data = d.Next.Data;
                        d.Next.Data = pl;
                        kt = true;
                    }
                    d = d.Next;
                }
            }
        }


        /// <summary>
        /// Removes player
        /// </summary>
        /// <param name="pl"></param>
        public void RemoveV(Player pl)
        {
            knot d1 = pr;
            while (d1 != null && d1.Next != null && d1.Data != pl)
                d1 = d1.Next;
            knot v = d1;
            v.Data = null;
            v.Next = null;
            v = null;
        }
    }
}
