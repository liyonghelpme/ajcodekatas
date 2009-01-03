namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class QuotationExpression : Expression
    {
        private List<Expression> expressions;

        public QuotationExpression(List<Expression> expressions)
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
            machine.Push(new CompositeExpression(this.expressions));
        }
    }
}
