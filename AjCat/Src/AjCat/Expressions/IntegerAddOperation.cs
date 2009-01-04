namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerAddOperation : IntegerBinaryOperation
    {
        private static IntegerAddOperation instance = new IntegerAddOperation();

        private IntegerAddOperation()
        {
        }

        public static IntegerAddOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int op1, int op2)
        {
            return op1 + op2;
        }

        public override string ToString()
        {
            return "add_int";
        }
    }
}
