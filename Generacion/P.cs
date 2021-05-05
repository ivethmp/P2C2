using System;
using System.Collections.Generic;
using System.Text;

namespace P1.Generacion
{
    
    class P
    {
        public static int apu = 0;
        public static LinkedList<int> Heap = new LinkedList<int>();
        public static LinkedList<P> Stack0 = new LinkedList<P>();

        public decimal val;
        public int punt;
        public P(Decimal val, int punt)
        {
            this.val = val;
            this.punt = punt;
        }


    }
}
