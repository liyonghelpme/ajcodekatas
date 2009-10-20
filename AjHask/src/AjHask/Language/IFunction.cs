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

        IFunction Apply(IList<IFunction> parameters);
    }
}
