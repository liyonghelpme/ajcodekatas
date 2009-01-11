namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleAddOperation : DoubleBinaryOperation
    {
        private static DoubleAddOperation instance = new DoubleAddOperation();

        private DoubleAddOperation()
        {
        }

        public static DoubleAddOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double op1, double op2)
        {
            return op1 + op2;
        }

        public override string ToString()
        {
            return "add_dbl";
        }
    }
}
