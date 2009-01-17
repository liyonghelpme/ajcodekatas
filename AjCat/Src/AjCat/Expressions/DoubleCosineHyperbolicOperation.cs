namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleCosineHyperbolicOperation : DoubleUnaryOperation
    {
        private static DoubleCosineHyperbolicOperation instance = new DoubleCosineHyperbolicOperation();

        private DoubleCosineHyperbolicOperation()
        {
        }

        public static DoubleCosineHyperbolicOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Cosh(operand);
        }

        public override string ToString()
        {
            return "cosh_dbl";
        }
    }
}
