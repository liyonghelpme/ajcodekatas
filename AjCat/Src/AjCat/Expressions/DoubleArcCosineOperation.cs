namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleArcCosineOperation : DoubleUnaryOperation
    {
        private static DoubleArcCosineOperation instance = new DoubleArcCosineOperation();

        private DoubleArcCosineOperation()
        {
        }

        public static DoubleArcCosineOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Acos(operand);
        }

        public override string ToString()
        {
            return "acos_dbl";
        }
    }
}
