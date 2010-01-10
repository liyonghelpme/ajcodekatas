using System;
using System.Collections.Generic;
using System.Text;

namespace AjConcurr.Tsp
{
    class GenomaComparer : IComparer<Genoma>
    {
        public int Compare(Genoma x, Genoma y)
        {
            if (x.value < y.value)
                return -1;
            if (x.value > y.value)
                return 1;

            return 0;
        }
    }
}
