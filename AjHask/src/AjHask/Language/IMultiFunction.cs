namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMultiFunction : IFunction
    {
        IFunction Apply(IList<IFunction> parameters);
    }
}
