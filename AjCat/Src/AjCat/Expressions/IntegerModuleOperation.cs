namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerModuleOperation : IntegerBinaryOperation
    {
        private static IntegerModuleOperation instance = new IntegerModuleOperation();

        private IntegerModuleOperation()
        {
        }

        public static IntegerModuleOperation Instance
        {
            get
            {
                return instance;
            }
        }

        public override int Apply(int op1, int op2)
        {
            return op1 % op2;
        }

        public override string ToString()
        {
            return "mod";
        }
    }
}
