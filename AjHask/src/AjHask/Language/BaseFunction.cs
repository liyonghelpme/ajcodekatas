namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseFunction : IFunction
    {
        public virtual object Value { get { return this; } }

        public abstract int Arity { get; }

        public abstract IFunction Apply(IFunction parameter);

        public virtual IFunction Evaluate(IList<IFunction> parameters)
        {
            IFunction result = this;

            foreach (IFunction parameter in parameters)
                result = result.Apply(parameter);

            return result;
        }

        public virtual IFunction Bind(IList<IFunction> parameters)
        {
            return this;
        }
    }
}
