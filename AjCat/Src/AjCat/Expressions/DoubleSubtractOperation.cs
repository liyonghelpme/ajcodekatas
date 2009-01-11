namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleSubtractOperation : DoubleBinaryOperation
    {
        private static DoubleSubtractOperation instance = new DoubleSubtractOperation();

        private DoubleSubtractOperation()
        {
        }

        public static DoubleSubtractOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double op1, double op2)
        {
            return op1 - op2;
        }

        public override string ToString()
        {
            return "sub_dbl";
        }
    }
}
