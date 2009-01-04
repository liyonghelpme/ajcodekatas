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

            List<Expression> list;

            if (expression2 is CompositeExpression)
            {
                list = new List<Expression>(((CompositeExpression)expression2).Expressions);
            }
            else
            {
                list = new List<Expression>();
                list.Add(expression2);
            }

            if (expression1 is CompositeExpression)
            {
                list = new List<Expression> ( ((CompositeExpression)expression1).Expressions.Concat(list));
            }
            else
            {
                list.Insert(0, expression1);
            }

            machine.Push(new CompositeExpression(list));
        }
    }
}
