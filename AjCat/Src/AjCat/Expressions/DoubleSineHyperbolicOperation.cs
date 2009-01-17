namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleSineHyperbolicOperation : DoubleUnaryOperation
    {
        private static DoubleSineHyperbolicOperation instance = new DoubleSineHyperbolicOperation();

        private DoubleSineHyperbolicOperation()
        {
        }

        public static DoubleSineHyperbolicOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Sinh(operand);
        }

        public override string ToString()
        {
            return "sinh_dbl";
        }
    }
}
