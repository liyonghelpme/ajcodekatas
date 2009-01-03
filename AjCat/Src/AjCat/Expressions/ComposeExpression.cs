namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ComposeExpression : Expression
    {
        private static ComposeExpression instance = new ComposeExpression();

        private ComposeExpression()
        {
        }

        public static ComposeExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            Expression expression2 = (Expression)machine.Pop();
            Expression expression1 = (Expression)machine.Pop();

            List<Expression> list = new List<Expression>();

            list.Add(expression1);
            list.Add(expression2);

            machine.Push(new CompositeExpression(list));
        }
    }
}
