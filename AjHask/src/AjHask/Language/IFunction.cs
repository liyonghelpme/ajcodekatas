namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFunction
    {
        int Arity { get; }

        object Apply(object parameter);

        object Apply(List<object> parameters);
    }
}
