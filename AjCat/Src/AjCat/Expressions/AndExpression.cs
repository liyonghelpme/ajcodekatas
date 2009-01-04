namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AndExpression : Expression
    {
        private static AndExpression instance = new AndExpression();

        private AndExpression()
        {
        }

        public static AndExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            bool op2 = (bool)machine.Pop();
            bool op1 = (bool)machine.Pop();

            machine.Push(op1 && op2);
        }

        public override string ToString()
        {
            return "and";
        }
    }
}
