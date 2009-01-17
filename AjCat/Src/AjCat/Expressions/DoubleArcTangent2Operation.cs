namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleArcTangent2Operation : DoubleBinaryOperation
    {
        private static DoubleArcTangent2Operation instance = new DoubleArcTangent2Operation();

        private DoubleArcTangent2Operation()
        {
        }

        public static DoubleArcTangent2Operation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double op1, double op2)
        {
            return Math.Atan2(op1, op2);
        }

        public override string ToString()
        {
            return "atan2_dbl";
        }
    }
}
