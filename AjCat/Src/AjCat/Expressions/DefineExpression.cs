namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat.Compiler;

    public class DefineExpression : Expression
    {
        private string name;
        private List<Expression> expressions = new List<Expression>();

        public DefineExpression(string name, List<Expression> expressions)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (expressions == null)
            {
                throw new ArgumentNullException("expressions");
            }

            this.name = name;
            this.expressions = expressions;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
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
            AjCat.Compiler.Expressions.DefineExpression(this.name, new CompositeExpression(this.expressions));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("define ");
            sb.Append(this.name);
            sb.Append(" { ");

            int n = 0;

            foreach (Expression expression in this.expressions)
            {
                if (n > 0)
                {
                    sb.Append(" ");
                }

                sb.Append(expression.ToString());

                n++;
            }

            sb.Append(" }");

            return sb.ToString();
        }
    }
}
