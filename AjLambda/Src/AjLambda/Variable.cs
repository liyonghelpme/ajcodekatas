namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Variable : Expression
    {
        private string name;

        public Variable(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override string ToString()
        {
            return this.Name;
        }

        public override Expression Replace(Variable variable, Expression expression)
        {
            if (this == variable)
                return expression;

            return this;
        }

        public override Expression Reduce()
        {
            return this;
        }
    }
}
