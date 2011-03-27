namespace AjAla.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFunction
    {
        object Evaluate(IContext context, IEnumerable<object> parameters);
    }
}
