namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Expression
    {
        public virtual Expression Replace(Variable variable, Expression expression);
    }
}
