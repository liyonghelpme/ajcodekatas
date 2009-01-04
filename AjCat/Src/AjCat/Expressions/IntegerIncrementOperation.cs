namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerIncrementOperation : IntegerUnaryOperation
    {
        private static IntegerIncrementOperation instance = new IntegerIncrementOperation();

        private IntegerIncrementOperation()
        {
        }

        public static IntegerIncrementOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int operand)
        {
            return operand + 1;
        }
    }
}
