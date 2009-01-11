namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleArcSineOperation : DoubleUnaryOperation
    {
        private static DoubleArcSineOperation instance = new DoubleArcSineOperation();

        private DoubleArcSineOperation()
        {
        }

        public static DoubleArcSineOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Asin(operand);
        }

        public override string ToString()
        {
            return "asin_dbl";
        }
    }
}
