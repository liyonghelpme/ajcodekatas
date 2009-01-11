namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleMultiplyOperation : DoubleBinaryOperation
    {
        private static DoubleMultiplyOperation instance = new DoubleMultiplyOperation();

        private DoubleMultiplyOperation()
        {
        }

        public static DoubleMultiplyOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double op1, double op2)
        {
            return op1 * op2;
        }

        public override string ToString()
        {
            return "mult_dbl";
        }
    }
}
