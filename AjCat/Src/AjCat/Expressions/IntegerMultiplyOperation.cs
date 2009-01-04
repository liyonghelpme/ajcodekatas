namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerMultiplyOperation : IntegerBinaryOperation
    {
        private static IntegerMultiplyOperation instance = new IntegerMultiplyOperation();

        private IntegerMultiplyOperation()
        {
        }

        public static IntegerMultiplyOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int op1, int op2)
        {
            return op1 * op2;
        }

        public override string ToString()
        {
            return "mult_int";
        }
    }
}
