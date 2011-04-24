using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberTheory
{
    public static class Numbers
    {
        public static int GreaterCommonDivisor(int x, int y)
        {
            if (x < 0)
                x = -x;

            if (y < 0)
                y = -y;

            if (x < y)
                return GreaterCommonDivisor(y, x);

            int residue = x % y;

            if (residue == 0)
                return y;

            return GreaterCommonDivisor(y, residue);
        }

        public static int Lagrange(int x, int y)
        {
            if (GreaterCommonDivisor(x, y) > 1)
                return 0;

            Modulus modulus = new Modulus(y);

            if (y % 2 == 1)
            {
                int r = modulus.Power(x, (y - 1) / 2);

                if (r == y - 1)
                    return -1;

                return r;
            }

            if (modulus.QuadraticResidues().Contains(x))
                return 1;

            return -1;
        }
    }
}
