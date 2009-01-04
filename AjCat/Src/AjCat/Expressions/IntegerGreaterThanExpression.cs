namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerGreaterThanExpression : Expression
    {
        private static IntegerGreaterThanExpression instance = new IntegerGreaterThanExpression();

        private IntegerGreaterThanExpression()
        {
        }

        public static IntegerGreaterThanExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            int op2 = (int)machine.Pop();
            int op1 = (int)machine.Pop();

            machine.Push(op1 > op2);
        }

        public override string ToString()
        {
            return "gt_int";
        }
    }
}
