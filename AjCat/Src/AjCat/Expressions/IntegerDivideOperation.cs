namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerDivideOperation : IntegerBinaryOperation
    {
        private static IntegerDivideOperation instance = new IntegerDivideOperation();

        private IntegerDivideOperation()
        {
        }

        public static IntegerDivideOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int op1, int op2)
        {
            return op1 / op2;
        }
    }
}
