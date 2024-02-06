using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_sem_2_
{
    public sealed class knot
    {
        public Player Data { get; set; }
        public knot Next { get; set; }
        public knot() { }
        public knot(Player Data, knot address)
        {
            this.Data = Data;
            Next = address;
        }
    }
}
