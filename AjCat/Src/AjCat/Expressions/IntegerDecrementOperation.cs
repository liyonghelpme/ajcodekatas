namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerDecrementOperation : IntegerUnaryOperation
    {
        private static IntegerDecrementOperation instance = new IntegerDecrementOperation();

        private IntegerDecrementOperation()
        {
        }

        public static IntegerDecrementOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int operand)
        {
            return operand-1;
        }
    }
}
