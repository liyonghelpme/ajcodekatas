namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleArcTangentOperation : DoubleUnaryOperation
    {
        private static DoubleArcTangentOperation instance = new DoubleArcTangentOperation();

        private DoubleArcTangentOperation()
        {
        }

        public static DoubleArcTangentOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Atan(operand);
        }

        public override string ToString()
        {
            return "atan_dbl";
        }
    }
}
