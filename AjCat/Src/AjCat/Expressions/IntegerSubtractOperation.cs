namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerSubtractOperation : IntegerBinaryOperation
    {
        private static IntegerSubtractOperation instance = new IntegerSubtractOperation();

        private IntegerSubtractOperation()
        {
        }

        public static IntegerSubtractOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int op1, int op2)
        {
            return op1 - op2;
        }
    }
}
