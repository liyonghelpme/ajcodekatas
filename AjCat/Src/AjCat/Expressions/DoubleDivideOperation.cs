namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleDivideOperation : DoubleBinaryOperation
    {
        private static DoubleDivideOperation instance = new DoubleDivideOperation();

        private DoubleDivideOperation()
        {
        }

        public static DoubleDivideOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override double Apply(double op1, double op2)
        {
            return op1 / op2;
        }

        public override string ToString()
        {
            return "div_dbl";
        }
    }
}
