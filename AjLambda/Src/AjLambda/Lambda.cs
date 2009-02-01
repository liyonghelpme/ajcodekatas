namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lambda : Expression
    {
        private Variable parameter;
        private Expression body;

        public Lambda(Variable parameter, Expression body)
        {
            this.parameter = parameter;
            this.body = body;
        }

        public override string ToString()
        {
            return "\\" + this.parameter.ToString() + "." + this.body.ToString();
        }

        public override Expression Replace(Variable variable, Expression expression)
        {
            // Case: the parameter hides the variable
            if (variable.Name == this.parameter.Name)
                return this;

            // TODO: case parameter collision with free variables in expression, rename parameter
            Expression newBody = this.body.Replace(variable, expression);

            if (newBody == this.body)
                return this;

            return new Lambda(this.parameter, newBody);
        }

        public override Expression Reduce()
        {
            Expression newBody = this.body.Reduce();

            if (newBody == this.body)
                return this;

            return new Lambda(this.parameter, newBody);
        }

        public Expression Apply(Expression value)
        {
            return this.body.Replace(this.parameter, value);
        }
    }
}

