namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleCosineOperation : DoubleUnaryOperation
    {
        private static DoubleCosineOperation instance = new DoubleCosineOperation();

        private DoubleCosineOperation()
        {
        }

        public static DoubleCosineOperation Instance
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
            return "cos_dbl";
        }
    }
}
