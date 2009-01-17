namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleTangentHyperbolicOperation : DoubleUnaryOperation
    {
        private static DoubleTangentHyperbolicOperation instance = new DoubleTangentHyperbolicOperation();

        private DoubleTangentHyperbolicOperation()
        {
        }

        public static DoubleTangentHyperbolicOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Tanh(operand);
        }

        public override string ToString()
        {
            return "tanh_dbl";
        }
    }
}
