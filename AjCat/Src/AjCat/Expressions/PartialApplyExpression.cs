namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialApplyExpression : Expression
    {
        private static PartialApplyExpression instance = new PartialApplyExpression();

        private PartialApplyExpression()
        {
        }

        public static PartialApplyExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            CompositeExpression expression = (CompositeExpression) machine.Pop();

            int stacksize = machine.StackCount;
            int nexpr = 0;

            foreach (Expression expr in expression.Expressions) 
            {
                nexpr++;
                expr.Evaluate(machine);

                if (machine.StackCount < stacksize)
                {
                    break;
                }
            }

            List<Expression> rest = new List<Expression>();

            for (int k = nexpr; k<expression.Expressions.Count; k++)
            {
                rest.Add(expression.Expressions[k]);
            }

            machine.Push(new CompositeExpression(rest));
        }
    }
}
