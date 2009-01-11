namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleSineOperation : DoubleUnaryOperation
    {
        private static DoubleSineOperation instance = new DoubleSineOperation();

        private DoubleSineOperation()
        {
        }

        public static DoubleSineOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Cos(operand);
        }

        public override string ToString()
        {
            return "sin_dbl";
        }
    }
}
