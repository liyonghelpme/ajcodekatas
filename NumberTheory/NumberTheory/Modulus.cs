using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberTheory
{
    public class Modulus
    {
        private int modulus;

        public Modulus(int modulus)
        {
            this.modulus = modulus;
        }

        public int Add(int x, int y)
        {
            return (x + y) % this.modulus;
        }

        public int Multiply(int x, int y)
        {
            return (x * y) % this.modulus;
        }

        public IEnumerable<int> Elements()
        {
            for (int k = 0; k < this.modulus; k++)
                yield return k;
        }

        public IEnumerable<int> NonZeroElements()
        {
            for (int k = 1; k < this.modulus; k++)
                yield return k;
        }

        public IEnumerable<int> QuadraticResidues()
        {
            return this.NonZeroElements().Select(x => this.Multiply(x,x)).Distinct().OrderBy(x=>x);
        }

        public int Inverse(int x)
        {
            return this.Elements().Where(y => this.Multiply(x, y) == 1).FirstOrDefault();
        }

        public int Power(int x, int n)
        {
            if (n < 0)
            {
                int y = this.Inverse(x);

                return this.Power(y, -n);
            }

            if (n == 0)
                return 1;

            if (n == 1)
                return x;

            int result = x;

            for (int k = 2; k <= n; k++)
                result = this.Multiply(result, x);

            return result;
        }

        public IEnumerable<int> Powers(int x)
        {
            int result = 1;

            for (int k = 1; k < this.modulus; k++)
            {
                result = this.Multiply(x, result);

                yield return result;

                if (result == 1 || result == 0)
                    break;
            }
        }
    }
}
