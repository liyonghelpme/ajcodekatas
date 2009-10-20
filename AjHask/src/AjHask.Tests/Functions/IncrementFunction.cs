namespace AjHask.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjHask.Language;

    class IncrementFunction : IFunction
    {
        public IFunction Apply(IFunction parameter)
        {
            return new ConstantFunction(((int)(parameter.Value)) + 1);
        }

        public IFunction Apply(IList<IFunction> parameters)
        {
            throw new NotImplementedException();
        }

        public int Arity
        {
            get { return 1; }
        }

        public object Value { get { return this; } }
    }
}
