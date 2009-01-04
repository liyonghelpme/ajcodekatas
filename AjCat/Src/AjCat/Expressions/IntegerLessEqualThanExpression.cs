namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerLessEqualThanExpression : Expression
    {
        private static IntegerLessEqualThanExpression instance = new IntegerLessEqualThanExpression();

        private IntegerLessEqualThanExpression()
        {
        }

        public static IntegerLessEqualThanExpression Instance
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

            machine.Push(op1 <= op2);
        }

        public override string ToString()
        {
            return "lteq_int";
        }
    }
}
