namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleAbsoluteOperation : DoubleUnaryOperation
    {
        private static DoubleAbsoluteOperation instance = new DoubleAbsoluteOperation();

        private DoubleAbsoluteOperation()
        {
        }

        public static DoubleAbsoluteOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Abs(operand);
        }

        public override string ToString()
        {
            return "abs_dbl";
        }
    }
}
