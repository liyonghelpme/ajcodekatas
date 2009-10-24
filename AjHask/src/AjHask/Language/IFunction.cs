namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFunction
    {
        int Arity { get; }

        object Value { get; }

        IFunction Apply(IFunction parameter);

        IFunction Bind(IList<IFunction> parameters);

        IFunction Evaluate(IList<IFunction> parameters);
    }
}

