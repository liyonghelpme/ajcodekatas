namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OrExpression : Expression
    {
        private static OrExpression instance = new OrExpression();

        private OrExpression()
        {
        }

        public static OrExpression Instance
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

            machine.Push(op1 || op2);
        }

        public override string ToString()
        {
            return "or";
        }
    }
}
