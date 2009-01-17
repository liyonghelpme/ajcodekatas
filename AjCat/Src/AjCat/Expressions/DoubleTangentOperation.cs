namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleTangentOperation : DoubleUnaryOperation
    {
        private static DoubleTangentOperation instance = new DoubleTangentOperation();

        private DoubleTangentOperation()
        {
        }

        public static DoubleTangentOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double operand)
        {
            return Math.Tan(operand);
        }

        public override string ToString()
        {
            return "tan_dbl";
        }
    }
}
