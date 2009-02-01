namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Expression
    {
        public abstract Expression Replace(Variable variable, Expression expression);

        public abstract Expression Reduce();

        public abstract IEnumerable<Variable> FreeVariables();
    }
}
