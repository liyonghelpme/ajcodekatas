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
            if (this.body is Lambda)
                return "\\" + this.parameter.ToString() + this.body.ToString().Substring(1);

            return "\\" + this.parameter.ToString() + "." + this.body.ToString();
        }

        public override Expression Replace(Variable variable, Expression expression)
        {
            // Case: the parameter hides the variable
            if (variable.Name == this.parameter.Name)
                return this;

            IEnumerable<Variable> freeVariables = expression.FreeVariables();
            IEnumerable<string> freeNames = freeVariables.Select(v => v.Name);

            Expression newBody = this.body;
            Variable newParameter = this.parameter;

            if (freeNames.Contains(this.parameter.Name))
            {
                newParameter = this.parameter.RenameVariable(freeVariables);
                newBody = newBody.Replace(this.parameter, newParameter);
            }

            newBody = newBody.Replace(variable, expression);

            if (newBody == this.body && newParameter == this.parameter)
                return this;

            return new Lambda(newParameter, newBody);
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

        public override IEnumerable<Variable> FreeVariables()
        {
            return this.body.FreeVariables().Except(this.parameter.FreeVariables());
        }
    }
}

