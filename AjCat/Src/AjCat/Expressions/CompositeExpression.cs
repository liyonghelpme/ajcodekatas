namespace AjCat.Expressions
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class CompositeExpression : Expression
    {
        private List<Expression> expressions = new List<Expression>();

        public CompositeExpression(List<Expression> expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException("expressions");
            }

            this.expressions = expressions;
        }

        public List<Expression> Expressions
        {
            get
            {
                return this.expressions;
            }
        }

        public override void Evaluate(Machine machine)
        {
            foreach (Expression expression in this.expressions)
            {
                expression.Evaluate(machine);
            }
        }
    }
}
